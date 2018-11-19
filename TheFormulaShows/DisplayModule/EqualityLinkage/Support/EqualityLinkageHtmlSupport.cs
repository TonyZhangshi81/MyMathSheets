using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.EqualityLinkage.Main.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.EqualityLinkage.Support
{
	/// <summary>
	/// 
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.EqualityLinkage)]
	[Substitute("<!--EQUALITYLINKAGESCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.EqualityLinkage.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--EQUALITYLINKAGEREADY-->", "MathSheets.EqualityLinkage.ready();")]
	[Substitute("//<!--EQUALITYLINKAGEMAKECORRECTIONS-->", "fault += MathSheets.EqualityLinkage.makeCorrections();")]
	[Substitute("//<!--EQUALITYLINKAGETHEIRPAPERS-->", "MathSheets.EqualityLinkage.theirPapers();")]
	[Substitute("//<!--EQUALITYLINKAGEPRINTSETTING-->", "MathSheets.EqualityLinkage.printSetting();")]
	[Substitute("//<!--EQUALITYLINKAGEPRINTAFTERSETTING-->", "MathSheets.EqualityLinkage.printAfterSetting();")]
	public class EqualityLinkageHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 左側計算式坐標列表（限定5個坐標）
		/// </summary>
		private readonly List<string> LeftFormulasArray;
		/// <summary>
		/// 右側計算式坐標列表（限定5個坐標）
		/// </summary>
		private readonly List<string> RightFormulasArray;

		/// <summary>
		/// 構造體
		/// </summary>
		public EqualityLinkageHtmlSupport()
		{
			// 左側計算式坐標列表
			LeftFormulasArray = new List<string>()
			{
				"left: 30px; top:10px;",
				"left: 30px; top:70px;",
				"left: 30px; top:130px;",
				"left: 30px; top:190px;",
				"left: 30px; top:250px;"
			};
			// 右側計算式坐標列表
			RightFormulasArray = new List<string>()
			{
				"left: 250px; top:10px;",
				"left: 250px; top:70px;",
				"left: 250px; top:130px;",
				"left: 250px; top:190px;",
				"left: 250px; top:250px;"
			};

			_answerString = new StringBuilder();
			_hidInitSettings = new StringBuilder();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			EqualityLinkageParameter p = parameter as EqualityLinkageParameter;

			if (p.Formulas.LeftFormulas.Count == 0)
			{
				return string.Empty;
			}

			StringBuilder html = new StringBuilder();
			html.Append(GetSvgHtml(p));
			html.Append(GetLeftFormulasHtml(p));
			html.Append(GetRightFormulasHtml(p));
			html.Append(GetInitSettingsHtml(p));
			html.AppendLine("<img id=\"imgOKEqualityLinkage\" src=\"../Content/image/correct.png\" style=\"width: 60px; height: 60px; display: none; \" />");
			html.AppendLine("<img id=\"imgNoEqualityLinkage\" src=\"../Content/image/fault.png\" style=\"width: 60px; height: 60px; display: none; \" />");

			html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">算式連一連</span></h4></div><hr />");

			return string.Format(HTML_START, html.ToString());
		}

		/// <summary>
		/// 
		/// </summary>
		private const string HTML_START = "<div class=\"row text-center row-margin-top drawLine-panel\">{0}</div>";

		/// <summary>
		/// 線型HTML模板
		/// </summary>
		private const string LINE_HTML_FORMAT = "<line id=\"line{0}\" x1=\"0\" y1=\"0\" x2=\"0\" y2=\"0\" stroke=\"#ff006e\" stroke-width=\"1\" />";
		/// <summary>
		/// 起始點（結束點）DIV的HTML模板
		/// </summary>
		private const string DIVDRAWLINE_HTML_FORMAT = "<div class=\"divDrawLine\" style=\"{0}\" id=\"div{1}{2}\">{3}</div>";
		/// <summary>
		/// 算式HTML模板
		/// </summary>
		private const string FORMULA_HTML_FORMAT = "<h5><span class=\"label\">{0} {1} {2}</span></h5>";
		/// <summary>
		/// 選擇控件HTML模板
		/// </summary>
		private const string CHECKBOX_HTML_FORMAT = "<input type=\"checkbox\" id=\"chkdiv{0}{1}\" style=\"display: none;\" />";
		/// <summary>
		/// 起始點（結束點）DIV的線型名稱模板
		/// </summary>
		private const string DIV_LINE_HTML_FORMAT = "<input type=\"hidden\" value=\"line{0}\" />";
		/// <summary>
		/// 初期化參數HTML模板
		/// </summary>
		private const string INIT_SETTINGS_HTML_FORMAT = "<input type = \"hidden\" id=\"hidInitSettings\" value=\"{0}\" />";
		/// <summary>
		/// 題型答案HTML模板
		/// </summary>
		private const string ANSWER_HTML_FORMAT = "<input type = \"hidden\" value=\"{0}\" id=\"hidElAnswer\" />";

		/// <summary>
		/// 右側計算式HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>右側計算式HTML</returns>
		private string GetRightFormulasHtml(EqualityLinkageParameter p)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder content = new StringBuilder();

			p.Formulas.Sort.ToList().ForEach(i =>
			{
				content.Length = 0;

				Formula formula = p.Formulas.RightFormulas[i];

				// 算式HTML模板
				content.AppendLine(string.Format(FORMULA_HTML_FORMAT, formula.LeftParameter, formula.Sign.ToOperationString(), formula.RightParameter));
				// 選擇控件HTML模板
				content.AppendLine(string.Format(CHECKBOX_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0'), "E"));
				// 起始點（結束點）DIV的線型名稱模板
				content.AppendLine(string.Format(DIV_LINE_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0')));
				// 起始點（結束點）DIV的HTML模板
				html.AppendLine(string.Format(DIVDRAWLINE_HTML_FORMAT, LeftFormulasArray[controlIndex], controlIndex.ToString().PadLeft(2, '0'), "E", content.ToString()));

				controlIndex++;
			});

			return html.ToString();
		}

		/// <summary>
		/// 答案列表
		/// </summary>
		private StringBuilder _answerString { get; set; }
		/// <summary>
		/// 初期化參數
		/// </summary>
		private StringBuilder _hidInitSettings { get; set; }

		/// <summary>
		/// 初期化參數HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>初期化參數HTML</returns>
		private string GetInitSettingsHtml(EqualityLinkageParameter p)
		{
			StringBuilder html = new StringBuilder();

			_hidInitSettings.AppendFormat("#div01S,#div01E,#div{0}E,1,#svg01", p.Formulas.RightFormulas.Count.ToString().PadLeft(2, '0'));

			html.AppendLine("<input type=\"hidden\" id=\"hidSelectedS\" />");
			html.AppendLine("<input type=\"hidden\" id=\"hidSelectedE\" />");
			html.AppendLine(string.Format(INIT_SETTINGS_HTML_FORMAT, _hidInitSettings.ToString()));
			html.AppendLine(string.Format(ANSWER_HTML_FORMAT, _answerString.ToString()));

			return html.ToString();
		}

		/// <summary>
		/// 左側計算式HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>左側計算式HTML</returns>
		private string GetLeftFormulasHtml(EqualityLinkageParameter p)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder content = new StringBuilder();

			p.Formulas.LeftFormulas.ToList().ForEach(d =>
			{
				content.Length = 0;

				// 算式HTML模板
				content.AppendLine(string.Format(FORMULA_HTML_FORMAT, d.LeftParameter, d.Sign.ToOperationString(), d.RightParameter));
				// 選擇控件HTML模板
				content.AppendLine(string.Format(CHECKBOX_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0'), "S"));
				// 起始點（結束點）DIV的線型名稱模板
				content.AppendLine(string.Format(DIV_LINE_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0')));
				// 起始點（結束點）DIV的HTML模板
				html.AppendLine(string.Format(DIVDRAWLINE_HTML_FORMAT, LeftFormulasArray[controlIndex], controlIndex.ToString().PadLeft(2, '0'), "S", content.ToString()));

				int seat = p.Formulas.Seats[controlIndex];
				_answerString.AppendFormat("div{0}S#div{1}E;", controlIndex.ToString().PadLeft(2, '0'), seat.ToString().PadLeft(2, '0'));

				controlIndex++;
			});

			if (_answerString.Length > 0)
			{
				_answerString.Length -= 1;
			}

			return html.ToString();
		}

		/// <summary>
		/// 線型HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>線型HTML</returns>
		private string GetSvgHtml(EqualityLinkageParameter p)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();

			html.AppendLine("<svg id=\"svg01\" style=\"left: 0px; top: 0px;\" width=\"0\" height=\"0\">");
			// 線型HTML
			p.Formulas.LeftFormulas.ToList().ForEach(d =>
			{
				html.AppendLine(string.Format(LINE_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0')));
				controlIndex++;
			});
			// 封閉
			html.AppendLine("</svg>");

			return html.ToString();
		}
	}
}
