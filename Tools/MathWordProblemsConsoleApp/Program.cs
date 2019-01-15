using MyMathSheets.MathWordProblemsConsoleApp.Ext;
using MyMathSheets.MathWordProblemsConsoleApp.WorkProcess;
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
						att.SetMathWordProblemsJson("Problems");
						break;
					// 填空題
					case "GapFillingProblems":
						att.SetGapFillingProblemsJson("GapFillingProblems");
						break;
				}
			}
		}
	}
}
