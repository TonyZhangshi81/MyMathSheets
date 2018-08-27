using ComputationalStrategy.Item;
using ComputationalStrategy.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFormulaShows
{
	public class Arithmetic : MakeHtmlBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string MakeHtml()
		{
			if (this.FormulaList.Count == 0)
			{
				return "<BR/>";
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;

			var html = new StringBuilder();
			var rowHtml = new StringBuilder();
			var colHtml = new StringBuilder();
			foreach (var item in this.FormulaList)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-3 form-inline\">");
				colHtml.AppendLine("<h5>");
				colHtml.AppendLine(this.GetHtml(item.Gap, item.LeftParameter, GapFilling.Left));
				colHtml.AppendLine("<span class=\"label\">+</span>");
				colHtml.AppendLine(this.GetHtml(item.Gap, item.RightParameter, GapFilling.Right));
				colHtml.AppendLine("<span class=\"label\">=</span>");
				colHtml.AppendLine(this.GetHtml(item.Gap, item.Answer, GapFilling.Answer));
				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("</div>");

				numberOfColumns++;
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

			return html.ToString();
		}

		private string GetHtml(GapFilling item, int parameter, GapFilling gap)
		{
			var html = string.Empty;
			if (item == gap)
			{
				html = "<input type = \"text\" placeholder=\" ?? \" class=\"form - control\" style=\"width: 50px; \" />";
			}
			else
			{
				html = string.Format("<span class=\"label\">{0}</span>", parameter);
			}
			return html;
		}
	}
}
