using MyMathSheets.MathWordProblemsConsoleApp.Ext;
using MyMathSheets.MathWordProblemsConsoleApp.WorkProcess;
using System.IO;

namespace MyMathSheets.MathWordProblemsConsoleApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			string fileName = Path.GetFullPath(@"..\App_Data\MathWordProblems.xlsx");
			using (SpireXls xls = new SpireXls())
			{
				xls.Load(fileName);

				AttendanceManagement att = new AttendanceManagement(xls);

				att.SetMathWordProblemsJson();
			}
		}
	}
}
