using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace MyMathSheets.WebApi.Filters.Constraint
{
    /// <summary>
    /// 自定義路由約束（驗證當前題型設置編號不可為空）
    /// </summary>
    public class NonEmptyConstraint : IHttpRouteConstraint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="route"></param>
        /// <param name="parameterName"></param>
        /// <param name="values"></param>
        /// <param name="routeDirection"></param>
        /// <returns></returns>
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
        IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                return string.IsNullOrWhiteSpace(value as string);
            }
            return false;
        }
    }
}