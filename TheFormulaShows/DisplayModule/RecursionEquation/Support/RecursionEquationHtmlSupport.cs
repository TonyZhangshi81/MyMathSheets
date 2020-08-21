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
using System.Text;

namespace MyMathSheets.TheFormulaShows.RecursionEquation.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("RecursionEquation")]
	[Substitute(SubstituteType.Stylesheet, "<link href=\"../Content/css/RecursionEquation.css\" rel=\"stylesheet\" type=\"text/css\" />")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.RecursionEquation.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.RecursionEquation.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.RecursionEquation.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.RecursionEquation.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.RecursionEquation.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.RecursionEquation.printAfterSetting();")]
	public class RecursionEquationHtmlSupport : HtmlSupportBase<RecursionEquationParameter>
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 答題結果項目HTML模板
		/// </summary>
		private const string ANSWER_HTML_FORMAT = "<input type=\"hidden\" id=\"hiddenRe{0}{1}\" value=\"{2}\" />";

		/// <summary>
		/// LABEL標籤HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 輸入域DIV標籤HTML模板
		/// </summary>
		private const string DIV_INPUTAREA_HTML_FORMAT = "<div class=\"col-md-12\" style=\"height: 120px\">{0}</div>";

		/// <summary>
		/// 輸入項目HTML模板
		/// </summary>
		private const string INPUT_HTML_FORMAT = "<input id=\"inputRe{0}{1}{2}\" type=\"text\" placeholder=\" ?? \" class=\"re form-control input-addBorder-max mt-1\" disabled=\"disabled\" onblur=\"if(MathSheets.RecursionEquation.calcInputContent(this) == -1) $(this).focus();\" />";

		/// <summary>
		/// 折疊面板HTML模板
		/// </summary>
		private const string DIV_HTML_ACCORDION_FORMAT = "<div id=\"divAccordion{0}\" class=\"easyui-accordion\" style=\"width:85%;\">{1}</div>";

		/// <summary>
		/// 折疊面板標題HTML模板
		/// </summary>
		private const string DIV_HTML_ACCORDION_TITLE_FORMAT = "<h3 class=\"re ui-accordion-header-ext\">{0}</h3>";

		/// <summary>
		/// 遞等式計算的HTML實現方法集合
		/// </summary>
		private readonly Dictionary<TopicType, Func<RecursionEquationFormula, string>> _calculations = new Dictionary<TopicType, Func<RecursionEquationFormula, string>>();

		/// <summary>
		/// <see cref="RecursionEquationHtmlSupport"/> 構造函數
		/// </summary>
		[ImportingConstructor]
		public RecursionEquationHtmlSupport()
		{
			// 遞等式計算[A+B+C]
			_calculations[TopicType.CleverA] = CleverA;
			// 遞等式計算[A-(B-C)]
			_calculations[TopicType.CleverB] = CleverB;
			// 遞等式計算[A-(B+C)]
			_calculations[TopicType.CleverC] = CleverC;
			// 遞等式計算[A+(B-C)]
			_calculations[TopicType.CleverD] = CleverD;
			// 遞等式計算[A+(B+C)]
			_calculations[TopicType.CleverE] = CleverE;
			// 遞等式計算[A+(B-C)]
			_calculations[TopicType.CleverF] = CleverF;
			// 遞等式計算[A-B+C]
			_calculations[TopicType.CleverG] = CleverG;
			// 遞等式計算[A-B-C]
			_calculations[TopicType.CleverH] = CleverH;
		}

		/// <summary>
		/// 遞等式計算[A+B+C]
		/// </summary>
		/// <param name="formula"> 計算式 </param>
		/// <returns> </returns>
		private string CleverA(RecursionEquationFormula formula)
		{
			StringBuilder html = new StringBuilder();

			html.AppendFormat("{0} {1} {2} {3} {4}",
								formula.ConfixFormulas[0].LeftParameter,
								formula.ConfixFormulas[0].Sign.ToOperationUnicode(),
								formula.ConfixFormulas[0].RightParameter,
								formula.ConfixFormulas[1].Sign.ToOperationUnicode(),
								formula.ConfixFormulas[1].RightParameter);

			return html.ToString();
		}

		/// <summary>
		/// 遞等式計算[A-(B-C)]
		/// </summary>
		/// <param name="formula"> 計算式 </param>
		/// <returns> </returns>
		private string CleverB(RecursionEquationFormula formula)
		{
			StringBuilder html = new StringBuilder();

			html.AppendFormat("{0} {1} ({2} {3} {4})",
								formula.ConfixFormulas[1].LeftParameter,
								formula.ConfixFormulas[1].Sign.ToOperationUnicode(),
								formula.ConfixFormulas[0].LeftParameter,
								formula.ConfixFormulas[0].Sign.ToOperationUnicode(),
								formula.ConfixFormulas[0].RightParameter);

			return html.ToString();
		}

		/// <summary>
		/// 遞等式計算[A-(B+C)]
		/// </summary>
		/// <param name="formula"> 計算式 </param>
		/// <returns> </returns>
		private string CleverC(RecursionEquationFormula formula)
		{
			return CleverB(formula);
		}

		/// <summary>
		/// 遞等式計算[A+(B-C)]
		/// </summary>
		/// <param name="formula"> 計算式 </param>
		/// <returns> </returns>
		private string CleverD(RecursionEquationFormula formula)
		{
			return CleverB(formula);
		}

		/// <summary>
		/// 遞等式計算[A+(B+C)]
		/// </summary>
		/// <param name="formula"> 計算式 </param>
		/// <returns> </returns>
		private string CleverE(RecursionEquationFormula formula)
		{
			return CleverB(formula);
		}

		/// <summary>
		/// 遞等式計算[A+(B-C)]
		/// </summary>
		/// <param name="formula"> 計算式 </param>
		/// <returns> </returns>
		private string CleverF(RecursionEquationFormula formula)
		{
			return CleverA(formula);
		}

		/// <summary>
		/// 遞等式計算[A-B+C]
		/// </summary>
		/// <param name="formula"> 計算式 </param>
		/// <returns> </returns>
		private string CleverG(RecursionEquationFormula formula)
		{
			return CleverA(formula);
		}

		/// <summary>
		/// 遞等式計算[A-B-C]
		/// </summary>
		/// <param name="formula"> 計算式 </param>
		/// <returns> </returns>
		private string CleverH(RecursionEquationFormula formula)
		{
			return CleverA(formula);
		}

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="p"> 題型參數 </param>
		/// <returns> 題型HTML模板信息 </returns>
		public override string MakeHtmlContent(RecursionEquationParameter p)
		{
			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			// 單雙列號
			int numberOfColumns = 1;

			StringBuilder html = new StringBuilder();
			int controlIndex1 = 1;
			StringBuilder accordion1Html = new StringBuilder();
			int controlIndex2 = 1;
			StringBuilder accordion2Html = new StringBuilder();

			// 算式作成
			p.Formulas.ToList().ForEach(d =>
			{
				_calculations.TryGetValue(d.Type, out Func<RecursionEquationFormula, string> calculation);

				if (numberOfColumns == 1)
				{
					accordion1Html.AppendFormat(DIV_HTML_ACCORDION_TITLE_FORMAT, calculation(d)).AppendLine();
					accordion1Html.AppendLine(GetInputAreaHtml(numberOfColumns, controlIndex1, d.Answer[0]));

					numberOfColumns = 2;
					controlIndex1++;
				}
				else
				{
					accordion2Html.AppendFormat(DIV_HTML_ACCORDION_TITLE_FORMAT, calculation(d)).AppendLine();
					accordion2Html.AppendLine(GetInputAreaHtml(numberOfColumns, controlIndex2, d.Answer[0]));

					numberOfColumns = 1;
					controlIndex2++;
				}
			});

			html.AppendLine("<div class=\"row text-center row-margin-top align-items-start\">");
			html.AppendLine("<div class=\"col-md-6 form-inline\">");
			// 折疊面板1
			html.AppendFormat(DIV_HTML_ACCORDION_FORMAT, 1, accordion1Html).AppendLine();
			html.AppendLine("</div>");
			html.AppendLine("<div class=\"col-md-6 form-inline\">");
			// 折疊面板2
			html.AppendFormat(DIV_HTML_ACCORDION_FORMAT, 2, accordion2Html).AppendLine();
			html.AppendLine("</div>");
			html.AppendLine("</div>");

			if (html.Length != 0)
			{
				html.Insert(0, "<div class=\"div-page-content\">").AppendLine();
				html.AppendLine("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "RecursionEquation", "遞等式計算"));
			}

			return html.ToString();
		}

		/// <summary>
		/// </summary>
		/// <param name="numberOfColumns"> </param>
		/// <param name="controlIndex"> </param>
		/// <param name="answer"> </param>
		/// <returns> </returns>
		private string GetInputAreaHtml(int numberOfColumns, int controlIndex, int answer)
		{
			StringBuilder html = new StringBuilder();

			var pControlId = controlIndex.ToString().PadLeft(2, '0');

			html.AppendFormat(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode());
			html.AppendLine(GetInputHtml(numberOfColumns, pControlId, 1));
			html.AppendFormat(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode());
			html.AppendLine(GetInputHtml(numberOfColumns, pControlId, 2));
			html.AppendFormat(LABEL_HTML_FORMAT, SignOfCompare.Equal.ToSignOfCompareUnicode());
			html.AppendLine(GetInputHtml(numberOfColumns, pControlId, 3));

			html.AppendFormat(ANSWER_HTML_FORMAT, numberOfColumns, pControlId, Base64.EncodeBase64(answer.ToString())).AppendLine();

			html.AppendLine("<div class=\"re divCorrectOrFault-1\">");
			html.AppendFormat("<img id=\"imgOKRecursionEquation{0}{1}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", numberOfColumns, pControlId).AppendLine();
			html.AppendFormat("<img id=\"imgNoRecursionEquation{0}{1}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", numberOfColumns, pControlId).AppendLine();
			html.AppendLine("</div>");

			var inputArea = string.Format(DIV_INPUTAREA_HTML_FORMAT, html.ToString());

			return inputArea;
		}

		/// <summary>
		/// </summary>
		/// <param name="numberOfColumns"> </param>
		/// <param name="pControlId"> </param>
		/// <param name="controlIndex"> </param>
		/// <returns> </returns>
		private string GetInputHtml(int numberOfColumns, string pControlId, int controlIndex)
		{
			StringBuilder html = new StringBuilder();

			html.AppendFormat(INPUT_HTML_FORMAT, numberOfColumns, pControlId, controlIndex);
			if (controlIndex != 3)
			{
				html.Append("<br />");
			}

			return html.ToString();
		}
	}
}