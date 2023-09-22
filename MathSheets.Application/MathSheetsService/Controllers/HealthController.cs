using MyMathSheets.CommonLib.Util;
using MyMathSheets.WebHealthChecks.Base;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyMathSheets.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [RoutePrefix("api/Health")]
    public class HealthController : BaseApiController
    {
        /// <summary>
        /// 運行狀態檢查
        /// </summary>
        [Import("HealthCheck", typeof(IHealthController))]
        public WebHealthChecks.Base.HealthController HealthCheck
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public HealthController() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Check")]
        public async Task<HttpResponseMessage> CheckAsync()
        {
            var result = await HealthCheck.GetHealthInfoAsync();

            HttpResponseMessage responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(result.GetJsonByObject(), encoding: Encoding.GetEncoding("UTF-8"), mediaType: "applicaton/json"),
                StatusCode = HttpStatusCode.OK
            };

            return responseMessage;
        }
    }

}