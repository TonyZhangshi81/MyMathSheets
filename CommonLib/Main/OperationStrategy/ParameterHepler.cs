using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Main.Provider;
using MyMathSheets.CommonLib.Provider;
using System;
using System.Configuration;
using System.Linq;

namespace MyMathSheets.CommonLib.OperationStrategy
{
	/// <summary>
	///
	/// </summary>
	public sealed class ParameterHepler
	{
		/// <summary>
		/// 共通處理模塊Composer
		/// </summary>
		private readonly Composer _composer;

		/// <summary>
		/// 實例化
		/// </summary>
		public ParameterHepler()
		{
			// 獲取共通處理模塊Composer
			_composer = ComposerFactory.GetComporser(this.GetType().Assembly);
		}

		/// <summary>
		/// 參數Provider 實例化
		/// </summary>
		/// <returns>Provider 實例</returns>
		public OperationParameterProvider CreateParameterProvider()
		{
			var section = (OperationProviderConfigurationSection)ConfigurationManager.GetSection("Operation");

			OperationParameterProvider provider = _composer
													.GetExports<OperationParameterProvider, IParameterProviderMetaDataView>()
													.Where(d => d.Metadata.Name.Equals(section.ProviderType, StringComparison.CurrentCultureIgnoreCase))
													.FirstOrDefault().Value;

			OperationParameterProvider instance = (OperationParameterProvider)Activator.CreateInstance(provider.GetType());
			instance.Argument = section.Argument;

			// 實例化
			return instance;
		}
	}
}