using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Item;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Main.Parameters;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Main.Strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace MyMathSheets.TheFormulaShows.CleverCalculation.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("CleverCalculation")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.CleverCalculation.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.CleverCalculation.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.CleverCalculation.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.CleverCalculation.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.CleverCalculation.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.CleverCalculation.printAfterSetting();")]
	public class CleverCalculationHtmlSupport : HtmlSupportBase<CleverCalculationParameter>
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 答題結果項目HTML模板
		/// </summary>
		private const string ANSWER_HTML_FORMAT = "<input id=\"hiddenClc{0}\" type=\"hidden\" value=\"{1}\"/>";

		/// <summary>
		/// LABEL標籤HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 輸入項目HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputClc{0}L{1}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";

		/// <summary>
		/// 巧算的HTML實現方法集合
		/// </summary>
		private readonly Dictionary<int, Func<CleverCalculationFormula, string>> _calculations = new Dictionary<int, Func<CleverCalculationFormula, string>>();

		/// <summary>
		/// <see cref="CleverCalculationHtmlSupport"/>構造函數
		/// </summary>
		[ImportingConstructor]
		public CleverCalculationHtmlSupport()
		{
			// 乘法巧算
			_calculations[(int)TopicType.Multiple] = CleverWithMultiple;
			// 加法和減法巧算
			_calculations[(int)TopicType.Plus] = CleverWithPlus;
			_calculations[(int)TopicType.Subtraction] = CleverWithSubtraction;
			// 綜合題型巧算（拆解）
			_calculations[(int)Synthetic.Unknit] = CleverWithSyntheticUnknit;
			// 綜合題型巧算（合併）
			_calculations[(int)Synthetic.Combine] = CleverWithSyntheticCombine;
		}

		private int _parentIndex;

		/// <summary>
		/// 綜合題型巧算（合併）
		/// </summary>
		/// <param name="formula">計算式</param>
		/// <returns></returns>
		private string CleverWithSyntheticCombine(CleverCalculationFormula formula)
		{
			StringBuilder html = new StringBuilder();
			StringBuilder answer = new StringBuilder();

			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[1].LeftParameter).AppendLine();
			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[1].Sign.ToOperationUnicode()).AppendLine();
			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[1].RightParameter).AppendLine();

			html.AppendLine(string.Format(LABEL_HTML_FORMAT, formula.ConfixFormulas[3].Sign.ToOperationUnicode()));

			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[2].LeftParameter).AppendLine();
			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[2].Sign.ToOperationUnicode()).AppendLine();
			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[2].RightParameter).AppendLine();

			html.AppendLine(string.Format(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode()));

			int controlIndex = 0;
			html.AppendLine(GetHtml(GapFilling.Left, formula.ConfixFormulas[0].LeftParameter, GapFilling.Left, _parentIndex, controlIndex++));
			html.AppendLine(string.Format(LABEL_HTML_FORMAT, formula.ConfixFormulas[0].Sign.ToOperationUnicode()));
			html.AppendLine(GetHtml(GapFilling.Right, formula.ConfixFormulas[0].RightParameter, GapFilling.Right, _parentIndex, controlIndex++));

			html.AppendLine(string.Format(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode()));

			html.AppendLine(GetHtml(GapFilling.Answer, formula.Answer[0], GapFilling.Answer, _parentIndex, controlIndex++));

			answer.AppendFormat("{0};", formula.ConfixFormulas[0].LeftParameter);
			answer.AppendFormat("{0};", formula.ConfixFormulas[0].RightParameter);
			answer.AppendFormat("{0}", formula.Answer[0]);

			// 用於結果驗證
			html.AppendFormat(ANSWER_HTML_FORMAT, _parentIndex.ToString().PadLeft(2, '0'), Base64.EncodeBase64(answer.ToString()));

			return html.ToString();
		}

		/// <summary>
		/// 綜合題型巧算（拆解）
		/// </summary>
		/// <param name="formula">計算式</param>
		/// <returns></returns>
		private string CleverWithSyntheticUnknit(CleverCalculationFormula formula)
		{
			StringBuilder html = new StringBuilder();
			StringBuilder answer = new StringBuilder();

			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[0].LeftParameter).AppendLine();
			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[0].Sign.ToOperationUnicode()).AppendLine();
			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[0].RightParameter).AppendLine();

			int controlIndex = 0;
			html.AppendLine(string.Format(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode()));
			html.AppendLine(GetHtml(GapFilling.Left, formula.ConfixFormulas[1].LeftParameter, GapFilling.Left, _parentIndex, controlIndex++));
			html.AppendLine(string.Format(LABEL_HTML_FORMAT, formula.ConfixFormulas[1].Sign.ToOperationUnicode()));
			html.AppendLine(GetHtml(GapFilling.Right, formula.ConfixFormulas[1].RightParameter, GapFilling.Right, _parentIndex, controlIndex++));

			html.AppendLine(string.Format(LABEL_HTML_FORMAT, formula.ConfixFormulas[3].Sign.ToOperationUnicode()));

			html.AppendLine(GetHtml(GapFilling.Left, formula.ConfixFormulas[2].LeftParameter, GapFilling.Left, _parentIndex, controlIndex++));
			html.AppendLine(string.Format(LABEL_HTML_FORMAT, formula.ConfixFormulas[2].Sign.ToOperationUnicode()));
			html.AppendLine(GetHtml(GapFilling.Right, formula.ConfixFormulas[2].RightParameter, GapFilling.Right, _parentIndex, controlIndex++));

			html.AppendLine(string.Format(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode()));

			html.AppendLine(GetHtml(GapFilling.Answer, formula.Answer[0], GapFilling.Answer, _parentIndex, controlIndex++));

			answer.AppendFormat("{0};", formula.ConfixFormulas[1].LeftParameter);
			answer.AppendFormat("{0};", formula.ConfixFormulas[1].RightParameter);
			answer.AppendFormat("{0};", formula.ConfixFormulas[2].LeftParameter);
			answer.AppendFormat("{0};", formula.ConfixFormulas[2].RightParameter);
			answer.AppendFormat("{0}", formula.Answer[0]);

			// 用於結果驗證
			html.AppendFormat(ANSWER_HTML_FORMAT, _parentIndex.ToString().PadLeft(2, '0'), Base64.EncodeBase64(answer.ToString()));

			return html.ToString();
		}

		/// <summary>
		/// 加法巧算
		/// </summary>
		/// <param name="formula">計算式</param>
		/// <returns></returns>
		private string CleverWithPlus(CleverCalculationFormula formula)
		{
			StringBuilder html = new StringBuilder();
			StringBuilder answer = new StringBuilder();

			string flag = "topic";
			int controlIndex = 0;
			formula.ConfixFormulas.ToList().ForEach(d =>
			{
				if ("topic".Equals(flag))
				{
					html.AppendFormat(LABEL_HTML_FORMAT, d.LeftParameter);
					html.AppendFormat(LABEL_HTML_FORMAT, d.Sign.ToOperationUnicode());
					html.AppendFormat(LABEL_HTML_FORMAT, d.RightParameter);
				}
				if ("result".Equals(flag))
				{
					html.AppendLine(string.Format(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode()));
					html.AppendLine(GetHtml(GapFilling.Left, d.LeftParameter, GapFilling.Left, _parentIndex, controlIndex++));
					html.AppendLine(string.Format(LABEL_HTML_FORMAT, d.Sign.ToOperationUnicode()));
					html.AppendLine(GetHtml(GapFilling.Right, d.RightParameter, GapFilling.Right, _parentIndex, controlIndex++));

					answer.AppendFormat("{0};", d.LeftParameter);
					answer.AppendFormat("{0};", d.RightParameter);
				}
				flag = "result";
			});

			html.AppendLine(string.Format(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode()));
			html.AppendLine(GetHtml(GapFilling.Answer, formula.Answer[0], GapFilling.Answer, _parentIndex, controlIndex++));
			answer.AppendFormat("{0}", formula.Answer[0]);

			// 用於結果驗證
			html.AppendFormat(ANSWER_HTML_FORMAT, _parentIndex.ToString().PadLeft(2, '0'), Base64.EncodeBase64(answer.ToString()));

			return html.ToString();
		}

		/// <summary>
		/// 減法巧算
		/// </summary>
		/// <param name="formula">計算式</param>
		/// <returns></returns>
		private string CleverWithSubtraction(CleverCalculationFormula formula)
		{
			return CleverWithPlus(formula);
		}

		/// <summary>
		/// 乘法巧算
		/// </summary>
		/// <param name="formula">計算式</param>
		/// <returns></returns>
		private string CleverWithMultiple(CleverCalculationFormula formula)
		{
			StringBuilder html = new StringBuilder();
			StringBuilder answer = new StringBuilder();

			html.AppendFormat(LABEL_HTML_FORMAT, formula.ConfixFormulas[0].Answer);

			int controlIndex = 0;
			formula.ConfixFormulas.ToList().ForEach(d =>
			{
				html.AppendLine(string.Format(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode()));
				html.AppendLine(GetHtml(d.Gap, d.LeftParameter, GapFilling.Left, _parentIndex, controlIndex++));
				html.AppendLine(string.Format(LABEL_HTML_FORMAT, SignOfOperation.Multiple.ToOperationUnicode()));
				html.AppendLine(GetHtml(d.Gap, d.RightParameter, GapFilling.Right, _parentIndex, controlIndex++));
			});

			formula.Answer.ForEach(d => answer.AppendFormat("{0};", d));
			answer.Length -= 1;
			// 用於結果驗證
			html.AppendFormat(ANSWER_HTML_FORMAT, _parentIndex.ToString().PadLeft(2, '0'), Base64.EncodeBase64(answer.ToString()));

			return html.ToString();
		}

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		public override string MakeHtmlContent(CleverCalculationParameter p)
		{
			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;

			_parentIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();

			// 算式作成
			p.Formulas.ToList().ForEach(d =>
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-6 form-inline\">");
				colHtml.AppendLine("<h5>");

				if (_calculations.TryGetValue(d.Type, out Func<CleverCalculationFormula, string> calculation))
				{
					colHtml.AppendLine(calculation(d));
				}

				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKCleverCalculation{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", _parentIndex.ToString().PadLeft(2, '0')));
				colHtml.AppendLine(string.Format("<img id=\"imgNoCleverCalculation{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", _parentIndex.ToString().PadLeft(2, '0')));
				colHtml.AppendLine("</div>");
				colHtml.AppendLine("</div>");

				_parentIndex++;
				numberOfColumns++;

				// 單位行上顯示2道題目
				if (numberOfColumns == 2)
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
			});

			if (!isRowHtmlClosed)
			{
				rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
				rowHtml.Append(colHtml.ToString());
				rowHtml.AppendLine("</div>");

				html.Append(rowHtml);
			}

			if (html.Length != 0)
			{
				html.Insert(0, "<div class=\"div-page-content\">").AppendLine();
				html.AppendLine("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "CleverCalculation", "巧算"));
			}

			return html.ToString();
		}

		/// <summary>
		/// 計算式HTML作成
		/// </summary>
		/// <param name="item">填空項目</param>
		/// <param name="parameter">計算式中的數值</param>
		/// <param name="gap">當前顯示項目所在計算式中的位置</param>
		/// <param name="parentIndex">控件索引號</param>
		/// <param name="controlIndex">子控件索引號</param>
		/// <returns>HTML模板信息</returns>
		private string GetHtml(GapFilling item, int parameter, GapFilling gap, int parentIndex, int controlIndex)
		{
			StringBuilder html = new StringBuilder();
			if (item == gap)
			{
				html.AppendFormat(INPUT_HTML_FORMAT, parentIndex.ToString().PadLeft(2, '0'), controlIndex.ToString());
			}
			else
			{
				html.AppendFormat(LABEL_HTML_FORMAT, parameter);
			}

			return html.ToString();
		}
	}
}