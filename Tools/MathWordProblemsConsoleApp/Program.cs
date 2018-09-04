using MathWordProblemsConsoleApp.Ext;
using MathWordProblemsConsoleApp.WorkProcess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathWordProblemsConsoleApp
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
