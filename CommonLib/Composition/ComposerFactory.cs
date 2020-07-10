using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MyMathSheets.CommonLib.Composition
{
	/// <summary>
	/// 用於緩存程序集為單位<see cref="Composer"/>的工廠類
	/// </summary>
	/// <remarks>
	/// 定義並讀取優先順序的組建
	/// </remarks>
	public static class ComposerFactory
	{
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
		public static event ModelLoadingEventHandler ModelLoadingEvent;

		/// <summary>
		/// 模塊加載完畢事件
		/// </summary>
		public static event ModelLoadCompleteEventHandler ModelLoadCompleteEvent;

		/// <summary>
		/// 模塊信息檢索事件
		/// </summary>
		public static event ModelInitializeEventHandler InitializeModelEvent;

		/// <summary>
		/// 文件名檢索用關鍵字
		/// </summary>
		private const string SEARCH_PATTERN = "MyMathSheets.*";

		/// <summary>
		///
		/// </summary>
		private static Assembly TempAssembly;

		/// <summary>
		/// 以程序集為單位的組合器<see cref="Composer"/>緩存
		/// </summary>
		private static readonly ConcurrentDictionary<string, Composer> ComposerCache;

		/// <summary>
		/// 以系統模塊為單位<see cref="SystemModelType"/>的組合器緩存
		/// </summary>
		internal static readonly ConcurrentDictionary<string, MathSheetMarkerAttribute> SystemModelInfoCache;

		/// <summary>
		///
		/// </summary>
		private static readonly List<FileInfo> LoadTopicModuleFiles;

		/// <summary>
		///
		/// </summary>
		static ComposerFactory()
		{
			Sync = new object();

			ComposerCache = new ConcurrentDictionary<string, Composer>();
			SystemModelInfoCache = new ConcurrentDictionary<string, MathSheetMarkerAttribute>();

			LoadTopicModuleFiles = new List<FileInfo>();
			GetDirectoryFiles(Configuration.Current.ApplicationRootPath, LoadTopicModuleFiles);
		}

		/// <summary>
		/// 獨佔模式開啟
		/// </summary>
		private static readonly object Sync;

		/// <summary>
		/// 模塊記載初期化處理
		/// </summary>
		public static void Init()
		{
			int currentIndex = 0;
			// 用於檢查題型注入的完整性（具備策略模塊和展示模塊）
			List<string> checkCache = new List<string>();

			var action = new Action<FileInfo>(f =>
			{
				var assembly = Assembly.LoadFile(f.FullName);
				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0028L, f.FullName));

				MathSheetMarkerAttribute attr = assembly.GetCustomAttributes(typeof(MathSheetMarkerAttribute), false).OfType<MathSheetMarkerAttribute>().FirstOrDefault();
				if (attr == null)
				{
					throw new ArgumentNullException(MessageUtil.GetMessage(() => MsgResources.E0027L, f.FullName));
				}

				var valueFunc = new Func<string, Composer>(c =>
				{
					lock (Sync)
					{
						System.Threading.Thread.Sleep(50);

						// 模塊加載事件傳播
						ModelLoadingEvent?.Invoke(assembly, currentIndex++);

						LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0029L, c));

						return new Composer(assembly);
					}
				});

				// 模塊識別號(模塊識別ID + 題型識別號)
				string composerKey = string.IsNullOrEmpty(attr.Preview) ? attr.SystemModel.ToString() : $"{attr.SystemModel}::{attr.Preview}";
				ComposerCache.GetOrAdd(composerKey, valueFunc);

				// （為確保模塊的完成性，規定同一個題型必須具備策略模塊和展示模塊）
				if (checkCache.Any(d => d.Equals(attr.Preview, StringComparison.CurrentCultureIgnoreCase)))
				{
					// 獲取題型模塊信息并保存
					if ((attr.SystemModel == SystemModelType.TheFormulaShows || attr.SystemModel == SystemModelType.ComputationalStrategy)
						&& !string.IsNullOrEmpty(attr.Description))
					{
						SystemModelInfoCache.GetOrAdd(attr.Preview, attr);
					}
				}
				else
				{
					checkCache.Add(attr.Preview);
				}

				// 當最後一個模塊加載完畢的時候
				if (currentIndex == LoadTopicModuleFiles.Count)
				{
					LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0030L, LoadTopicModuleFiles.Count));

					// 模塊加載完畢事件傳播
					ModelLoadCompleteEvent?.Invoke(null, LoadTopicModuleFiles.Count);
				}
			});

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0031L));

			// 模塊信息事件傳播
			InitializeModelEvent?.Invoke(null, LoadTopicModuleFiles.Count);

			LoadTopicModuleFiles.ForEach(f => action(f));
		}

		/// <summary>
		/// 返回指定模塊識別ID和題型編號下的<see cref="Composer"/>對象
		/// </summary>
		/// <param name="assembly">模塊識別ID</param>
		/// <returns><see cref="Composer"/>對象</returns>
		public static Composer GetComporser(Assembly assembly)
		{
			Guard.ArgumentNotNull(assembly, "assembly");

			var systemModel = assembly.GetCustomAttributes(typeof(MathSheetMarkerAttribute), true)
										.Cast<MathSheetMarkerAttribute>()
										.FirstOrDefault();
			if (systemModel == null)
			{
				throw new ComposerException(MessageUtil.GetMessage(() => MsgResources.E0001L, assembly.GetName().FullName));
			}

			var valueFunc = new Func<string, Composer>(c =>
			{
				lock (Sync)
				{
					return new Composer(assembly);
				}
			});
			return ComposerCache.GetOrAdd(systemModel.ToString(), valueFunc);
		}

		/// <summary>
		/// 返回指定模塊識別ID和題型編號下的<see cref="Composer"/>對象
		/// </summary>
		/// <param name="systemModel">模塊識別ID</param>
		/// <param name="preview">題型編號</param>
		/// <returns><see cref="Composer"/>對象</returns>
		public static Composer GetComporser(SystemModelType systemModel, string preview)
		{
			// 模塊識別號(模塊識別ID + 題型識別號)
			string composerKey = string.IsNullOrEmpty(preview) ? systemModel.ToString() : $"{systemModel}::{preview}";
			if (ComposerCache.ContainsKey(composerKey))
			{
				if (ComposerCache.TryGetValue(composerKey, out Composer composer))
				{
					return composer;
				}
			}

			var valueFunc = new Func<string, Composer>(c =>
			{
				lock (Sync)
				{
					return new Composer(GetAssembly(systemModel, preview));
				}
			});

			return ComposerCache.GetOrAdd(composerKey, valueFunc);
		}

		/// <summary>
		/// 返回指定模塊識別ID和題型編號下的<see cref="Composer"/>對象
		/// </summary>
		/// <param name="systemModel">模塊識別ID</param>
		/// <returns><see cref="Composer"/>對象</returns>
		public static Composer GetComporser(SystemModelType systemModel)
		{
			return GetComporser(systemModel, string.Empty);
		}

		/// <summary>
		/// 返回指定模塊識別ID和題型編號下的<see cref="Assembly"/>對象
		/// </summary>
		/// <param name="systemModel">模塊識別ID</param>
		/// <param name="preview">題型編號</param>
		/// <returns><see cref="Assembly"/>對象</returns>
		private static Assembly GetAssembly(SystemModelType systemModel, string preview)
		{
			var action = new Action<FileInfo>(f =>
			{
				var assembly = Assembly.LoadFile(f.FullName);
				var attr = assembly.GetCustomAttributes(typeof(MathSheetMarkerAttribute), true).Cast<MathSheetMarkerAttribute>().FirstOrDefault();
				if (attr != null && preview.Equals(attr.Preview, StringComparison.CurrentCultureIgnoreCase) && systemModel.Equals(attr.SystemModel))
				{
					TempAssembly = assembly;
				}
			});

			

			LoadTopicModuleFiles.ForEach(f => action(f));

			if (TempAssembly == null)
			{
				throw new ComposerException(MessageUtil.GetMessage(() => MsgResources.E0001L, $"{systemModel}::{preview}"));
			}
			return TempAssembly;
		}

		/// <summary>
		/// 獲取指定目錄下所有文件的列表信息
		/// </summary>
		/// <param name="path">指定目錄</param>
		/// <param name="files">文件列表信息</param>
		private static void GetDirectoryFiles(string path, List<FileInfo> files)
		{
			DirectoryInfo theFolder = new DirectoryInfo(path);

			// 遍歷文件
			foreach (var fi in from _ in theFolder.GetFiles(SEARCH_PATTERN)
							   where _.Name.ToLower(CultureInfo.CurrentCulture).EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase)
							   orderby _.Name.Length descending
							   select _)
			{
				files.Add(fi);
			}

			// 遍歷文件夾
			theFolder.GetDirectories().ToList().ForEach(d => GetDirectoryFiles(d.FullName, files));
		}

		/// <summary>
		/// 對當前應用程序域按照程序集的參照優先順序生成容器並返回
		/// </summary>
		/// <param name="path">程序集目錄</param>
		/// <returns></returns>
		public static IEnumerable<ComposablePartCatalog> GetCatalog(string path)
		{
			foreach (var fi in from _ in new DirectoryInfo(path).GetFiles(SEARCH_PATTERN)
							   where _.Name.ToLower(CultureInfo.CurrentCulture).EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase)
							   orderby _.Name.Length descending
							   select _)
			{
				yield return new DirectoryCatalog(path, fi.Name);
			}
		}

		/// <summary>
		/// 復原組合器並釋放<see cref="Composer"/>對象
		/// </summary>
		public static void Reset()
		{
			lock (Sync)
			{
				if (SystemModelInfoCache != null)
				{
					SystemModelInfoCache.Clear();
				}

				if (LoadTopicModuleFiles != null)
				{
					LoadTopicModuleFiles.Clear();
				}

				foreach (KeyValuePair<string, Composer> item in ComposerCache)
				{
					((IDisposable)item.Value)?.Dispose();
				}
				ComposerCache.Clear();
			}
		}
	}
}