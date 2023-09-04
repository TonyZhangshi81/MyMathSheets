using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.FromProcess;
using MyMathSheets.CommonLib.Main.FromProcess.Support;
using MyMathSheets.WebApi.Filters;
using MyMathSheets.WebApi.Filters.Security;
using MyMathSheets.WebApi.Filters.Swagger;
using MyMathSheets.WebApi.Main.FromProcess;
using MyMathSheets.WebApi.Models.Request;
using MyMathSheets.WebApi.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.ServiceModel.Activation;
using System.Threading;
using System.Web.Http;

namespace MyMathSheets.WebApi.Controllers
{
    /// <summary>
    /// API控制類
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CompileController : ApiController
    {
        /// <summary>
        /// API處理類
        /// </summary>
        [Import("Api", typeof(IMainProcess))]
        private ApiProcess Process
        {
            get;
            set;
        }

        /// <summary>
        /// <see cref="CompileController"/>構造	構築依賴組合並導入<see cref="ApiProcess"/>API處理類
        /// </summary>
        public CompileController()
        {
            // 獲取HTML支援類Composer
            Composer composer = ComposerFactory.GetComporser(this.GetType().Assembly);
            composer.Compose(this);
        }

        /// <summary>
        /// 題型作成API訪問接口
        /// </summary>
        /// <param name="list">輸入參數(題型設定參數集合)</param>
        /// <returns>接口應答結果</returns>
        [HttpPost]
        [WaitResponse()]
        [Route("Do")]
        [ApiAuthorize]
        public HttpResponseMessage Do(List<TopicManagementReq> list)
        {
            LogUtil.LogDebug(JsonConvert.SerializeObject(list));

            // 參數校驗
            var (IsValid, Messages) = this.CheckModelValid();
            if (!IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new TopicRes { Messages = Messages });
            }

            // 訪問權限驗證
            if (!this.IsAuthorized())
            {
                var messages = new List<string>() { "Cannot access because there is no access permission" };
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new TopicRes { Messages = messages });
            }

            // 題型參數設置
            list.ForEach(d => Process.TopicManagementList.Add(new TopicManagement() { TopicIdentifier = d.Id, Number = d.Number }));
            // 題型作成
            FileInfo exerciseFile = Process.Compile();
            // 應答作成
            var result = new TopicRes()
            {
                // 頁面地址返回
                Url = $"{ConfigurationUtil.GetIISUrl()}{exerciseFile.Name}"
            };

            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsAuthorized()
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Value.Equals("admin") && c.Type.Equals("auth"));
            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 用戶登錄處理-獲取Token信息
        /// </summary>
        /// <param name="login">登錄用戶</param>
        /// <returns>接口應答結果</returns>
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login(LoginReq login)
        {
            LogUtil.LogDebug(JsonConvert.SerializeObject(login));
            // 參數校驗
            var (IsValid, Messages) = this.CheckModelValid();
            if (!IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new TopicRes { Messages = Messages });
            }

            // 應答作成
            var result = new LoginRes()
            {
                // 頁面地址返回
                Token = this.GetToken(login)
            };

            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private string GetToken(LoginReq login)
        {
            // 測試階段暫時不實現用戶登錄權限驗證 TODO
            var dic = new Dictionary<string, object>
            {
                { "id", login.UserId },
                { "auth", "admin" }
            };

            var token = new JWTHelper().Encode(dic, ConfigurationUtil.GetKeyValue("jwtKey"), Convert.ToInt32(ConfigurationUtil.GetKeyValue("ExpiredMinute")));
            return token;
        }

        /// <summary>
        /// 參數校驗
        /// </summary>
        /// <returns>驗證後的錯誤信息</returns>
        private (bool IsValid, List<string> Messages) CheckModelValid()
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

        /// <summary>
        /// 默認測試頁
        /// </summary>
        /// <returns>應答結果</returns>
        [HttpGet]
        [Route("Test")]
        public HttpResponseMessage Index()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, "Request OK!");
            return response;
        }

    }
}
