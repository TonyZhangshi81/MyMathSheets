using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.MathUpright.Item;
using MyMathSheets.ComputationalStrategy.MathUpright.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.MathUpright.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("MathUpright")]
	[Substitute(SubstituteType.Stylesheet, "<link href=\"../Content/MathUpright.css\" rel=\"stylesheet\" type=\"text/css\" />")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.MathUpright.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.MathUpright.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.MathUpright.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.MathUpright.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.MathUpright.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.MathUpright.printAfterSetting();")]
	public class MathUprightHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// LABEL標籤HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 輸入項目HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputMu{0}{1}\" type=\"text\" placeholder=\" ? \" class=\"form-control input-addBorder\" style=\"width: 20px; text-align:center;\" disabled=\"disabled\" onFocus=\"MathSheets.MathUpright.inputOnFocus(this);\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			MathUprightParameter p = parameter as MathUprightParameter;

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
			foreach (MathUprightFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-2 form-inline\">");
				// 題型表格HTML模板作成
				colHtml.Append(CreateTeableHtml(item, controlIndex));
				// 題型答案項目HTML模板作成
				colHtml.Append(CreateAnswerHtml(item, controlIndex));
				colHtml.AppendLine("</div>");
				// 間隔
				colHtml.AppendLine("<div class=\"col-md-1\">");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKMathUpright{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex.ToString().PadLeft(2, '0')));
				colHtml.AppendLine(string.Format("<img id=\"imgNoMathUpright{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex.ToString().PadLeft(2, '0')));
				colHtml.AppendLine("</div>");
				colHtml.AppendLine("</div>");

				controlIndex++;
				numberOfColumns++;

				// 單位行上顯示4道題目
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
				html.Insert(0, "<div class=\"div-page-content\">").AppendLine();
				html.AppendLine().Append("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "MathUpright", "豎式計算"));
			}

			return html.ToString();
		}

		/// <summary>
		/// 答案項目HTML模板作成
		/// </summary>
		/// <param name="item"></param>
		/// <param name="parentControlIndex"></param>
		/// <returns></returns>
		private string CreateAnswerHtml(MathUprightFormula item, int parentControlIndex)
		{
			StringBuilder answer = new StringBuilder();
			answer.AppendFormat("{0};{1}", Base64.EncodeBase64(item.FormulaDataLink[(item.FillPosition / 10) - 1].ToString()), Base64.EncodeBase64(item.FormulaDataLink[(item.FillPosition % 10) - 1].ToString()));
			return string.Format("<input id=\"hiddenMuAnswer{0}\" type=\"hidden\" value=\"{1}\"/>", parentControlIndex.ToString().PadLeft(2, '0'), answer.ToString());
		}

		/// <summary>
		/// 題型表格HTML模板作成
		/// </summary>
		/// <param name="item">題型參數</param>
		/// <param name="parentControlIndex">控件索引號</param>
		/// <returns>題型HTML</returns>
		private string CreateTeableHtml(MathUprightFormula item, int parentControlIndex)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			html.AppendLine("<table class=\"table table-striped table-ext\">");
			html.AppendLine("<tbody>");

			// 第一行
			html.AppendLine("<tr class=\"info\">");
			html.AppendLine("<th><br /></th>");
			// 第一個項目
			html.AppendFormat("<th>{0}</th>", IsFillItem(1, item.FillPosition) ? string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), controlIndex++) : string.Format(LABEL_HTML_FORMAT, item.FormulaDataLink[0])).AppendLine();
			// 第二個項目
			html.AppendFormat("<th>{0}</th>", IsFillItem(2, item.FillPosition) ? string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), controlIndex++) : string.Format(LABEL_HTML_FORMAT, item.FormulaDataLink[1])).AppendLine();
			html.AppendLine("</tr>");

			// 第二行
			html.AppendLine("<tr class=\"info\">");
			html.AppendFormat("<th class=\"table-th-sign\">{0}</th>", item.Arithmetic.Sign.ToOperationUnicode()).AppendLine();
			// 第三個項目
			html.AppendFormat("<th>{0}</th>", IsFillItem(3, item.FillPosition) ? string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), controlIndex++) : string.Format(LABEL_HTML_FORMAT, item.FormulaDataLink[2])).AppendLine();
			// 第四個項目
			html.AppendFormat("<th>{0}</th>", IsFillItem(4, item.FillPosition) ? string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), controlIndex++) : string.Format(LABEL_HTML_FORMAT, item.FormulaDataLink[3])).AppendLine();
			html.AppendLine("</tr>");

			// 第三行
			html.AppendLine("<tr class=\"table-tr-hr\">");
			html.AppendLine("<th colspan=\"3\"></th>");
			html.AppendLine("</tr>");

			// 第四行
			html.AppendLine("<tr class=\"info table-tr-sum\">");
			html.AppendLine("<th><br /></th>");
			// 第五個項目
			html.AppendFormat("<th>{0}</th>", IsFillItem(5, item.FillPosition) ? string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), controlIndex++) : string.Format(LABEL_HTML_FORMAT, item.FormulaDataLink[4])).AppendLine();
			// 第六個項目
			html.AppendFormat("<th>{0}</th>", IsFillItem(6, item.FillPosition) ? string.Format(INPUT_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), controlIndex++) : string.Format(LABEL_HTML_FORMAT, item.FormulaDataLink[5])).AppendLine();
			html.AppendLine("</tr>");

			html.AppendLine("</tbody>");
			html.AppendLine("</table>");

			return html.ToString();
		}

		/// <summary>
		/// 判斷是否為填空項目
		/// </summary>
		/// <param name="currentIndex">當前項目索引號</param>
		/// <param name="fillPosition">填空項目索引號</param>
		/// <returns>是:True	不是:False</returns>
		private bool IsFillItem(int currentIndex, int fillPosition)
		{
			if (currentIndex == (fillPosition / 10) || currentIndex == (fillPosition % 10))
			{
				return true;
			}
			return false;
		}
	}
}