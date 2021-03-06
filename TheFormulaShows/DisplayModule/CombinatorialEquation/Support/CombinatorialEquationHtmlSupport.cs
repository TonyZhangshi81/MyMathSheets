﻿using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Item;
using MyMathSheets.ComputationalStrategy.CombinatorialEquation.Main.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.CombinatorialEquation.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("CombinatorialEquation")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.CombinatorialEquation.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.CombinatorialEquation.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.CombinatorialEquation.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.CombinatorialEquation.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.CombinatorialEquation.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.CombinatorialEquation.printAfterSetting();")]
	public class CombinatorialEquationHtmlSupport : HtmlSupportBase<CombinatorialEquationParameter>
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 標籤HTML模板
		/// </summary>
		private const string SPAN_BADGE_HTML_FORMAT = "<span class=\"badge {0}\" style=\"width: 40px;\">{1}</span>";

		/// <summary>
		/// 算式組合HTML作成
		/// </summary>
		/// <param name="p">相關計算式</param>
		/// <returns>HTML語句</returns>
		public override string MakeHtmlContent(CombinatorialEquationParameter p)
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
			StringBuilder listGroupHtml = new StringBuilder();

			foreach (CombinatorialFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;

				listGroupHtml.AppendLine("<div class=\"col-md-4 form-inline\">");
				listGroupHtml.AppendLine("<ul class=\"list-group list-group-ext\">");
				listGroupHtml.AppendLine("<li class=\"list-group-item list-group-item-number\">");
				listGroupHtml.AppendLine("<h4>");
				listGroupHtml.AppendLine(string.Format(SPAN_BADGE_HTML_FORMAT, "badge-warning", item.ParameterA));
				listGroupHtml.AppendLine(string.Format(SPAN_BADGE_HTML_FORMAT, "badge-success", item.ParameterB));
				listGroupHtml.AppendLine(string.Format(SPAN_BADGE_HTML_FORMAT, "badge-primary", item.ParameterC));
				listGroupHtml.AppendLine(string.Format(SPAN_BADGE_HTML_FORMAT, "badge-info", item.ParameterD));
				listGroupHtml.AppendLine("</h4>");
				listGroupHtml.AppendLine(string.Format("<input id=\"hiddenCe{0}\" type=\"hidden\" value=\"{1}\" />", controlIndex, GetAnswer(item.CombinatorialFormulas)));
				listGroupHtml.AppendLine("</li>");
				listGroupHtml.AppendLine(GetHtml(item.CombinatorialFormulas, controlIndex));
				listGroupHtml.AppendLine("</ul>");
				listGroupHtml.AppendLine("<div class=\"divCorrectOrFault-3\">");
				listGroupHtml.AppendLine(string.Format("<img id=\"imgOKCombinatorial{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex));
				listGroupHtml.AppendLine(string.Format("<img id=\"imgNoCombinatorial{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex));
				listGroupHtml.AppendLine("</div>");
				listGroupHtml.AppendLine("</div>");

				controlIndex++;
				numberOfColumns++;
				if (numberOfColumns == 3)
				{
					rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
					rowHtml.Append(listGroupHtml.ToString());
					rowHtml.AppendLine("</div>");

					html.Append(rowHtml);

					isRowHtmlClosed = true;
					numberOfColumns = 0;
					rowHtml.Length = 0;
					listGroupHtml.Length = 0;
				}
			}

			if (!isRowHtmlClosed)
			{
				rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
				rowHtml.Append(listGroupHtml.ToString());
				rowHtml.AppendLine("</div>");

				html.Append(rowHtml);
			}

			if (html.Length != 0)
			{
				html.Insert(0, "<div class=\"div-page-content\">").AppendLine();
				html.AppendLine().Append("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "CombinatorialEquation", "算式組合"));
			}

			return html.ToString();
		}

		/// <summary>
		/// 一組計算式答題的HTML內容作成
		/// </summary>
		/// <param name="items">4個計算式</param>
		/// <param name="parentControlIndex">答題書索引號</param>
		/// <returns>HTML內容</returns>
		private string GetHtml(IList<Formula> items, int parentControlIndex)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			items.ToList().ForEach(d =>
			{
				html.AppendLine("<li class=\"list-group-item\">");
				html.AppendLine("<h4>");
				html.AppendLine(string.Format("<input id=\"inputCe{0}L{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, controlIndex));
				html.AppendLine(string.Format("<img src=\"../Content/image/help.png\" width=\"30\" height=\"30\" id=\"imgCe{0}S{1}\" title=\"help\" />", parentControlIndex, controlIndex));
				html.AppendLine(string.Format("<input id=\"inputCe{0}R{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, controlIndex));
				html.AppendLine("<img src=\"../Content/image/calculator.png\" style=\"width: 30px; height: 30px; \" />");
				html.AppendLine(string.Format("<input id=\"inputCe{0}A{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, controlIndex));
				html.AppendLine("</h4>");
				html.AppendLine("</li>");

				controlIndex++;
			});

			return html.ToString();
		}

		/// <summary>
		/// 編輯答案存放用於驗證答題
		/// </summary>
		/// <param name="items">4個計算式</param>
		/// <returns>各計算式拼接以逗號分隔形式的字符串</returns>
		private string GetAnswer(IList<Formula> items)
		{
			StringBuilder answer = new StringBuilder();
			items.ToList().ForEach(d =>
			{
				// 加密處理
				answer.AppendFormat("{0};", Base64.EncodeBase64(string.Format("{0}{1}{2}={3}", d.LeftParameter, d.Sign.ToOperationString(), d.RightParameter, d.Answer)));
			});
			answer.Length -= 1;
			return answer.ToString();
		}
	}
}