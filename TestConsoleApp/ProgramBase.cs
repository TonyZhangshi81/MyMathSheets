using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Text;

namespace MyMathSheets.TestConsoleApp
{
	/// <summary>
	/// <see cref="Program"/>的父類
	/// </summary>
	public class ProgramBase
	{
		/// <summary>
		/// 啟動時所使用的函數
		/// </summary>
		/// <param name="args">調試用參數</param>
		public virtual void Start(string[] args)
		{
			Console.OutputEncoding = Encoding.Unicode;

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
						CommonUtil.ConsoleFormulas("ArithmeticOperations", key);
						break;

					case "CE0":
						Console.WriteLine();
						Console.WriteLine("算式組合");
						CommonUtil.ConsoleFormulas("CombinatorialEquation", key);
						break;

					case "CC0":
						Console.WriteLine();
						Console.WriteLine("等式接龍");
						CommonUtil.ConsoleFormulas("ComputingConnection", key);
						break;

					case "CL0":
						Console.WriteLine();
						Console.WriteLine("認識價格");
						CommonUtil.ConsoleFormulas("CurrencyLinkage", key);
						break;

					case "CO0":
						Console.WriteLine();
						Console.WriteLine("貨幣運算");
						CommonUtil.ConsoleFormulas("CurrencyOperation", key);
						break;

					case "EC0":
						Console.WriteLine();
						Console.WriteLine("算式比大小");
						CommonUtil.ConsoleFormulas("EqualityComparison", key);
						break;

					case "EL0":
						Console.WriteLine();
						Console.WriteLine("算式連一連");
						CommonUtil.ConsoleFormulas("EqualityLinkage", key);
						break;

					case "MP0":
						Console.WriteLine();
						Console.WriteLine("算式應用題");
						CommonUtil.ConsoleFormulas("MathWordProblems", key);
						break;

					case "FL0":
						Console.WriteLine();
						Console.WriteLine("水果連連看");
						CommonUtil.ConsoleFormulas("FruitsLinkage", key);
						break;

					case "FNN":
						Console.WriteLine();
						Console.WriteLine("找到最近的數字");
						CommonUtil.ConsoleFormulas("FindNearestNumber", key);
						break;

					case "FTL":
						Console.WriteLine();
						Console.WriteLine("找規律");
						CommonUtil.ConsoleFormulas("FindTheLaw", key);
						break;

					case "GFP":
						Console.WriteLine();
						Console.WriteLine("基礎填空題");
						CommonUtil.ConsoleFormulas("GapFillingProblems", key);
						break;

					case "HMM":
						Console.WriteLine();
						Console.WriteLine("比多少");
						CommonUtil.ConsoleFormulas("HowMuchMore", key);
						break;

					case "LC0":
						Console.WriteLine();
						Console.WriteLine("認識貨幣");
						CommonUtil.ConsoleFormulas("LearnCurrency", key);
						break;

					case "LLU":
						Console.WriteLine();
						Console.WriteLine("認識長度單位");
						CommonUtil.ConsoleFormulas("LearnLengthUnit", key);
						break;

					case "NS0":
						Console.WriteLine();
						Console.WriteLine("數字排序");
						CommonUtil.ConsoleFormulas("NumericSorting", key);
						break;

					case "SC0":
						Console.WriteLine();
						Console.WriteLine("時鐘學習板");
						CommonUtil.ConsoleFormulas("SchoolClock", key);
						break;

					case "SG0":
						Console.WriteLine();
						Console.WriteLine("射門得分");
						CommonUtil.ConsoleFormulas("ScoreGoal", key);
						break;

					case "TC0":
						Console.WriteLine();
						Console.WriteLine("時間運算");
						CommonUtil.ConsoleFormulas("TimeCalculation", key);
						break;

					case "MU0":
						Console.WriteLine();
						Console.WriteLine("豎式計算");
						CommonUtil.ConsoleFormulas("MathUpright", key);
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

			LogUtil.LogError(MessageUtil.GetMessage(() => MsgResources.E0001T), exception);

			Console.WriteLine(exception.Message);
			Console.ReadKey();
			Environment.Exit(-1);
		}
	}
}