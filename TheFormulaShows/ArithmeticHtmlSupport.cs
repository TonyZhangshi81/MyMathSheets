using ComputationalStrategy.Item;
using System.Collections.Generic;
using System.Text;

namespace TheFormulaShows
{
	/// <summary>
	/// 
	/// </summary>
	public class ArithmeticHtmlSupport : MakeHtmlBase<List<Formula>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="sign"></param>
		/// <param name="questionType"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
		public ArithmeticHtmlSupport(FourOperationsType fourOperationsType, SignOfOperation sign, QuestionType questionType, int maximumLimit, int numberOfQuestions) 
			: base(fourOperationsType, sign, questionType, maximumLimit, numberOfQuestions)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="signs"></param>
		/// <param name="questionType"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
		public ArithmeticHtmlSupport(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, QuestionType questionType, int maximumLimit, int numberOfQuestions) 
			: base(fourOperationsType, signs, questionType, maximumLimit, numberOfQuestions)
		{
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string MakeHtml()
		{
			if (_formulas.Count == 0)
			{
				return "<BR/>";
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;

			var controlIndex = 0;
			var html = new StringBuilder();
			var rowHtml = new StringBuilder();
			var colHtml = new StringBuilder();
			foreach (var item in _formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-3 form-inline\">");
				colHtml.AppendLine("<h5>");
				colHtml.AppendLine(this.GetHtml(item.Gap, item.LeftParameter, GapFilling.Left, controlIndex));
				colHtml.AppendLine(string.Format("<span class=\"label\">{0}</span>", this.GetOperation(item.Sign)));
				colHtml.AppendLine(this.GetHtml(item.Gap, item.RightParameter, GapFilling.Right, controlIndex));
				colHtml.AppendLine("<span class=\"label\">=</span>");
				colHtml.AppendLine(this.GetHtml(item.Gap, item.Answer, GapFilling.Answer, controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgOK{0}\" src=\"../Content/image/icon_52.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgNo{0}\" src=\"../Content/image/delete.png\" style=\"width: 40px; height: 40px; display: none; \" />", controlIndex));
				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("</div>");

				controlIndex++;
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		private string GetOperation(SignOfOperation operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfOperation.Plus:
					flag = "+";
					break;
				case SignOfOperation.Subtraction:
					flag = "-";
					break;
				case SignOfOperation.Division:
					flag = "÷";
					break;
				case SignOfOperation.Multiple:
					flag = "×";
					break;
			}
			return flag;
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
				html += string.Format("<input id=\"input{0}\" type = \"text\" placeholder=\" ?? \" class=\"form-control\" style=\"width: 50px; text-align:center;\" disabled=\"disabled\" />", index);
				html += string.Format("<input id=\"hidden{0}\" type=\"hidden\" value=\"{1}\"/>", index, parameter);
			}
			else
			{
				html = string.Format("<span class=\"label\">{0}</span>", parameter);
			}
			return html;
		}
	}
}
