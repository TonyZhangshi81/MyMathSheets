using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Main.VirtualHelper;
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
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 等號HTML模板
		/// </summary>
		private const string EQUALTO_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// LABEL標籤HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 答題提示JS事件註冊模板
		/// </summary>
		private const string DIALOGUE_JS_HTML_FORMAT = "onFocus=\"MathSheets.Arithmetic.inputOnFocus(this, {0});\"";

		/// <summary>
		/// 答題提示項目HTML模板
		/// </summary>
		private const string DIALOGUE_CONTENT_HTML_FORMAT = "<input id=\"hiddenAcTony\" type=\"hidden\" value=\"{0}\"/>";

		/// <summary>
		/// 答題結果項目HTML模板
		/// </summary>
		private const string ANSWER_HTML_FORMAT = "<input id=\"hiddenAc{0}\" type=\"hidden\" value=\"{1}\"/>";

		/// <summary>
		/// 輸入項目HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputAc{0}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" {1} onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";

		/// <summary>
		/// 智能提示
		/// </summary>
		private HelperDialogue BrainpowerHint { get; set; }

		private int _brainpowerIndex;

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
			BrainpowerHint = p.BrainpowerHint;
			_brainpowerIndex = 0;

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
					//if (item.Arithmetic.IsNeedBracket)
					//{
					//	// 左括號（注意：右括號在第二級計算式中）
					//	colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, "("));
					//}

					// 第一級計算式左側的值
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.LeftParameter, GapFilling.Left, controlIndex));

					// 第一級計算式運算符
					colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, item.Arithmetic.Sign.ToOperationUnicode()));

					// 是否運用多級四則運算
					//if (item.MultistageArithmetic is null)
					//{
					// 第一級計算式右側的值
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.RightParameter, GapFilling.Right, controlIndex));
					//}
					//else
					//{
					//	// 第二級計算式
					//	colHtml.AppendLine(GetMultistageFormula(item.Arithmetic, item.MultistageArithmetic, controlIndex));
					//}

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
					//if (item.Arithmetic.IsNeedBracket)
					//{
					//	// 左括號（注意：右括號在第二級計算式中）
					//	colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, "("));
					//}

					// 第一級計算式左側的值
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.LeftParameter, GapFilling.Left, controlIndex));

					// 第一級計算式運算符
					colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, item.Arithmetic.Sign.ToOperationUnicode()));

					// 是否運用多級四則運算
					//if (item.MultistageArithmetic is null)
					//{
					// 第一級計算式右側的值
					colHtml.AppendLine(GetHtml(item.Arithmetic.Gap, item.Arithmetic.RightParameter, GapFilling.Right, controlIndex));
					//}
					//else
					//{
					//	// 第二級計算式
					//	colHtml.AppendLine(GetMultistageFormula(item.Arithmetic, item.MultistageArithmetic, controlIndex));
					//}
				}

				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKArithmetic{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex.ToString().PadLeft(2, '0')));
				colHtml.AppendLine(string.Format("<img id=\"imgNoArithmetic{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex.ToString().PadLeft(2, '0')));
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
				html.Insert(0, "<div class=\"div-page-content\">").AppendLine();
				// 會話提示內容保存至畫面
				html.AppendLine(string.Format(DIALOGUE_CONTENT_HTML_FORMAT, GetArithmeticDialogue()));
				html.AppendLine().Append("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, LayoutSetting.Preview.Arithmetic.ToString(), LayoutSetting.Preview.Arithmetic.ToComputationalStrategyName()));
			}

			return html.ToString();
		}

		/// <summary>
		/// 會話提示內容保存至畫面
		/// </summary>
		/// <returns>HTML模板信息</returns>
		private string GetArithmeticDialogue()
		{
			if (BrainpowerHint == null)
			{
				return string.Empty;
			}

			StringBuilder html = new StringBuilder();
			BrainpowerHint.Dialogues.ForEach(d =>
			{
				html.AppendFormat("{0};", d);
			});
			html.Length -= 1;

			return html.ToString();
		}

		/// <summary>
		/// 計算式HTML作成
		/// </summary>
		/// <param name="item">填空項目</param>
		/// <param name="parameter">計算式中的數值</param>
		/// <param name="gap">當前顯示項目所在計算式中的位置</param>
		/// <param name="index">控件索引號</param>
		/// <returns>HTML模板信息</returns>
		private string GetHtml(GapFilling item, int parameter, GapFilling gap, int index)
		{
			StringBuilder html = new StringBuilder();
			if (item == gap)
			{
				if (BrainpowerHint.FormulaIndex.Count > _brainpowerIndex && BrainpowerHint.FormulaIndex[_brainpowerIndex] == index)
				{
					html.AppendFormat(INPUT_HTML_FORMAT, index.ToString().PadLeft(2, '0'), string.Format(DIALOGUE_JS_HTML_FORMAT, _brainpowerIndex++));
				}
				else
				{
					html.AppendFormat(INPUT_HTML_FORMAT, index.ToString().PadLeft(2, '0'), string.Empty);
				}
				html.AppendFormat(ANSWER_HTML_FORMAT, index.ToString().PadLeft(2, '0'), Base64.EncodeBase64(parameter.ToString()));
			}
			else
			{
				html.AppendFormat(LABEL_HTML_FORMAT, parameter);
			}

			return html.ToString();
		}
	}
}