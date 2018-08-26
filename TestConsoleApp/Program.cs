using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFormulaShows;

namespace TestConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			MakeHtml work = new MakeHtml();
			work.MaximumLimit = 50;
			work.NumberOf = 35;
			work.Structure();

			work.FormulaList.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} + {1} = {2}", d.LeftParameter, d.RightParameter, d.Answer));
			});
			Console.ReadKey();
		}
	}
}
