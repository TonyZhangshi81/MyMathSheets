using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CurrencyLinkage.Main.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.CurrencyLinkage.Support
{
	/// <summary>
	/// 
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.CurrencyLinkage)]
	[Substitute("<!--CURRENCYLINKAGESTYLESHEET-->", "<link href=\"../Content/CurrencyLinkage.css\" rel=\"stylesheet\" type=\"text/css\" />")]
	[Substitute("<!--CURRENCYLINKAGESCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.CurrencyLinkage.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--CURRENCYLINKAGEREADY-->", "MathSheets.CurrencyLinkage.ready();")]
	[Substitute("//<!--CURRENCYLINKAGEMAKECORRECTIONS-->", "fault += MathSheets.CurrencyLinkage.makeCorrections();")]
	[Substitute("//<!--CURRENCYLINKAGETHEIRPAPERS-->", "MathSheets.CurrencyLinkage.theirPapers();")]
	[Substitute("//<!--CURRENCYLINKAGEPRINTSETTING-->", "MathSheets.CurrencyLinkage.printSetting();")]
	[Substitute("//<!--CURRENCYLINKAGEPRINTAFTERSETTING-->", "MathSheets.CurrencyLinkage.printAfterSetting();")]
	public class CurrencyLinkageHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 左側計算式坐標列表（限定5個坐標）
		/// </summary>
		private readonly Dictionary<DivQueueType, List<string>> LeftFormulasArray;
		/// <summary>
		/// 右側計算式坐標列表（限定5個坐標）
		/// </summary>
		private readonly Dictionary<DivQueueType, List<string>> RightFormulasArray;

		/// <summary>
		/// 構造體
		/// </summary>
		public CurrencyLinkageHtmlSupport()
		{
			LeftFormulasArray = new Dictionary<DivQueueType, List<string>>();
			// 左側計算式坐標列表(縱向連線)
			LeftFormulasArray[DivQueueType.Lengthways] = new List<string>()
			{
				"left: 30px; top:10px;",
				"left: 30px; top:70px;",
				"left: 30px; top:130px;",
				"left: 30px; top:190px;",
				"left: 30px; top:250px;"
			};
			// 上位計算式坐標列表(橫向連線)
			LeftFormulasArray[DivQueueType.Crosswise] = new List<string>()
			{
				"left: 30px; top:10px;",
				"left: 160px; top:10px;",
				"left: 290px; top:10px;",
				"left: 420px; top:10px;",
				"left: 550px; top:10px;"
			};

			RightFormulasArray = new Dictionary<DivQueueType, List<string>>();
			// 右側計算式坐標列表(縱向連線)
			RightFormulasArray[DivQueueType.Lengthways] = new List<string>()
			{
				"left: 250px; top:10px;",
				"left: 250px; top:70px;",
				"left: 250px; top:130px;",
				"left: 250px; top:190px;",
				"left: 250px; top:250px;"
			};
			// 下位計算式坐標列表(橫向連線)
			RightFormulasArray[DivQueueType.Crosswise] = new List<string>()
			{
				"left: 30px; top:150px;",
				"left: 160px; top:150px;",
				"left: 290px; top:150px;",
				"left: 420px; top:150px;",
				"left: 550px; top:150px;"
			};

			_answerString = new StringBuilder();
			_hidCurrencyInitSettings = new StringBuilder();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			CurrencyLinkageParameter p = parameter as CurrencyLinkageParameter;

			if (p.Formulas.LeftFormulas.Count == 0)
			{
				return string.Empty;
			}

			StringBuilder html = new StringBuilder();

			html.AppendLine(string.Format("<div class=\"row text-center row-margin-top {0}\">", p.QueueType == DivQueueType.Lengthways ? "drawLine-panel-currency-lengthways" : "drawLine-panel-currency-crosswise"));
			html.Append(GetSvgHtml(p));
			html.Append(GetLeftFormulasHtml(p));
			html.Append(GetRightFormulasHtml(p));
			html.Append(GetInitSettingsHtml(p));
			html.AppendLine("</div>");
			html.AppendLine(string.Format("<img id=\"imgOKCurrencyLinkage\" src=\"../Content/image/correct.png\" class=\"{0}\" style=\"display: none; \" />", p.QueueType == DivQueueType.Lengthways ? "OKCurrencyLinkage-lengthways" : "OKCurrencyLinkage-crosswise"));
			html.AppendLine(string.Format("<img id=\"imgNoCurrencyLinkage\" src=\"../Content/image/fault.png\" class=\"{0}\" style=\"display: none; \" />", p.QueueType == DivQueueType.Lengthways ? "NoCurrencyLinkage-lengthways" : "NoCurrencyLinkage-crosswise"));

			html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">認識價格</span></h4></div><hr />");

			return html.ToString();
		}

		/// <summary>
		/// 線型HTML模板
		/// </summary>
		private const string LINE_HTML_FORMAT = "<line id=\"lineCl{0}\" x1=\"0\" y1=\"0\" x2=\"0\" y2=\"0\" stroke=\"#ff006e\" stroke-width=\"1\" />";
		/// <summary>
		/// 起始點（結束點）DIV的HTML模板
		/// </summary>
		private const string DIVDRAWLINE_HTML_FORMAT = "<div class=\"divDrawLine-currency\" style=\"{0}\" id=\"divCl{1}{2}\">{3}</div>";
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
		private const string DIV_LINE_HTML_FORMAT = "<input type=\"hidden\" value=\"lineCl{0}\" />";
		/// <summary>
		/// 初期化參數HTML模板
		/// </summary>
		private const string INIT_SETTINGS_HTML_FORMAT = "<input type = \"hidden\" id=\"hidCurrencyInitSettings\" value=\"{0}\" />";
		/// <summary>
		/// 題型答案HTML模板
		/// </summary>
		private const string ANSWER_HTML_FORMAT = "<input type = \"hidden\" value=\"{0}\" id=\"hidClAnswer\" />";

		/// <summary>
		/// 右側計算式HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>右側計算式HTML</returns>
		private string GetRightFormulasHtml(CurrencyLinkageParameter p)
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
				html.AppendLine(string.Format(DIVDRAWLINE_HTML_FORMAT, RightFormulasArray[p.QueueType][controlIndex], controlIndex.ToString().PadLeft(2, '0'), "E", content.ToString()));

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
		private StringBuilder _hidCurrencyInitSettings { get; set; }

		/// <summary>
		/// 初期化參數HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>初期化參數HTML</returns>
		private string GetInitSettingsHtml(CurrencyLinkageParameter p)
		{
			StringBuilder html = new StringBuilder();

			if(p.QueueType == DivQueueType.Lengthways)
			{
				int divLastLastIndex = p.Formulas.RightFormulas.Count - 1;
				_hidCurrencyInitSettings.AppendFormat("#divCl00S,#divCl00E,#divCl{0}E,{1},#svgCl01", divLastLastIndex.ToString().PadLeft(2, '0'), (int)p.QueueType);
			}
			else
			{
				int divLastIndex = p.Formulas.LeftFormulas.Count - 1;
				_hidCurrencyInitSettings.AppendFormat("#divCl00S,#divCl{0}S,#divCl00E,{1},#svgCl01", divLastIndex.ToString().PadLeft(2, '0'), (int)p.QueueType);
			}

			html.AppendLine("<input type=\"hidden\" id=\"hidCurrencySelectedS\" />");
			html.AppendLine("<input type=\"hidden\" id=\"hidCurrencySelectedE\" />");
			html.AppendLine(string.Format(INIT_SETTINGS_HTML_FORMAT, _hidCurrencyInitSettings.ToString()));
			html.AppendLine(string.Format(ANSWER_HTML_FORMAT, _answerString.ToString()));

			return html.ToString();
		}

		/// <summary>
		/// 左側計算式HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>左側計算式HTML</returns>
		private string GetLeftFormulasHtml(CurrencyLinkageParameter p)
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
				html.AppendLine(string.Format(DIVDRAWLINE_HTML_FORMAT, LeftFormulasArray[p.QueueType][controlIndex], controlIndex.ToString().PadLeft(2, '0'), "S", content.ToString()));

				int seat = p.Formulas.Seats[controlIndex];
				_answerString.AppendFormat("divCl{0}S#divCl{1}E;", controlIndex.ToString().PadLeft(2, '0'), seat.ToString().PadLeft(2, '0'));

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
		private string GetSvgHtml(CurrencyLinkageParameter p)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();

			html.AppendLine("<svg id=\"svgCl01\" style=\"left: 0px; top: 0px;\" width=\"0\" height=\"0\">");
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
