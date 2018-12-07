using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Main.Parameters;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Main.Parameters;
using MyMathSheets.ComputationalStrategy.ComputingConnection.Main.Parameters;
using MyMathSheets.ComputationalStrategy.CurrencyLinkage.Main.Parameters;
using MyMathSheets.ComputationalStrategy.EqualityComparison.Main.Parameters;
using MyMathSheets.ComputationalStrategy.EqualityLinkage.Main.Parameters;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Main.Parameters;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Parameters;
using MyMathSheets.ComputationalStrategy.FruitsLinkage.Main.Parameters;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Parameters;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Parameters;
using MyMathSheets.ComputationalStrategy.NumericSorting.Main.Parameters;
using MyMathSheets.ComputationalStrategy.SchoolClock.Main.Parameters;
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
			SchoolClockParameter scParameter = null;
			CurrencyOperationParameter coParameter = null;
			CurrencyLinkageParameter clParameter = null;

			bool isShowMenu = true;

			while (1 == 1)
			{
				if (isShowMenu)
				{
					Console.WriteLine("參數選擇：");
					Console.WriteLine("************************* 四則運算 *************************");
					Console.WriteLine("    A-四則運算填空");
					Console.WriteLine("    B-加法填空");
					Console.WriteLine("    C-減法填空");
					Console.WriteLine("    D-乘法填空");
					Console.WriteLine("    E-除法填空");
					Console.WriteLine("************************* 計算比大小 ***********************");
					Console.WriteLine("    F-四則運算比較");
					Console.WriteLine("    G-加法比較");
					Console.WriteLine("    H-減法比較");
					Console.WriteLine("************************* 計算接龍 ***********************");
					Console.WriteLine("    I-加减運算接龍");
					Console.WriteLine("    J-加法運算接龍");
					Console.WriteLine("    K-減法運算接龍");
					Console.WriteLine("************************* 應用題 ***********************");
					Console.WriteLine("    L-四則運算應用題");
					Console.WriteLine("    M-加法應用題");
					Console.WriteLine("    N-減法應用題");
					Console.WriteLine("************************* 水果連連看 ***********************");
					Console.WriteLine("    O-四則運算連連看");
					Console.WriteLine("    P-加法連連看");
					Console.WriteLine("    Q-減法連連看");
					Console.WriteLine("************************* 尋找最近的數字 ***********************");
					Console.WriteLine("    R-四則運算");
					Console.WriteLine("    S-加法");
					Console.WriteLine("    T-減法");
					Console.WriteLine("************************* 算式組合 ***********************");
					Console.WriteLine("    U-算式組合");
					Console.WriteLine("************************* 射門得分 ***********************");
					Console.WriteLine("    V-四則運算");
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
					Console.WriteLine("    C1-四則運算連一連");
					Console.WriteLine("    C2-加法連一連");
					Console.WriteLine("    C3-減法連一連");
					Console.WriteLine("************************* 時鐘學習板 ***********************");
					Console.WriteLine("    D1-指定分鐘");
					Console.WriteLine("    D2-隨機時間");
					Console.WriteLine("************************* 貨幣運算 ***********************");
					Console.WriteLine("    E1-貨幣運算（元角分）");
					Console.WriteLine("    E2-貨幣運算加法");
					Console.WriteLine("    E3-貨幣運算減法");
					Console.WriteLine("    E4-貨幣運算（分）");
					Console.WriteLine("    E5-貨幣運算減法（角分）");
					Console.WriteLine("************************* 認識價格 ***********************");
					Console.WriteLine("    F1-商品價格(橫向)");
					Console.WriteLine("    F2-商品價格(縱向)");
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
						Console.WriteLine("四則運算填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case "B":
						Console.WriteLine();
						Console.WriteLine("加法填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case "C":
						Console.WriteLine();
						Console.WriteLine("減法填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case "D":
						Console.WriteLine();
						Console.WriteLine("乘法填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC004");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case "E":
						Console.WriteLine();
						Console.WriteLine("除法填空");

						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, "AC005");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;
					case "F":
						Console.WriteLine();
						Console.WriteLine("四則運算比較");

						ecParameter = (EqualityComparisonParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityComparison, "EC001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case "G":
						Console.WriteLine();
						Console.WriteLine("加法比較");
						ecParameter = (EqualityComparisonParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityComparison, "EC002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case "H":
						Console.WriteLine();
						Console.WriteLine("減法比較");
						ecParameter = (EqualityComparisonParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityComparison, "EC003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case "I":
						Console.WriteLine();
						Console.WriteLine("加減法運算接龍");
						ccParameter = (ComputingConnectionParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ComputingConnection, "CC001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case "J":
						Console.WriteLine();
						Console.WriteLine("加法運算接龍");
						ccParameter = (ComputingConnectionParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ComputingConnection, "CC002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case "K":
						Console.WriteLine();
						Console.WriteLine("減法運算接龍");
						ccParameter = (ComputingConnectionParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ComputingConnection, "CC003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case "L":
						Console.WriteLine();
						Console.WriteLine("四則運算應用題");
						mpParameter = (MathWordProblemsParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.MathWordProblems, "MP001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case "M":
						Console.WriteLine();
						Console.WriteLine("加法應用題");
						mpParameter = (MathWordProblemsParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.MathWordProblems, "MP002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case "N":
						Console.WriteLine();
						Console.WriteLine("減法應用題");
						mpParameter = (MathWordProblemsParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.MathWordProblems, "MP003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case "O":
						Console.WriteLine();
						Console.WriteLine("四則運算連連看");
						flParameter = (FruitsLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FruitsLinkage, "FL001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case "P":
						Console.WriteLine();
						Console.WriteLine("加法連連看");
						flParameter = (FruitsLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FruitsLinkage, "FL002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case "Q":
						Console.WriteLine();
						Console.WriteLine("減法連連看");
						flParameter = (FruitsLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FruitsLinkage, "FL003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case "R":
						Console.WriteLine();
						Console.WriteLine("尋找最近的數字");
						fnParameter = (FindNearestNumberParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindNearestNumber, "FN001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case "S":
						Console.WriteLine();
						Console.WriteLine("加法");
						fnParameter = (FindNearestNumberParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindNearestNumber, "FN002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case "T":
						Console.WriteLine();
						Console.WriteLine("減法");
						fnParameter = (FindNearestNumberParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindNearestNumber, "FN003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case "U":
						Console.WriteLine();
						Console.WriteLine("算式組合");
						ceParameter = (CombinatorialEquationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CombinatorialEquation, "CE001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CombinatorialEquation, ceParameter.Formulas.ToList());
						break;

					case "V":
						Console.WriteLine();
						Console.WriteLine("四則運算");
						sgParameter = (ScoreGoalParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ScoreGoal, "SG001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case "W":
						Console.WriteLine();
						Console.WriteLine("加法");
						sgParameter = (ScoreGoalParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ScoreGoal, "SG002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case "X":
						Console.WriteLine();
						Console.WriteLine("減法");
						sgParameter = (ScoreGoalParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ScoreGoal, "SG003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case "Y":
						Console.WriteLine();
						Console.WriteLine("比多少");
						hmmParameter = (HowMuchMoreParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.HowMuchMore, "HMM001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.HowMuchMore, hmmParameter.Formulas.ToList());
						break;

					case "Z":
						Console.WriteLine();
						Console.WriteLine("找規律");
						ftlParameter = (FindTheLawParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindTheLaw, "FTL001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FindTheLaw, Enumerable.ToList<ComputationalStrategy.FindTheLaw.Item.FindTheLawFormula>(ftlParameter.Formulas));
						break;

					case "AA":
						Console.WriteLine();
						Console.WriteLine("數字排序");
						nsParameter = (NumericSortingParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.NumericSorting, "NS001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.NumericSorting, nsParameter.Formulas.ToList());
						break;

					case "B1":
						Console.WriteLine();
						Console.WriteLine("B1-認識貨幣(標準/元轉角、標準填空)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;
					case "B2":
						Console.WriteLine();
						Console.WriteLine("B2-認識貨幣(隨機)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;
					case "B3":
						Console.WriteLine();
						Console.WriteLine("B3-認識貨幣(標準/元轉分、隨機填空)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;
					case "B4":
						Console.WriteLine();
						Console.WriteLine("B4-認識貨幣(隨機/角轉元分/分轉元角)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC004");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;
					case "B5":
						Console.WriteLine();
						Console.WriteLine("B5-認識貨幣(隨機/元角分擴展)");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, "LC005");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;

					case "C1":
						Console.WriteLine();
						Console.WriteLine("四則運算連一連");
						elParameter = (EqualityLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityLinkage, "EL001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.EqualityLinkage, elParameter.Formulas);
						break;
					case "C2":
						Console.WriteLine();
						Console.WriteLine("加法連一連");
						elParameter = (EqualityLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityLinkage, "EL002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.EqualityLinkage, elParameter.Formulas);
						break;
					case "C3":
						Console.WriteLine();
						Console.WriteLine("減法連一連");
						elParameter = (EqualityLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityLinkage, "EL003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.EqualityLinkage, elParameter.Formulas);
						break;
					case "D1":
						Console.WriteLine();
						Console.WriteLine("指定分鐘");
						scParameter = (SchoolClockParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.SchoolClock, "SC001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.SchoolClock, scParameter.Formulas.ToList());
						break;
					case "D2":
						Console.WriteLine();
						Console.WriteLine("隨機時間");
						scParameter = (SchoolClockParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.SchoolClock, "SC002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.SchoolClock, scParameter.Formulas.ToList());
						break;

					case "E1":
						Console.WriteLine();
						Console.WriteLine("隨機貨幣運算（元角分）");
						coParameter = (CurrencyOperationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CurrencyOperation, "CO005");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CurrencyOperation, coParameter.Formulas.ToList());
						break;
					case "E2":
						Console.WriteLine();
						Console.WriteLine("貨幣運算加法");
						coParameter = (CurrencyOperationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CurrencyOperation, "CO001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CurrencyOperation, coParameter.Formulas.ToList());
						break;
					case "E3":
						Console.WriteLine();
						Console.WriteLine("貨幣運算減法");
						coParameter = (CurrencyOperationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CurrencyOperation, "CO002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CurrencyOperation, coParameter.Formulas.ToList());
						break;
					case "E4":
						Console.WriteLine();
						Console.WriteLine("貨幣運算（分）");
						coParameter = (CurrencyOperationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CurrencyOperation, "CO003");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CurrencyOperation, coParameter.Formulas.ToList());
						break;
					case "E5":
						Console.WriteLine();
						Console.WriteLine("貨幣運算（角）");
						coParameter = (CurrencyOperationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CurrencyOperation, "CO004");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CurrencyOperation, coParameter.Formulas.ToList());
						break;

					case "F1":
						Console.WriteLine();
						Console.WriteLine("商品價格(橫向)");
						clParameter = (CurrencyLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CurrencyLinkage, "CL001");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CurrencyLinkage, clParameter.Currencys);
						break;
					case "F2":
						Console.WriteLine();
						Console.WriteLine("商品價格(縱向)");
						clParameter = (CurrencyLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CurrencyLinkage, "CL002");
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CurrencyLinkage, clParameter.Currencys);
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
