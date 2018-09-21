using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameter;
using MyMathSheets.TheFormulaShows.Attributes;
using System.Text;

namespace MyMathSheets.TheFormulaShows.Support
{
	/// <summary>
	/// 
	/// </summary>
	[Substitute("<!--FINDNEARESTNUMBERSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.FindNearestNumber.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--FINDNEARESTNUMBERREADY-->", "MathSheets.FindNearestNumber.ready();")]
	[Substitute("//<!--FINDNEARESTNUMBERMAKECORRECTIONS-->", "fault += MathSheets.FindNearestNumber.makeCorrections();")]
	[Substitute("//<!--FINDNEARESTNUMBERTHEIRPAPERS-->", "MathSheets.FindNearestNumber.theirPapers();")]
	[Substitute("//<!--FINDNEARESTNUMBERPRINTSETTING-->", "MathSheets.FindNearestNumber.printSetting();")]
	[Substitute("//<!--FINDNEARESTNUMBERPRINTAFTERSETTING-->", "MathSheets.FindNearestNumber.printAfterSetting();")]
	public class FindNearestNumberHtmlSupport : IMakeHtml<ParameterBase>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		/// <returns></returns>
		public string MakeHtml(ParameterBase parameter)
		{
			FindNearestNumberParameter p = parameter as FindNearestNumberParameter;

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
			foreach (EqualityFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-4 form-inline\">");
				colHtml.AppendLine("<h5>");
				colHtml.AppendLine(this.GetHtml(item.LeftFormula.Gap, item.LeftFormula.LeftParameter, GapFilling.Left, controlIndex));
				colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.LeftFormula.Sign.ToOperationString()));
				colHtml.AppendLine(this.GetHtml(item.LeftFormula.Gap, item.LeftFormula.RightParameter, GapFilling.Right, controlIndex));
				colHtml.AppendLine(string.Format("<img src=\"../Content/image/{0}.png\" width=\"30\" height=\"30\" />", item.Answer.ToSignOfCompareEnString()));
				colHtml.AppendLine(this.GetHtml(item.RightFormula.Gap, item.RightFormula.LeftParameter, GapFilling.Left, controlIndex));
				colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.RightFormula.Sign.ToOperationString()));
				colHtml.AppendLine(this.GetHtml(item.RightFormula.Gap, item.RightFormula.RightParameter, GapFilling.Right, controlIndex));

				colHtml.AppendLine(string.Format("<img id=\"imgOKFindNearestNumber{0}\" src=\"../Content/image/correct.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgNoFindNearestNumber{0}\" src=\"../Content/image/fault.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
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
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">找出最近的數字</span></h4></div><hr />");
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
				html += string.Format("<input id=\"inputFnn{0}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", index);
				html += string.Format("<input id=\"hiddenFnn{0}\" type=\"hidden\" value=\"{1}\"/>", index, parameter);
			}
			else
			{
				html = string.Format("<span class=\"label\">{0}</span>", parameter);
			}
			return html;
		}
	}
}
