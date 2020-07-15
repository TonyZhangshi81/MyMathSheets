using MyMathSheets.CommonLib.Configurations;
using System;
using System.Configuration;

namespace MyMathSheets.CommonLib.Plugin
{
	/// <summary>
	/// 插件管理支援類
	/// </summary>
	public class PluginHelper : IDisposable
	{
		private bool isDisposed;

		/// <summary>
		/// 管理對象實例
		/// </summary>
		private static PluginsManagerBase _instance;

		/// <summary>
		/// 取得<see cref="PluginsManagerBase"/>的管理對象實例
		/// </summary>
		/// <returns>實例</returns>
		public PluginsManagerBase GetManager()
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
		/// 資源釋放
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 資源釋放
		/// </summary>
		/// <param name="disposing">是否正在釋放</param>
		protected virtual void Dispose(bool disposing)
		{
			if (isDisposed) return;

			// 正在釋放資源
			if (disposing)
			{
				if (_instance != null)
				{
					_instance.Dispose();
				}
			}

			isDisposed = true;
		}

		/// <summary>
		/// 析構函數
		/// </summary>
		~PluginHelper()
		{
			Dispose(false);
		}
	}
}