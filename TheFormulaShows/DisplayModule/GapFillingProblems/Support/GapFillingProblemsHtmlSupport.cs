using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Item;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Main.Parameters;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyMathSheets.TheFormulaShows.GapFillingProblems.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.GapFillingProblems)]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.GapFillingProblems.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.GapFillingProblems.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.GapFillingProblems.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.GapFillingProblems.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.GapFillingProblems.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.GapFillingProblems.printAfterSetting();")]
	public class GapFillingProblemsHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">{1}</span></h4></div><hr />";
		/// <summary>
		/// 輸入框HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputGfp{0}{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" />";
		/// <summary>
		/// 文字表示HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";
		/// <summary>
		/// 答題答案HTML模板
		/// </summary>
		private const string ANSWER_HIDDEN_HTML_FORMAT = "<input id=\"hiddenGfpAnswer{0}\" type=\"hidden\" value=\"{1}\" />";

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			GapFillingProblemsParameter p = parameter as GapFillingProblemsParameter;

			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int parentControlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			foreach (GapFillingProblemsFormula item in p.Formulas)
			{
				rowHtml.AppendLine("<div class=\"col-md-12 form-inline\">");
				rowHtml.AppendLine("<h5>");
				rowHtml.AppendLine("<p class=\"text-info\">");
				// 題號
				rowHtml.AppendLine(string.Format("<span class=\"label label-default\">{0}. </span>", parentControlIndex + 1));

				int index = 0;
				StringBuilder answer = new StringBuilder();
				// 填空題內容中的參數拼接
				Regex.Split(item.GapFillingProblem, "INPUT").ToList().ForEach(d =>
				{
					// 文字描述部分
					rowHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, d));
					if (!string.IsNullOrEmpty(item.Answers[index]))
					{
						// 答題輸入框
						rowHtml.AppendLine(string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), index));
						answer.AppendFormat("{0},", item.Answers[index]);
					}
					index++;
				});

				// 答題答案
				answer.Length -= 1;
				rowHtml.AppendLine(string.Format(ANSWER_HIDDEN_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), answer));

				rowHtml.AppendLine("</p>");
				rowHtml.AppendLine("</h5>");
				rowHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				rowHtml.AppendLine(string.Format("<img id=\"imgOKGapFillingProblems{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", parentControlIndex.ToString().PadLeft(2, '0')));
				rowHtml.AppendLine(string.Format("<img id=\"imgNoGapFillingProblems{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", parentControlIndex.ToString().PadLeft(2, '0')));
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
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, LayoutSetting.Preview.GapFillingProblems.ToString(), "基礎填空題"));
			}

			return html.ToString();
		}
	}
}
