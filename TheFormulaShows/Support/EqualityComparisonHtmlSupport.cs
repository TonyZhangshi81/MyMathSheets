using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.TheFormulaShows.Attributes;
using System.Collections.Generic;
using System.Text;

namespace MyMathSheets.TheFormulaShows.Support
{
	/// <summary>
	/// 
	/// </summary>
	[Substitute("<!--EQUALITYCOMPARISONSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.EqualityComparison.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--EQUALITYCOMPARISONREADY-->", "MathSheets.EqualityComparison.ready();")]
	[Substitute("//<!--EQUALITYCOMPARISONMAKECORRECTIONS-->", "fault += MathSheets.EqualityComparison.makeCorrections();")]
	[Substitute("//<!--EQUALITYCOMPARISONTHEIRPAPERS-->", "MathSheets.EqualityComparison.theirPapers();")]
	[Substitute("//<!--EQUALITYCOMPARISONPRINTSETTING-->", "MathSheets.EqualityComparison.printSetting();")]
	[Substitute("//<!--EQUALITYCOMPARISONPRINTAFTERSETTING-->", "MathSheets.EqualityComparison.printAfterSetting();")]
	public class EqualityComparisonHtmlSupport : IMakeHtml<List<EqualityFormula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		/// <returns></returns>
		public string MakeHtml(List<EqualityFormula> formulas)
		{
			if (formulas.Count == 0)
			{
				return string.Empty;
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;

			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();
			foreach (EqualityFormula item in formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-4 form-inline\">");
				colHtml.AppendLine("<h5>");
				colHtml.AppendLine(this.GetHtml(GapFilling.Left, item.LeftFormula.LeftParameter, GapFilling.Default, controlIndex));
				colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.LeftFormula.Sign.ToOperationString()));
				colHtml.AppendLine(this.GetHtml(GapFilling.Right, item.LeftFormula.RightParameter, GapFilling.Default, controlIndex));
				colHtml.AppendLine(string.Format("<img src=\"../Content/image/help.png\" width=\"30\" height=\"30\" id=\"imgEc{0}\" title=\"help\" />", controlIndex));
				colHtml.AppendLine(string.Format("<input id=\"hiddenEc{0}\" type=\"hidden\" value=\"{1}\" />", controlIndex, item.Answer.ToSignOfCompareEnString()));
				colHtml.AppendLine(this.GetHtml(GapFilling.Left, item.RightFormula.LeftParameter, GapFilling.Default, controlIndex));
				colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.RightFormula.Sign.ToOperationString()));
				colHtml.AppendLine(this.GetHtml(GapFilling.Right, item.RightFormula.RightParameter, GapFilling.Default, controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgOKEquality{0}\" src=\"../Content/image/correct.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgNoEquality{0}\" src=\"../Content/image/fault.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				colHtml.AppendLine("</h5>");
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
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">算式比大小</span></h4></div><hr />");
			}
			return html.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="parameter"></param>
		/// <param name="gap"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private string GetHtml(GapFilling item, int parameter, GapFilling gap, int index)
		{
			var html = string.Empty;
			if (item == gap)
			{
				html += string.Format("<input id=\"inputEc{0}\" type = \"text\" placeholder=\" ?? \" class=\"form-control\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", index);
				html += string.Format("<input id=\"hiddenEc{0}\" type=\"hidden\" value=\"{1}\"/>", index, parameter);
			}
			else
			{
				html = string.Format("<span class=\"label\">{0}</span>", parameter);
			}
			return html;
		}
	}
}
