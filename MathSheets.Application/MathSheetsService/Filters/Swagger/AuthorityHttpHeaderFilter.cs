using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace MyMathSheets.WebApi.Filters.Swagger
{
    /// <summary>
    /// 為支持調試API時使用的過濾器
    /// </summary>
    public class AuthorityHttpHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// 用於向待測試API的Head中埋入Toke信息
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            // 判斷是否添加權限過濾器
            var isAuthorized = apiDescription.ActionDescriptor.GetCustomAttributes<ApiAuthorizeAttribute>().Any();
            if (isAuthorized)
            {
                operation.parameters.Add(new Parameter { name = "Authorization", @in = "header", description = "令牌", required = false, type = "string" });
            }
        }
    }

}