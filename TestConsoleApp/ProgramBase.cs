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
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Main.Parameters;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Parameters;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
using MyMathSheets.ComputationalStrategy.LearnLengthUnit.Main.Parameters;
using MyMathSheets.ComputationalStrategy.MathUpright.Main.Parameters;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Parameters;
using MyMathSheets.ComputationalStrategy.NumericSorting.Main.Parameters;
using MyMathSheets.ComputationalStrategy.SchoolClock.Main.Parameters;
using MyMathSheets.ComputationalStrategy.ScoreGoal.Main.Parameters;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Main.Parameters;
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
		/// <param name="args">調試用參數</param>
		public virtual void Start(string[] args)
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
			TimeCalculationParameter tcParameter = null;
			LearnLengthUnitParameter lluParameter = null;
			GapFillingProblemsParameter gfpParameter = null;
			MathUprightParameter muParameter = null;

			bool isShowMenu = !(args.Length > 0);

			while (1 == 1)
			{
				if (isShowMenu)
				{
					Console.WriteLine("參數選擇：");
					Console.WriteLine("四則運算(AC001~AC006)");
					Console.WriteLine("算式組合(CE001)");
					Console.WriteLine("等式接龍(CC001~CC003)");
					Console.WriteLine("認識價格(CL001~CL002)");
					Console.WriteLine("貨幣運算(CO001~CO005)");
					Console.WriteLine("算式比大小(EC001~EC004)");
					Console.WriteLine("算式連一連(EL001~EL005)");
					Console.WriteLine("算式應用題(MP001~MP004)");
					Console.WriteLine("水果連連看(FL001~FL004)");
					Console.WriteLine("找到最近的數字(FNN01~FNN04)");
					Console.WriteLine("找規律(FTL01~FTL04)");
					Console.WriteLine("基礎填空題(GFP01~GFP02)");
					Console.WriteLine("比多少(HMM01~HMM02)");
					Console.WriteLine("認識貨幣(LC001~LC006)");
					Console.WriteLine("認識長度單位(LLU01~LLU07)");
					Console.WriteLine("數字排序(NS001~NS002)");
					Console.WriteLine("時鐘學習板(SC001~SC004)");
					Console.WriteLine("射門得分(SG001~SG004)");
					Console.WriteLine("時間運算(TC001~TC006)");
					Console.WriteLine("豎式計算(MU001~MU003)");
					Console.WriteLine("    9-菜單    0-退出");
					Console.WriteLine("");
					Console.Write("");

					isShowMenu = false;
				}

				string key = ((args.Length > 0) ? args[0] : Console.ReadLine()).PadRight(3, '0').ToUpper();
				switch (key.Substring(0, 3))
				{
					case "AC0":
						Console.WriteLine();
						Console.WriteLine("四則運算");
						acParameter = (ArithmeticParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.Arithmetic, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.Arithmetic, acParameter.Formulas.ToList());
						break;

					case "CE0":
						Console.WriteLine();
						Console.WriteLine("算式組合");
						ceParameter = (CombinatorialEquationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CombinatorialEquation, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CombinatorialEquation, ceParameter.Formulas.ToList());
						break;

					case "CC0":
						Console.WriteLine();
						Console.WriteLine("等式接龍");
						ccParameter = (ComputingConnectionParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ComputingConnection, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.ComputingConnection, ccParameter.Formulas.ToList());
						break;

					case "CL0":
						Console.WriteLine();
						Console.WriteLine("認識價格");
						clParameter = (CurrencyLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CurrencyLinkage, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CurrencyLinkage, clParameter.Currencys);
						break;

					case "CO0":
						Console.WriteLine();
						Console.WriteLine("貨幣運算");
						coParameter = (CurrencyOperationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.CurrencyOperation, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.CurrencyOperation, coParameter.Formulas.ToList());
						break;

					case "EC0":
						Console.WriteLine();
						Console.WriteLine("算式比大小");
						ecParameter = (EqualityComparisonParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityComparison, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.EqualityComparison, ecParameter.Formulas.ToList());
						break;

					case "EL0":
						Console.WriteLine();
						Console.WriteLine("算式連一連");
						elParameter = (EqualityLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.EqualityLinkage, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.EqualityLinkage, elParameter.Formulas);
						break;

					case "MP0":
						Console.WriteLine();
						Console.WriteLine("算式應用題");
						mpParameter = (MathWordProblemsParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.MathWordProblems, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.MathWordProblems, mpParameter.Formulas.ToList());
						break;

					case "FL0":
						Console.WriteLine();
						Console.WriteLine("水果連連看");
						flParameter = (FruitsLinkageParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FruitsLinkage, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FruitsLinkage, flParameter.Formulas);
						break;

					case "FNN":
						Console.WriteLine();
						Console.WriteLine("找到最近的數字");
						fnParameter = (FindNearestNumberParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindNearestNumber, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FindNearestNumber, fnParameter.Formulas.ToList());
						break;

					case "FTL":
						Console.WriteLine();
						Console.WriteLine("找規律");
						ftlParameter = (FindTheLawParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.FindTheLaw, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.FindTheLaw, ftlParameter.Formulas.ToList());
						break;

					case "GFP":
						Console.WriteLine();
						Console.WriteLine("基礎填空題");
						gfpParameter = (GapFillingProblemsParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.GapFillingProblems, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.GapFillingProblems, gfpParameter.Formulas.ToList());
						break;

					case "HMM":
						Console.WriteLine();
						Console.WriteLine("比多少");
						hmmParameter = (HowMuchMoreParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.HowMuchMore, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.HowMuchMore, hmmParameter.Formulas.ToList());
						break;

					case "LC0":
						Console.WriteLine();
						Console.WriteLine("認識貨幣");
						lcParameter = (LearnCurrencyParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnCurrency, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.LearnCurrency, lcParameter.Formulas.ToList());
						break;

					case "LLU":
						Console.WriteLine();
						Console.WriteLine("認識長度單位");
						lluParameter = (LearnLengthUnitParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.LearnLengthUnit, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.LearnLengthUnit, lluParameter.Formulas.ToList());
						break;

					case "NS0":
						Console.WriteLine();
						Console.WriteLine("數字排序");
						nsParameter = (NumericSortingParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.NumericSorting, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.NumericSorting, nsParameter.Formulas.ToList());
						break;

					case "SC0":
						Console.WriteLine();
						Console.WriteLine("時鐘學習板");
						scParameter = (SchoolClockParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.SchoolClock, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.SchoolClock, scParameter.Formulas.ToList());
						break;

					case "SG0":
						Console.WriteLine();
						Console.WriteLine("射門得分");
						sgParameter = (ScoreGoalParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.ScoreGoal, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.ScoreGoal, sgParameter.Formulas);
						break;

					case "TC0":
						Console.WriteLine();
						Console.WriteLine("時間運算");
						tcParameter = (TimeCalculationParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.TimeCalculation, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.TimeCalculation, tcParameter.Formulas.ToList());
						break;

					case "MU0":
						Console.WriteLine();
						Console.WriteLine("豎式計算");
						muParameter = (MathUprightParameter)OperationStrategyHelper.Instance.Structure(LayoutSetting.Preview.MathUpright, key);
						CommonUtil.ConsoleFormulas(LayoutSetting.Preview.MathUpright, muParameter.Formulas.ToList());
						break;

					case "900":
						isShowMenu = true;
						break;

					case "000":
						Console.WriteLine();
						Console.WriteLine("Close");
						Console.ReadKey();
						Environment.Exit(0);
						break;

					default:
						Console.WriteLine();
						Console.WriteLine("題型不存在");
						isShowMenu = true;
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
