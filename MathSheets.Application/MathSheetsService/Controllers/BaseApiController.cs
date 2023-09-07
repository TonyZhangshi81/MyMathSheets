using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Main.FromProcess;
using MyMathSheets.WebApi.Filters.Security;
using MyMathSheets.WebApi.Main.FromProcess;
using MyMathSheets.WebApi.Models.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace MyMathSheets.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        private static readonly JwtHelper _JwtHelper = new JwtHelper();

        /// <summary>
        /// API處理類
        /// </summary>
        [Import("Api", typeof(IMainProcess))]
        public ApiProcess Process
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public BaseApiController()
        {
            // 獲取HTML支援類Composer
            Composer composer = ComposerFactory.GetComporser(this.GetType().Assembly);
            composer.Compose(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected virtual string CreateToken(LoginReq user)
        {
            var expiredMinute = Convert.ToInt32(ConfigurationUtil.GetKeyValue("ExpiredMinute"));

            // 測試階段暫時不實現用戶登錄權限驗證 TODO
            var token = _JwtHelper.CreateToken(user, expiredMinute);
            return token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsAuthorized()
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            var isExist = claimsPrincipal.Claims.Any(d => d.Value.Equals("P@ssw0rd") && d.Type.Equals("Passord"))
                            && claimsPrincipal.Claims.Any(d => d.Value.Equals("admin") && d.Type.Equals("UserId"));

            return isExist;
        }

        /// <summary>
        /// 參數校驗
        /// </summary>
        /// <returns>驗證後的錯誤信息</returns>
        protected virtual (bool IsValid, List<string> Messages) CheckModelValid()
        {
            if (!base.ModelState.IsValid)
            {
                var messages = new List<string>();
                foreach (var entry in base.ModelState.Values)
                {
                    foreach (var error in entry.Errors)
                    {
                        messages.Add(error.ErrorMessage);
                    }
                }
                return (false, messages);
            }
            return (true, null);
        }

    }
}