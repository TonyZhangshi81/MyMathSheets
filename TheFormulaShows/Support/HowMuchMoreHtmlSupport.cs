using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using MyMathSheets.TheFormulaShows.Attributes;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

namespace MyMathSheets.TheFormulaShows.Support
{
	/// <summary>
	/// 比多少題型HTML支援類,動態作成html并按照一定的格式注入html模板中
	/// </summary>
	[Substitute("<!--HOWMUCHMORESCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.HowMuchMore.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--HOWMUCHMOREREADY-->", "MathSheets.HowMuchMore.ready();")]
	[Substitute("//<!--HOWMUCHMOREMAKECORRECTIONS-->", "fault += MathSheets.HowMuchMore.makeCorrections();")]
	[Substitute("//<!--HOWMUCHMORETHEIRPAPERS-->", "MathSheets.HowMuchMore.theirPapers();")]
	[Substitute("//<!--HOWMUCHMOREPRINTSETTING-->", "MathSheets.HowMuchMore.printSetting();")]
	[Substitute("//<!--HOWMUCHMOREPRINTAFTERSETTING-->", "MathSheets.HowMuchMore.printAfterSetting();")]
	public class HowMuchMoreHtmlSupport : IMakeHtml<ParameterBase>
	{
		/// <summary>
		/// 可選圖片列表
		/// </summary>
		private readonly List<HowMuchMoreType> MoreTypeArray;

		/// <summary>
		/// 構造體
		/// </summary>
		public HowMuchMoreHtmlSupport()
		{
			// 可選圖片列表
			MoreTypeArray = new List<HowMuchMoreType>()
			{
				HowMuchMoreType.Circle,
				HowMuchMoreType.Diamond,
				HowMuchMoreType.Fish,
				HowMuchMoreType.HappyFace,
				HowMuchMoreType.Humburger,
				HowMuchMoreType.Like,
				HowMuchMoreType.Square
			};

			// 隨機排序
			MoreTypeArray = MoreTypeArray.OrderBy(x => Guid.NewGuid()).ToList();
		}

		/// <summary>
		/// 動態作成html并按照一定的格式注入html模板中
		/// </summary>
		/// <param name="parameter">通用參數類</param>
		/// <returns>html文言</returns>
		public string MakeHtml(ParameterBase parameter)
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

			foreach (HowMuchMoreFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;

				listGroupHtml.AppendLine("<div class=\"col-md-6 form-inline\">");
				listGroupHtml.AppendLine("<ul class=\"list-group list-group-ext\">");
				listGroupHtml.AppendLine("<li class=\"list-group-item\">");
				listGroupHtml.AppendLine("<h4>");
				listGroupHtml.AppendLine(string.Format("<img src=\"../Content/image/more/{0}.png\" width=\"30\" height=\"30\" />", MoreTypeArray[0]));
				listGroupHtml.AppendLine("<span>比</span>");
				listGroupHtml.AppendLine(string.Format("<img src=\"../Content/image/more/{0}.png\" width=\"30\" height=\"30\" />", MoreTypeArray[1]));
				listGroupHtml.AppendLine(string.Format("<span>比</span>", item.LeftOrRightParameter == MuchMoreSideType.Left ? "多" : "少"));
				listGroupHtml.AppendLine(string.Format("<span>{0}個</span>", item.Answer));
				listGroupHtml.AppendLine(string.Format("<img id=\"imgOKHmm{0}\" src=\"../Content/image/correct.png\" style=\"width: 30px; height: 30px; display: none; \" />", controlIndex));
				listGroupHtml.AppendLine(string.Format("<img id=\"imgNOHmm{0}\" src=\"../Content/image/fault.png\" style=\"width: 30px; height: 30px; display: none; \" />", controlIndex));
				listGroupHtml.AppendLine("</h4>");

				listGroupHtml.AppendLine("<li class=\"list-group-item\">");
				listGroupHtml.AppendLine("<h4>");

				listGroupHtml.AppendLine("</h4>");

				controlIndex++;


			}

			if (html.Length != 0)
			{
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">比多少</span></h4></div><hr />");
			}
			return html.ToString();
		}

	}
}
