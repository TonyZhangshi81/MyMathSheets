using Swashbuckle.Swagger;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MyMathSheets.WebApi.Swagger.Shared
{
    /// <summary>
    /// swagger 顯示控制器的描述
    /// </summary>
    public class SwaggerControllerDescProvider : ISwaggerProvider
    {
        private readonly ISwaggerProvider _swaggerProvider;
        private static ConcurrentDictionary<string, SwaggerDocument> _cache = new ConcurrentDictionary<string, SwaggerDocument>();
        private readonly string _xmlPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="swaggerProvider"></param>
        /// <param name="xmlPath">XML文檔路徑</param>
        public SwaggerControllerDescProvider(ISwaggerProvider swaggerProvider, string xmlPath)
        {
            _swaggerProvider = swaggerProvider;
            _xmlPath = xmlPath;
        }

        /// <summary>
        /// GetSwagger
        /// </summary>
        /// <param name="rootUrl"></param>
        /// <param name="apiVersion"></param>
        /// <returns></returns>
        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = $"{rootUrl}_{apiVersion}";
            // 只讀取一次
            if (!_cache.TryGetValue(cacheKey, out SwaggerDocument srcDoc))
            {
                srcDoc = _swaggerProvider.GetSwagger(rootUrl, apiVersion);

                srcDoc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", GetControllerDesc() } };
                _cache.TryAdd(cacheKey, srcDoc);
            }
            return srcDoc;
        }

        /// <summary>
        /// 從API文檔中讀取控制器描述
        /// </summary>
        /// <returns>所有控制器描述</returns>
        public ConcurrentDictionary<string, string> GetControllerDesc()
        {
            var controllerDescDict = new ConcurrentDictionary<string, string>();

            if (File.Exists(_xmlPath))
            {
                var xmldoc = new XmlDocument();
                xmldoc.Load(_xmlPath);

                string[] arrPath;
                int cCount = "Controller".Length;
                foreach (XmlNode node in xmldoc.SelectNodes("//member"))
                {
                    var type = node.Attributes["name"].Value;
                    if (type.StartsWith("T:"))
                    {
                        // 控制器
                        arrPath = type.Split('.');
                        var controllerName = arrPath[arrPath.Length - 1];
                        if (controllerName.EndsWith("Controller"))
                        {
                            // 獲取控制器注釋
                            XmlNode summaryNode = node.SelectSingleNode("summary");
                            string key = controllerName.Remove(controllerName.Length - cCount, cCount);
                            if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
                            {
                                controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                            }
                        }
                    }
                }
            }
            return controllerDescDict;
        }
    }
}
