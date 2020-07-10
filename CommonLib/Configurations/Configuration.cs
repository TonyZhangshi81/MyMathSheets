using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using System;
using System.Configuration;

namespace MyMathSheets.CommonLib.Configurations
{
	/// <summary>
	/// config 管理類
	/// </summary>
	public class Configuration
	{
		/// <summary>
		/// 管理類實例
		/// </summary>
		private static readonly MyMathSheets.CommonLib.Configurations.Configuration Instance;

		/// <summary>
		/// 對外公開的管理類實例
		/// </summary>
		public static MyMathSheets.CommonLib.Configurations.Configuration Current => Instance;

		/// <summary>
		/// <see cref="Configuration"/>的構築
		/// </summary>
		public Configuration()
		{
		}

		/// <summary>
		/// <see cref="Configuration"/>的靜態構築
		/// </summary>
		static Configuration()
		{
			Instance = new MyMathSheets.CommonLib.Configurations.Configuration();
		}

		/// <summary>
		/// 程序集的讀取位置
		/// </summary>
		/// <returns>配置信息</returns>
		public string ApplicationRootPath
		{
			get
			{
				var path = ConfigurationManager.AppSettings["Path.ApplicationRoot"];
				if (string.IsNullOrWhiteSpace(path))
				{
					path = AppDomain.CurrentDomain.BaseDirectory;
				}
				return path;
			}
		}

		/// <summary>
		/// HTML模板所在位置
		/// </summary>
		/// <returns>配置信息</returns>
		public string HtmlTemplatePath => GetConfigValue("Path.HtmlTemplate");

		/// <summary>
		/// 題型參數配置管理
		/// </summary>
		/// <returns>配置信息</returns>
		public string TopicManagementConfig => GetConfigValue("App.TopicManagement");

		/// <summary>
		/// 取得設定文件的設定信息
		/// </summary>
		/// <param name="key">設定KEY</param>
		/// <returns>設定信息</returns>
		public string GetConfigValue(string key)
		{
			string value = ConfigurationManager.AppSettings[key];
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ConfigurationErrorsException(MessageUtil.GetMessage(() => MsgResources.E0042L, key));
			}
			return value;
		}
	}
}