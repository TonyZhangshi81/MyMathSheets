using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Item;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Main.Parameters;
using MyMathSheets.TheFormulaShows.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.Support
{
	/// <summary>
	/// 
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.CombinatorialEquation)]
	[Substitute("<!--COMBINATORIALEQUATIONSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.CombinatorialEquation.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--COMBINATORIALEQUATIONREADY-->", "MathSheets.CombinatorialEquation.ready();")]
	[Substitute("//<!--COMBINATORIALEQUATIONMAKECORRECTIONS-->", "fault += MathSheets.CombinatorialEquation.makeCorrections();")]
	[Substitute("//<!--COMBINATORIALEQUATIONTHEIRPAPERS-->", "MathSheets.CombinatorialEquation.theirPapers();")]
	[Substitute("//<!--COMBINATORIALEQUATIONPRINTSETTING-->", "MathSheets.CombinatorialEquation.printSetting();")]
	[Substitute("//<!--COMBINATORIALEQUATIONPRINTAFTERSETTING-->", "MathSheets.CombinatorialEquation.printAfterSetting();")]
	public class CombinatorialEquationHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 算式組合HTML作成
		/// </summary>
		/// <param name="parameter">相關計算式</param>
		/// <returns>HTML語句</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			CombinatorialEquationParameter p = parameter as CombinatorialEquationParameter;

			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;

			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder listGroupHtml = new StringBuilder();

			foreach (CombinatorialFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;

				listGroupHtml.AppendLine("<div class=\"col-md-4 form-inline\">");
				listGroupHtml.AppendLine("<ul class=\"list-group list-group-ext\">");
				listGroupHtml.AppendLine("<li class=\"list-group-item\">");
				listGroupHtml.AppendLine("<h4>");
				listGroupHtml.AppendLine(string.Format("<span class=\"badge badge-warning\" style=\"width: 40px;\">{0}</span>", item.ParameterA));
				listGroupHtml.AppendLine(string.Format("<span class=\"badge badge-success\" style=\"width: 40px;\">{0}</span>", item.ParameterB));
				listGroupHtml.AppendLine(string.Format("<span class=\"badge badge-primary\" style=\"width: 40px;\">{0}</span>", item.ParameterC));
				listGroupHtml.AppendLine(string.Format("<img id=\"imgOKCombinatorial{0}\" src=\"../Content/image/correct.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				listGroupHtml.AppendLine(string.Format("<img id=\"imgNoCombinatorial{0}\" src=\"../Content/image/fault.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				listGroupHtml.AppendLine("</h4>");
				listGroupHtml.AppendLine(string.Format("<input id=\"hiddenCe{0}\" type=\"hidden\" value=\"{1}\" />", controlIndex, GetAnswer(item.CombinatorialFormulas)));
				listGroupHtml.AppendLine("</li>");
				listGroupHtml.AppendLine(GetHtml(item.CombinatorialFormulas, controlIndex));
				listGroupHtml.AppendLine("</ul>");
				listGroupHtml.AppendLine("</div>");

				controlIndex++;
				numberOfColumns++;
				if (numberOfColumns == 3)
				{
					rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
					rowHtml.Append(listGroupHtml.ToString());
					rowHtml.AppendLine("</div>");

					html.Append(rowHtml);

					isRowHtmlClosed = true;
					numberOfColumns = 0;
					rowHtml.Length = 0;
					listGroupHtml.Length = 0;
				}
			}

			if (!isRowHtmlClosed)
			{
				rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
				rowHtml.Append(listGroupHtml.ToString());
				rowHtml.AppendLine("</div>");

				html.Append(rowHtml);
			}

			if (html.Length != 0)
			{
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">運算組合</span></h4></div><hr />");
			}

			return html.ToString();
		}

		/// <summary>
		/// 一組計算式答題的HTML內容作成
		/// </summary>
		/// <param name="items">4個計算式</param>
		/// <param name="parentControlIndex">答題書索引號</param>
		/// <returns>HTML內容</returns>
		private string GetHtml(IList<Formula> items, int parentControlIndex)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			items.ToList().ForEach(d =>
			{
				html.AppendLine("<li class=\"list-group-item\">");
				html.AppendLine("<h4>");
				html.AppendLine(string.Format("<input id=\"inputCe{0}L{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, controlIndex));
				html.AppendLine(string.Format("<img src=\"../Content/image/help.png\" width=\"30\" height=\"30\" id=\"imgCe{0}S{1}\" title=\"help\" />", parentControlIndex, controlIndex));
				html.AppendLine(string.Format("<input id=\"inputCe{0}R{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, controlIndex));
				html.AppendLine("<img src=\"../Content/image/calculator.png\" style=\"width: 30px; height: 30px; \" />");
				html.AppendLine(string.Format("<input id=\"inputCe{0}A{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, controlIndex));
				html.AppendLine("</h4>");
				html.AppendLine("</li>");

				controlIndex++;
			});

			return html.ToString();
		}

		/// <summary>
		/// 編輯答案存放用於驗證答題
		/// </summary>
		/// <param name="items">4個計算式</param>
		/// <returns>各計算式拼接以逗號分隔形式的字符串</returns>
		private string GetAnswer(IList<Formula> items)
		{
			StringBuilder answer = new StringBuilder();
			items.ToList().ForEach(d =>
			{
				answer.AppendFormat("{0}{1}{2}={3},", d.LeftParameter, d.Sign.ToOperationString(), d.RightParameter, d.Answer);
			});
			answer.Length -= 1;
			return answer.ToString();
		}
	}
}
