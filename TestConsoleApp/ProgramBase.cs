using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.TestConsoleApp.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMathSheets.TestConsoleApp
{
	/// <summary>
	/// <see cref="Program"/>的父類
	/// </summary>
	public class ProgramBase
	{
		private readonly Dictionary<string, string[]> CommandMaps;

		public ProgramBase()
		{
			CommandMaps = new Dictionary<string, string[]>()
			{
				{"AC0", new string []{ "ArithmeticOperations", "四則運算" } },
				{"CE0", new string []{ "CombinatorialEquation", "算式組合" } },
				{"CC0", new string []{ "ComputingConnection", "等式接龍" } },
				{"CL0", new string []{ "CurrencyLinkage", "認識價格" } },
				{"CO0", new string []{ "CurrencyOperation", "貨幣運算" } },
				{"EC0", new string []{ "EqualityComparison", "算式比大小" } },
				{"EL0", new string []{ "EqualityLinkage", "算式連一連" } },
				{"MP0", new string []{ "MathWordProblems", "算式應用題" } },
				{"FL0", new string []{ "FruitsLinkage", "水果連連看" } },
				{"FNN", new string []{ "FindNearestNumber", "找到最近的數字" } },
				{"FTL", new string []{ "FindTheLaw", "找規律" } },
				{"GFP", new string []{ "GapFillingProblems", "基礎填空題" } },
				{"HMM", new string []{ "HowMuchMore", "比多少" } },
				{"LC0", new string []{ "LearnCurrency", "認識貨幣" } },
				{"LLU", new string []{ "LearnLengthUnit", "認識長度單位" } },
				{"NS0", new string []{ "NumericSorting", "數字排序" } },
				{"SC0", new string []{ "SchoolClock", "時鐘學習板" } },
				{"SG0", new string []{ "ScoreGoal", "射門得分" } },
				{"TC0", new string []{ "TimeCalculation", "時間運算" } },
				{"MU0", new string []{ "MathUpright", "豎式計算" } },
				{"CLC", new string []{ "CleverCalculation", "巧算" } },
				{"RE0", new string []{ "RecursionEquation", "遞等式計算" } },
				{"900", new string []{ "Menu", "菜單" } },
				{"000", new string []{ "Close", "退出" } }
			};

			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

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
					Console.WriteLine("巧算(CLC01~CLC04)");
					Console.WriteLine("遞等式計算(RE001~RE007)");
					Console.WriteLine("    9-菜單    0-退出");
					Console.WriteLine("");
					Console.Write("");

					isShowMenu = false;
				}

				string key = ((args.Length > 0) ? args[0] : Console.ReadLine()).PadRight(3, '0').ToUpper();

				if (!CommandMaps.TryGetValue(key.Substring(0, 3), out string[] map))
				{
					Console.WriteLine();
					Console.WriteLine("題型不存在");
					isShowMenu = true;
					continue;
				}

				switch (key.Substring(0, 3))
				{
					case "900":
						isShowMenu = true;
						break;

					case "000":
						Console.WriteLine();
						Console.WriteLine("Close");
						ComposerFactory.Reset();
						Console.ReadKey();
						Environment.Exit(0);
						break;

					default:
						Console.WriteLine();
						Console.WriteLine(map[1]);
						CommonUtil.ConsoleFormulas(map[0], key);
						break;
				}
			}
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
		}
	}
}