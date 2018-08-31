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
			MakeHtml<List<Formula>, Arithmetic> work = null;
			MakeHtml<List<EqualityFormula>, EqualityComparison> work1 = null;
			bool isShowMenu = true;

			while (1 == 1)
			{
				if (isShowMenu)
				{
					Console.WriteLine("參數選擇：");
					Console.WriteLine(" 四則運算： ");
					Console.WriteLine("    1-隨機四則運算填空    2-標準加法填空    3-標準減法填空    4-標準乘法填空    5-標準除法填空");
					Console.WriteLine(" 計算比大小： ");
					Console.WriteLine("    6-隨機四則運算比較    7-標準加法比較    8-標準減法比較");
					Console.WriteLine("*************************");
					Console.Write("    9-菜单    0-退出");
					Console.WriteLine("");
					Console.Write("");

					isShowMenu = false;
				}

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
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Random, signs, QuestionType.GapFilling, 100, 35);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.D2:
						Console.WriteLine();
						Console.WriteLine("標準加法填空");
						// TestCase0002
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.GapFilling, 50, 20);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.D3:
						Console.WriteLine();
						Console.WriteLine("標準減法填空");
						// TestCase0003
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.GapFilling, 200, 15);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.D4:
						Console.WriteLine();
						Console.WriteLine("標準乘法填空");
						// TestCase0004
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Standard, SignOfOperation.Multiple, QuestionType.GapFilling, 81, 20);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.D5:
						Console.WriteLine();
						Console.WriteLine("標準除法填空");
						// TestCase0005
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Standard, SignOfOperation.Division, QuestionType.GapFilling, 81, 20);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.D6:
						Console.WriteLine();
						Console.WriteLine("隨機四則運算比較");
						// TestCase0006
						work1 = new MakeHtml<List<EqualityFormula>, EqualityComparison>(FourOperationsType.Random, new List<SignOfOperation> { SignOfOperation.Plus, SignOfOperation.Subtraction }, QuestionType.GapFilling, 81, 20);
						work1.Structure();
						Util.CreateOperatorObjectFactory<List<EqualityFormula>>("EqualityFormulaWrite", work1.Formulas);
						break;

					case ConsoleKey.D7:
						Console.WriteLine();
						Console.WriteLine("標準加法比較");
						// TestCase0005
						work1 = new MakeHtml<List<EqualityFormula>, EqualityComparison>(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.GapFilling, 81, 20);
						work1.Structure();
						Util.CreateOperatorObjectFactory<List<EqualityFormula>>("EqualityFormulaWrite", work1.Formulas);
						break;

					case ConsoleKey.D8:
						Console.WriteLine();
						Console.WriteLine("標準減法比較");
						// TestCase0005
						work1 = new MakeHtml<List<EqualityFormula>, EqualityComparison>(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.GapFilling, 81, 20);
						work1.Structure();
						Util.CreateOperatorObjectFactory<List<EqualityFormula>>("EqualityFormulaWrite", work1.Formulas);
						break;

					case ConsoleKey.D9:
						isShowMenu = true;
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


	}
}
