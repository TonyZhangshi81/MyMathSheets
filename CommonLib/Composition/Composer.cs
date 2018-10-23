using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Composition
{
    /// <summary>
    /// 
    /// </summary>
    public class Composer : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly CompositionContainer _container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        public Composer(Assembly assembly)
        {
            var catalog = new AggregateCatalog();
            var cache = new List<Assembly>();

            // アセンブリを参照順に追加（最初に渡したAssemblyが返ってくる）
            foreach(var asm in ReflectionUtil.GetReferencedAssemblies(assembly))
            {
                if(!cache.Contains(asm))
                {
                    catalog.Catalogs.Add(new AssemblyCatalog(asm));
                    cache.Add(asm);
                }
            }

            // プロパティにマッピング
            this._container = new CompositionContainer(catalog, true);
        }

        /// <summary>
        /// 指定されたインスタンスに対して合成処理を行います。
        /// </summary>
        /// <param name="target"></param>
        public void Compose(object target)
        {
            var part = AttributedModelServices.CreatePart(target);
            if(part.ImportDefinitions.Any())
            {
                this._container.SatisfyImportsOnce(part);
            }
        }

        /// <summary>
        /// 対象となるExportが見つからなかった場合の例外を生成します。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="contractName"></param>
        /// <param name="innerException"></param>
        /// <returns></returns>
        public Exception CreateLogicComposerException(Type type, string contractName, Exception innerException)
        {
            var sb = new StringBuilder();
            sb.Append(MessageUtil.GetException(() => MsgResources.E0001L));
            sb.Append(this.GetInnerExportedInfo(type, contractName));
            return new ComposerException(sb.ToString(), innerException);
        }

        /// <summary>
        /// <see cref="Composer"/>の内部で管理されている情報を文字列で取得します。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="contractName"></param>
        /// <returns></returns>
        internal string GetInnerExportedInfo(Type type, string contractName)
        {
            var sb = new StringBuilder();
            if(type != null)
            {
                sb.AppendLine();
                sb.AppendFormat("  引数：T = {0}", type.FullName);
            }
            if(contractName != null)
            {
                sb.AppendLine();
                sb.AppendFormat("  引数：contractName = {0}", contractName);
            }
            sb.AppendLine();
            sb.Append("現在ExportAttributeが定義されているものは以下になります。");
            foreach(var part in this._container.Catalog.Parts)
            {
                sb.AppendLine();
                sb.AppendFormat("  {0}", part.ToString());
            }
            return sb.ToString();
        }

		/// <summary>
		/// 指定された型のインスタンスを生成します。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public Lazy<T> GetExport<T>()
		{
			try
			{
				return this._container.GetExport<T>();
			}
			catch (Exception ex)
			{
				throw this.CreateLogicComposerException(typeof(T), null, ex);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="contractName"></param>
		/// <returns></returns>
		public Lazy<T> GetExport<T>(string contractName)
        {
            try
            {
                return this._container.GetExport<T>(contractName);
            }
            catch(Exception ex)
            {
                throw this.CreateLogicComposerException(typeof(T), contractName, ex);
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="contractName"></param>
		/// <returns></returns>
		public T GetExportedValue<T>(string contractName)
        {
            try
            {
                return this._container.GetExportedValue<T>(contractName);
            }
            catch(Exception ex)
            {
                throw this.CreateLogicComposerException(typeof(T), contractName, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetExportedValue<T>()
        {
            try
            {
                return this._container.GetExportedValue<T>();
            }
            catch(Exception ex)
            {
                throw this.CreateLogicComposerException(typeof(T), null, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMetadataView"></typeparam>
        /// <param name="contractName"></param>
        /// <returns></returns>
        public Lazy<T, TMetadataView> GetExport<T, TMetadataView>(string contractName)
        {
            try
            {
                return this._container.GetExport<T, TMetadataView>(contractName);
            }
            catch(Exception ex)
            {
                throw this.CreateLogicComposerException(typeof(T), contractName, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Export> GetExports(string contractName)
        {
            var id = new ContractBasedImportDefinition(contractName, null, null, ImportCardinality.ZeroOrMore, false, false,
                                                       CreationPolicy.NonShared);
            return this._container.GetExports(id);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public IEnumerable<Lazy<T>> GetExports<T>()
		{
			return this._container.GetExports<T>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TMetadataView"></typeparam>
		/// <returns></returns>
		public IEnumerable<Lazy<T, TMetadataView>> GetExports<T, TMetadataView>()
        {
            return this._container.GetExports<T, TMetadataView>();
        }

        /// <summary>
        /// 
        /// </summary>
        void IDisposable.Dispose()
        {
            if(this._container != null)
            {
                this._container.Dispose();
            }
        }

		/// <summary>
		/// Exportオブジェクトをコンテナから開放します。
		/// </summary>
		/// <param name="export">解放するExport</param>
		public void ReleaseExport(Export export)
		{
			this._container.ReleaseExport(export);
		}

		/// <summary>
		/// Exportオブジェクトをコンテナから開放します。
		/// </summary>
		/// <param name="export">解放するExport</param>
		public void ReleaseExport<T>(Lazy<T> export)
		{
			this._container.ReleaseExport<T>(export);
		}

		/// <summary>
		/// Exportオブジェクトをコンテナから開放します。
		/// </summary>
		/// <param name="exports">解放するExport</param>
		public void ReleaseExports(IEnumerable<Export> exports)
		{
			this._container.ReleaseExports(exports);
		}

		/// <summary>
		/// Exportオブジェクトをコンテナから開放します。
		/// </summary>
		/// <param name="exports">解放するExport</param>
		public void ReleaseExport<T>(IEnumerable<Lazy<T>> exports)
		{
			this._container.ReleaseExports<T>(exports);
		}
	}
}
