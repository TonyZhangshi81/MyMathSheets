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
	public class Program
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			MakeHtmlBase work = null;

			while (1 == 1)
			{
				ConsoleKey key = Console.ReadKey().Key;
				switch (key)
				{
					case ConsoleKey.D1:
						IList<SignOfOperation> signs = new List<SignOfOperation>
						{
							SignOfOperation.Plus,
							SignOfOperation.Subtraction,
							SignOfOperation.Multiple,
							SignOfOperation.Division
						};

						Console.WriteLine();
						Console.WriteLine("TestCase0001");
						// TestCase0001
						work = new Arithmetic(FourOperationsType.Random, signs, QuestionType.GapFilling, 100, 35);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D2:
						Console.WriteLine();
						Console.WriteLine("TestCase0002");
						// TestCase0002
						work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.GapFilling, 50, 20);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D3:
						Console.WriteLine();
						Console.WriteLine("TestCase0003");
						// TestCase0003
						work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.GapFilling, 200, 15);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D4:
						Console.WriteLine();
						Console.WriteLine("TestCase0004");
						// TestCase0004
						work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Multiple, QuestionType.GapFilling, 81, 20);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D5:
						Console.WriteLine();
						Console.WriteLine("TestCase0005");
						// TestCase0005
						work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Division, QuestionType.GapFilling, 81, 20);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					default:
						Console.WriteLine();
						Console.WriteLine("Close");
						Console.ReadKey();
						Environment.Exit(0);
						break;
				}
			}
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
