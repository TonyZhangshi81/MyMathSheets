using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Item;
using MyMathSheets.ComputationalStrategy.HowMuchMore.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.HowMuchMore.Support
{
	/// <summary>
	/// 比多少題型HTML支援類,動態作成html并按照一定的格式注入html模板中
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.HowMuchMore)]
	[Substitute(SubstituteType.Stylesheet, "<link href=\"../Content/HowMuchMore.css\" rel=\"stylesheet\" type=\"text/css\" />")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.HowMuchMore.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.HowMuchMore.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.HowMuchMore.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.HowMuchMore.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.HowMuchMore.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.HowMuchMore.printAfterSetting();")]
	public class HowMuchMoreHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">{1}</span></h4></div><hr />";

		/// <summary>
		/// 可選圖片列表
		/// </summary>
		private List<HowMuchMoreType> _moreTypeArray;
		/// <summary>
		/// 填空題中可以選擇的圖片集合
		/// </summary>
		private StringBuilder _gapFillingItems;

		/// <summary>
		/// 構造體
		/// </summary>
		public HowMuchMoreHtmlSupport()
		{
			// 可選圖片列表
			_moreTypeArray = new List<HowMuchMoreType>()
			{
				HowMuchMoreType.Circle,
				HowMuchMoreType.Diamond,
				HowMuchMoreType.Fish,
				HowMuchMoreType.HappyFace,
				HowMuchMoreType.Humburger,
				HowMuchMoreType.Like,
				HowMuchMoreType.Square
			};
		}

		/// <summary>
		/// 動態作成html并按照一定的格式注入html模板中
		/// </summary>
		/// <param name="parameter">通用參數類</param>
		/// <returns>html文言</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			// 比多少題型的參數類
			HowMuchMoreParameter p = parameter as HowMuchMoreParameter;

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
			_gapFillingItems = new StringBuilder();

			foreach (HowMuchMoreFormula item in p.Formulas)
			{
				// 隨機排序（個體顯示圖片是隨機的）
				_moreTypeArray = _moreTypeArray.OrderBy(x => Guid.NewGuid()).ToList();

				isRowHtmlClosed = false;

				listGroupHtml.AppendLine("<div class=\"col-md-6 form-inline\">");
				listGroupHtml.AppendLine("<ul class=\"list-group list-group-ext\">");
				listGroupHtml.AppendLine("<li class=\"list-group-item list-group-item-ext-1\">");
				listGroupHtml.AppendLine("<h4>");
				listGroupHtml.AppendLine(GetProblemHtml(item));
				listGroupHtml.AppendLine("</h4>");
				listGroupHtml.AppendLine("<div class=\"divCorrectOrFault-3\">");
				listGroupHtml.AppendLine(string.Format("<img id=\"imgOKHmm{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-3\" />", controlIndex));
				listGroupHtml.AppendLine(string.Format("<img id=\"imgNoHmm{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-3\" />", controlIndex));
				listGroupHtml.AppendLine("</div>");
				listGroupHtml.AppendLine("</li>");

				// 表示項目顯示
				listGroupHtml.AppendLine("<li class=\"list-group-item list-group-item-ext-2\">");
				listGroupHtml.AppendLine("<h4>");
				listGroupHtml.AppendLine(SetDisplayItem(item));
				listGroupHtml.AppendLine("</h4>");
				listGroupHtml.AppendLine("</li>");

				// 填空項目顯示
				listGroupHtml.AppendLine("<li class=\"list-group-item list-group-item-ext-3\">");
				listGroupHtml.AppendLine("<h4>");
				listGroupHtml.AppendLine(SetDisplayGapFillingItem(item, controlIndex));
				listGroupHtml.AppendLine("</h4>");
				listGroupHtml.AppendLine("</li>");

				// 結束符
				listGroupHtml.AppendLine("</ul>");
				listGroupHtml.AppendLine("</div>");

				// 答案項目
				listGroupHtml.AppendLine(string.Format("<input type=\"hidden\" id=\"hidHmmAnswer{0}\" value=\"{1}\" />", controlIndex, item.Answer));

				controlIndex++;
				numberOfColumns++;
				if (numberOfColumns == 2)
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
				StringBuilder head = new StringBuilder();
				// 題目標題顯示
				head.AppendLine(string.Format(PAGE_HEADER_HTML_FORMAT, LayoutSetting.Preview.HowMuchMore.ToString(), "比多少"));
				// 答案項目
				_gapFillingItems.Length -= 1;
				head.AppendLine(string.Format("<input type=\"hidden\" id=\"hidImgHmmHelpArray\" value=\"{0}\" />", _gapFillingItems.ToString()));

				html.Insert(0, head);
			}
			return html.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private string GetProblemHtml(HowMuchMoreFormula item)
		{
			StringBuilder html = new StringBuilder();

			// 如果條件顯示是“多”
			if (item.ChooseMore)
			{
				html.AppendLine(string.Format("<img src=\"../Content/image/more/{0}.png\" width=\"30\" height=\"30\" />", _moreTypeArray[0].ToString()));
				html.AppendLine("<span>比</span>");
				html.AppendLine(string.Format("<img src=\"../Content/image/more/{0}.png\" width=\"30\" height=\"30\" />", _moreTypeArray[1].ToString()));
				html.AppendLine("<span>多</span>");
			}
			else
			{
				html.AppendLine(string.Format("<img src=\"../Content/image/more/{0}.png\" width=\"30\" height=\"30\" />", _moreTypeArray[1].ToString()));
				html.AppendLine("<span>比</span>");
				html.AppendLine(string.Format("<img src=\"../Content/image/more/{0}.png\" width=\"30\" height=\"30\" />", _moreTypeArray[0].ToString()));
				html.AppendLine("<span>少</span>");
			}
			html.AppendLine(string.Format("<span>{0}個</span>", item.DefaultFormula.Answer));

			return html.ToString();
		}

		/// <summary>
		/// 填空項目顯示
		/// </summary>
		/// <param name="item">比多少算式</param>
		/// <param name="controlIndex">題目編號</param>
		/// <returns>HTML</returns>
		private string SetDisplayGapFillingItem(HowMuchMoreFormula item, int controlIndex)
		{
			StringBuilder html = new StringBuilder();

			// 填空項目個數（最多10個）
			var displayItemCount = 10;
			for (var index = 0; index < displayItemCount; index++)
			{
				// HTML作成
				html.AppendLine(string.Format("<img src=\"../Content/image/help.png\" id=\"imgHmmHelp{0}{1}\" width=\"30\" height=\"30\" title=\"help\" />", controlIndex, index));
			}

			// 填空題中可以選擇的圖片集合
			_gapFillingItems.AppendFormat("{0},", item.DisplayLeft ? _moreTypeArray[1].ToString() : _moreTypeArray[0].ToString());

			return html.ToString();
		}

		/// <summary>
		/// 已知項目的個數并表示
		/// </summary>
		/// <param name="item">比多少算式</param>
		/// <returns>HTML</returns>
		private string SetDisplayItem(HowMuchMoreFormula item)
		{
			StringBuilder html = new StringBuilder();
			// 顯示個數
			var displayItemCount = item.DisplayLeft ? item.DefaultFormula.LeftParameter : item.DefaultFormula.RightParameter;
			// 顯示項目（圖片顯示）
			var displayItem = item.DisplayLeft ? _moreTypeArray[0] : _moreTypeArray[1];
			for (var index = 0; index < displayItemCount; index++)
			{
				// HTML作成
				html.AppendLine(string.Format("<img src=\"../Content/image/more/{0}.png\" width=\"30\" height=\"30\" />", displayItem.ToString()));
			}

			return html.ToString();
		}

	}
}
