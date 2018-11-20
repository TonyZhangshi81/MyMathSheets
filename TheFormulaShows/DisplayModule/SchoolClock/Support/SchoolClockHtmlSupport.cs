using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using System.Text;

namespace MyMathSheets.TheFormulaShows.SchoolClock.Support
{
	/// <summary>
	/// 
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.SchoolClock)]
	[Substitute("<!--SCHOOLCLOCKSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.SchoolClock.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--SCHOOLCLOCKREADY-->", "MathSheets.SchoolClock.ready();")]
	[Substitute("//<!--SCHOOLCLOCKMAKECORRECTIONS-->", "fault += MathSheets.SchoolClock.makeCorrections();")]
	[Substitute("//<!--SCHOOLCLOCKTHEIRPAPERS-->", "MathSheets.SchoolClock.theirPapers();")]
	[Substitute("//<!--SCHOOLCLOCKPRINTSETTING-->", "MathSheets.SchoolClock.printSetting();")]
	[Substitute("//<!--SCHOOLCLOCKPRINTAFTERSETTING-->", "MathSheets.SchoolClock.printAfterSetting();")]
	public class SchoolClockHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			//SchoolClockParameter p = parameter as SchoolClockParameter;

			

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;

			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();
			

			if (html.Length != 0)
			{
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">時鐘學習板</span></h4></div><hr />");
			}

			return html.ToString();
		}
	}
}
