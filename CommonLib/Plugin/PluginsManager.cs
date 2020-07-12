using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MyMathSheets.CommonLib.Plugin
{
	/// <summary>
	///
	/// </summary>
	public sealed class PluginsManager : IPluginsManager
	{
		/// <summary>
		/// 文件名檢索用關鍵字
		/// </summary>
		private const string SEARCH_PATTERN = "MyMathSheets.*";

		/// <summary>
		/// 模塊
		/// </summary>
		private static readonly string[] ExcludeAssemblies =
			new[]
				{
					"MyMathSheets.CommonLib.dll",
					"MyMathSheets.BasicOperationsLib.dll",
				};

		/// <summary>
		/// 模塊加載
		/// </summary>
		/// <param name="sender">程序集對象</param>
		/// <param name="current">當前加載對象計數</param>
		public delegate void ModelLoadingEventHandler(object sender, int current);

		/// <summary>
		/// 模塊加載完畢
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="current">當前加載對象計數</param>
		public delegate void ModelLoadCompleteEventHandler(object sender, int current);

		/// <summary>
		/// 模塊信息檢索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="modelCount">預加載對象合計數</param>
		public delegate void ModelInitializeEventHandler(object sender, int modelCount);

		/// <summary>
		/// 模塊加載事件
		/// </summary>
		public event ModelLoadingEventHandler ModelLoadingEvent;

		/// <summary>
		/// 模塊加載完畢事件
		/// </summary>
		public event ModelLoadCompleteEventHandler ModelLoadCompleteEvent;

		/// <summary>
		/// 模塊信息檢索事件
		/// </summary>
		public event ModelInitializeEventHandler InitializeModelEvent;

		/// <summary>
		/// 模塊文件列表
		/// </summary>
		public static readonly IList<PluginInfo> InitPluginsModuleList;

		/// <summary>
		/// <see cref="PluginsManager"/>的靜態構築
		/// </summary>
		static PluginsManager()
		{
			Sync = new object();

			InitPluginsModuleList = new List<PluginInfo>();
		}

		/// <summary>
		/// 獨佔模式開啟
		/// </summary>
		private static readonly object Sync;

		/// <summary>
		/// 插件列表初始化
		/// </summary>
		public void Initialize()
		{
			int currentIndex = 0;
			// 用於檢查題型注入的完整性（具備策略模塊和展示模塊）
			List<string> checkCache = new List<string>();

			// 檢索插件信息
			var plugins = SearchPlugins();

			var action = new Action<FileInfo>(f =>
			{
				var valueFunc = new Func<FileInfo, PluginInfo>(c =>
				{
					lock (Sync)
					{
						System.Threading.Thread.Sleep(50);

						// 模塊加載事件傳播
						ModelLoadingEvent?.Invoke(f, currentIndex++);

						LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0029L, c.Name));

						return new PluginInfo(f);
					}
				});

				InitPluginsModuleList.Add(valueFunc(f));

				/*
				var pluginAttr = pluginAttrFunc(Path.GetFileNameWithoutExtension(f.FullName));
				// （為確保模塊的完成性，規定同一個題型必須具備策略模塊和展示模塊）
				if (checkCache.Any(d => d.Equals(pluginAttr.Preview, StringComparison.CurrentCultureIgnoreCase)))
				{
					// 獲取題型模塊信息并保存
					if ((pluginAttr.SystemModel == SystemModelType.TheFormulaShows || pluginAttr.SystemModel == SystemModelType.ComputationalStrategy)
						&& !string.IsNullOrEmpty(pluginAttr.Description))
					{
						InitPluginsModuleList.GetOrAdd(pluginAttr.Preview, pluginAttr);
					}
				}
				else
				{
					checkCache.Add(pluginAttr.Preview);
				}
				*/

				// 當最後一個模塊加載完畢的時候
				if (currentIndex == plugins.Count)
				{
					LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0030L, plugins.Count));

					// 模塊加載完畢事件傳播
					ModelLoadCompleteEvent?.Invoke(this, plugins.Count);
				}
			});

			// 模塊信息事件傳播
			InitializeModelEvent?.Invoke(this, plugins.Count);

			plugins.ToList().ForEach(f => action(f));
		}

		/// <summary>
		/// 檢索插件信息
		/// </summary>
		/// <returns>文件列表信息</returns>
		public IList<FileInfo> SearchPlugins()
		{
			var fileList = new List<FileInfo>();
			GetDirectoryFiles(Configurations.Configuration.Current.ApplicationRootPath, fileList);

			return fileList;
		}

		/// <summary>
		/// 獲取指定目錄下所有文件的列表信息
		/// </summary>
		/// <param name="path">指定目錄</param>
		/// <param name="fileList">文件列表信息</param>
		private void GetDirectoryFiles(string path, List<FileInfo> fileList)
		{
			DirectoryInfo theFolder = new DirectoryInfo(path);

			// 遍歷文件
			foreach (var fi in from _ in theFolder.GetFiles(SEARCH_PATTERN)
							   where _.Name.ToLower(CultureInfo.CurrentCulture).EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase) && !ExcludeAssemblies.Contains(_.Name)
							   orderby _.Name.Length descending
							   select _)
			{
				fileList.Add(fi);
			}

			// 遍歷文件夾
			theFolder.GetDirectories().ToList().ForEach(d => GetDirectoryFiles(d.FullName, fileList));
		}
	}
}