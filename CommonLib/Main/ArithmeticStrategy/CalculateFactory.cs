using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MyMathSheets.CommonLib.Main.ArithmeticStrategy
{
	/// <summary>
	/// 運算符對象生產工廠
	/// </summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(ICalculateFactory))]
	public class CalculateFactory : ICalculateFactory
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly Dictionary<string, ICalculate> _cacheStrategy;

		/// <summary>
		/// 構造函數
		/// </summary>
		[ImportingConstructor]
		public CalculateFactory()
		{
			_cacheStrategy = new Dictionary<string, ICalculate>();
		}

		/// <summary>
		/// 
		/// </summary>
		public IEnumerable<Lazy<CalculateBase, ICalculateMetadata>> Operations { get; set; }

		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		public ICalculate CreateCalculateInstance(SignOfOperation sign)
		{
			if (!_cacheStrategy.ContainsKey(sign.ToString()))
			{
				Operations = ComposerFactory.GetComporser(SystemModel.ComputationalStrategy).GetExports<CalculateBase, ICalculateMetadata>();

				var operation = Operations.Where(d => d.Metadata.Sign == sign);
				_cacheStrategy.Add(sign.ToString(), (ICalculate)Activator.CreateInstance(operation.First().Value.GetType()));
			}
			return _cacheStrategy[sign.ToString()];
		}
	}
}
