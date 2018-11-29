﻿using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Item;
using MyMathSheets.ComputationalStrategy.Arithmetic.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.Arithmetic.Support
{
	/// <summary>
	/// 
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.Arithmetic)]
	[Substitute("<!--ARITHMETICSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.Arithmetic.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--ARITHMETICREADY-->", "MathSheets.Arithmetic.ready();")]
	[Substitute("//<!--ARITHMETICMAKECORRECTIONS-->", "fault += MathSheets.Arithmetic.makeCorrections();")]
	[Substitute("//<!--ARITHMETICTHEIRPAPERS-->", "MathSheets.Arithmetic.theirPapers();")]
	[Substitute("//<!--ARITHMETICPRINTSETTING-->", "MathSheets.Arithmetic.printSetting();")]
	[Substitute("//<!--ARITHMETICPRINTAFTERSETTING-->", "MathSheets.Arithmetic.printAfterSetting();")]
	public class ArithmeticHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			ArithmeticParameter p = parameter as ArithmeticParameter;

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
			foreach (ArithmeticFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-3 form-inline\">");
				colHtml.AppendLine("<h5>");

				if (item.AnswerIsRight)
				{
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.LeftParameter, GapFilling.Left, controlIndex));
					colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.Arithmetic.Sign.ToOperationString()));
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.RightParameter, GapFilling.Right, controlIndex));
					colHtml.AppendLine("<span class=\"label\">=</span>");
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.Answer, GapFilling.Answer, controlIndex));
				}
				else
				{
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.Answer, GapFilling.Answer, controlIndex));
					colHtml.AppendLine("<span class=\"label\">=</span>");
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.LeftParameter, GapFilling.Left, controlIndex));
					colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.Arithmetic.Sign.ToOperationString()));
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.RightParameter, GapFilling.Right, controlIndex));
				}

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
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">四則運算</span></h4></div><hr />");
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
