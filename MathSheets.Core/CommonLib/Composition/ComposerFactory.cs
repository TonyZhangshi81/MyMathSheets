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
		/// 獨佔模式開啟
		/// </summary>
		private static readonly object Sync;

		/// <summary>
		/// 文件名檢索用關鍵字
		/// </summary>
		private const string SEARCH_PATTERN = "MyMathSheets.*";

		/// <summary>
		/// 管理<see cref="Composer"/>對象的緩存
		/// </summary>
		private static readonly ConcurrentDictionary<string, Composer> ComposerCache;

		/// <summary>
		/// <see cref="ComposerFactory"/>的構造函數
		/// </summary>
		static ComposerFactory()
		{
			Sync = new object();

			ComposerCache = new ConcurrentDictionary<string, Composer>();
		}

		/// <summary>
		/// 返回指定程序集對象下的<see cref="Composer"/>對象
		/// </summary>
		/// <param name="assembly">程序集對象</param>
		/// <returns><see cref="Composer"/>對象</returns>
		public static Composer GetComporser(Assembly assembly)
		{
			Guard.ArgumentNotNull(assembly, "assembly");

			var valueFunc = new Func<string, Composer>(c =>
			{
				lock (Sync)
				{
					return new Composer(assembly);
				}
			});
			return ComposerCache.GetOrAdd(assembly.GetName().FullName, valueFunc);
		}

		/// <summary>
		/// 返回指定模塊識別ID和題型編號下的<see cref="Composer"/>對象
		/// </summary>
		/// <param name="topicIdentifier">題型編號</param>
		/// <returns><see cref="Composer"/>對象</returns>
		public static Composer GetComporser(string topicIdentifier)
		{
			if (ComposerCache.ContainsKey(topicIdentifier))
			{
				if (ComposerCache.TryGetValue(topicIdentifier, out Composer composer))
				{
					return composer;
				}
			}

			var valueFunc = new Func<string, Composer>(c =>
			{
				lock (Sync)
				{
					return new Composer(topicIdentifier);
				}
			});

			return ComposerCache.GetOrAdd(topicIdentifier, valueFunc);
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
				foreach (KeyValuePair<string, Composer> item in ComposerCache)
				{
					((IDisposable)item.Value)?.Dispose();
				}
				ComposerCache.Clear();
			}
		}
	}
}