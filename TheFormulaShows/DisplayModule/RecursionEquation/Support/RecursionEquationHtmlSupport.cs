using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.RecursionEquation.Item;
using MyMathSheets.ComputationalStrategy.RecursionEquation.Main.Parameters;
using MyMathSheets.ComputationalStrategy.RecursionEquation.Main.Strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace MyMathSheets.TheFormulaShows.RecursionEquation.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("RecursionEquation")]
	[Substitute(SubstituteType.Stylesheet, "<link href=\"../Content/RecursionEquation.css\" rel=\"stylesheet\" type=\"text/css\" />")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.RecursionEquation.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.RecursionEquation.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.RecursionEquation.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.RecursionEquation.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.RecursionEquation.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.RecursionEquation.printAfterSetting();")]
	public class RecursionEquationHtmlSupport : HtmlSupportBase<RecursionEquationParameter>
	{
		private int _parentIndex;

		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 答題結果項目HTML模板
		/// </summary>
		private const string ANSWER_HTML_FORMAT = "<input id=\"hiddenClc{0}\" type=\"hidden\" value=\"{1}\"/>";

		/// <summary>
		/// LABEL標籤HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 輸入項目HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputClc{0}L{1}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";

		/// <summary>
		/// 遞等式計算的HTML實現方法集合
		/// </summary>
		private readonly Dictionary<TopicType, Func<RecursionEquationFormula, string>> _calculations = new Dictionary<TopicType, Func<RecursionEquationFormula, string>>();

		/// <summary>
		/// <see cref="RecursionEquationHtmlSupport"/>構造函數
		/// </summary>
		[ImportingConstructor]
		public RecursionEquationHtmlSupport()
		{
			// 遞等式計算[A+B+C]
			_calculations[TopicType.CleverA] = CleverA;
		}

		/// <summary>
		/// 遞等式計算[A+B+C]
		/// </summary>
		/// <param name="formula">計算式</param>
		/// <returns></returns>
		private string CleverA(RecursionEquationFormula formula)
		{
			StringBuilder html = new StringBuilder();
			StringBuilder answer = new StringBuilder();

			return html.ToString();
		}

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		public override string MakeHtmlContent(RecursionEquationParameter p)
		{
			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;

			_parentIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();

			// 算式作成
			p.Formulas.ToList().ForEach(d =>
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-6 form-inline\">");
				colHtml.AppendLine("<h5>");

				if (_calculations.TryGetValue(d.Type, out Func<RecursionEquationFormula, string> calculation))
				{
					colHtml.AppendLine(calculation(d));
				}

				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKRecursionEquation{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", _parentIndex.ToString().PadLeft(2, '0')));
				colHtml.AppendLine(string.Format("<img id=\"imgNoRecursionEquation{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", _parentIndex.ToString().PadLeft(2, '0')));
				colHtml.AppendLine("</div>");
				colHtml.AppendLine("</div>");

				_parentIndex++;
				numberOfColumns++;

				// 單位行上顯示2道題目
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
			});

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
				html.AppendLine("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "RecursionEquation", "遞等式"));
			}

			return html.ToString();
		}

		/// <summary>
		/// 計算式HTML作成
		/// </summary>
		/// <param name="item">填空項目</param>
		/// <param name="parameter">計算式中的數值</param>
		/// <param name="gap">當前顯示項目所在計算式中的位置</param>
		/// <param name="parentIndex">控件索引號</param>
		/// <param name="controlIndex">子控件索引號</param>
		/// <returns>HTML模板信息</returns>
		private string GetHtml(GapFilling item, int parameter, GapFilling gap, int parentIndex, int controlIndex)
		{
			StringBuilder html = new StringBuilder();
			if (item == gap)
			{
				html.AppendFormat(INPUT_HTML_FORMAT, parentIndex.ToString().PadLeft(2, '0'), controlIndex.ToString());
			}
			else
			{
				html.AppendFormat(LABEL_HTML_FORMAT, parameter);
			}

			return html.ToString();
		}
	}
}