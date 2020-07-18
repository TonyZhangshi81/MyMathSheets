using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.CurrencyOperation.Item;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.CurrencyOperation.Support
{
	/// <summary>
	/// 貨幣運算題型HTML支援類
	/// </summary>
	[HtmlSupport("CurrencyOperation")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.CurrencyOperation.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.CurrencyOperation.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.CurrencyOperation.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.CurrencyOperation.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.CurrencyOperation.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.CurrencyOperation.printAfterSetting();")]
	public class CurrencyOperationHtmlSupport : HtmlSupportBase<CurrencyOperationParameter>
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 輸入項目HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputCo{0}L{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";

		/// <summary>
		/// 等號HTML
		/// </summary>
		private const string CALCULATOR_HTML = "<img src=\"../Content/image/calculator.png\" width=\"30\" height=\"30\" />";

		/// <summary>
		/// 運算符HTML模板
		/// </summary>
		private const string SIGN_HTML_FORMAT = "<img src=\"../Content/image/{0}.png\" width=\"30\" height=\"30\" />";

		/// <summary>
		/// 對錯HTML模板
		/// </summary>
		private const string IMG_CURRENCY_OPERATION_HTML_FORMAT = "<img id=\"img{0}CurrencyOperation{1}\" src=\"../Content/image/{2}.png\" class=\"{3}\" />";

		/// <summary>
		/// SPAN標籤HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>HTML上下文內容</returns>
		public override string MakeHtmlContent(CurrencyOperationParameter p)
		{
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
			foreach (CurrencyOperationFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-6 form-inline\">");
				colHtml.AppendLine("<h6>");

				string pIndex = controlIndex.ToString().PadLeft(2, '0');
				GapFilling gap = item.CurrencyArithmetic.Gap;
				// 方程式左右是否互換位置
				if (item.AnswerIsRight)
				{
					// 加數、被減數
					colHtml.Append(GetHtml(gap == GapFilling.Left, item.CurrencyArithmetic.LeftParameter, pIndex));
					// 算式運算符
					colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, item.CurrencyArithmetic.Sign.ToOperationUnicode()));
					// 加數、減數
					colHtml.Append(GetHtml(gap == GapFilling.Right, item.CurrencyArithmetic.RightParameter, pIndex));
					// 等號
					colHtml.AppendLine(CALCULATOR_HTML);
					// 和、差
					colHtml.Append(GetHtml(gap == GapFilling.Answer, item.CurrencyArithmetic.Answer, pIndex));
				}
				else
				{
					// 和、差
					colHtml.Append(GetHtml(gap == GapFilling.Answer, item.CurrencyArithmetic.Answer, pIndex));
					// 等號
					colHtml.AppendLine(CALCULATOR_HTML);
					// 加數、被減數
					colHtml.Append(GetHtml(gap == GapFilling.Left, item.CurrencyArithmetic.LeftParameter, pIndex));
					// 算式運算符
					colHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, item.CurrencyArithmetic.Sign.ToOperationUnicode()));
					// 加數、減數
					colHtml.Append(GetHtml(gap == GapFilling.Right, item.CurrencyArithmetic.RightParameter, pIndex));
				}

				colHtml.AppendLine("</h6>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format(IMG_CURRENCY_OPERATION_HTML_FORMAT, "OK", pIndex, "correct", "imgCorrect-1"));
				colHtml.AppendLine(string.Format(IMG_CURRENCY_OPERATION_HTML_FORMAT, "No", pIndex, "fault", "imgFault-1"));
				colHtml.AppendLine("</div>");
				colHtml.AppendLine("</div>");

				controlIndex++;
				numberOfColumns++;
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
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "CurrencyOperation", "貨幣運算"));
			}

			return html.ToString();
		}

		/// <summary>
		/// 方程式HTML模板輸出
		/// </summary>
		/// <param name="isFill">是否為填空項目</param>
		/// <param name="parameter">題型參數</param>
		/// <param name="parentIndex">控件ID</param>
		/// <returns>HTML模板</returns>
		private string GetHtml(bool isFill, int parameter, string parentIndex)
		{
			int index = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder answer = new StringBuilder();
			if (isFill)
			{
				switch (parameter.IntToCurrencyUnitType())
				{
					case CurrencyOperationUnitType.Fen:
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.FEN_UNIT));
						answer.AppendFormat("{0}", Base64.EncodeBase64(parameter.IntToCurrency().Fen.Value.ToString()));
						break;

					case CurrencyOperationUnitType.JF:
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.JIAO_UNIT));
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.FEN_UNIT));
						answer.AppendFormat("{0};{1}", Base64.EncodeBase64(parameter.IntToCurrency().Jiao.Value.ToString()), Base64.EncodeBase64(parameter.IntToCurrency().Fen.Value.ToString()));
						break;

					case CurrencyOperationUnitType.Jiao:
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.JIAO_UNIT));
						answer.AppendFormat("{0}", Base64.EncodeBase64(parameter.IntToCurrency().Jiao.Value.ToString()));
						break;

					case CurrencyOperationUnitType.YF:
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.YUAN_UNIT));
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.FEN_UNIT));
						answer.AppendFormat("{0};{1}", Base64.EncodeBase64(parameter.IntToCurrency().Yuan.Value.ToString()), Base64.EncodeBase64(parameter.IntToCurrency().Fen.Value.ToString()));
						break;

					case CurrencyOperationUnitType.YJ:
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.YUAN_UNIT));
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.JIAO_UNIT));
						answer.AppendFormat("{0};{1}", Base64.EncodeBase64(parameter.IntToCurrency().Yuan.Value.ToString()), Base64.EncodeBase64(parameter.IntToCurrency().Jiao.Value.ToString()));
						break;

					case CurrencyOperationUnitType.YJF:
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.YUAN_UNIT));
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.JIAO_UNIT));
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.FEN_UNIT));
						answer.AppendFormat("{0};{1};{2}", Base64.EncodeBase64(parameter.IntToCurrency().Yuan.Value.ToString()), Base64.EncodeBase64(parameter.IntToCurrency().Jiao.Value.ToString()), Base64.EncodeBase64(parameter.IntToCurrency().Fen.Value.ToString()));
						break;

					case CurrencyOperationUnitType.Yuan:
						html.AppendLine(string.Format(INPUT_HTML_FORMAT, parentIndex, index++));
						html.AppendLine(string.Format(LABEL_HTML_FORMAT, Consts.YUAN_UNIT));
						answer.AppendFormat("{0}", Base64.EncodeBase64(parameter.IntToCurrency().Yuan.Value.ToString()));
						break;
				}
				// 題型答案
				html.AppendLine(string.Format("<input id=\"hidCo{0}\" type=\"hidden\" value=\"{1}\"/>", parentIndex, answer.ToString()));
			}
			else
			{
				html.AppendLine(string.Format(LABEL_HTML_FORMAT, parameter.IntToCurrency().CurrencyToString()));
			}
			return html.ToString();
		}
	}
}