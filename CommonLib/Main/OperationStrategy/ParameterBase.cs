using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Provider;
using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;
using System.Configuration;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public class ParameterBase : IParameter
	{
		/// <summary>
		/// 
		/// </summary>
		private ProviderConfigurationSection config = null;
		/// <summary>
		/// 
		/// </summary>
		private OperationParameterProvider _provider;
		/// <summary>
		/// 
		/// </summary>
		private OperationParameterProviderCollection _providerCollection;

		/// <summary>
		/// 識別號
		/// </summary>
		public string Identifier { get; set; }
		/// <summary>
		/// 題型（標準、隨機填空）
		/// </summary>
		public QuestionType QuestionType { get; set; }
		/// <summary>
		/// 在四则运算标准题下指定运算法（加减乘除）
		/// </summary>
		public IList<SignOfOperation> Signs { get; set; }
		/// <summary>
		/// 四则运算类型（标准、随机出题）
		/// </summary>
		public FourOperationsType FourOperationsType { get; set; }
		/// <summary>
		/// 运算结果最大限度值
		/// </summary>
		public int MaximumLimit { get; set; }
		/// <summary>
		/// 出题数量
		/// </summary>
		public int NumberOfQuestions { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public virtual void InitParameter()
		{
		}

		private void LoadProvider()
		{
			if (_provider == null)
			{
				// 获得blogProvider节点信息
				config = (ProviderConfigurationSection)ConfigurationManager.GetSection("OperationParameterProvider");

				_providerCollection = new OperationParameterProviderCollection();
				//下面这个方法是系统提供的，位于System.web下。如果编写的是form程序则需要自己实现这个providhelper
				//有兴趣的 可以查看一下他的源码
				ProvidersHelper.InstantiateProviders(config.Providers, _providerCollection, typeof(OperationParameterProvider));
				//上面那个方法已经加载了providerCollection，这里我们只要DefaultProvider的provider即可
				_provider = _providerCollection[config.DefaultProvider];
			}
		}
	}
}
