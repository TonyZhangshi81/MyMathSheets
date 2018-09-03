using ComputationalStrategy.Item;
using System.Collections.Generic;
using System.Text;
using TheFormulaShows.Attributes;

namespace TheFormulaShows.Support
{
	/// <summary>
	/// 
	/// </summary>
	[Substitute("<!--MATHWORDPROBLEMSSCRIPT-->", "<script src=\"../Scripts/Ext/MathWordProblems.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--MATHWORDPROBLEMSREADY-->", "mathWordProblemsReady();")]
	[Substitute("//<!--MATHWORDPROBLEMSMAKECORRECTIONS-->", "fault += mathWordProblemsMakeCorrections();")]
	[Substitute("//<!--MATHWORDPROBLEMSTHEIRPAPERS-->", "mathWordProblemsTheirPapers();")]
	public class MathWordProblemsSupport : IMakeHtml<List<MathWordProblemsFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		/// <returns></returns>
		public string MakeHtml(List<MathWordProblemsFormula> formulas)
		{
			if (formulas.Count == 0)
			{
				return "<BR/>";
			}

			int parentControlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			foreach (MathWordProblemsFormula item in formulas)
			{
				rowHtml.AppendLine("<div class=\"col-md-12 form-inline\">");
				rowHtml.AppendLine("<h5>");
				rowHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.MathWordProblem));
				rowHtml.AppendLine("</h5>");
				rowHtml.AppendLine("</div>");
				rowHtml.AppendLine("<div class=\"col-md-12 form-inline\">");
				rowHtml.AppendLine("<h5>");
				rowHtml.AppendLine(string.Format("<input id=\"inputMwp{0}{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, 0));
				rowHtml.AppendLine(string.Format("<img src=\"../Content/image/help.png\" id=\"imgMwp{0}\" style=\"width: 30px; height: 30px;\" title=\"help\" />", parentControlIndex));
				rowHtml.AppendLine(string.Format("<input id=\"inputMwp{0}{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, 1));
				rowHtml.AppendLine("<img src=\"../Content/image/calculator.png\" style=\"width: 30px; height: 30px;\" />");
				rowHtml.AppendLine(string.Format("<input id=\"inputMwp{0}{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, 2));
				rowHtml.AppendLine(string.Format("<input id=\"hiddenMwp{0}\" type=\"hidden\" value=\"{1}\" />", parentControlIndex, item.Verify));
				rowHtml.AppendLine(string.Format("<img id=\"imgProblemsOK{0}\" src=\"../Content/image/correct.png\" style=\"width: 40px; height: 40px; display: none; \" />", parentControlIndex));
				rowHtml.AppendLine(string.Format("<img id=\"imgProblemsNo{0}\" src=\"../Content/image/fault.png\" style=\"width: 40px; height: 40px; display: none; \" />", parentControlIndex));
				rowHtml.AppendLine("</h5>");
				rowHtml.AppendLine("</div>");

				parentControlIndex++;
			}

			if(formulas.Count > 0)
			{
				html.AppendLine("<div class=\"row text-center row-margin-top\">");
				html.Append(rowHtml.ToString());
				html.AppendLine("</div>");
			}

			if (html.Length != 0)
			{
				html.Insert(0, "<div class=\"page - header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" />算式应用题</h4></div><hr />");
			}

			return html.ToString();
		}
	}
}
