using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyMathSheets.CommonLib.Composition
{
	/// <summary>
	/// 
	/// </summary>
	public static class ComposerFactory
	{
		/// <summary>
		/// 模塊加載委託
		/// </summary>
		/// <param name="current">當前加載對象計數</param>
		public delegate void ModelLoadEventHandler(int current);

		/// <summary>
		/// 模塊信息檢索委託
		/// </summary>
		/// <param name="modelCount">預加載對象合計數</param>
		public delegate void SearchModelEventHandler(int modelCount);


		/// <summary>
		/// 模塊加載事件
		/// </summary>
		public static event ModelLoadEventHandler ModelLoadEvent;
		/// <summary>
		/// 模塊信息檢索事件
		/// </summary>
		public static event SearchModelEventHandler SearchModelEvent;

		/// <summary>
		/// 文件名檢索用關鍵字
		/// </summary>
		const string SEARCH_PATTERN = "MyMathSheets.*.dll";
		/// <summary>
		/// 日誌對象作成
		/// </summary>
		private static readonly Log log = Log.LogReady(typeof(ComposerFactory));

		/// <summary>
		/// 
		/// </summary>
		private static Assembly TempAssembly;

		/// <summary>
		/// 
		/// </summary>
		private static readonly ConcurrentDictionary<string, Composer> ComposerCache;
		/// <summary>
		/// 
		/// </summary>
		internal static readonly ConcurrentDictionary<string, MathSheetMarkerAttribute> AssemblyInfoCache;

		/// <summary>
		/// 
		/// </summary>
		static ComposerFactory()
		{
			ComposerCache = new ConcurrentDictionary<string, Composer>();
			AssemblyInfoCache = new ConcurrentDictionary<string, MathSheetMarkerAttribute>();
		}

		/// <summary>
		/// 獨佔模式開啟
		/// </summary>
		private static readonly object Sync = new object();

		/// <summary>
		/// 模塊記載初期化處理
		/// </summary>
		public static void Init()
		{
			int currentIndex = 1;
			// 用於檢查題型注入的完整性（具備策略模塊和展示模塊）
			List<LayoutSetting.Preview> checkCache = new List<LayoutSetting.Preview>();

			var action = new Action<FileInfo>(file =>
			{
				var assembly = Assembly.LoadFile(file.FullName);
				log.Debug(MessageUtil.GetException(() => MsgResources.I0028L, file.FullName));

				MathSheetMarkerAttribute attr = assembly.GetCustomAttributes(typeof(MathSheetMarkerAttribute), false)
					.Cast<MathSheetMarkerAttribute>()
					.FirstOrDefault();
				if (attr == null)
				{
					log.Debug(MessageUtil.GetException(() => MsgResources.E0027L, file.FullName));
					throw new Exception();
				}
				var valueFunc = new Func<string, Composer>(c =>
				{
					lock (Sync)
					{
						System.Threading.Thread.Sleep(50);

						// 模塊加載事件傳播
						ModelLoadEvent?.Invoke(currentIndex++);
						return new Composer(assembly);
					}
				});

				// 模塊識別號(模塊識別ID + 題型識別號)
				string composerKey = (attr.Preview == LayoutSetting.Preview.Null) ? attr.SystemId.ToString() : string.Format("{0}::{1}", attr.SystemId, attr.Preview);

				ComposerCache.GetOrAdd(composerKey, valueFunc);
				log.Debug(MessageUtil.GetException(() => MsgResources.I0029L, composerKey));

				// （為確保模塊的完成性，規定同一個題型必須具備策略模塊和展示模塊）
				if (checkCache.Any(d => d == attr.Preview))
				{
					// 獲取題型模塊信息并保存
					if ((attr.SystemId == SystemModel.TheFormulaShows || attr.SystemId == SystemModel.ComputationalStrategy) && !string.IsNullOrEmpty(attr.Description))
					{
						AssemblyInfoCache.GetOrAdd(attr.Preview.ToString(), attr);
					}
				}
				else
				{
					checkCache.Add(attr.Preview);
				}
			});

			log.Debug(MessageUtil.GetException(() => MsgResources.I0031L));

			var basePath = AppDomain.CurrentDomain.BaseDirectory;
			DirectoryInfo directory = new DirectoryInfo(basePath);
			List<FileInfo> files = directory.GetFiles(SEARCH_PATTERN).ToList();

			// 模塊信息事件傳播
			SearchModelEvent?.Invoke(files.Count);

			files.ForEach(f => action(f));

			log.Debug(MessageUtil.GetException(() => MsgResources.I0030L, files.Count));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="systemId"></param>
		/// <param name="preview"></param>
		/// <returns></returns>
		public static Composer GetComporser(SystemModel systemId, LayoutSetting.Preview preview = LayoutSetting.Preview.Null)
		{
			string composerKey = (preview == LayoutSetting.Preview.Null) ? systemId.ToString() : string.Format("{0}::{1}", systemId, preview);

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
					return new Composer(GetAssembly(systemId, preview));
				}
			});

			return ComposerCache.GetOrAdd(composerKey, valueFunc);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="systemId"></param>
		/// <param name="preview"></param>
		/// <returns></returns>
		private static Assembly GetAssembly(SystemModel systemId, LayoutSetting.Preview preview = LayoutSetting.Preview.Null)
		{
			var action = new Action<FileInfo>(f =>
			{
				var assembly = Assembly.LoadFile(f.FullName);
				var attr = assembly.GetCustomAttributes(typeof(MathSheetMarkerAttribute), true).Cast<MathSheetMarkerAttribute>().FirstOrDefault();
				if (attr != null && preview == attr.Preview && systemId.Equals(attr.SystemId))
				{
					TempAssembly = assembly;
				}
			});

			var basePath = AppDomain.CurrentDomain.BaseDirectory;
			DirectoryInfo directory = new DirectoryInfo(basePath);
			directory.GetFiles(SEARCH_PATTERN).ToList().ForEach(f => action(f));

			if (TempAssembly == null)
			{
				var sb = new StringBuilder();
				sb.Append(MessageUtil.GetException(() => MsgResources.E0001L));
				throw new ComposerException(sb.ToString());
			}
			return TempAssembly;
		}

		/// <summary>
		/// アプリケーション基盤での優先順位付けされたアセンブリの順序で生成されたコンテナを返します。
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static IEnumerable<ComposablePartCatalog> GetCatalog(string path)
		{
			foreach (var fi in new DirectoryInfo(path)
						.GetFiles(SEARCH_PATTERN)
						.OrderByDescending(_ => _.Name.Length))
			{
				yield return new DirectoryCatalog(path, fi.Name);
			}
		}

		/// <summary>
		/// UT用にコンテナーをリセットします。
		/// </summary>
		/// <remarks>
		/// UT以外では呼び出されることはありません。
		/// </remarks>
		internal static void Reset()
		{
			lock (Sync)
			{
				foreach (var keyValue in ComposerCache)
				{
					if (keyValue.Value is IDisposable value)
					{
						value.Dispose();
					}
				}
				ComposerCache.Clear();
			}
		}
	}
}
