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
		const string SEARCH_PATTERN = "MyMathSheets.*.dll";

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
		/// 
		/// </summary>
		private static readonly object Sync = new object();

		/// <summary>
		/// 
		/// </summary>
		public static void Init()
		{
			var action = new Action<FileInfo>(f =>
			{
				var assembly = Assembly.LoadFile(f.FullName);
				MathSheetMarkerAttribute attr = assembly.GetCustomAttributes(typeof(MathSheetMarkerAttribute), false)
					.Cast<MathSheetMarkerAttribute>()
					.FirstOrDefault();
				if (attr == null)
				{
					throw new Exception();
				}
				var valueFunc = new Func<string, Composer>(c =>
				{
					lock (Sync)
					{
						return new Composer(assembly);
					}
				});

				string composerKey = (attr.Preview == LayoutSetting.Preview.Null) ? attr.SystemId.ToString() : string.Format("{0}::{1}", attr.SystemId, attr.Preview);

				ComposerCache.GetOrAdd(composerKey, valueFunc);

				if((attr.SystemId == SystemModel.TheFormulaShows || attr.SystemId == SystemModel.ComputationalStrategy) && !string.IsNullOrEmpty(attr.Description))
				{
					if (AssemblyInfoCache.ContainsKey(attr.Preview.ToString()) && !string.IsNullOrEmpty(attr.Identifier))
					{
						AssemblyInfoCache[attr.Preview.ToString()].Identifier = attr.Identifier;
					}
					else
					{
						AssemblyInfoCache.GetOrAdd(attr.Preview.ToString(), attr);
					}
				}
			});

			var basePath = AppDomain.CurrentDomain.BaseDirectory;
			DirectoryInfo directory = new DirectoryInfo(basePath);
			directory.GetFiles(SEARCH_PATTERN).ToList().ForEach(f => action(f));
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
