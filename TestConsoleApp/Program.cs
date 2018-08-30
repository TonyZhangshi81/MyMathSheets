using ComputationalStrategy.Item;
using ComputationalStrategy.Main.Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
				Console.WriteLine("參數選擇：");
				Console.WriteLine("******************* 四則運算 *******************");
				Console.WriteLine("    1-隨機四則運算填空");
				Console.WriteLine("    2-標準加法填空");
				Console.WriteLine("    3-標準減法填空");
				Console.WriteLine("    4-標準乘法填空");
				Console.WriteLine("    5-標準除法填空");
				Console.WriteLine("************************************************");
				Console.WriteLine("******************* 計算比大小 *******************");
				Console.WriteLine("    6-隨機四則運算比較");
				Console.WriteLine("    7-標準加法比較");
				Console.WriteLine("    8-標準減法比較");
				Console.WriteLine("************************************************");
				Console.Write("    0-退出");
				Console.WriteLine("");
				Console.Write("");


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
						Console.WriteLine("隨機四則運算填空");
						// TestCase0001
						work = new Arithmetic(FourOperationsType.Random, signs, QuestionType.GapFilling, 100, 35);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D2:
						Console.WriteLine();
						Console.WriteLine("標準加法填空");
						// TestCase0002
						work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.GapFilling, 50, 20);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D3:
						Console.WriteLine();
						Console.WriteLine("標準減法填空");
						// TestCase0003
						work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.GapFilling, 200, 15);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D4:
						Console.WriteLine();
						Console.WriteLine("標準乘法填空");
						// TestCase0004
						work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Multiple, QuestionType.GapFilling, 81, 20);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D5:
						Console.WriteLine();
						Console.WriteLine("標準除法填空");
						// TestCase0005
						work = new Arithmetic(FourOperationsType.Standard, SignOfOperation.Division, QuestionType.GapFilling, 81, 20);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D7:
						Console.WriteLine();
						Console.WriteLine("標準加法比較");
						// TestCase0005
						work = new EqualityComparison(FourOperationsType.Standard, SignOfOperation.Division, QuestionType.GapFilling, 81, 20);
						work.Structure();
						ConsoleFormulas(work.Formulas);
						break;

					case ConsoleKey.D8:
						Console.WriteLine();
						Console.WriteLine("標準減法比較");
						// TestCase0005
						work = new EqualityComparison(FourOperationsType.Standard, SignOfOperation.Division, QuestionType.GapFilling, 81, 20);
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
					 GetOperation(d.Sign),
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
