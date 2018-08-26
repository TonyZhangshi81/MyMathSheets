﻿using ComputationalStrategy.Item;
using ComputationalStrategy.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFormulaShows
{
	/// <summary>
	/// 
	/// </summary>
    public class MakeHtml
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
		public IList<Formula> FormulaList { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public MakeHtml()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public void Structure()
		{
			var main = new BuildOperation(new AditionStrategy(), this.MaximumLimit);
			this.FormulaList = main.GetFormulaList(this.NumberOf);
		}
	}
}
