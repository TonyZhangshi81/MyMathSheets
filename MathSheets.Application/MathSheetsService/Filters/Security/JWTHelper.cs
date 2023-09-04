using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;

namespace MyMathSheets.WebApi.Filters.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class JWTHelper
    {
        private IJsonSerializer _jsonSerializer;
        private IDateTimeProvider _dateTimeProvider;
        private IJwtValidator _jwtValidator;
        private IBase64UrlEncoder _base64UrlEncoder;
        private IJwtAlgorithm _jwtAlgorithm;
        private IJwtDecoder _jwtDecoder;
        private IJwtEncoder _jwtEncoder;

        /// <summary>
        /// 
        /// </summary>
        public JWTHelper()
        {
            // 非fluent寫法
            this._jsonSerializer = new JsonNetSerializer();
            this._dateTimeProvider = new UtcDateTimeProvider();
            this._jwtValidator = new JwtValidator(_jsonSerializer, _dateTimeProvider);
            this._base64UrlEncoder = new JwtBase64UrlEncoder();
            this._jwtAlgorithm = new HMACSHA256Algorithm();
            this._jwtDecoder = new JwtDecoder(_jsonSerializer, _jwtValidator, _base64UrlEncoder, new HMACSHA256Algorithm());
            this._jwtEncoder = new JwtEncoder(_jwtAlgorithm, _jsonSerializer, _base64UrlEncoder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="key"></param>
        /// <param name="isValid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string Decode(string token, string key, out bool isValid, out string errMsg)
        {
            isValid = false;
            var result = string.Empty;
            try
            {
                result = _jwtDecoder.Decode(token, key, true);
                isValid = true;
                errMsg = "正確的token";
                return result;
            }
            catch (TokenExpiredException)
            {
                errMsg = "token過期";
                return result;
            }
            catch (SignatureVerificationException)
            {
                errMsg = "簽名無效";
                return result;
            }
            catch (Exception)
            {
                errMsg = "token無效";
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <param name="key"></param>
        /// <param name="isValid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public T DecodeToObject<T>(string token, string key, out bool isValid, out string errMsg)
        {
            isValid = false;
            try
            {
                var result = _jwtDecoder.DecodeToObject<T>(token, key, true);
                isValid = true;
                errMsg = "正確的token";
                return result;
            }
            catch (TokenExpiredException)
            {
                errMsg = "token過期";
                return default(T);
            }
            catch (SignatureVerificationException)
            {
                errMsg = "簽名無效";
                return default(T);
            }
            catch (Exception)
            {
                errMsg = "token無效";
                return default(T);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="key"></param>
        /// <param name="isValid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public IDictionary<string, object> DecodeToObject(string token, string key, out bool isValid, out string errMsg)
        {
            isValid = false;
            try
            {
                var result = _jwtDecoder.DecodeToObject(token, key, true);
                isValid = true;
                errMsg = "正確的token";
                return result;
            }
            catch (TokenExpiredException)
            {
                errMsg = "token過期";
                return null;
            }
            catch (SignatureVerificationException)
            {
                errMsg = "簽名無效";
                return null;
            }
            catch (Exception)
            {
                errMsg = "token無效";
                return null;
            }
        }

        /// <summary>
        /// 解密處理
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="key"></param>
        /// <param name="expiredMinute"></param>
        /// <returns></returns>
        public string Encode(Dictionary<string, object> payload, string key, int expiredMinute = 30)
        {
            if (!payload.ContainsKey("exp"))
            {
                var exp = Math.Round((_dateTimeProvider.GetNow().AddMinutes(expiredMinute) - new DateTime(1970, 1, 1)).TotalSeconds);
                payload.Add("exp", exp);
            }
            return _jwtEncoder.Encode(payload, key);
        }

    }
}