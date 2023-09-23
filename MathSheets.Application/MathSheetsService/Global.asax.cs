using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Plugin;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;

namespace MyMathSheets.WebApi
{
    /// <summary>
    ///
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        ///
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            PluginHelper.GetManager().Initialize();
        }
    }
}