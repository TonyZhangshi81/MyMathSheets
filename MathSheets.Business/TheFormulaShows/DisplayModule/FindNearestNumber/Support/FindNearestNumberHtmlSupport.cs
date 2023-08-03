using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Item;
using MyMathSheets.ComputationalStrategy.FindNearestNumber.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.FindNearestNumber.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("FindNearestNumber")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.FindNearestNumber.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.FindNearestNumber.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.FindNearestNumber.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.FindNearestNumber.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.FindNearestNumber.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.FindNearestNumber.printAfterSetting();")]
	public class FindNearestNumberHtmlSupport : HtmlSupportBase<FindNearestNumberParameter>
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		public override string MakeHtmlContent(FindNearestNumberParameter p)
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
			foreach (NearestNumberFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-4 form-inline\">");
				colHtml.AppendLine("<h5>");
				colHtml.AppendLine(this.GetHtml(item.LeftFormula.Gap, item.LeftFormula.LeftParameter, GapFilling.Left, controlIndex));
				colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.LeftFormula.Sign.ToOperationUnicode()));
				colHtml.AppendLine(this.GetHtml(item.LeftFormula.Gap, item.LeftFormula.RightParameter, GapFilling.Right, controlIndex));
				colHtml.AppendLine(string.Format("<img src=\"../Content/image/{0}.png\" width=\"30\" height=\"30\" />", item.Answer.ToSignOfCompareEnString()));
				colHtml.AppendLine(this.GetHtml(item.RightFormula.Gap, item.RightFormula.LeftParameter, GapFilling.Left, controlIndex));
				colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.RightFormula.Sign.ToOperationUnicode()));
				colHtml.AppendLine(this.GetHtml(item.RightFormula.Gap, item.RightFormula.RightParameter, GapFilling.Right, controlIndex));

				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKFindNearestNumber{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgNoFindNearestNumber{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex));
				colHtml.AppendLine("</div>");
				colHtml.AppendLine("</div>");

				controlIndex++;
				numberOfColumns++;
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
				html.AppendLine().Append("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "FindNearestNumber", "找到最近的數字"));
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
				html += string.Format("<input id=\"inputFnn{0}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", index);
				html += string.Format("<input id=\"hiddenFnn{0}\" type=\"hidden\" value=\"{1}\"/>", index, Base64.EncodeBase64(parameter.ToString()));
			}
			else
			{
				html = string.Format("<span class=\"label\">{0}</span>", parameter);
			}
			return html;
		}
	}
}