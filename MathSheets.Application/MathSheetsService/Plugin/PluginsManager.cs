using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Plugin;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMathSheets.WebApi.Plugin
{
    /// <summary>
    /// 插件管理類
    /// </summary>
    public class PluginsManager : PluginsManagerBase
    {
        /// <summary>
        /// <see cref="PluginsManager"/>的構造函數
        /// </summary>
        /// <param name="searchKeyword">文件名檢索用關鍵字</param>
        /// <param name="excludeAssemblies">檢索以外的條件</param>
        public PluginsManager(string searchKeyword, List<string> excludeAssemblies)
            : base(searchKeyword, excludeAssemblies)
        {
        }

        /// <summary>
        /// 檢索並獲取插件文件信息
        /// </summary>
        /// <returns>插件文件信息集合</returns>
        public override IList<FileInfo> SearchPlugins()
        {
            var fileList = new List<FileInfo>();

            DirectoryInfo theFolder = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationUtil.ApplicationRootPath));
            // 遍歷文件
            foreach (var fi in from _ in theFolder.GetFiles(base.SearchKeyword)
                               where !base.ExcludeAssemblies.Contains(_.Name)
                               orderby _.Name.Length descending
                               select _)
            {
                fileList.Add(fi);
            }

            return fileList;
        }


    }
}