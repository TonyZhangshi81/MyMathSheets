using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Main.Provider;
using System;
using System.Configuration;
using System.Linq;

namespace MyMathSheets.CommonLib.OperationStrategy
{
	/// <summary>
	/// 策略參數支援類，完成參數從JSON的導入處理
	/// </summary>
	public sealed class TopicParameterHepler
	{
		/// <summary>
		/// 共通處理模塊Composer
		/// </summary>
		private readonly Composer _composer;

		/// <summary>
		/// 實例化
		/// </summary>
		public TopicParameterHepler()
		{
			// 獲取共通處理模塊Composer
			_composer = ComposerFactory.GetComporser(this.GetType().Assembly);
		}

		/// <summary>
		/// 參數Provider 實例化
		/// </summary>
		/// <returns>Provider 實例</returns>
		public TopicParameterProvider CreateParameterProvider()
		{
			var section = (TopicProviderConfigurationSection)ConfigurationManager.GetSection("Topic");

			TopicParameterProvider provider = _composer
													.GetExports<TopicParameterProvider, ITopicParameterProviderMetaDataView>()
													.Where(d => d.Metadata.ImportType.Equals(section.ImportType, StringComparison.CurrentCultureIgnoreCase))
													.FirstOrDefault().Value;

			TopicParameterProvider instance = (TopicParameterProvider)Activator.CreateInstance(provider.GetType());
			instance.Argument = section.Argument;
			instance.ReplenishArgument = section.ReplenishArgument;

			// 實例化
			return instance;
		}
	}
}