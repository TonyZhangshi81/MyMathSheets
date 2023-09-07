using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace MyMathSheets.WebApi.Filters.Security
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class JwtHelper
    {
        internal const string secretKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        internal IJwtEncoder Encoder { get; }
        internal IJwtDecoder Decoder { get; }
        internal IDateTimeProvider DateTimeProvider { get; }
        internal IJsonSerializer JsonSerializer { get; }

        private IBase64UrlEncoder UrlEncoder { get; }

        /// <summary>
        /// 
        /// </summary>
        internal JwtHelper()
        {
            DateTimeProvider = new UtcDateTimeProvider();
            JsonSerializer = new JsonNetSerializer();
            UrlEncoder = new JwtBase64UrlEncoder();

            Encoder = this.CreateJwtEncoder();
            Decoder = this.CreateJwtDecorder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IJwtEncoder CreateJwtEncoder()
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            return new JwtEncoder(algorithm, this.JsonSerializer, this.UrlEncoder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IJwtDecoder CreateJwtDecorder()
        {
            IAlgorithmFactory algorithm = new HMACSHAAlgorithmFactory();
            IJwtValidator validator = new JwtValidator(this.JsonSerializer, this.DateTimeProvider);
            return new JwtDecoder(this.JsonSerializer, validator, this.UrlEncoder, algorithm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="isValid"></param>
        /// <param name="authenticateResult"></param>
        /// <returns></returns>
        public IEnumerable<Claim> VaridateToken(string token, out bool isValid, out string authenticateResult)
        {
            isValid = false;
            authenticateResult = string.Empty;

            try
            {
                //var values = token.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                var json = this.Decoder.Decode(token, secretKey, verify: true);

                var user = this.JsonSerializer.Deserialize<IDictionary<string, object>>(json)["user"].ToString();

                var payload = this.JsonSerializer.Deserialize<IDictionary<string, object>>(user);

                var payLoadClaims = new List<Claim>();
                foreach (var keyValuePair in payload)
                {
                    payLoadClaims.Add(new Claim(keyValuePair.Key, keyValuePair.Value.ToString()));
                }

                isValid = true;
                return payLoadClaims;
            }
            catch (TokenExpiredException)
            {
                authenticateResult = "expired";
                //errorMsg = "token過期";
                return null;
            }
            catch (SignatureVerificationException)
            {
                authenticateResult = "invalid signature";
                //errorMsg = "簽名無效";
                return null;
            }
            catch (Exception)
            {
                authenticateResult = "invalid";
                //errorMsg = "token無效";
                return null;
            }
        }

        /// <summary>
        /// 創建Token信息
        /// </summary>
        /// <param name="info">需要傳遞的數據對象（Payload 部分）</param>
        /// <param name="expiredMinute">有效期限設定（單位：分鐘）</param>
        /// <returns>Token信息</returns>
        public string CreateToken<T>(T info, int expiredMinute = 30)
        {
            // 有效期限
            DateTimeOffset expiration = this.DateTimeProvider.GetNow().AddMinutes(expiredMinute);

            var expirySeconds = expiration.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

            //var expirySeconds = Math.Round((expiration - DateTime.).TotalSeconds);

            var payload = new Dictionary<string, object>
            {
                { "exp", expirySeconds },
                { "user", this.JsonSerializer.Serialize(info) }
            };

            return this.Encoder.Encode(payload, secretKey);
        }

    }
}