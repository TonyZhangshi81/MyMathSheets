using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFormulaShows;

namespace TestConsoleApp
{
	/// <summary>
	/// 
	/// </summary>
	class Program
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			MakeHtmlBase work = new Arithmetic
			{
				MaximumLimit = 100,
				NumberOf = 35,
				QType = QuestionTypes.GapFilling
			};
			work.Structure();

			work.FormulaList.ToList().ForEach(d =>
			{
				Console.WriteLine(string.Format("{0} - {1} = {2}",
					GetValue(GapFilling.Left, d.LeftParameter, d.Gap),
					GetValue(GapFilling.Right, d.RightParameter, d.Gap),
					GetValue(GapFilling.Answer, d.Answer, d.Gap)));
			});

			string html = work.MakeHtml();
			//Write(html);

			Console.ReadKey();


			var sourceFileName = System.Configuration.ConfigurationManager.AppSettings.Get("Template");
			var destFileName = System.Configuration.ConfigurationManager.AppSettings.Get("HtmlWork") + string.Format("HTMLPage_{0}.html", DateTime.Now.ToString("HHmmssfff"));
			File.Copy(sourceFileName, destFileName);

			var index = 0;
			string[] allTextLines = File.ReadAllLines(destFileName, Encoding.UTF8);
			allTextLines.ToList().ForEach(d =>
			{
				index++;
				if (d.IndexOf("<!--ARITHMETIC-->") >= 0)
				{
					allTextLines[index] = html;
				}
			});
			File.WriteAllLines(destFileName, allTextLines, Encoding.Unicode);

			System.Diagnostics.Process.Start(@"IExplore.exe", destFileName);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="html"></param>
		static void Write(string html)
		{
			var path = @"C:\Users\zhangcg.NTDOMAIN\source\repos\MyMathSheets\TestConsoleApp\App_Data\01.txt";
			using (FileStream fs = new FileStream(path, FileMode.Create))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(html);
					sw.Flush();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="parameter"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		static string GetValue(GapFilling item, int parameter, GapFilling gap)
		{
			if (item == gap)
			{
				return string.Empty.PadLeft(2);
			}
			return parameter.ToString();
		}
	}
}
