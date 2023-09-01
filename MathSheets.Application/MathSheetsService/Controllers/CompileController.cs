using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.FromProcess;
using MyMathSheets.CommonLib.Main.FromProcess.Support;
using MyMathSheets.WebApi.Filters;
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
        public HttpResponseMessage Do(List<TopicManagementReq> list)
        {
            LogUtil.LogDebug(JsonConvert.SerializeObject(list));

            // 參數校驗
            var (IsValid, Messages) = this.CheckModelValid();
            if (!IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new TopicRes { Messages = Messages });
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
        public HttpResponseMessage Index()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, "Request OK!");
            return response;
        }
    }
}
