using MyMathSheets.CommonLib.Main.HtmlSupport;
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
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.Arithmetic)]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.Arithmetic.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.Arithmetic.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.Arithmetic.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.Arithmetic.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.Arithmetic.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.Arithmetic.printAfterSetting();")]
	public class ArithmeticHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">{1}</span></h4></div><hr />";
		/// <summary>
		/// 等號HTML模板
		/// </summary>
		private const string EQUALTO_HTML = "<span class=\"label\">=</span>";
		/// <summary>
		/// LABEL標籤HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
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
					colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, item.Arithmetic.Sign.ToOperationString()));
					// 是否運用多級四則運算
					colHtml.AppendLine((item.MultistageArithmetic is null) ? GetHtml(item.Arithmetic.Gap, item.Arithmetic.RightParameter, GapFilling.Right, controlIndex) : GetMultistageFormula(item.Arithmetic, item.MultistageArithmetic, controlIndex));
					colHtml.AppendLine(EQUALTO_HTML);
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.Answer, GapFilling.Answer, controlIndex));
				}
				else
				{
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.Answer, GapFilling.Answer, controlIndex));
					colHtml.AppendLine(EQUALTO_HTML);
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.LeftParameter, GapFilling.Left, controlIndex));
					colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, item.Arithmetic.Sign.ToOperationString()));
					// 是否運用多級四則運算
					colHtml.AppendLine((item.MultistageArithmetic is null) ? GetHtml(item.Arithmetic.Gap, item.Arithmetic.RightParameter, GapFilling.Right, controlIndex) : GetMultistageFormula(item.Arithmetic, item.MultistageArithmetic, controlIndex));
				}

				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKArithmetic{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgNoArithmetic{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex));
				colHtml.AppendLine("</div>");
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
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, LayoutSetting.Preview.Arithmetic.ToString(), "四則運算"));
			}

			return html.ToString();
		}

		/// <summary>
		/// 第二級四則運算打印顯示
		/// </summary>
		/// <param name="leftFormula">前一級運算式</param>
		/// <param name="multistageFormula">第二級運算式</param>
		/// <param name="controlIndex">控件索引號</param>
		/// <returns>四則運算打印顯示信息</returns>
		private string GetMultistageFormula(Formula leftFormula, Formula multistageFormula, int controlIndex)
		{
			var html = string.Empty;

			html += GetHtml(multistageFormula.Gap, multistageFormula.LeftParameter, GapFilling.Left, controlIndex);
			// 前一級運算符是減法的話,下一級的運算符需要變換
			if (leftFormula.Sign == SignOfOperation.Subtraction)
			{
				html += (multistageFormula.Sign == SignOfOperation.Plus) ? string.Format(LABEL_HTML_FORMAT, SignOfOperation.Subtraction.ToOperationString()) : string.Format(LABEL_HTML_FORMAT, SignOfOperation.Plus.ToOperationString());
			}
			if (leftFormula.Sign == SignOfOperation.Plus)
			{
				html += string.Format(LABEL_HTML_FORMAT, multistageFormula.Sign.ToOperationString());
			}
			html += GetHtml(multistageFormula.Gap, multistageFormula.RightParameter, GapFilling.Right, controlIndex);
			
			return html;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="parameter"></param>
		/// <param name="gap"></param>
		/// <param name="index"></param>
		/// <param name="isMultistage"></param>
		/// <returns></returns>
		private string GetHtml(GapFilling item, int parameter, GapFilling gap, int index, bool isMultistage = false)
		{
			var html = string.Empty;
			if (item == gap)
			{
				html += string.Format("<input id=\"inputAc{0}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", index);
				html += string.Format("<input id=\"hiddenAc{0}\" type=\"hidden\" value=\"{1}\"/>", index, parameter);
			}
			else
			{
				html = string.Format(LABEL_HTML_FORMAT, parameter);
			}

			/*
			if (isMultistage)
			{
				html += string.Format("<input id=\"hiddenAc{0}\" type=\"hidden\" value=\"{1}\"/>", index, parameter);
			}
			else
			{
				html += string.Format("<input id=\"hiddenAc{0}\" type=\"hidden\" value=\"{1}\"/>", index, parameter);
			}
			*/

			return html;
		}
	}
}
