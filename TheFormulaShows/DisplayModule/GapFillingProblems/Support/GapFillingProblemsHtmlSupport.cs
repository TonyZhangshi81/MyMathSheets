using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Item;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Main.Parameters;
using System;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.GapFillingProblems.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("GapFillingProblems")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.GapFillingProblems.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.GapFillingProblems.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.GapFillingProblems.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.GapFillingProblems.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.GapFillingProblems.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.GapFillingProblems.printAfterSetting();")]
	public class GapFillingProblemsHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 輸入框HTML模板
		/// </summary>
		private const string INPUT1_HTML_FORMAT = "<input id=\"inputGfp{0}{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder\" disabled=\"disabled\" />";

		/// <summary>
		/// 輸入框HTML模板（計算式輸入框（200px））
		/// </summary>
		private const string INPUT3_HTML_FORMAT = "<input id=\"inputGfp{0}{1}\" type = \"text\" placeholder=\" ?? \" class=\"form-control input-addBorder-7\" disabled=\"disabled\" />";

		/// <summary>
		/// 文字表示HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 答題答案HTML模板
		/// </summary>
		private const string ANSWER_HIDDEN_HTML_FORMAT = "<input id=\"hiddenGfpAnswer{0}\" type=\"hidden\" value=\"{1}\" />";

		/// <summary>
		/// 圖片表示HTML模板
		/// </summary>
		private const string IMAGE_HTML_FORMAT = "<img id=\"imgGfp{0}{1}\" src=\"../Content/image/fill/{2}.png\" />";

		/// <summary>
		/// 換行HTML模板
		/// </summary>
		private const string BR_HTML = "<br /><span style=\"padding-left: 8px;\" />";

		/// <summary>
		/// 填空題內容作成
		/// </summary>
		/// <param name="item">題型參數對象</param>
		/// <param name="rowHtml">HTML輸出對象</param>
		/// <param name="parentControlIndex">控件ID</param>
		/// <param name="answer">題型答案</param>
		private void GapFillingProblemToHtml(GapFillingProblemsFormula item, StringBuilder rowHtml, int parentControlIndex, StringBuilder answer)
		{
			// 子控件ID
			int chindControlIndex = 0;
			// 答案參數索引號
			int answerIndex = 0;
			// 題型參數索引號
			int parameterIndex = 0;

			StringBuilder completed = new StringBuilder();
			// 填空題內容中的參數拼接
			item.GapFillingProblem.Split(new string[4] { "IPT1", "IPT5", "IMG1", "LFBR" }, StringSplitOptions.None).ToList().ForEach(d =>
			{
				// 文字描述部分
				rowHtml.AppendLine(string.Format(LABEL_HTML_FORMAT, d));
				// 已完成部分的內容
				completed.Append(d);

				if (completed.Length == item.GapFillingProblem.Length)
				{
					return;
				}

				// 判斷對象（固定4位長度）
				string judge = item.GapFillingProblem.Substring(completed.Length, 4);

				switch (judge)
				{
					case "IPT1":
						// 答題輸入框
						rowHtml.AppendLine(string.Format(INPUT1_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), chindControlIndex));
						// 答案項目加密處理
						answer.AppendFormat("{0};", Base64.EncodeBase64(item.Answers[answerIndex]));
						answerIndex++;
						break;

					case "IPT5":
						// 答題輸入框
						rowHtml.AppendLine(string.Format(INPUT3_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), chindControlIndex));
						// 答案項目加密處理
						answer.AppendFormat("{0};", Base64.EncodeBase64(item.Answers[answerIndex]));
						answerIndex++;
						break;

					case "IMG1":
						// 圖形內容
						rowHtml.AppendLine(string.Format(IMAGE_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), chindControlIndex, item.Parameters[parameterIndex]));
						parameterIndex++;
						break;

					case "LFBR":
						// 添加換行
						rowHtml.AppendLine(BR_HTML);
						break;
				}

				// 已完成部分的內容
				completed.Append(judge);

				chindControlIndex++;
			});
		}

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			GapFillingProblemsParameter p = parameter as GapFillingProblemsParameter;

			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int parentControlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			foreach (GapFillingProblemsFormula item in p.Formulas)
			{
				rowHtml.AppendLine("<div class=\"col-md-12 form-inline\">");
				rowHtml.AppendLine("<p class=\"text-info-ext text-left\">");
				// 題號
				rowHtml.AppendLine(string.Format("<span class=\"label label-default\">{0}. </span>", parentControlIndex + 1));

				StringBuilder answer = new StringBuilder();
				// 填空題內容中的參數拼接
				GapFillingProblemToHtml(item, rowHtml, parentControlIndex, answer);

				// 難度等級在3級以上（包含3級）的顯示星星
				if (item.Level >= 3)
				{
					for (int i = 1; i <= item.Level; i++)
					{
						rowHtml.AppendLine("<img src=\"../Content/image/bookmark.png\" class=\"imgBookmark\" />");
					}
				}

				// 答題答案
				answer.Length -= 1;
				rowHtml.AppendLine(string.Format(ANSWER_HIDDEN_HTML_FORMAT, parentControlIndex.ToString().PadLeft(2, '0'), answer));
				rowHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				rowHtml.AppendLine(string.Format("<img id=\"imgOKGapFillingProblems{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", parentControlIndex.ToString().PadLeft(2, '0')));
				rowHtml.AppendLine(string.Format("<img id=\"imgNoGapFillingProblems{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", parentControlIndex.ToString().PadLeft(2, '0')));
				rowHtml.AppendLine("</div>");
				rowHtml.AppendLine("</p>");
				rowHtml.AppendLine("</div>");

				parentControlIndex++;
			}

			if (p.Formulas.Count > 0)
			{
				html.AppendLine("<div class=\"row text-center row-margin-top\">");
				html.Append(rowHtml.ToString());
				html.AppendLine("</div>");
			}

			if (html.Length != 0)
			{
				html.Insert(0, "<div class=\"div-page-content\">").AppendLine();
				html.AppendLine().Append("</div>");
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "GapFillingProblems".ToString(), "基礎填空"));
			}

			return html.ToString();
		}
	}
}