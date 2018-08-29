using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFormulaShows;

namespace TestConsoleApp
{
	/// <summary>
	/// 
	/// </summary>
	class Program
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			IList<SignOfOperation> signs = new List<SignOfOperation>
			{
				SignOfOperation.Plus,
				SignOfOperation.Subtraction
			};

			Console.WriteLine("TestCase0001");
			Console.ReadKey();
			// TestCase0001
			MakeHtmlBase work = new Arithmetic(FourOperationsType.Random, signs, QuestionType.GapFilling, 100, 12);
			work.Structure();
			ConsoleFormulas(work.Formulas);

			Console.WriteLine("TestCase0002");
			Console.ReadKey();
			// TestCase0002
			work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.GapFilling, 50, 20);
			work.Structure();
			ConsoleFormulas(work.Formulas);

			Console.WriteLine("TestCase0003");
			Console.ReadKey();
			// TestCase0003
			work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.GapFilling, 200, 15);
			work.Structure();
			ConsoleFormulas(work.Formulas);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		private static void ConsoleFormulas(IList<Formula> formulas)
		{
			formulas.ToList().ForEach(d =>
			 {
				 Console.WriteLine(string.Format("{0} {1} {2} = {3}",
					 GetValue(GapFilling.Left, d.LeftParameter, d.Gap),
					 GetOperation(d.SignOfOperation),
					 GetValue(GapFilling.Right, d.RightParameter, d.Gap),
					 GetValue(GapFilling.Answer, d.Answer, d.Gap)));
			 });
			Console.ReadKey();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="html"></param>
		static void Write(string html)
		{
			var path = @"C:\Users\zhangcg.NTDOMAIN\source\repos\MyMathSheets\TestConsoleApp\App_Data\01.txt";
			using (FileStream fs = new FileStream(path, FileMode.Create))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(html);
					sw.Flush();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		static string GetOperation(SignOfOperation operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfOperation.Plus:
					flag = "+";
					break;
				case SignOfOperation.Subtraction:
					flag = "-";
					break;
				case SignOfOperation.Division:
					flag = "÷";
					break;
				case SignOfOperation.Multiple:
					flag = "×";
					break;
			}
			return flag;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="parameter"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		static string GetValue(GapFilling item, int parameter, GapFilling gap)
		{
			if (item == gap)
			{
				return string.Format("({0})", parameter);
			}
			return parameter.ToString();
		}
	}
}
