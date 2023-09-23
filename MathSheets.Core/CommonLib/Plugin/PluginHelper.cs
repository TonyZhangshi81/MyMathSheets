using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Main;
using System;
using System.Configuration;

namespace MyMathSheets.CommonLib.Plugin
{
    /// <summary>
    /// 插件管理支援類
    /// </summary>
    public class PluginHelper : ObjectBase
    {
        /// <summary>
        /// 管理對象實例
        /// </summary>
        private static PluginsManagerBase _instance;

        /// <summary>
        /// 取得<see cref="PluginsManagerBase"/>的管理對象實例
        /// </summary>
        /// <returns>實例</returns>
        public static PluginsManagerBase GetManager()
        {
            if (_instance != null)
            {
                return _instance;
            }
            var section = (PluginManageProviderConfigurationSection)ConfigurationManager.GetSection("PluginManage");
            _instance = Activator.CreateInstance(Type.GetType(section.ImportType), section.SearchKeyword, section.ExcludeAssemblies) as PluginsManagerBase;
            return _instance;
        }

        /// <summary>
        /// 指定題型標識是否存在于加載列表中
        /// </summary>
        /// <param name="topicId">題型標識ID</param>
        /// <returns>存在:true</returns>
        public static bool IsExist(string topicId)
        {
            var topic = _instance.FindByTopicId(topicId);
            return topic != null;
        }

        /// <summary>
        /// 資源釋放
        /// </summary>
        protected override void DisposeManaged()
        {
            if (_instance != null)
            {
                _instance.Dispose();
            }
        }
    }
}