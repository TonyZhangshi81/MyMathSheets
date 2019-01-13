using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Item;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.MathWordProblems.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.MathWordProblems)]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.MathWordProblems.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.MathWordProblems.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.MathWordProblems.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.MathWordProblems.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.MathWordProblems.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.MathWordProblems.printAfterSetting();")]
	public class MathWordProblemsHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">{1}</span></h4></div><hr />";
		/// <summary>
		/// 輸入框HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputMwp{0}{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";
		/// <summary>
		/// 輸入框HTML模板
		/// </summary>
		private const string INPUT_UNIT_HTML_FORMAT = "<input id=\"inputMwp{0}{1}\" type = \"text\" placeholder=\" 字 \" class=\"form-control input-addBorder\" disabled=\"disabled\" />";

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			MathWordProblemsParameter p = parameter as MathWordProblemsParameter;

			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int parentControlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			foreach (MathWordProblemsFormula item in p.Formulas)
			{
				rowHtml.AppendLine("<div class=\"col-md-12 form-inline\">");
				rowHtml.AppendLine("<h5>");
				rowHtml.AppendLine("<p class=\"text-info\">");
				rowHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.MathWordProblem));
				rowHtml.AppendLine("</p>");
				rowHtml.AppendLine("</h5>");
				rowHtml.AppendLine("</div>");
				rowHtml.AppendLine("<div class=\"col-md-12 form-inline\">");
				rowHtml.AppendLine("<h5>");
				rowHtml.AppendLine(string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), 0));
				rowHtml.AppendLine(string.Format("<img src=\"../Content/image/help.png\" id=\"imgMwp{0}\" style=\"width: 30px; height: 30px;\" title=\"help\" />", parentControlIndex.ToString().PadLeft(2, '0')));
				rowHtml.AppendLine(string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), 1));
				rowHtml.AppendLine("<img src=\"../Content/image/calculator.png\" style=\"width: 30px; height: 30px;\" />");
				rowHtml.AppendLine(string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), 2));

				// 單位填寫
				if (!string.IsNullOrEmpty(item.Unit))
				{
					rowHtml.AppendLine("<span class=\"label\">(</span>");
					rowHtml.AppendLine(string.Format(INPUT_UNIT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), 3));
					rowHtml.AppendLine("<span class=\"label\">)</span>");
				}

				// 標準方程式（答案）
				rowHtml.AppendLine(string.Format("<input id=\"hiddenMwpAnswer{0}\" type=\"hidden\" value=\"{1}\" />", parentControlIndex.ToString().PadLeft(2, '0'), item.Verify));
				// 單位（答案）
				rowHtml.AppendLine(string.Format("<input id=\"hiddenMwpUnit{0}\" type=\"hidden\" value=\"{1}\" />", parentControlIndex.ToString().PadLeft(2, '0'), item.Unit));
				rowHtml.AppendLine("</h5>");
				rowHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				rowHtml.AppendLine(string.Format("<img id=\"imgOKProblems{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", parentControlIndex.ToString().PadLeft(2, '0')));
				rowHtml.AppendLine(string.Format("<img id=\"imgNoProblems{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", parentControlIndex.ToString().PadLeft(2, '0')));
				rowHtml.AppendLine("</div>");
				rowHtml.AppendLine("</div>");

				parentControlIndex++;
			}

			if (p.Formulas.Count > 0)
			{
				html.AppendLine("<div class=\"row text-center row-margin-top\">");
				html.Append(rowHtml.ToString());
				html.AppendLine("</div>");
			}

			if (html.Length != 0)
			{
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, LayoutSetting.Preview.MathWordProblems.ToString(), "算數應用題"));
			}

			return html.ToString();
		}
	}
}
