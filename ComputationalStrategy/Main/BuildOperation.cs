using ComputationalStrategy.Item;
using Spring.Context.Support;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Main
{
	public class BuildOperation
	{
		/// <summary>
		/// 
		/// </summary>
		private ICalculatePattern strategy;
		/// <summary>
		/// 
		/// </summary>
		public QuestionTypes QType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		private readonly int maximumLimit;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="strategy"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="type"></param>
		public BuildOperation(ICalculatePattern strategy, int maximumLimit, QuestionTypes type = QuestionTypes.Standard)
		{
			this.strategy = strategy;
			this.maximumLimit = maximumLimit;
			this.QType = type;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="type"></param>
		public BuildOperation(int maximumLimit, QuestionTypes type = QuestionTypes.Standard)
		{
			IResource input = new FileSystemResource(@"..\Config\Operator.xml");
			IObjectFactory factory = new XmlObjectFactory(input);


			ICalculatePattern per = (ICalculatePattern)factory.GetObject("Adition");

		}








		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public Formula GetFormula()
		{
			return strategy.Make(maximumLimit, this.QType);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="numberOf"></param>
		/// <returns></returns>
		public IList<Formula> GetFormulaList(int numberOf)
		{
			var list = new List<Formula>();
			for (var i=0; i < numberOf; i++)
			{
				list.Add(strategy.Make(maximumLimit, this.QType));
			}

			return list;
		}
	}
}
