using MyMathSheets.CommonLib.Configurations;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace MyMathSheets.WebApi.Filters.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityBasicAuthentication : IAuthenticationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AllowMultiple { get; }

        /// <summary>
        /// 請求前
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // 獲取token
            context.Request.Headers.TryGetValues("Authorization", out var tokenHeaders);
            // 如果沒有token，不做後續處理
            if (tokenHeaders == null || !tokenHeaders.Any())
            {
                return Task.FromResult(0);
            }
            // 如果token驗證通過，寫入到identity，如果誒通過則是指錯誤
            var jwtHelper = new JWTHelper();
            var payLoadClaims = jwtHelper.DecodeToObject(tokenHeaders.FirstOrDefault(), ConfigurationUtil.GetKeyValue("jwtKey"), out bool isValid, out string errMsg);
            if (isValid)
            {
                // 只要ClaimsIdentity設置了authenticationType，authenticated就是true，後面的authority根據authenticated=true來做權限
                var identity = new ClaimsIdentity("jwt", "id", "auth");
                foreach (var keyValuePair in payLoadClaims)
                {
                    identity.AddClaim(new Claim(keyValuePair.Key, keyValuePair.Value.ToString()));
                }
                // 最好是http上下文的principal和進程的currentPrincipal都設置
                context.Principal = new ClaimsPrincipal(identity);
                Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            }
            else
            {
                throw new AuthenticationException(errMsg);
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// 請求後
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}