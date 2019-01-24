using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Item;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.TimeCalculation.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.TimeCalculation)]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.TimeCalculation.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.TimeCalculation.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.TimeCalculation.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.TimeCalculation.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.TimeCalculation.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.TimeCalculation.printAfterSetting();")]
	public class TimeCalculationHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">{1}</span></h4></div><hr />";
		/// <summary>
		/// 輸入項目HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputTc{0}{1}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";
		private const string INPUT_HTML_ON_SCRIPT_FORMAT = "<input id=\"inputTc{0}{1}\" type=\"text\" placeholder=\"{2}\" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" onFocus=\"MathSheets.TimeCalculation.inputOnFocus(this);\" onBlur=\"MathSheets.TimeCalculation.inputOnBlur(this);\" />";
		private const string SPAN_COLON_HTML = "<span class=\"label\">:</span>";
		/// <summary>
		/// 時間HTML模板(數字)
		/// </summary>
		private const string SPAN_TIME_NUM_HTML_FORMAT = "<span class=\"label p-2\">{0}</span>";
		/// <summary>
		/// 時間HTML模板(單位)
		/// </summary>
		private const string SPAN_TIME_CN_HTML_FORMAT = "<span class=\"label p-2\">{0}{1}</span>";
		/// <summary>
		/// 題型答案
		/// </summary>
		private const string INPUT_ANSWER_HTML_FORMAT = "<input id=\"hiddenTc{0}\" type=\"hidden\" value=\"{1}\" />";

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			TimeCalculationParameter p = parameter as TimeCalculationParameter;

			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			foreach (TimeCalculationFormula item in p.Formulas)
			{
				rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
				rowHtml.AppendLine("<div class=\"col-md-12 form-inline\">");
				rowHtml.AppendLine("<h5>");
				rowHtml.AppendLine("<span class=\"span-tc-lightoff\"></span>");

				// 開始時間計算式
				rowHtml.AppendLine(GetStartTimeHtml(item.StartTime, item.Gap, controlIndex));
				// 經過時間計算式
				rowHtml.AppendLine(GetElapsedTimeHtml(item.ElapsedTime, item.Gap, item.Sign, controlIndex));
				// 結果時間計算式
				rowHtml.AppendLine(GetEndTimeHtml(item.EndTime, item.Gap, controlIndex));
				
				// 閉合
				rowHtml.AppendLine("</h5>");
				// 對錯結果圖示
				rowHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				rowHtml.AppendLine(string.Format("<img id=\"imgOKTimeCalculation{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex));
				rowHtml.AppendLine(string.Format("<img id=\"imgNoTimeCalculation{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex));
				rowHtml.AppendLine("</div>");
				rowHtml.AppendLine("</div>");
				rowHtml.AppendLine("</div>");

				controlIndex++;
			}

			html.Append(rowHtml);
			if (html.Length != 0)
			{
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, LayoutSetting.Preview.TimeCalculation.ToString(), LayoutSetting.Preview.TimeCalculation.ToComputationalStrategyName()));
			}

			return html.ToString();
		}

		/// <summary>
		/// 結果時間計算式
		/// </summary>
		/// <param name="endTime">時間計算式</param>
		/// <param name="gap">填空項目</param>
		/// <param name="index">控件索引ID</param>
		/// <returns>HTML模板信息</returns>
		private string GetEndTimeHtml(Time endTime, GapFilling gap, int index)
		{
			var html = string.Empty;
			html += string.Format(SPAN_TIME_NUM_HTML_FORMAT, "是");
			// 如果是填空項目
			if (gap == GapFilling.Answer)
			{
				// 小時
				html += string.Format(INPUT_HTML_ON_SCRIPT_FORMAT, index.ToString().PadLeft(2, '0'), "0", Consts.HR_UNIT);
				html += SPAN_COLON_HTML;
				// 分鐘
				html += string.Format(INPUT_HTML_ON_SCRIPT_FORMAT, index.ToString().PadLeft(2, '0'), "1", Consts.MIN_UNIT);
				html += SPAN_COLON_HTML;
				// 秒
				html += string.Format(INPUT_HTML_ON_SCRIPT_FORMAT, index.ToString().PadLeft(2, '0'), "2", Consts.SEC_UNIT);

				// 題型答案
				html += string.Format(INPUT_ANSWER_HTML_FORMAT, index.ToString().PadLeft(2, '0'), endTime.HMSValue);
			}
			else
			{
				// 時間顯示(eg: 09:23:03)
				html += string.Format(SPAN_TIME_NUM_HTML_FORMAT, endTime.HMSValue);
			}
			return html;
		}

		/// <summary>
		/// 開始時間計算式
		/// </summary>
		/// <param name="startTime">時間計算式</param>
		/// <param name="gap">填空項目</param>
		/// <param name="index">控件索引ID</param>
		/// <returns>HTML模板信息</returns>
		private string GetStartTimeHtml(Time startTime, GapFilling gap, int index)
		{
			var html = string.Empty;
			html += string.Format(SPAN_TIME_NUM_HTML_FORMAT, "现在是");
			// 如果是填空項目
			if (gap == GapFilling.Left)
			{
				// 小時
				html += string.Format(INPUT_HTML_ON_SCRIPT_FORMAT, index.ToString().PadLeft(2, '0'), "0", Consts.HR_UNIT);
				html += SPAN_COLON_HTML;
				// 分鐘
				html += string.Format(INPUT_HTML_ON_SCRIPT_FORMAT, index.ToString().PadLeft(2, '0'), "1", Consts.MIN_UNIT);
				html += SPAN_COLON_HTML;
				// 秒
				html += string.Format(INPUT_HTML_ON_SCRIPT_FORMAT, index.ToString().PadLeft(2, '0'), "2", Consts.SEC_UNIT);
				// 題型答案
				html += string.Format(INPUT_ANSWER_HTML_FORMAT, index.ToString().PadLeft(2, '0'), startTime.HMSValue);
			}
			else
			{
				// 時間顯示(eg: 09:23:03)
				html += string.Format(SPAN_TIME_NUM_HTML_FORMAT, startTime.HMSValue);
			}
			return html;
		}

		/// <summary>
		/// 經過時間計算式
		/// </summary>
		/// <param name="elapsedTime">時間計算式</param>
		/// <param name="gap">填空項目</param>
		/// <param name="sign">運算符(之前\之後)</param>
		/// <param name="index">控件索引ID</param>
		/// <returns>HTML模板信息</returns>
		private string GetElapsedTimeHtml(Time elapsedTime, GapFilling gap, SignOfOperation sign, int index)
		{
			var html = string.Empty;
			html += string.Format(SPAN_TIME_NUM_HTML_FORMAT, "在");
			// 如果是填空項目
			if (gap == GapFilling.Right)
			{
				// 小時
				html += string.Format(INPUT_HTML_FORMAT, index.ToString().PadLeft(2, '0'), "0");
				html += string.Format(SPAN_TIME_NUM_HTML_FORMAT, Consts.HOUR_UNIT);
				// 分鐘
				html += string.Format(INPUT_HTML_FORMAT, index.ToString().PadLeft(2, '0'), "1");
				html += string.Format(SPAN_TIME_NUM_HTML_FORMAT, Consts.MINUTE_UNIT);
				// 秒
				html += string.Format(INPUT_HTML_FORMAT, index.ToString().PadLeft(2, '0'), "2");
				html += string.Format(SPAN_TIME_NUM_HTML_FORMAT, Consts.SEC_UNIT);

				// 題型答案
				html += string.Format(INPUT_ANSWER_HTML_FORMAT, index.ToString().PadLeft(2, '0'), elapsedTime.HMSValue);
			}
			else
			{
				// 時間顯示(eg: 9小時23分鐘3秒)
				// 小時
				if (elapsedTime.Hours.Value != 0)
				{
					html += string.Format(SPAN_TIME_CN_HTML_FORMAT, elapsedTime.Hours, Consts.HOUR_UNIT);
				}
				// 分鐘
				if (elapsedTime.Minutes.Value != 0)
				{
					html += string.Format(SPAN_TIME_CN_HTML_FORMAT, elapsedTime.Minutes, Consts.MINUTE_UNIT);
				}
				// 秒
				if (elapsedTime.Seconds.Value != 0)
				{
					html += string.Format(SPAN_TIME_CN_HTML_FORMAT, elapsedTime.Seconds, Consts.SEC_UNIT);
				}
			}
			html += string.Format(SPAN_TIME_NUM_HTML_FORMAT, sign.ToOperationUnicode());
			return html;
		}
	}
}
