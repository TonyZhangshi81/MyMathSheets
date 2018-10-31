using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Item;
using MyMathSheets.ComputationalStrategy.FindTheLaw.Main.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.FindTheLaw.Support
{
	/// <summary>
	/// 找規律題型HTML支援類
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.FindTheLaw)]
	[Substitute("<!--FINDTHELAWSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.FindTheLaw.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--FINDTHELAWREADY-->", "MathSheets.FindTheLaw.ready();")]
	[Substitute("//<!--FINDTHELAWMAKECORRECTIONS-->", "fault += MathSheets.FindTheLaw.makeCorrections();")]
	[Substitute("//<!--FINDTHELAWTHEIRPAPERS-->", "MathSheets.FindTheLaw.theirPapers();")]
	[Substitute("//<!--FINDTHELAWPRINTSETTING-->", "MathSheets.FindTheLaw.printSetting();")]
	[Substitute("//<!--FINDTHELAWPRINTAFTERSETTING-->", "MathSheets.FindTheLaw.printAfterSetting();")]
	public class FindTheLawHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>HTML模板信息</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			FindTheLawParameter p = parameter as FindTheLawParameter;

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

			foreach (FindTheLawFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;

				listGroupHtml.AppendLine("<div class=\"col-md-4 form-inline\">");
				listGroupHtml.AppendLine("<ul class=\"list-group list-group-ext\">");
				listGroupHtml.AppendLine("<li class=\"list-group-item\">");
				listGroupHtml.AppendLine("<h5>");
				listGroupHtml.AppendLine(GetHtml(item, controlIndex));
				listGroupHtml.AppendLine(string.Format("<img id=\"imgOKFindTheLaw{0}\" src=\"../Content/image/correct.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				listGroupHtml.AppendLine(string.Format("<img id=\"imgNoFindTheLaw{0}\" src=\"../Content/image/fault.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				listGroupHtml.AppendLine("</h5>");
				listGroupHtml.AppendLine(string.Format("<input id=\"hiddenFtl{0}\" type=\"hidden\" value=\"{1}\" />", controlIndex, GetAnswer(item.NumberList, item.RandomIndexList)));
				listGroupHtml.AppendLine("</li>");
				listGroupHtml.AppendLine("</ul>");
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
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">找規律</span></h4></div><hr />");
			}

			return html.ToString();
		}

		/// <summary>
		/// 一組計算式答題的HTML內容作成
		/// </summary>
		/// <param name="items">3個計算式</param>
		/// <param name="parentControlIndex">答題書索引號</param>
		/// <returns>HTML內容</returns>
		private string GetHtml(FindTheLawFormula items, int parentControlIndex)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();

			items.NumberList.ForEach(d =>
			{
				if (items.RandomIndexList.Any(_ => _ == controlIndex))
				{
					html.AppendLine(string.Format("<input id=\"inputFtl{0}L{1}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", parentControlIndex, controlIndex));
				}
				else
				{
					html.AppendLine(string.Format("<span class=\"badge badge-warning\" style=\"width: 30px;\">{0}</span>", d));
				}
				controlIndex++;
			});

			return html.ToString();
		}

		/// <summary>
		/// 編輯答案存放用於驗證答題
		/// </summary>
		/// <param name="numberList">自然數列表</param>
		/// <param name="randomIndexList">隨機項目編號(填空項目)</param>
		/// <returns>各計算式拼接以逗號分隔形式的字符串</returns>
		private string GetAnswer(List<int> numberList, List<int> randomIndexList)
		{
			StringBuilder answer = new StringBuilder();
			randomIndexList.ForEach(d =>
			{
				answer.AppendFormat("{0},", numberList[d]);
			});
			answer.Length -= 1;
			return answer.ToString();
		}
	}
}
