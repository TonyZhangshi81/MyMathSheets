using CommonLib.Util;
using ComputationalStrategy.Item;
using ComputationalStrategy.Main.Operation;
using System;
using System.Collections.Generic;
using System.Text;
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
			Console.OutputEncoding = Encoding.Unicode;

			MakeHtml<List<Formula>, Arithmetic> work = null;
			MakeHtml<List<EqualityFormula>, EqualityComparison> work1 = null;
			MakeHtml<List<ConnectionFormula>, ComputingConnection> work2 = null;
			MakeHtml<List<MathWordProblemsFormula>, MathWordProblems> work3 = null;
			MakeHtml<FruitsLinkageFormula, FruitsLinkage> work4 = null;
			MakeHtml<List<EqualityFormula>, FindNearestNumber> work5 = null;

			bool isShowMenu = true;

			while (1 == 1)
			{
				if (isShowMenu)
				{
					Console.WriteLine("參數選擇：");
					Console.WriteLine("************************* 四則運算 *************************");
					Console.WriteLine("    a-隨機四則運算填空");
					Console.WriteLine("    b-標準加法填空");
					Console.WriteLine("    c-標準減法填空");
					Console.WriteLine("    d-標準乘法填空");
					Console.WriteLine("    e-標準除法填空");
					Console.WriteLine("************************* 計算比大小 ***********************");
					Console.WriteLine("    f-隨機四則運算比較");
					Console.WriteLine("    g-標準加法比較");
					Console.WriteLine("    h-標準減法比較");
					Console.WriteLine("************************* 計算接龍 ***********************");
					Console.WriteLine("    i-隨機加减運算接龍");
					Console.WriteLine("    j-標準加法運算接龍");
					Console.WriteLine("    k-標準減法運算接龍");
					Console.WriteLine("************************* 應用題 ***********************");
					Console.WriteLine("    l-隨機四則運算應用題");
					Console.WriteLine("    m-加法應用題");
					Console.WriteLine("    n-減法應用題");
					Console.WriteLine("************************* 水果連連看 ***********************");
					Console.WriteLine("    o-隨機四則運算連連看");
					Console.WriteLine("    p-加法連連看");
					Console.WriteLine("    q-減法連連看");
					Console.WriteLine("************************* 尋找最近的數字 ***********************");
					Console.WriteLine("    r-隨機四則運算");
					Console.WriteLine("    s-加法");
					Console.WriteLine("    t-減法");
					Console.WriteLine("*************************");
					Console.Write("    9-菜單    0-退出");
					Console.WriteLine("");
					Console.Write("");

					isShowMenu = false;
				}

				ConsoleKey key = Console.ReadKey().Key;
				switch (key)
				{
					case ConsoleKey.A:
						IList<SignOfOperation> signs = new List<SignOfOperation>
						{
							SignOfOperation.Plus,
							SignOfOperation.Subtraction,
							SignOfOperation.Multiple,
							SignOfOperation.Division
						};

						Console.WriteLine();
						Console.WriteLine("隨機四則運算填空");
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Random, signs, QuestionType.GapFilling, 100, 35);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.B:
						Console.WriteLine();
						Console.WriteLine("標準加法填空");
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.Standard, 50, 20);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.C:
						Console.WriteLine();
						Console.WriteLine("標準減法填空");
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.Standard, 200, 15);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.D:
						Console.WriteLine();
						Console.WriteLine("標準乘法填空");
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Standard, SignOfOperation.Multiple, QuestionType.Standard, 81, 20);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.E:
						Console.WriteLine();
						Console.WriteLine("標準除法填空");
						work = new MakeHtml<List<Formula>, Arithmetic>(FourOperationsType.Standard, SignOfOperation.Division, QuestionType.Standard, 81, 20);
						work.Structure();
						Util.CreateOperatorObjectFactory<List<Formula>>("FormulaWrite", work.Formulas);
						break;

					case ConsoleKey.F:
						Console.WriteLine();
						Console.WriteLine("隨機四則運算比較");
						work1 = new MakeHtml<List<EqualityFormula>, EqualityComparison>(FourOperationsType.Random, new List<SignOfOperation> { SignOfOperation.Plus, SignOfOperation.Subtraction }, QuestionType.Default, 81, 20);
						work1.Structure();
						Util.CreateOperatorObjectFactory<List<EqualityFormula>>("EqualityFormulaWrite", work1.Formulas);
						break;

					case ConsoleKey.G:
						Console.WriteLine();
						Console.WriteLine("標準加法比較");
						work1 = new MakeHtml<List<EqualityFormula>, EqualityComparison>(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.Default, 81, 20);
						work1.Structure();
						Util.CreateOperatorObjectFactory<List<EqualityFormula>>("EqualityFormulaWrite", work1.Formulas);
						break;

					case ConsoleKey.H:
						Console.WriteLine();
						Console.WriteLine("標準減法比較");
						work1 = new MakeHtml<List<EqualityFormula>, EqualityComparison>(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.Default, 81, 20);
						work1.Structure();
						Util.CreateOperatorObjectFactory<List<EqualityFormula>>("EqualityFormulaWrite", work1.Formulas);
						break;

					case ConsoleKey.I:
						Console.WriteLine();
						Console.WriteLine("標準加法運算接龍");
						work2 = new MakeHtml<List<ConnectionFormula>, ComputingConnection>(FourOperationsType.Random, new List<SignOfOperation> { SignOfOperation.Plus, SignOfOperation.Subtraction }, QuestionType.GapFilling, 100, 4);
						work2.Structure();
						Util.CreateOperatorObjectFactory<List<ConnectionFormula>>("ComputingConnectionWrite", work2.Formulas);
						break;

					case ConsoleKey.J:
						Console.WriteLine();
						Console.WriteLine("標準加法運算接龍");
						work2 = new MakeHtml<List<ConnectionFormula>, ComputingConnection>(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.GapFilling, 100, 4);
						work2.Structure();
						Util.CreateOperatorObjectFactory<List<ConnectionFormula>>("ComputingConnectionWrite", work2.Formulas);
						break;

					case ConsoleKey.K:
						Console.WriteLine();
						Console.WriteLine("標準減法運算接龍");
						work2 = new MakeHtml<List<ConnectionFormula>, ComputingConnection>(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.GapFilling, 100, 4);
						work2.Structure();
						Util.CreateOperatorObjectFactory<List<ConnectionFormula>>("ComputingConnectionWrite", work2.Formulas);
						break;

					case ConsoleKey.L:
						Console.WriteLine();
						Console.WriteLine("隨機四則運算應用題");
						work3 = new MakeHtml<List<MathWordProblemsFormula>, MathWordProblems>(FourOperationsType.Random,
																								new List<SignOfOperation>
																								{
																									SignOfOperation.Plus,
																									SignOfOperation.Subtraction,
																									SignOfOperation.Multiple,
																									SignOfOperation.Division
																								}, QuestionType.Default, 50, 10);
						work3.Structure();
						Util.CreateOperatorObjectFactory<List<MathWordProblemsFormula>>("MathWordProblemsFormulaWrite", work3.Formulas);
						break;

					case ConsoleKey.M:
						Console.WriteLine();
						Console.WriteLine("加法應用題");
						work3 = new MakeHtml<List<MathWordProblemsFormula>, MathWordProblems>(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.Default, 20, 10);
						work3.Structure();
						Util.CreateOperatorObjectFactory<List<MathWordProblemsFormula>>("MathWordProblemsFormulaWrite", work3.Formulas);
						break;

					case ConsoleKey.N:
						Console.WriteLine();
						Console.WriteLine("減法應用題");
						work3 = new MakeHtml<List<MathWordProblemsFormula>, MathWordProblems>(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.Default, 20, 10);
						work3.Structure();
						Util.CreateOperatorObjectFactory<List<MathWordProblemsFormula>>("MathWordProblemsFormulaWrite", work3.Formulas);
						break;

					case ConsoleKey.O:
						Console.WriteLine();
						Console.WriteLine("隨機四則運算連連看");
						work4 = new MakeHtml<FruitsLinkageFormula, FruitsLinkage>(FourOperationsType.Random,
																								new List<SignOfOperation>
																								{
																									SignOfOperation.Plus,
																									SignOfOperation.Subtraction
																								}, QuestionType.Default, 50, 10);
						work4.Structure();
						Util.CreateOperatorObjectFactory<FruitsLinkageFormula>("FruitsLinkageWrite", work4.Formulas);
						break;

					case ConsoleKey.P:
						Console.WriteLine();
						Console.WriteLine("加法連連看");
						work4 = new MakeHtml<FruitsLinkageFormula, FruitsLinkage>(FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.Default, 50, 10);
						work4.Structure();
						Util.CreateOperatorObjectFactory<FruitsLinkageFormula>("FruitsLinkageWrite", work4.Formulas);
						break;

					case ConsoleKey.Q:
						Console.WriteLine();
						Console.WriteLine("減法連連看");
						work4 = new MakeHtml<FruitsLinkageFormula, FruitsLinkage>(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.Default, 40, 5);
						work4.Structure();
						Util.CreateOperatorObjectFactory<FruitsLinkageFormula>("FruitsLinkageWrite", work4.Formulas);
						break;

					case ConsoleKey.R:
						Console.WriteLine();
						Console.WriteLine("尋找最近的數字");
						work5 = new MakeHtml<List<EqualityFormula>, FindNearestNumber>(FourOperationsType.Random,
																								new List<SignOfOperation>
																								{
																									SignOfOperation.Plus,
																									SignOfOperation.Subtraction
																								}, QuestionType.GapFilling, 50, 6);
						work5.Structure();
						Util.CreateOperatorObjectFactory<List<EqualityFormula>>("FindNearestNumberWrite", work5.Formulas);
						break;

					case ConsoleKey.S:
						Console.WriteLine();
						Console.WriteLine("加法");
						work5 = new MakeHtml<List<EqualityFormula>, FindNearestNumber> (FourOperationsType.Standard, SignOfOperation.Plus, QuestionType.GapFilling, 50, 3);
						work5.Structure();
						Util.CreateOperatorObjectFactory<List<EqualityFormula>>("FindNearestNumberWrite", work5.Formulas);
						break;

					case ConsoleKey.T:
						Console.WriteLine();
						Console.WriteLine("減法");
						work5 = new MakeHtml<List<EqualityFormula>, FindNearestNumber>(FourOperationsType.Standard, SignOfOperation.Subtraction, QuestionType.GapFilling, 40, 5);
						work5.Structure();
						Util.CreateOperatorObjectFactory<List<EqualityFormula>>("FindNearestNumberWrite", work5.Formulas);
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
