using MyMathSheets.CommonLib.Util;
using MyMathSheets.WebHealthChecks.Base;
using System.Collections.Generic;
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
    public class HealthController : BaseApiController
    {
        /// <summary>
        /// 運行狀態檢查
        /// </summary>
        [ImportMany(typeof(IHealthCheckProvider))]
        public IEnumerable<IHealthCheckProvider> HealthCheckProviders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HealthController() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Health/Check")]
        public async Task<HttpResponseMessage> CheckAsync()
        {
            var result = new HealthCheckResult();
            foreach (var provider in HealthCheckProviders)
            {
                result.HealthChecks.Add(await provider.GetHealthCheckAsync());
            }

            HttpResponseMessage responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(result.GetJsonByObject(), encoding: Encoding.GetEncoding("UTF-8"), mediaType: "applicaton/json"),
                StatusCode = HttpStatusCode.OK
            };

            return responseMessage;
        }
    }

}