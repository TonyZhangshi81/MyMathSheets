using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
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
		private const string EQUALTO_HTML_FORMAT = "<span class=\"label\">{0}</span>";
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
				colHtml.AppendLine("<div class=\"col-md-4 form-inline\">");
				colHtml.AppendLine("<h5>");

				if (item.AnswerIsRight)
				{
					// 第一級計算式中是否使用括號
					if (item.Arithmetic.IsNeedBracket)
					{
						// 左括號（注意：右括號在第二級計算式中）
						colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, "("));
					}

					// 第一級計算式左側的值
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.LeftParameter, GapFilling.Left, controlIndex));

					// 第一級計算式運算符
					colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, item.Arithmetic.Sign.ToOperationUnicode()));

					// 是否運用多級四則運算
					if (item.MultistageArithmetic is null)
					{
						// 第一級計算式右側的值
						colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.RightParameter, GapFilling.Right, controlIndex));
					}
					else
					{
						// 第二級計算式
						colHtml.AppendLine(GetMultistageFormula(item.Arithmetic, item.MultistageArithmetic, controlIndex));
					}

					// 等號
					colHtml.AppendLine(string.Format(EQUALTO_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareString()));

					// 結果值
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.Answer, GapFilling.Answer, controlIndex));
				}
				else
				{
					// 結果值
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.Answer, GapFilling.Answer, controlIndex));

					// 等號
					colHtml.AppendLine(string.Format(EQUALTO_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareString()));

					// 第一級計算式中是否使用括號
					if (item.Arithmetic.IsNeedBracket)
					{
						// 左括號（注意：右括號在第二級計算式中）
						colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, "("));
					}

					// 第一級計算式左側的值
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.LeftParameter, GapFilling.Left, controlIndex));

					// 第一級計算式運算符
					colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, item.Arithmetic.Sign.ToOperationUnicode()));

					// 是否運用多級四則運算
					if (item.MultistageArithmetic is null)
					{
						// 第一級計算式右側的值
						colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.RightParameter, GapFilling.Right, controlIndex));
					}
					else
					{
						// 第二級計算式
						colHtml.AppendLine(GetMultistageFormula(item.Arithmetic, item.MultistageArithmetic, controlIndex));
					}
				}

				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKArithmetic{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgNoArithmetic{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex));
				colHtml.AppendLine("</div>");
				colHtml.AppendLine("</div>");

				controlIndex++;
				numberOfColumns++;

				// 單位行上顯示3道題目
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
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, LayoutSetting.Preview.Arithmetic.ToString(), LayoutSetting.Preview.Arithmetic.ToComputationalStrategyName()));
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

			// 第二級計算式中是否使用括號
			if (multistageFormula.IsNeedBracket)
			{
				// 左括號
				html += string.Format(LABEL_HTML_FORMAT, "(");
			}

			// 第二級計算式的左側值
			html += GetHtml(multistageFormula.Gap, multistageFormula.LeftParameter, GapFilling.Left, controlIndex);

			// 第一級計算式中是否使用括號
			if (leftFormula.IsNeedBracket)
			{
				// 右括號（注意：左括號在第一級計算式中）
				html += string.Format(LABEL_HTML_FORMAT, ")");
			}

			// 前一級運算符是減法的話,下一級的運算符需要變換
			if (leftFormula.Sign == SignOfOperation.Subtraction && !multistageFormula.IsNeedBracket)
			{
				// 運算符
				html += (multistageFormula.Sign == SignOfOperation.Plus) ? string.Format(LABEL_HTML_FORMAT, SignOfOperation.Subtraction.ToOperationUnicode()) : string.Format(LABEL_HTML_FORMAT, SignOfOperation.Plus.ToOperationUnicode());
			}
			else
			{
				// 運算符
				html += string.Format(LABEL_HTML_FORMAT, multistageFormula.Sign.ToOperationUnicode());
			}

			// 第二級計算式的右側值
			html += GetHtml(multistageFormula.Gap, multistageFormula.RightParameter, GapFilling.Right, controlIndex);

			// 第二級計算式中是否使用括號
			if (multistageFormula.IsNeedBracket)
			{
				// 左括號
				html += string.Format(LABEL_HTML_FORMAT, ")");
			}

			return html;
		}

		/// <summary>
		/// 計算式HTML作成
		/// </summary>
		/// <param name="item">填空項目</param>
		/// <param name="parameter">計算式中的數值</param>
		/// <param name="gap">當前顯示項目所在計算式中的位置</param>
		/// <param name="index">控件索引號</param>
		/// <param name="isMultistage">是否使用多集計算</param>
		/// <returns>HTML模板信息</returns>
		private string GetHtml(GapFilling item, int parameter, GapFilling gap, int index, bool isMultistage = false)
		{
			var html = string.Empty;
			if (item == gap)
			{
				html += string.Format("<input id=\"inputAc{0}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", index);
				html += string.Format("<input id=\"hiddenAc{0}\" type=\"hidden\" value=\"{1}\"/>", index, Base64.EncodeBase64(parameter.ToString()));
			}
			else
			{
				html = string.Format(LABEL_HTML_FORMAT, parameter);
			}

			return html;
		}
	}
}
