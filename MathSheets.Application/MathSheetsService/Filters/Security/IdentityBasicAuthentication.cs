using MyMathSheets.CommonLib.Configurations;
using System;
using System.Collections.Generic;
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
        private static readonly JwtHelper _JwtHelper = new JwtHelper();

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
            if (!TryGetToken(context, out var token))
            {
                // 如果沒有token，不做後續處理
                return Task.FromResult(0);
            }

            // 如果token驗證通過，寫入到identity，如果誒通過則是指錯誤
            var payLoadClaims = _JwtHelper.VaridateToken(token, out bool isValid, out string authenticateResult);
            if (isValid)
            {
                var identity = new ClaimsIdentity();
                identity.AddClaims(payLoadClaims);

                // 最好是http上下文的principal和進程的currentPrincipal都設置
                context.Principal = new ClaimsPrincipal(identity);
                Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            }
            else
            {
                context.Request.Headers.Add(AuthHeaderKeys.AuthenticateResult, authenticateResult);
                throw new AuthenticationException(authenticateResult);
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool TryGetToken(HttpAuthenticationContext context, out string token)
        {
            token = null;

            foreach (var metadata in context.Request.Headers)
            {
                if (metadata.Key == AuthHeaderKeys.AuthorizationToken)
                {
                    token = metadata.Value.ElementAt(0);
                    return true;
                }
            }

            return false;
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