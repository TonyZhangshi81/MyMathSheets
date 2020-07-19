﻿using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using System;
using System.Configuration;

namespace MyMathSheets.CommonLib.Configurations
{
	/// <summary>
	/// config 配置文件通用訪問類
	/// </summary>
	public static class ConfigurationUtil
	{
		/// <summary>
		/// 程序集的讀取位置
		/// </summary>
		/// <returns>配置信息</returns>
		public static string ApplicationRootPath
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
		public static string HtmlTemplatePath => GetKeyValue("Path.HtmlTemplate");

		/// <summary>
		/// 題型參數配置管理
		/// </summary>
		/// <returns>配置信息</returns>
		public static string TopicManagementConfig => GetKeyValue("App.TopicManagement");

		/// <summary>
		/// 取得設定文件的設定信息
		/// </summary>
		/// <param name="key">設定KEY</param>
		/// <returns>設定信息</returns>
		public static string GetKeyValue(string key)
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