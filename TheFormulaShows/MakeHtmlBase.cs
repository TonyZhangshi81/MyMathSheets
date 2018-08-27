using ComputationalStrategy.Item;
using ComputationalStrategy.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFormulaShows
{
	public abstract class MakeHtmlBase : IMakeHtml
	{
		/// <summary>
		/// 
		/// </summary>
		public int MaximumLimit { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int NumberOf { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public QuestionTypes QType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public IList<Formula> FormulaList { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public abstract string MakeHtml();

		/// <summary>
		/// 
		/// </summary>
		public void Structure()
		{
			var main = new BuildOperation(new SubtractionStrategy(), this.MaximumLimit, this.QType);
			this.FormulaList = main.GetFormulaList(this.NumberOf);
		}
	}
}
