using MyMathSheets.MathWordProblemsConsoleApp.Ext;
using MyMathSheets.MathWordProblemsConsoleApp.WorkProcess;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

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

			args.ToList().ForEach(d =>
			{
				string fileName = Path.GetFullPath(ConfigurationManager.AppSettings.Get(d));
				using (SpireXls xls = new SpireXls())
				{
					xls.Load(fileName);

					AttendanceManagement att = new AttendanceManagement(xls);

					switch (d)
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
			});

			Console.Write("準備移動文件...");
			Console.ReadLine();

			RunCopyBat();

			Console.Write("文件移動完畢...");
			Console.ReadLine();

			Environment.Exit(0);
		}

		static void RunCopyBat()
		{
			Process proc = null;
			try
			{
				proc = new Process();
				proc.StartInfo.WorkingDirectory = Path.GetFullPath(@"..\App_Data\output\");
				proc.StartInfo.FileName = "jsoncopy.bat";
				proc.StartInfo.CreateNoWindow = false;
				proc.Start();
				proc.WaitForExit();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
			}
		}

		/// <summary>
		/// 系統異常捕捉事件註冊
		/// </summary>
		protected Program()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

		/// <summary>
		/// 系統異常捕捉事件
		/// </summary>
		/// <param name="sender">異常</param>
		/// <param name="e">異常參數</param>
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var exception = (Exception)e.ExceptionObject;

			Console.WriteLine(exception.Message);
			Console.ReadKey();
			Environment.Exit(-1);
		}
	}
}
