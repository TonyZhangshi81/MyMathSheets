using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.SchoolClock.Item;
using MyMathSheets.ComputationalStrategy.SchoolClock.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.SchoolClock.Support
{
	/// <summary>
	/// 時鐘學習板題型HTML模板作成
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.SchoolClock)]
	[Substitute("<!--SCHOOLCLOCKSTYLESHEET-->", "<link href=\"../Content/SchoolClock.css\" rel=\"stylesheet\" type=\"text/css\" />")]
	[Substitute("<!--SCHOOLCLOCKSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.SchoolClock.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--SCHOOLCLOCKREADY-->", "MathSheets.SchoolClock.ready();")]
	[Substitute("//<!--SCHOOLCLOCKMAKECORRECTIONS-->", "fault += MathSheets.SchoolClock.makeCorrections();")]
	[Substitute("//<!--SCHOOLCLOCKTHEIRPAPERS-->", "MathSheets.SchoolClock.theirPapers();")]
	[Substitute("//<!--SCHOOLCLOCKPRINTSETTING-->", "MathSheets.SchoolClock.printSetting();")]
	[Substitute("//<!--SCHOOLCLOCKPRINTAFTERSETTING-->", "MathSheets.SchoolClock.printAfterSetting();")]
	public class SchoolClockHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 時鐘答題輸出區域HTML作成
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputClock{0}{1}\" type=\"text\" placeholder=\"{2}\" class=\"form-control input-addBorder {3}\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";
		private const string SPAN_HTML = "<span class=\"label\">:</span>";

		/// <summary>
		/// 消息提示信息HTML作成
		/// </summary>
		private const string TOOLTIP_HTML_FORMAT = "<img src=\"../Content/image/clock/{0}.png\" style=\"width: 30px; height: 30px;\" data-toggle=\"tooltip\" title=\"{1}\" />";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			SchoolClockParameter p = parameter as SchoolClockParameter;
			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;

			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();
			StringBuilder clocksAnswer = new StringBuilder();
			foreach (SchoolClockFormula item in p.Formulas)
			{
				clocksAnswer.AppendFormat("{0}:{1}:{2};", item.LatestTime.Hours, item.LatestTime.Minutes, item.LatestTime.Seconds);

				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-4 line-height\">");

				// 消息提示信息HTML作成
				colHtml.Append(GetTooltipHtml(item));
				// 時鐘信息HTML作成
				colHtml.Append(GetFaceClockHtml(controlIndex));
				// 時鐘答題輸出區域HTML作成
				colHtml.Append(GetClockInlineHtml(controlIndex));
				// 閉合
				colHtml.AppendLine("</div>");

				controlIndex++;
				numberOfColumns++;
				if (numberOfColumns == 3)
				{
					rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
					rowHtml.Append(colHtml.ToString());
					rowHtml.AppendLine("</div>");

					html.Append(rowHtml);

					isRowHtmlClosed = true;
					numberOfColumns = 0;
					rowHtml.Length = 0;
					colHtml.Length = 0;
				}
			}

			if (!isRowHtmlClosed)
			{
				rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
				rowHtml.Append(colHtml.ToString());
				rowHtml.AppendLine("</div>");

				html.Append(rowHtml);
			}

			if (html.Length != 0)
			{
				clocksAnswer.Length -= 1;
				html.AppendLine(string.Format("<input type=\"hidden\" id=\"hidClocksAnswer\" value=\"{0}\" />", clocksAnswer.ToString()));
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">時鐘學習板</span></h4></div><hr />");
			}

			return html.ToString();
		}

		/// <summary>
		/// 時鐘信息HTML作成
		/// </summary>
		/// <param name="controlIndex">控件ID</param>
		/// <returns>HTML模板</returns>
		private string GetFaceClockHtml(int controlIndex)
		{
			StringBuilder html = new StringBuilder();

			html.AppendLine("<div class=\"watch-section\">");
			html.AppendLine("<div class=\"watch-inner\">");
			html.AppendLine("<div class=\"faces\">");
			html.AppendLine("<div class=\"face face4\">");
			html.AppendLine(string.Format("<svg id=\"clock{0}\" class=\"clock\"></svg>", controlIndex));
			html.AppendLine("</div>");
			html.AppendLine("</div>");
			html.AppendLine("</div>");
			html.AppendLine("</div>");

			return html.ToString();
		}


		/// <summary>
		/// 消息提示信息HTML作成
		/// </summary>
		/// <param name="item">題型參數</param>
		/// <returns>HTML模板</returns>
		private string GetTooltipHtml(SchoolClockFormula item)
		{
			StringBuilder html = new StringBuilder();

			html.AppendLine(string.Format(TOOLTIP_HTML_FORMAT, item.LatestTime.TimeType.ToTimeSystemString(), item.LatestTime.TimeInterval.ToTimeIntervalTypeString()));

			return html.ToString();
		}

		/// <summary>
		/// 時鐘答題輸出區域HTML作成
		/// </summary>
		/// <param name="controlIndex">控件ID</param>
		/// <returns>HTML模板</returns>
		private string GetClockInlineHtml(int controlIndex)
		{
			StringBuilder html = new StringBuilder();

			html.AppendLine("<div class=\"form-inline clock-inline\">");
			// 小時數輸入框
			html.AppendLine(string.Format(INPUT_HTML_FORMAT, "H", controlIndex, "時", "hours"));
			html.AppendLine(SPAN_HTML);
			// 分鐘數輸入框
			html.AppendLine(string.Format(INPUT_HTML_FORMAT, "M", controlIndex, "分", "minutes"));
			html.AppendLine(SPAN_HTML);
			// 秒數輸入框
			html.AppendLine(string.Format(INPUT_HTML_FORMAT, "S", controlIndex, "秒", "seconds"));
			// 答題對錯顯示
			html.AppendLine("<div class=\"divCorrectOrFault-1\">");
			html.AppendLine(string.Format("<img id=\"imgOKSchoolClock{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex));
			html.AppendLine(string.Format("<img id=\"imgNoSchoolClock{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex));
			html.AppendLine("</div>");
			// 閉合
			html.AppendLine("</div>");

			return html.ToString();
		}
	}
}
