﻿using MyMathSheets.CommonLib.Main;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyMathSheets.CommonLib.Composition
{
	/// <summary>
	/// 基於程序集的優先順序由MEF對其實例進行檢索
	/// </summary>
	/// <remarks>
	/// 影響導出部件成功匹配的主要因素有：ContractType（協定類型），ContractName（協定名稱），Metadata（元數據），CreationPolicy（創建策略）
	/// </remarks>
	public class Composer : ObjectBase
	{
		/// <summary>
		/// 容器聲明
		/// </summary>
		private readonly CompositionContainer _container;

		/// <summary>
		/// 返回當前容器對象
		/// </summary>
		public CompositionContainer Container { get { return _container; } }

		/// <summary>
		/// <see cref="Composer"/>的實例化創建MEF容器實例
		/// </summary>
		public Composer()
		{
			_container = new CompositionContainer();
		}

		/// <summary>
		/// <see cref="Composer"/>的實例化創建MEF容器實例
		/// </summary>
		/// <param name="container">MEF容器對象</param>
		public Composer(CompositionContainer container)
		{
			_container = container;
		}

		/// <summary>
		/// 以程序集對象構造<see cref="Composer"/>
		/// </summary>
		/// <param name="assembly">程序集對象</param>
		/// <exception cref="ArgumentNullException"><paramref name="assembly"/>為NULL的情況</exception>
		public Composer(Assembly assembly)
		{
			Guard.ArgumentNotNull(assembly, "assembly");

			// 對象元素目錄
			var catalog = new AggregateCatalog();
			var cache = new List<Assembly>();

			// 遍歷程序集集合以參照的順序將程序集對象添加至對象元素目錄
			foreach (var asm in ReflectionUtil.GetReferencedAssemblies(assembly))
			{
				// 優先讀取指定程序集下已設定的關聯程序集
				foreach (var a in this.GetPriorAssemblyNames(asm))
				{
					if (!cache.Contains(a))
					{
						catalog.Catalogs.Add(new AssemblyCatalog(a));
						cache.Add(a);
					}
				}

				// 避免重複導入元素目錄
				if (!cache.Contains(asm))
				{
					catalog.Catalogs.Add(new AssemblyCatalog(asm));
					cache.Add(asm);
				}
			}

			// 創建容器
			_container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
		}

		/// <summary>
		/// 以程序集參數構造<see cref="Composer"/>
		/// </summary>
		/// <param name="assemblyId">參數</param>
		public Composer(string assemblyId)
			: this(ReflectionUtil.GetAssembly(assemblyId))
		{
		}

		/// <summary>
		/// 優先讀取指定程序集下已設定的關聯程序集
		/// </summary>
		/// <param name="assembly">程序集對象</param>
		/// <returns>關聯程序集</returns>
		private IEnumerable<Assembly> GetPriorAssemblyNames(Assembly assembly)
		{
			foreach (var name in assembly.GetCustomAttributes(false).OfType<ComposerAttribute>().Select(attr => attr.Name))
			{
				var asm = ReflectionUtil.GetAssembly(name);
				if (asm != null)
				{
					yield return asm;
					foreach (var a in this.GetPriorAssemblyNames(asm))
					{
						yield return a;
					}
				}
			}
		}

		/// <summary>
		/// 對指定的對象實例進行合成處理
		/// </summary>
		/// <param name="target">對象實例</param>
		public void Compose(object target)
		{
			// 為該對象創建一個可組合的部件
			var part = AttributedModelServices.CreatePart(target);
			// 該組合部件中存在所需導入的對象
			if (part.ImportDefinitions.Any())
			{
				// 對該部件進行導入
				_container.SatisfyImportsOnce(part);
			}
		}

		/// <summary>
		/// 容器組合中是否存在指定的類型
		/// </summary>
		/// <param name="type">類型</param>
		/// <returns>存在:TRUE返回</returns>
		public bool HasType(Type type)
		{
			return _container.Catalog.Any((ComposablePartDefinition _) => _.ExportDefinitions.OfType<ICompositionElement>().Any((ICompositionElement __) => __.Origin.DisplayName == type.FullName));
		}

		/// <summary>
		/// 生成沒有找到對象的Export時發生的例外
		/// </summary>
		/// <param name="type">類型</param>
		/// <param name="contractName">契約名稱</param>
		/// <param name="innerException">例外</param>
		/// <returns>特定例外</returns>
		public Exception CreateComposerException(Type type, string contractName, Exception innerException)
		{
			var sb = new StringBuilder();
			sb.Append(MessageUtil.GetMessage(() => MsgResources.E0001L));
			sb.Append(GetInnerExportedInfo(type, contractName));

			// 返回特定例外
			return new ComposerException(sb.ToString(), innerException);
		}

		/// <summary>
		/// <see cref="Composer"/>內部管理用的文字信息
		/// </summary>
		/// <param name="type">類型</param>
		/// <param name="contractName">契約名稱</param>
		/// <returns>組織後的文字信息</returns>
		internal string GetInnerExportedInfo(Type type, string contractName)
		{
			var sb = new StringBuilder();
			if (type != null)
			{
				sb.AppendLine();
				sb.AppendFormat("  參數：T = {0}", type.FullName);
			}
			if (contractName != null)
			{
				sb.AppendLine();
				sb.AppendFormat("  參數：contractName = {0}", contractName);
			}
			sb.AppendLine();
			sb.Append("現在定義了ExportAttribute的如下內容");
			foreach (var part in _container.Catalog.Parts)
			{
				sb.AppendLine();
				sb.AppendFormat("  {0}", part.ToString());
			}
			return sb.ToString();
		}

		/// <summary>
		/// 生成指定類型的實例
		/// </summary>
		/// <typeparam name="T">類型參數</typeparam>
		/// <returns>指定類型的實例</returns>
		public Lazy<T> GetExport<T>()
		{
			try
			{
				return _container.GetExport<T>();
			}
			catch (Exception ex)
			{
				throw CreateComposerException(typeof(T), null, ex);
			}
		}

		/// <summary>
		/// 生成指定類型的實例
		/// </summary>
		/// <typeparam name="T">類型參數</typeparam>
		/// <param name="contractName">契約名稱</param>
		/// <returns>指定類型的實例</returns>
		public Lazy<T> GetExport<T>(string contractName)
		{
			try
			{
				return _container.GetExport<T>(contractName);
			}
			catch (Exception ex)
			{
				throw CreateComposerException(typeof(T), contractName, ex);
			}
		}

		/// <summary>
		/// 生成指定類型的實例
		/// </summary>
		/// <typeparam name="T">類型參數</typeparam>
		/// <param name="contractName">契約名稱</param>
		/// <returns>指定類型的實例</returns>
		public T GetExportedValue<T>(string contractName)
		{
			try
			{
				return _container.GetExportedValue<T>(contractName);
			}
			catch (Exception ex)
			{
				throw CreateComposerException(typeof(T), contractName, ex);
			}
		}

		/// <summary>
		/// 生成指定類型的實例
		/// </summary>
		/// <typeparam name="T">類型參數</typeparam>
		/// <returns>指定類型的實例</returns>
		public T GetExportedValue<T>()
		{
			try
			{
				return _container.GetExportedValue<T>();
			}
			catch (Exception ex)
			{
				throw CreateComposerException(typeof(T), null, ex);
			}
		}

		/// <summary>
		/// 生成指定類型的實例
		/// </summary>
		/// <typeparam name="T">類型參數</typeparam>
		/// <typeparam name="TMetadataView">元数据</typeparam>
		/// <param name="contractName">契約名稱</param>
		/// <returns>指定類型的實例</returns>
		public Lazy<T, TMetadataView> GetExport<T, TMetadataView>(string contractName)
		{
			try
			{
				return _container.GetExport<T, TMetadataView>(contractName);
			}
			catch (Exception ex)
			{
				throw CreateComposerException(typeof(T), contractName, ex);
			}
		}

		/// <summary>
		/// 生成指定類型的實例
		/// </summary>
		/// <param name="contractName">契約名稱</param>
		/// <param name="policy">部件共享性（默認值 = <see cref="CreationPolicy.NonShared"/>）</param>
		/// <returns>指定類型的實例</returns>
		public IEnumerable<Export> GetExports(string contractName, CreationPolicy policy = CreationPolicy.NonShared)
		{
			// 使用指定的契約名稱、必須的類型標識、必須的元數據、基數、創建策略、指定導入定義是否可重新組合或是必備組建，該類型實例化（初期化）
			var id = new ContractBasedImportDefinition(contractName, null, null, ImportCardinality.ZeroOrMore, false, false, policy);
			return _container.GetExports(id);
		}

		/// <summary>
		/// 生成指定類型的實例
		/// </summary>
		/// <typeparam name="T">類型參數</typeparam>
		/// <returns>指定類型的實例</returns>
		public IEnumerable<Lazy<T>> GetExports<T>()
		{
			return _container.GetExports<T>();
		}

		/// <summary>
		/// 生成指定類型的實例
		/// </summary>
		/// <typeparam name="T">類型參數</typeparam>
		/// <param name="contractName">契約名稱</param>
		/// <returns>指定類型的實例</returns>
		public IEnumerable<Lazy<T>> GetExports<T>(string contractName)
		{
			return _container.GetExports<T>(contractName);
		}

		/// <summary>
		/// 生成指定類型的實例
		/// </summary>
		/// <typeparam name="T">類型參數</typeparam>
		/// <typeparam name="TMetadataView">元數據</typeparam>
		/// <returns>指定類型的實例</returns>
		public IEnumerable<Lazy<T, TMetadataView>> GetExports<T, TMetadataView>()
		{
			return _container.GetExports<T, TMetadataView>();
		}

		/// <summary>
		/// 從容器中釋放Export對象
		/// </summary>
		/// <param name="export">要釋放的Export對象</param>
		public void ReleaseExport(Export export)
		{
			_container.ReleaseExport(export);
		}

		/// <summary>
		/// 從容器中釋放Export對象
		/// </summary>
		/// <param name="export">要釋放的Export對象</param>
		public void ReleaseExport<T>(Lazy<T> export)
		{
			_container.ReleaseExport<T>(export);
		}

		/// <summary>
		/// 從容器中釋放Export對象
		/// </summary>
		/// <param name="exports">要釋放的Export對象</param>
		public void ReleaseExports(IEnumerable<Export> exports)
		{
			_container.ReleaseExports(exports);
		}

		/// <summary>
		/// 從容器中釋放Export對象
		/// </summary>
		/// <param name="exports">要釋放的Export對象</param>
		public void ReleaseExport<T>(IEnumerable<Lazy<T>> exports)
		{
			_container.ReleaseExports<T>(exports);
		}

		/// <summary>
		/// 資源釋放
		/// </summary>
		protected override void DisposeManaged()
		{
			if (_container != null)
			{
				_container.Dispose();
			}
		}
	}
}