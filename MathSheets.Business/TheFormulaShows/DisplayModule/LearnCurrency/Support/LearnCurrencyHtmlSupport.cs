using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Item;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.LearnCurrency.Support
{
	/// <summary>
	/// 根據題型輸出結果作成HTML模板信息
	/// </summary>
	[HtmlSupport("LearnCurrency")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.LearnCurrency.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.LearnCurrency.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.LearnCurrency.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.LearnCurrency.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.LearnCurrency.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.LearnCurrency.printAfterSetting();")]
	public class LearnCurrencyHtmlSupport : HtmlSupportBase<LearnCurrencyParameter>
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 輸入項目HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputLc{0}{1}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder-2\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";

		/// <summary>
		/// 貨幣單位HTML模板
		/// </summary>
		private const string CURRENCY_UNIT_HTML_FORMAT = "<span class=\"label p-2\">{0}</span>";

		/// <summary>
		/// 符號HTML模板
		/// </summary>
		private const string SPAN_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 等號HTML模板
		/// </summary>
		private const string EQUALTO_HTML = "<span class=\"label\">&#61;</span>";

		/// <summary>
		/// 答案列表(eg: 1,5,12,10,4,8,1.....)
		/// </summary>
		private StringBuilder _answers;

		/// <summary>
		/// 根據題型輸出結果作成HTML模板信息
		/// </summary>
		/// <param name="p">題型輸出結果</param>
		/// <returns>HTML模板信息</returns>
		public override string MakeHtmlContent(LearnCurrencyParameter p)
		{
			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;
			_answers = new StringBuilder();

			// 輸入框控件ID
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();
			foreach (LearnCurrencyFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-6 form-inline\">");
				colHtml.AppendLine("<h5>");
				colHtml.Append(GetHtml(item, controlIndex));
				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKLearnCurrency{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgNoLearnCurrency{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex));
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
				// 題型標題
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "LearnCurrency", "認識貨幣"));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回元單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetYuanHtml(LearnCurrencyFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.CurrencyUnit.Yuan.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.YUAN_UNIT));
			}
			else
			{
				html.AppendFormat(SPAN_HTML_FORMAT, item.CurrencyUnit.Yuan).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.YUAN_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回角單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetJiaoHtml(LearnCurrencyFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.CurrencyUnit.Jiao.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.JIAO_UNIT));
			}
			else
			{
				html.AppendFormat(SPAN_HTML_FORMAT, item.CurrencyUnit.Jiao).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.JIAO_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回角單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetRemainderJiaoHtml(LearnCurrencyFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.RemainderJiao.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.JIAO_UNIT));
			}
			else
			{
				// 考慮剩餘角的輸出
				html.AppendFormat(SPAN_HTML_FORMAT, item.RemainderJiao.Value).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.JIAO_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回分單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetFenHtml(LearnCurrencyFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.CurrencyUnit.Fen.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.FEN_UNIT));
			}
			else
			{
				html.AppendFormat(SPAN_HTML_FORMAT, item.CurrencyUnit.Fen).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.FEN_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回分單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetRemainderFenHtml(LearnCurrencyFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.RemainderFen.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.FEN_UNIT));
			}
			else
			{
				// 考慮剩餘分的輸出
				html.AppendFormat(SPAN_HTML_FORMAT, item.RemainderFen.Value).AppendLine(string.Format(CURRENCY_UNIT_HTML_FORMAT, Consts.FEN_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 根據題型輸出結果作成HTML模板信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID</param>
		/// <returns>HTML模板信息</returns>
		private string GetHtml(LearnCurrencyFormula item, int controlIndex)
		{
			_answers.Length = 0;
			StringBuilder html = new StringBuilder();

			switch (item.CurrencyTransType)
			{
				// 元轉角
				case CurrencyTransformType.Y2J:
					html.Append(GetYuanHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetJiaoHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;
				// 元轉分
				case CurrencyTransformType.Y2F:
					html.Append(GetYuanHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetFenHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;
				// 角轉元
				case CurrencyTransformType.J2Y:
					html.Append(GetJiaoHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetYuanHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;
				// 角轉分
				case CurrencyTransformType.J2F:
					html.Append(GetJiaoHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetFenHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;
				// 角轉元分
				case CurrencyTransformType.J2YF:
					html.Append(GetJiaoHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetYuanHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetFenHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					break;
				// 分轉元
				case CurrencyTransformType.F2Y:
					html.Append(GetFenHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetYuanHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;
				// 分轉角
				case CurrencyTransformType.F2J:
					html.Append(GetFenHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetJiaoHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;
				// 分轉元角
				case CurrencyTransformType.F2YJ:
					html.Append(GetFenHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetYuanHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetJiaoHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					break;
				// 角轉元（有剩餘）
				case CurrencyTransformType.J2YExt:
					html.Append(GetJiaoHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetYuanHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetRemainderJiaoHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;
				// 分轉元角（有剩餘）
				case CurrencyTransformType.F2YJExt:
					html.Append(GetFenHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetYuanHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetJiaoHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					html.Append(GetRemainderFenHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S2"));
					break;
			}

			// 答案列表
			_answers.Length -= 1;
			html.AppendLine(string.Format("<input id=\"hidLcAnswer{0}\" type=\"hidden\" value=\"{1}\" />", controlIndex, _answers.ToString()));

			return html.ToString();
		}
	}
}