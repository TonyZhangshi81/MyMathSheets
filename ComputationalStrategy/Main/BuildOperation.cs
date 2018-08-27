using ComputationalStrategy.Item;
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
		public BuildOperation(ICalculatePattern strategy, int maximumLimit)
		{
			this.strategy = strategy;
			this.maximumLimit = maximumLimit;
			this.QType = QuestionTypes.Standard;
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
