using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMathSheets.CommonLib.Plugin
{
	/// <summary>
	/// 插件管理類
	/// </summary>
	public sealed class PluginsManager : PluginsManagerBase
	{
		/// <summary>
		/// <see cref="PluginsManager"/>的靜態構築
		/// </summary>
		static PluginsManager()
		{
			Sync = new object();
		}

		/// <summary>
		/// 獨佔模式開啟
		/// </summary>
		private static readonly object Sync;

		/// <summary>
		/// 插件列表初始化
		/// </summary>
		public override void Initialize()
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
						base.OnModelLoading(currentIndex++);

						LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0029L, c.Name));

						return new PluginInfo(f);
					}
				});

				InitPluginsModuleList.Add(valueFunc(f));

				// 當最後一個模塊加載完畢的時候
				if (currentIndex == plugins.Count)
				{
					LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0030L, plugins.Count));

					// 模塊加載完畢事件傳播
					base.OnModelLoadComplete(currentIndex++);
				}
			});

			// 模塊加載事件傳播
			base.OnModelPreLoad(plugins.Count);

			plugins.ToList().ForEach(f => action(f));
		}
	}
}