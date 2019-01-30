using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.NumericSorting.Item;
using MyMathSheets.ComputationalStrategy.NumericSorting.Main.Parameters;
using System.Collections.Generic;
using System.Text;

namespace MyMathSheets.TheFormulaShows.NumericSorting.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.NumericSorting)]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.NumericSorting.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.NumericSorting.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.NumericSorting.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.NumericSorting.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.NumericSorting.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.NumericSorting.printAfterSetting();")]
	public class NumericSortingHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			NumericSortingParameter p = parameter as NumericSortingParameter;

			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int parentControlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();
			foreach (NumericSortingFormula item in p.Formulas)
			{
				rowHtml.Length = 0;
				rowHtml.AppendLine("<div class=\"col-md-9 form-inline\">");
				rowHtml.AppendLine("<ul class=\"list-group list-group-number list-group-ext\">");
				rowHtml.AppendLine("<li class=\"list-group-item\">");
				rowHtml.AppendLine("<h5>");
				item.NumberList.ForEach(d =>
				{
					rowHtml.AppendLine(string.Format("<span class=\"badge badge-warning\" style=\"width: 30px;\">{0}</span>", d));
				});
				rowHtml.AppendLine(string.Format("<input type=\"hidden\" id=\"hidNsAnswer{0}\" value=\"{1}\" />", parentControlIndex, GetAnswer(item.AnswerList)));
				rowHtml.AppendLine("</h5>");
				rowHtml.AppendLine("</li>");
				rowHtml.AppendLine("<li class=\"list-group-item\">");
				rowHtml.AppendLine("<h5>");

				int controlIndex = 0;
				item.NumberList.ForEach(d =>
				{
					rowHtml.AppendLine(string.Format("<input id=\"inputNs{0}L{1}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, controlIndex));
					if (controlIndex != p.Numbers - 1)
					{
						rowHtml.AppendLine(string.Format("<img src=\"../Content/image/{0}.png\" width=\"30\" height=\"30\" />", item.Sign.ToSignOfCompareEnString()));
					}
					controlIndex++;
				});
				rowHtml.AppendLine("</h5>");
				rowHtml.AppendLine("</li>");
				rowHtml.AppendLine("</ul>");
				rowHtml.AppendLine("</div>");

				rowHtml.AppendLine("<div class=\"col-md-3\">");
				rowHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				rowHtml.AppendLine(string.Format("<img id=\"imgOKNumericSorting{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", parentControlIndex));
				rowHtml.AppendLine(string.Format("<img id=\"imgNoNumericSorting{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", parentControlIndex));
				rowHtml.AppendLine("</div>");
				rowHtml.AppendLine("</div>");

				colHtml.Append(rowHtml.ToString());

				parentControlIndex++;
			}

			html.AppendLine("<div class=\"row text-center row-margin-top\">");
			html.Append(colHtml.ToString());
			html.AppendLine("</div>");

			if (html.Length != 0)
			{
				html.Insert(0, "<div class=\"div-page-content\">").AppendLine();
				html.AppendLine().Append("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, LayoutSetting.Preview.NumericSorting.ToString(), LayoutSetting.Preview.NumericSorting.ToComputationalStrategyName()));
			}
			return html.ToString();
		}

		/// <summary>
		/// 題型結果輸出
		/// </summary>
		/// <param name="anwsers">結果數組</param>
		/// <returns>題型結果(eg:1,2,3,4,5,6....)</returns>
		private string GetAnswer(List<int> anwsers)
		{
			StringBuilder result = new StringBuilder();
			anwsers.ForEach(d => result.AppendFormat("{0};", Base64.EncodeBase64(d.ToString())));
			result.Length -= 1;
			return result.ToString();
		}
	}
}
