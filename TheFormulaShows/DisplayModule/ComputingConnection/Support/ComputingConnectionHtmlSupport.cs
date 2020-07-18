using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.ComputingConnection.Item;
using MyMathSheets.ComputationalStrategy.ComputingConnection.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.ComputingConnection.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("ComputingConnection")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.ComputingConnection.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.ComputingConnection.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.ComputingConnection.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.ComputingConnection.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.ComputingConnection.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.ComputingConnection.printAfterSetting();")]
	public class ComputingConnectionHtmlSupport : HtmlSupportBase<ComputingConnectionParameter>
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
		public override string MakeHtmlContent(ComputingConnectionParameter p)
		{
			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int controlIndex = 0;
			int parentControlIndex = 0;
			StringBuilder html = new StringBuilder();
			foreach (ConnectionFormula items in p.Formulas)
			{
				html.AppendLine("<div class=\"row text-center row-margin-top\">");
				html.AppendLine("<div class=\"col-md-10 form-inline\">");
				html.AppendLine("<h5>");

				foreach (Formula item in items.ConfixFormulas)
				{
					html.AppendLine(this.GetHtml(GapFilling.Left, item.LeftParameter, GapFilling.Right, parentControlIndex, controlIndex));
					html.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.Sign.ToOperationUnicode()));
					html.AppendLine(this.GetHtml(GapFilling.Right, item.RightParameter, GapFilling.Right, parentControlIndex, controlIndex));
					html.AppendLine("<span class=\"label\">=</span>");
					controlIndex++;

					if (items.ConfixNumber == controlIndex)
					{
						// 最后一层时需要将算式结果显示在页面上
						html.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.Answer));
						html.AppendLine("</h5>");
						html.AppendLine("<div class=\"divCorrectOrFault-1\">");
						html.AppendLine(string.Format("<img id=\"imgOKComputingConnection{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", parentControlIndex));
						html.AppendLine(string.Format("<img id=\"imgNoComputingConnection{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", parentControlIndex));
						html.AppendLine("</div>");
						html.AppendLine(string.Format("<input id=\"hiddenAllCc{0}\" type=\"hidden\" value=\"{1}\"/>", parentControlIndex, items.ConfixNumber));
					}
				}
				html.AppendLine("</div>");
				html.AppendLine("</div>");

				parentControlIndex++;
				controlIndex = 0;
			}

			if (html.Length != 0)
			{
				html.Insert(0, "<div class=\"div-page-content\">").AppendLine();
				html.AppendLine().Append("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "ComputingConnection", "等式接龍"));
			}
			return html.ToString();
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="item">当前处理项的位置</param>
		/// <param name="parameter">算是值</param>
		/// <param name="gap">填空项位置</param>
		/// <param name="pIndex">上一级控件索引</param>
		/// <param name="index">当前控件索引</param>
		/// <returns></returns>
		private string GetHtml(GapFilling item, int parameter, GapFilling gap, int pIndex, int index)
		{
			var html = string.Empty;
			if (item == gap)
			{
				html += string.Format("<input id=\"inputCc{0}{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", pIndex, index);
				html += string.Format("<input id=\"hiddenCc{0}{1}\" type=\"hidden\" value=\"{2}\"/>", pIndex, index, Base64.EncodeBase64(parameter.ToString()));
			}
			else
			{
				html = string.Format("<span class=\"label\">{0}</span>", parameter);
			}
			return html;
		}
	}
}