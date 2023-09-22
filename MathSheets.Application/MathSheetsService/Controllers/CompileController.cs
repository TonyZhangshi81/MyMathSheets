using Microsoft.Ajax.Utilities;
using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.FromProcess;
using MyMathSheets.CommonLib.Main.FromProcess.Support;
using MyMathSheets.WebApi.Filters;
using MyMathSheets.WebApi.Filters.Swagger;
using MyMathSheets.WebApi.Main.FromProcess;
using MyMathSheets.WebApi.Models.Request;
using MyMathSheets.WebApi.Models.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Activation;
using System.Web.Http;

namespace MyMathSheets.WebApi.Controllers
{
    /// <summary>
    /// API控制類
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [RoutePrefix("api/Compile")]
    public class CompileController : BaseApiController
    {
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
        public CompileController() : base() { }

        /// <summary>
        /// 題型作成API訪問接口
        /// </summary>
        /// <param name="list">輸入參數(題型設定參數集合)</param>
        /// <returns>接口應答結果</returns>
        [HttpPost]
        [WaitResponse()]
        [Route("Launch")]
        [ApiAuthorize]
        public HttpResponseMessage Launch(List<TopicManagementReq> list)
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
        /// 題型作成API訪問接口
        /// </summary>
        /// <param name="topicId">題型識別ID</param>
        /// <param name="number">題型配置編號</param>
        /// <returns>接口應答結果</returns>
        [HttpGet]
        [WaitResponse()]
        [Route("Launch/{topicId:isexist}/{number:nonempty}")]
        [ApiAuthorize]
        public HttpResponseMessage Launch(string topicId, string number)
        {
            LogUtil.LogDebug($"TopicIdentifier:{topicId} Number:{number}");

            // 訪問權限驗證
            if (!this.IsAuthorized())
            {
                var messages = new List<string>() { "Cannot access because there is no access permission" };
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new TopicRes { Messages = messages });
            }

            // 題型參數設置
            Process.TopicManagementList.Add(new TopicManagement() { TopicIdentifier = topicId, Number = number });
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
                GeneralToken = this.CreateToken(login)
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
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
