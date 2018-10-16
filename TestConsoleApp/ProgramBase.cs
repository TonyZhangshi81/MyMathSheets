using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using MyMathSheets.TheFormulaShows;
using System;
using System.Linq;
using System.Text;

namespace MyMathSheets.TestConsoleApp
{
	/// <summary>
	/// Programの父類
	/// </summary>
	public class ProgramBase
	{
		/// <summary>
		/// 啟動時所使用的函數
		/// </summary>
		public virtual void Start()
		{
			Console.OutputEncoding = Encoding.Unicode;

			MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
			ArithmeticParameter acParameter = null;
			EqualityComparisonParameter ecParameter = null;
			ComputingConnectionParameter ccParameter = null;
			MathWordProblemsParameter mpParameter = null;
			FruitsLinkageParameter flParameter = null;
			FindNearestNumberParameter fnParameter = null;
			CombinatorialEquationParameter ceParameter = null;
			ScoreGoalParameter sgParameter = null;
			HowMuchMoreParameter hmmParameter = null;

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
					Console.WriteLine("************************* 算式組合 ***********************");
					Console.WriteLine("    u-算式組合");
					Console.WriteLine("************************* 射門得分 ***********************");
					Console.WriteLine("    v-隨機四則運算");
					Console.WriteLine("    w-加法");
					Console.WriteLine("    x-減法");
					Console.WriteLine("************************* 比多少 ***********************");
					Console.WriteLine("    y-比多少");
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
						Console.WriteLine();
						Console.WriteLine("隨機四則運算填空");

						acParameter = (ArithmeticParameter)work.Structure(LayoutSetting.Preview.Arithmetic, "AC001");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case ConsoleKey.B:
						Console.WriteLine();
						Console.WriteLine("標準加法填空");

						acParameter = (ArithmeticParameter)work.Structure(LayoutSetting.Preview.Arithmetic, "AC002");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case ConsoleKey.C:
						Console.WriteLine();
						Console.WriteLine("標準減法填空");

						acParameter = (ArithmeticParameter)work.Structure(LayoutSetting.Preview.Arithmetic, "AC003");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case ConsoleKey.D:
						Console.WriteLine();
						Console.WriteLine("標準乘法填空");

						acParameter = (ArithmeticParameter)work.Structure(LayoutSetting.Preview.Arithmetic, "AC004");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case ConsoleKey.E:
						Console.WriteLine();
						Console.WriteLine("標準除法填空");

						acParameter = (ArithmeticParameter)work.Structure(LayoutSetting.Preview.Arithmetic, "AC005");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;
					case ConsoleKey.F:
						Console.WriteLine();
						Console.WriteLine("隨機四則運算比較");

						ecParameter = (EqualityComparisonParameter)work.Structure(LayoutSetting.Preview.EqualityComparison, "EC001");
						Util.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case ConsoleKey.G:
						Console.WriteLine();
						Console.WriteLine("標準加法比較");
						ecParameter = (EqualityComparisonParameter)work.Structure(LayoutSetting.Preview.EqualityComparison, "EC002");
						Util.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case ConsoleKey.H:
						Console.WriteLine();
						Console.WriteLine("標準減法比較");
						ecParameter = (EqualityComparisonParameter)work.Structure(LayoutSetting.Preview.EqualityComparison, "EC003");
						Util.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case ConsoleKey.I:
						Console.WriteLine();
						Console.WriteLine("標準加法運算接龍");
						ccParameter = (ComputingConnectionParameter)work.Structure(LayoutSetting.Preview.ComputingConnection, "CC001");
						Util.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case ConsoleKey.J:
						Console.WriteLine();
						Console.WriteLine("標準加法運算接龍");
						ccParameter = (ComputingConnectionParameter)work.Structure(LayoutSetting.Preview.ComputingConnection, "CC002");
						Util.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case ConsoleKey.K:
						Console.WriteLine();
						Console.WriteLine("標準減法運算接龍");
						ccParameter = (ComputingConnectionParameter)work.Structure(LayoutSetting.Preview.ComputingConnection, "CC003");
						Util.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case ConsoleKey.L:
						Console.WriteLine();
						Console.WriteLine("隨機四則運算應用題");
						mpParameter = (MathWordProblemsParameter)work.Structure(LayoutSetting.Preview.MathWordProblems, "MP001");
						Util.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case ConsoleKey.M:
						Console.WriteLine();
						Console.WriteLine("加法應用題");
						mpParameter = (MathWordProblemsParameter)work.Structure(LayoutSetting.Preview.MathWordProblems, "MP002");
						Util.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case ConsoleKey.N:
						Console.WriteLine();
						Console.WriteLine("減法應用題");
						mpParameter = (MathWordProblemsParameter)work.Structure(LayoutSetting.Preview.MathWordProblems, "MP003");
						Util.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case ConsoleKey.O:
						Console.WriteLine();
						Console.WriteLine("隨機四則運算連連看");
						flParameter = (FruitsLinkageParameter)work.Structure(LayoutSetting.Preview.FruitsLinkage, "FL001");
						Util.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case ConsoleKey.P:
						Console.WriteLine();
						Console.WriteLine("加法連連看");
						flParameter = (FruitsLinkageParameter)work.Structure(LayoutSetting.Preview.FruitsLinkage, "FL002");
						Util.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case ConsoleKey.Q:
						Console.WriteLine();
						Console.WriteLine("減法連連看");
						flParameter = (FruitsLinkageParameter)work.Structure(LayoutSetting.Preview.FruitsLinkage, "FL003");
						Util.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case ConsoleKey.R:
						Console.WriteLine();
						Console.WriteLine("尋找最近的數字");
						fnParameter = (FindNearestNumberParameter)work.Structure(LayoutSetting.Preview.FindNearestNumber, "FN001");
						Util.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case ConsoleKey.S:
						Console.WriteLine();
						Console.WriteLine("加法");
						fnParameter = (FindNearestNumberParameter)work.Structure(LayoutSetting.Preview.FindNearestNumber, "FN002");
						Util.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case ConsoleKey.T:
						Console.WriteLine();
						Console.WriteLine("減法");
						fnParameter = (FindNearestNumberParameter)work.Structure(LayoutSetting.Preview.FindNearestNumber, "FN003");
						Util.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case ConsoleKey.U:
						Console.WriteLine();
						Console.WriteLine("算式組合");
						ceParameter = (CombinatorialEquationParameter)work.Structure(LayoutSetting.Preview.CombinatorialEquation, "CE001");
						Util.ConsoleFormulas(LayoutSetting.Preview.CombinatorialEquation, ceParameter.Formulas.ToList());
						break;

					case ConsoleKey.V:
						Console.WriteLine();
						Console.WriteLine("隨機足球射門");
						sgParameter = (ScoreGoalParameter)work.Structure(LayoutSetting.Preview.ScoreGoal, "SG001");
						Util.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case ConsoleKey.W:
						Console.WriteLine();
						Console.WriteLine("加法足球射門");
						sgParameter = (ScoreGoalParameter)work.Structure(LayoutSetting.Preview.ScoreGoal, "SG002");
						Util.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case ConsoleKey.X:
						Console.WriteLine();
						Console.WriteLine("減法足球射門");
						sgParameter = (ScoreGoalParameter)work.Structure(LayoutSetting.Preview.ScoreGoal, "SG003");
						Util.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case ConsoleKey.Y:
						Console.WriteLine();
						Console.WriteLine("比多少");
						hmmParameter = (HowMuchMoreParameter)work.Structure(LayoutSetting.Preview.HowMuchMore, "HMM001");
						Util.ConsoleFormulas(LayoutSetting.Preview.HowMuchMore, hmmParameter.Formulas.ToList());
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


		/// <summary>
		/// 
		/// </summary>
		protected ProgramBase()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var exception = (Exception)e.ExceptionObject;
			Console.WriteLine(exception.Message);
			Console.ReadKey();
			Environment.Exit(-1);
		}
	}

}
