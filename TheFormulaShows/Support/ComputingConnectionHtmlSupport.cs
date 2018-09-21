using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameter;
using MyMathSheets.TheFormulaShows.Attributes;
using System.Text;

namespace MyMathSheets.TheFormulaShows.Support
{
	/// <summary>
	/// 
	/// </summary>
	[Substitute("<!--COMPUTINGCONNECTIONSCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.ComputingConnection.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--COMPUTINGCONNECTIONREADY-->", "MathSheets.ComputingConnection.ready();")]
	[Substitute("//<!--COMPUTINGCONNECTIONMAKECORRECTIONS-->", "fault += MathSheets.ComputingConnection.makeCorrections();")]
	[Substitute("//<!--COMPUTINGCONNECTIONTHEIRPAPERS-->", "MathSheets.ComputingConnection.theirPapers();")]
	[Substitute("//<!--COMPUTINGCONNECTIONPRINTSETTING-->", "MathSheets.ComputingConnection.printSetting();")]
	[Substitute("//<!--COMPUTINGCONNECTIONPRINTAFTERSETTING-->", "MathSheets.ComputingConnection.printAfterSetting();")]
	public class ComputingConnectionHtmlSupport : IMakeHtml<ParameterBase>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		/// <returns></returns>
		public string MakeHtml(ParameterBase parameter)
		{
			ComputingConnectionParameter p = parameter as ComputingConnectionParameter;

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
					html.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.Sign.ToOperationString()));
					html.AppendLine(this.GetHtml(GapFilling.Right, item.RightParameter, GapFilling.Right, parentControlIndex, controlIndex));
					html.AppendLine("<span class=\"label\">=</span>");
					controlIndex++;

					if (items.ConfixNumber == controlIndex)
					{
						// 最后一层时需要将算式结果显示在页面上
						html.AppendLine(string.Format("<span class=\"label\">{0}</span>", item.Answer));
						html.AppendLine(string.Format("<img id=\"imgOKComputingConnection{0}\" src=\"../Content/image/correct.png\" style=\"width: 40px; height: 40px; display: none; \" />", parentControlIndex));
						html.AppendLine(string.Format("<img id=\"imgNoComputingConnection{0}\" src=\"../Content/image/fault.png\" style=\"width: 40px; height: 40px; display: none; \" />", parentControlIndex));
						html.AppendLine(string.Format("<input id=\"hiddenAllCc{0}\" type=\"hidden\" value=\"{1}\"/>", parentControlIndex, items.ConfixNumber));
					}
				}
				html.AppendLine("</h5>");
				html.AppendLine("</div>");
				html.AppendLine("</div>");

				parentControlIndex++;
				controlIndex = 0;
			}

			if (html.Length != 0)
			{
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">等式接龍</span></h4></div><hr />");
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
				html += string.Format("<input id=\"inputCc{0}{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />", pIndex, index);
				html += string.Format("<input id=\"hiddenCc{0}{1}\" type=\"hidden\" value=\"{2}\"/>", pIndex, index, parameter);
			}
			else
			{
				html = string.Format("<span class=\"label\">{0}</span>", parameter);
			}
			return html;
		}
	}
}
