using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.WebApi.Filters;
using MyMathSheets.WebApi.Filters.Constraint;
using MyMathSheets.WebApi.Filters.Security;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;

namespace MyMathSheets.WebApi
{
    /// <summary>
    ///
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 過濾器註冊器
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // 跨域設置
            config.EnableCors(new EnableCorsAttribute(ConfigurationUtil.GetKeyValue("cors:allowedOrigin")
                                                    , ConfigurationUtil.GetKeyValue("cors:allowedHeaders")
                                                    , ConfigurationUtil.GetKeyValue("cors:allowedMethods"))
            {
                SupportsCredentials = true
            });

            // 設定自定義路由約束
            var constraintResolver = new DefaultInlineConstraintResolver();
            // 題型設置編號不可為空
            constraintResolver.ConstraintMap.Add("nonempty", typeof(NonEmptyConstraint));
            // 題型標識必須存在
            constraintResolver.ConstraintMap.Add("isexist", typeof(TopicExistConstraint));

            // 路由設置（開啟屬性路由）
            config.MapHttpAttributeRoutes(constraintResolver);

            // ssl認證相關
            config.Filters.Add(new SslClientCertAuthorizationFilterAttribute());
            // JSON Web Token（JWT）認證相關
            config.Filters.Add(new IdentityBasicAuthentication());

            // 異常篩選器
            config.Filters.Add(new WebApiExceptionFilterAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );
        }
    }
}