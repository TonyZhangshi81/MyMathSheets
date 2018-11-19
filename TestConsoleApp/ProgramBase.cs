using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Main.Parameters;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Main.Parameters;
using MyMathSheets.ComputationalStrategy.ComputingConnection.Main.Parameters;
using MyMathSheets.ComputationalStrategy.EqualityComparison.Main.Parameters;
using MyMathSheets.ComputationalStrategy.EqualityLinkage.Main.Parameters;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Main.Parameters;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Parameters;
using MyMathSheets.ComputationalStrategy.FruitsLinkage.Main.Parameters;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Parameters;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Parameters;
using MyMathSheets.ComputationalStrategy.NumericSorting.Main.Parameters;
using MyMathSheets.ComputationalStrategy.ScoreGoal.Main.Parameters;
using MyMathSheets.TestConsoleApp.Properties;
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
		private static Log log = Log.LogReady(typeof(ProgramBase));

		/// <summary>
		/// 啟動時所使用的函數
		/// </summary>
		public virtual void Start()
		{
			Console.OutputEncoding = Encoding.Unicode;

			ArithmeticParameter acParameter = null;
			EqualityComparisonParameter ecParameter = null;
			ComputingConnectionParameter ccParameter = null;
			MathWordProblemsParameter mpParameter = null;
			FruitsLinkageParameter flParameter = null;
			FindNearestNumberParameter fnParameter = null;
			CombinatorialEquationParameter ceParameter = null;
			ScoreGoalParameter sgParameter = null;
			HowMuchMoreParameter hmmParameter = null;
			FindTheLawParameter ftlParameter = null;
			NumericSortingParameter nsParameter = null;
			LearnCurrencyParameter lcParameter = null;
			EqualityLinkageParameter elParameter = null;

			bool isShowMenu = true;

			while (1 == 1)
			{
				if (isShowMenu)
				{
					Console.WriteLine("參數選擇：");
					Console.WriteLine("************************* 四則運算 *************************");
					Console.WriteLine("    A-隨機四則運算填空");
					Console.WriteLine("    B-標準加法填空");
					Console.WriteLine("    C-標準減法填空");
					Console.WriteLine("    D-標準乘法填空");
					Console.WriteLine("    E-標準除法填空");
					Console.WriteLine("************************* 計算比大小 ***********************");
					Console.WriteLine("    F-隨機四則運算比較");
					Console.WriteLine("    G-標準加法比較");
					Console.WriteLine("    H-標準減法比較");
					Console.WriteLine("************************* 計算接龍 ***********************");
					Console.WriteLine("    I-隨機加减運算接龍");
					Console.WriteLine("    J-標準加法運算接龍");
					Console.WriteLine("    K-標準減法運算接龍");
					Console.WriteLine("************************* 應用題 ***********************");
					Console.WriteLine("    L-隨機四則運算應用題");
					Console.WriteLine("    M-加法應用題");
					Console.WriteLine("    N-減法應用題");
					Console.WriteLine("************************* 水果連連看 ***********************");
					Console.WriteLine("    O-隨機四則運算連連看");
					Console.WriteLine("    P-加法連連看");
					Console.WriteLine("    Q-減法連連看");
					Console.WriteLine("************************* 尋找最近的數字 ***********************");
					Console.WriteLine("    R-隨機四則運算");
					Console.WriteLine("    S-加法");
					Console.WriteLine("    T-減法");
					Console.WriteLine("************************* 算式組合 ***********************");
					Console.WriteLine("    U-算式組合");
					Console.WriteLine("************************* 射門得分 ***********************");
					Console.WriteLine("    V-隨機四則運算");
					Console.WriteLine("    W-加法");
					Console.WriteLine("    X-減法");
					Console.WriteLine("************************* 比多少 ***********************");
					Console.WriteLine("    Y-比多少");
					Console.WriteLine("************************* 找規律 ***********************");
					Console.WriteLine("    Z-找規律");
					Console.WriteLine("************************* 數字排序 ***********************");
					Console.WriteLine("    AA-數字排序");
					Console.WriteLine("************************* 認識貨幣 ***********************");
					Console.WriteLine("    B1-認識貨幣(標準/元轉角)");
					Console.WriteLine("    B2-認識貨幣(隨機)");
					Console.WriteLine("    B3-認識貨幣(標準/元轉分)");
					Console.WriteLine("    B4-認識貨幣(隨機/角轉元分/分轉元角)");
					Console.WriteLine("    B5-認識貨幣(隨機/元角分擴展)");
					Console.WriteLine("************************* 算式連一連 ***********************");
					Console.WriteLine("    C1-隨機四則運算連一連");
					Console.WriteLine("    C2-加法連一連");
					Console.WriteLine("    C3-減法連一連");
					Console.WriteLine("*************************");
					Console.Write("    9-菜單    0-退出");
					Console.WriteLine("");
					Console.Write("");

					isShowMenu = false;
				}

				string key = Console.ReadLine();
				switch (key)
				{
					case "A":
						Console.WriteLine();
						Console.WriteLine("隨機四則運算填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC001");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case "B":
						Console.WriteLine();
						Console.WriteLine("標準加法填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC002");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case "C":
						Console.WriteLine();
						Console.WriteLine("標準減法填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC003");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case "D":
						Console.WriteLine();
						Console.WriteLine("標準乘法填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC004");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case "E":
						Console.WriteLine();
						Console.WriteLine("標準除法填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC005");
						Util.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;
					case "F":
						Console.WriteLine();
						Console.WriteLine("隨機四則運算比較");

						ecParameter = (EqualityComparisonParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityComparison, "EC001");
						Util.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case "G":
						Console.WriteLine();
						Console.WriteLine("標準加法比較");
						ecParameter = (EqualityComparisonParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityComparison, "EC002");
						Util.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case "H":
						Console.WriteLine();
						Console.WriteLine("標準減法比較");
						ecParameter = (EqualityComparisonParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityComparison, "EC003");
						Util.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case "I":
						Console.WriteLine();
						Console.WriteLine("標準加法運算接龍");
						ccParameter = (ComputingConnectionParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ComputingConnection, "CC001");
						Util.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case "J":
						Console.WriteLine();
						Console.WriteLine("標準加法運算接龍");
						ccParameter = (ComputingConnectionParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ComputingConnection, "CC002");
						Util.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case "K":
						Console.WriteLine();
						Console.WriteLine("標準減法運算接龍");
						ccParameter = (ComputingConnectionParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ComputingConnection, "CC003");
						Util.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case "L":
						Console.WriteLine();
						Console.WriteLine("隨機四則運算應用題");
						mpParameter = (MathWordProblemsParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.MathWordProblems, "MP001");
						Util.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case "M":
						Console.WriteLine();
						Console.WriteLine("加法應用題");
						mpParameter = (MathWordProblemsParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.MathWordProblems, "MP002");
						Util.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case "N":
						Console.WriteLine();
						Console.WriteLine("減法應用題");
						mpParameter = (MathWordProblemsParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.MathWordProblems, "MP003");
						Util.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case "O":
						Console.WriteLine();
						Console.WriteLine("隨機四則運算連連看");
						flParameter = (FruitsLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FruitsLinkage, "FL001");
						Util.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case "P":
						Console.WriteLine();
						Console.WriteLine("加法連連看");
						flParameter = (FruitsLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FruitsLinkage, "FL002");
						Util.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case "Q":
						Console.WriteLine();
						Console.WriteLine("減法連連看");
						flParameter = (FruitsLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FruitsLinkage, "FL003");
						Util.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case "R":
						Console.WriteLine();
						Console.WriteLine("尋找最近的數字");
						fnParameter = (FindNearestNumberParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindNearestNumber, "FN001");
						Util.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case "S":
						Console.WriteLine();
						Console.WriteLine("加法");
						fnParameter = (FindNearestNumberParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindNearestNumber, "FN002");
						Util.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case "T":
						Console.WriteLine();
						Console.WriteLine("減法");
						fnParameter = (FindNearestNumberParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindNearestNumber, "FN003");
						Util.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case "U":
						Console.WriteLine();
						Console.WriteLine("算式組合");
						ceParameter = (CombinatorialEquationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CombinatorialEquation, "CE001");
						Util.ConsoleFormulas(LayoutSetting.Preview.CombinatorialEquation, ceParameter.Formulas.ToList());
						break;

					case "V":
						Console.WriteLine();
						Console.WriteLine("隨機足球射門");
						sgParameter = (ScoreGoalParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ScoreGoal, "SG001");
						Util.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case "W":
						Console.WriteLine();
						Console.WriteLine("加法足球射門");
						sgParameter = (ScoreGoalParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ScoreGoal, "SG002");
						Util.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case "X":
						Console.WriteLine();
						Console.WriteLine("減法足球射門");
						sgParameter = (ScoreGoalParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ScoreGoal, "SG003");
						Util.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case "Y":
						Console.WriteLine();
						Console.WriteLine("比多少");
						hmmParameter = (HowMuchMoreParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.HowMuchMore, "HMM001");
						Util.ConsoleFormulas(LayoutSetting.Preview.HowMuchMore, hmmParameter.Formulas.ToList());
						break;

					case "Z":
						Console.WriteLine();
						Console.WriteLine("找規律");
						ftlParameter = (FindTheLawParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindTheLaw, "FTL001");
						Util.ConsoleFormulas(LayoutSetting.Preview.FindTheLaw, ftlParameter.Formulas.ToList());
						break;

					case "AA":
						Console.WriteLine();
						Console.WriteLine("數字排序");
						nsParameter = (NumericSortingParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.NumericSorting, "NS001");
						Util.ConsoleFormulas(LayoutSetting.Preview.NumericSorting, nsParameter.Formulas.ToList());
						break;

					case "B1":
						Console.WriteLine();
						Console.WriteLine("B1-認識貨幣(標準/元轉角、標準填空)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC001");
						Util.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;
					case "B2":
						Console.WriteLine();
						Console.WriteLine("B2-認識貨幣(隨機)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC002");
						Util.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;
					case "B3":
						Console.WriteLine();
						Console.WriteLine("B3-認識貨幣(標準/元轉分、隨機填空)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC003");
						Util.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;
					case "B4":
						Console.WriteLine();
						Console.WriteLine("B4-認識貨幣(隨機/角轉元分/分轉元角)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC004");
						Util.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;
					case "B5":
						Console.WriteLine();
						Console.WriteLine("B5-認識貨幣(隨機/元角分擴展)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC005");
						Util.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;

					case "C1":
						Console.WriteLine();
						Console.WriteLine("隨機四則運算連一連");
						elParameter = (EqualityLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityLinkage, "EL001");
						Util.ConsoleFormulas(LayoutSetting.Preview.EqualityLinkage, elParameter.Formulas);
						break;
					case "C2":
						Console.WriteLine();
						Console.WriteLine("加法連一連");
						elParameter = (EqualityLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityLinkage, "EL002");
						Util.ConsoleFormulas(LayoutSetting.Preview.EqualityLinkage, elParameter.Formulas);
						break;
					case "C3":
						Console.WriteLine();
						Console.WriteLine("減法連一連");
						elParameter = (EqualityLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityLinkage, "EL003");
						Util.ConsoleFormulas(LayoutSetting.Preview.EqualityLinkage, elParameter.Formulas);
						break;

					case "D9":
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

			log.Debug(MessageUtil.GetException(() => MsgResources.E0001T), exception);

			Console.WriteLine(exception.Message);
			Console.ReadKey();
			Environment.Exit(-1);
		}
	}

}
