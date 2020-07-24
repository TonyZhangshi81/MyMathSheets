using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Item;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Main.Parameters;
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
		/// 智能提示
		/// </summary>
		private HelperDialogue BrainpowerHint { get; set; }

		private int _brainpowerIndex;

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
			BrainpowerHint = p.BrainpowerHint;
			_brainpowerIndex = 0;

			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();
			foreach (CleverCalculationFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-4 form-inline\">");
				colHtml.AppendLine("<h5>");

				// TODO

				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKCleverCalculation{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex.ToString().PadLeft(2, '0')));
				colHtml.AppendLine(string.Format("<img id=\"imgNoCleverCalculation{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex.ToString().PadLeft(2, '0')));
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
				//html.AppendLine(string.Format(DIALOGUE_CONTENT_HTML_FORMAT, GetCleverCalculationDialogue()));
				html.AppendLine().Append("</div>");
				//html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "CleverCalculation", "巧算"));
			}

			return html.ToString();
		}

		/// <summary>
		/// 會話提示內容保存至畫面
		/// </summary>
		/// <returns>HTML模板信息</returns>
		private string GetCleverCalculationDialogue()
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
					//html.AppendFormat(INPUT_HTML_FORMAT, index.ToString().PadLeft(2, '0'), string.Format(DIALOGUE_JS_HTML_FORMAT, _brainpowerIndex++));
				}
				else
				{
					//html.AppendFormat(INPUT_HTML_FORMAT, index.ToString().PadLeft(2, '0'), string.Empty);
				}
				//html.AppendFormat(ANSWER_HTML_FORMAT, index.ToString().PadLeft(2, '0'), Base64.EncodeBase64(parameter.ToString()));
			}
			else
			{
				//html.AppendFormat(LABEL_HTML_FORMAT, parameter);
			}

			return html.ToString();
		}
	}
}