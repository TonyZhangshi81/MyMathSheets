using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using MyMathSheets.ComputationalStrategy.NumericSorting.Main.Parameters;
using System.Collections.Generic;
using System.Text;

namespace MyMathSheets.TheFormulaShows.EqualityComparison.Support
{
	/// <summary>
	/// 
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.NumericSorting)]
	[Substitute("<!--NUMERICSORTINGSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.NumericSorting.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--NUMERICSORTINGREADY-->", "MathSheets.NumericSorting.ready();")]
	[Substitute("//<!--NUMERICSORTINGMAKECORRECTIONS-->", "fault += MathSheets.NumericSorting.makeCorrections();")]
	[Substitute("//<!--NUMERICSORTINGTHEIRPAPERS-->", "MathSheets.NumericSorting.theirPapers();")]
	[Substitute("//<!--NUMERICSORTINGPRINTSETTING-->", "MathSheets.NumericSorting.printSetting();")]
	[Substitute("//<!--NUMERICSORTINGPRINTAFTERSETTING-->", "MathSheets.NumericSorting.printAfterSetting();")]
	public class NumericSortingHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
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
				rowHtml.AppendLine("<div class=\"col-md-12 form-inline\">");
				rowHtml.AppendLine("<ul class=\"list-group list-group-ext\">");
				rowHtml.AppendLine("<li class=\"list-group-item\">");
				rowHtml.AppendLine("<h5>");
				item.NumberList.ForEach(d =>
				{
					rowHtml.AppendLine(string.Format("<span class=\"badge badge-warning\" style=\"width: 30px;\">{0}</span>", d));
				});
				rowHtml.AppendLine(string.Format("<input type=\"hidden\" id=\"hidNsAnswer{0}\" value=\"{1}\" />", parentControlIndex, GetAnswer(item.AnswerList)));
				rowHtml.AppendLine(string.Format("<img id=\"imgOKNumericSorting{0}\" src=\"../Content/image/correct.png\" style=\"width: 40px; height: 40px; display: none; \" />", parentControlIndex));
				rowHtml.AppendLine(string.Format("<img id=\"imgNoNumericSorting{0}\" src=\"../Content/image/fault.png\" style=\"width: 40px; height: 40px; display: none; \" />", parentControlIndex));
				rowHtml.AppendLine("</h5>");
				rowHtml.AppendLine("</li>");
				rowHtml.AppendLine("<li class=\"list-group-item\">");
				rowHtml.AppendLine("<h5>");

				int controlIndex = 0;
				item.NumberList.ForEach(d =>
				{
					rowHtml.AppendLine(string.Format("<input id=\"inputNs{0}L{1}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, controlIndex));
					if (controlIndex != p.Amount - 1)
					{
						rowHtml.AppendLine(string.Format("<img src=\"../Content/image/{0}.png\" width=\"30\" height=\"30\" />", item.Sign.ToSignOfCompareEnString()));
					}
					controlIndex++;
				});
				rowHtml.AppendLine("</h5>");
				rowHtml.AppendLine("</li>");
				rowHtml.AppendLine("</ul>");
				rowHtml.AppendLine("</div>");

				colHtml.Append(rowHtml.ToString());

				parentControlIndex++;
			}

			html.AppendLine("<div class=\"row text-center row-margin-top\">");
			html.Append(colHtml.ToString());
			html.AppendLine("</div>");

			if (html.Length != 0)
			{
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">數字排序</span></h4></div><hr />");
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
			anwsers.ForEach(d => result.AppendFormat("{0},", d));
			result.Length -= 1;
			return anwsers.ToString();
		}
	}
}
