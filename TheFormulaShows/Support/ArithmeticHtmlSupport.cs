using CommonLib.Util;
using ComputationalStrategy.Item;
using System.Collections.Generic;
using System.Text;
using TheFormulaShows.Attributes;

namespace TheFormulaShows.Support
{
	/// <summary>
	/// 
	/// </summary>
	[Substitute("<!--ARITHMETICSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.Arithmetic.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--ARITHMETICREADY-->", "MathSheets.Arithmetic.ready();")]
	[Substitute("//<!--ARITHMETICMAKECORRECTIONS-->", "fault += MathSheets.Arithmetic.makeCorrections();")]
	[Substitute("//<!--ARITHMETICTHEIRPAPERS-->", "MathSheets.Arithmetic.theirPapers();")]
	[Substitute("//<!--ARITHMETICPRINTSETTING-->", "MathSheets.Arithmetic.printSetting();")]
	[Substitute("//<!--ARITHMETICPRINTAFTERSETTING-->", "MathSheets.Arithmetic.printAfterSetting();")]
	public class ArithmeticHtmlSupport : IMakeHtml<List<Formula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		/// <returns></returns>
		public string MakeHtml(List<Formula> formulas)
		{
			if (formulas.Count == 0)
			{
				return "<BR/>";
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;

			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();
			foreach (Formula item in formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-3 form-inline\">");
				colHtml.AppendLine("<h5>");
				colHtml.AppendLine(this.GetHtml(item.Gap, item.LeftParameter, GapFilling.Left, controlIndex));
				colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.Sign.ToOperationString()));
				colHtml.AppendLine(this.GetHtml(item.Gap, item.RightParameter, GapFilling.Right, controlIndex));
				colHtml.AppendLine("<span class=\"label\">=</span>");
				colHtml.AppendLine(this.GetHtml(item.Gap, item.Answer, GapFilling.Answer, controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgOKArithmetic{0}\" src=\"../Content/image/correct.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgNoArithmetic{0}\" src=\"../Content/image/fault.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("</div>");

				controlIndex++;
				numberOfColumns++;
				if (numberOfColumns == 4)
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
				html.Insert(0, "<div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">四則運算</span></h4></div><hr />");
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
				html += string.Format("<input id=\"inputAc{0}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", index);
				html += string.Format("<input id=\"hiddenAc{0}\" type=\"hidden\" value=\"{1}\"/>", index, parameter);
			}
			else
			{
				html = string.Format("<span class=\"label\">{0}</span>", parameter);
			}
			return html;
		}
	}
}
