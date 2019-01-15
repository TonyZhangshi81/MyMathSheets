using MyMathSheets.MathWordProblemsConsoleApp.Ext;
using MyMathSheets.MathWordProblemsConsoleApp.WorkProcess;
using System;
using System.Configuration;
using System.IO;

namespace MyMathSheets.MathWordProblemsConsoleApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("沒有可執行的參數！");
				Console.ReadLine();
				Environment.Exit(-1);
				return;
			}

			string fileName = Path.GetFullPath(ConfigurationManager.AppSettings.Get(@args[0]));
			using (SpireXls xls = new SpireXls())
			{
				xls.Load(fileName);

				AttendanceManagement att = new AttendanceManagement(xls);

				switch (args[0])
				{
					// 應用題
					case "MathWordProblems":
						att.SetMathWordProblemsJson("MathWordProblemsLibrary");
						break;
					// 填空題
					case "GapFillingProblems":
						att.SetGapFillingProblemsJson("GapFillingProblemsLibrary");
						break;
				}
			}

			Console.WriteLine("JSON文件已經作成，請及時更新。");
			Console.ReadLine();
			Environment.Exit(0);
		}
	}
}
