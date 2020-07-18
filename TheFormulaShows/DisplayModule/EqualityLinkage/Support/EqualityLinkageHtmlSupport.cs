using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.EqualityLinkage.Main.Parameters;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.EqualityLinkage.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("EqualityLinkage")]
	[Substitute(SubstituteType.Stylesheet, "<link href=\"../Content/EqualityLinkage.css\" rel=\"stylesheet\" type=\"text/css\" />")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.EqualityLinkage.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.EqualityLinkage.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.EqualityLinkage.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.EqualityLinkage.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.EqualityLinkage.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.EqualityLinkage.printAfterSetting();")]
	public class EqualityLinkageHtmlSupport : HtmlSupportBase<EqualityLinkageParameter>
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

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
		[ImportingConstructor]
		public EqualityLinkageHtmlSupport()
		{
			LeftFormulasArray = new Dictionary<DivQueueType, List<string>>
			{
				// 左側計算式坐標列表(縱向連線)
				[DivQueueType.Lengthways] = new List<string>()
				{
					"left: 30px; top:10px;",
					"left: 30px; top:70px;",
					"left: 30px; top:130px;",
					"left: 30px; top:190px;",
					"left: 30px; top:250px;"
				},
				// 上位計算式坐標列表(橫向連線)
				[DivQueueType.Crosswise] = new List<string>()
				{
					"left: 30px; top:10px;",
					"left: 160px; top:10px;",
					"left: 290px; top:10px;",
					"left: 420px; top:10px;",
					"left: 550px; top:10px;"
				}
			};

			RightFormulasArray = new Dictionary<DivQueueType, List<string>>
			{
				// 右側計算式坐標列表(縱向連線)
				[DivQueueType.Lengthways] = new List<string>()
				{
					"left: 250px; top:10px;",
					"left: 250px; top:70px;",
					"left: 250px; top:130px;",
					"left: 250px; top:190px;",
					"left: 250px; top:250px;"
				},
				// 下位計算式坐標列表(橫向連線)
				[DivQueueType.Crosswise] = new List<string>()
				{
					"left: 30px; top:150px;",
					"left: 160px; top:150px;",
					"left: 290px; top:150px;",
					"left: 420px; top:150px;",
					"left: 550px; top:150px;"
				}
			};

			_answerString = new StringBuilder();
			_hidInitSettings = new StringBuilder();
		}

		/// <summary>
		/// 題型HTML上下文作成並返回
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>題型HTML上下文內容</returns>
		public override string MakeHtmlContent(EqualityLinkageParameter p)
		{
			if (p.Formulas.LeftFormulas.Count == 0)
			{
				return string.Empty;
			}

			StringBuilder html = new StringBuilder();

			html.AppendLine(string.Format("<div class=\"row text-center row-margin-top {0}\">", p.QueueType == DivQueueType.Lengthways ? "drawLine-panel-lengthways" : "drawLine-panel-crosswise"));
			html.Append(GetSvgHtml(p));
			html.Append(GetLeftFormulasHtml(p));
			html.Append(GetRightFormulasHtml(p));
			html.Append(GetInitSettingsHtml(p));
			html.AppendLine("</div>");
			html.AppendLine(string.Format("<div class=\"{0}\">", p.QueueType == DivQueueType.Lengthways ? "divCorrectOrFault-lengthways" : "divCorrectOrFault-crosswise"));
			html.AppendLine(string.Format("<img id=\"imgOKEqualityLinkage\" src=\"../Content/image/correct.png\" class=\"{0}\" style=\"display: none; \" />", p.QueueType == DivQueueType.Lengthways ? "OKEqualityLinkage-lengthways" : "OKEqualityLinkage-crosswise"));
			html.AppendLine(string.Format("<img id=\"imgNoEqualityLinkage\" src=\"../Content/image/fault.png\" class=\"{0}\" style=\"display: none; \" />", p.QueueType == DivQueueType.Lengthways ? "NoEqualityLinkage-lengthways" : "NoEqualityLinkage-crosswise"));
			html.AppendLine("</div>");

			html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "EqualityLinkage", "算式連一連"));

			return html.ToString();
		}

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
		private const string FORMULA_HTML_FORMAT = "<span class=\"label\">{0} {1} {2}</span>";

		/// <summary>
		/// 算式圖標HTML
		/// </summary>
		private const string IMAGE_FORMULA_HTML = "<img src=\"../Content/image/project/Calculator.png\" width=\"16\" height=\"16\" />";

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

				// 開始
				content.AppendLine("<h5>");
				// 算式HTML模板
				content.AppendLine(string.Format(FORMULA_HTML_FORMAT, formula.LeftParameter, formula.Sign.ToOperationUnicode(), formula.RightParameter));
				// 閉合
				content.AppendLine("</h5>");

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
		private readonly StringBuilder _answerString;

		/// <summary>
		/// 初期化參數
		/// </summary>
		private readonly StringBuilder _hidInitSettings;

		/// <summary>
		/// 初期化參數HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>初期化參數HTML</returns>
		private string GetInitSettingsHtml(EqualityLinkageParameter p)
		{
			StringBuilder html = new StringBuilder();

			if (p.QueueType == DivQueueType.Lengthways)
			{
				int divLastLastIndex = p.Formulas.RightFormulas.Count - 1;
				_hidInitSettings.AppendFormat("#div00S,#div00E,#div{0}E,{1},#svg01", divLastLastIndex.ToString().PadLeft(2, '0'), (int)p.QueueType);
			}
			else
			{
				int divLastIndex = p.Formulas.LeftFormulas.Count - 1;
				_hidInitSettings.AppendFormat("#div00S,#div{0}S,#div00E,{1},#svg01", divLastIndex.ToString().PadLeft(2, '0'), (int)p.QueueType);
			}

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

				// 開始
				content.AppendLine("<h5>");
				// 算式圖標
				content.AppendLine(IMAGE_FORMULA_HTML);
				// 算式HTML模板
				content.AppendLine(string.Format(FORMULA_HTML_FORMAT, d.LeftParameter, d.Sign.ToOperationUnicode(), d.RightParameter));
				// 閉合
				content.AppendLine("</h5>");

				// 選擇控件HTML模板
				content.AppendLine(string.Format(CHECKBOX_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0'), "S"));
				// 起始點（結束點）DIV的線型名稱模板
				content.AppendLine(string.Format(DIV_LINE_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0')));
				// 起始點（結束點）DIV的HTML模板
				html.AppendLine(string.Format(DIVDRAWLINE_HTML_FORMAT, LeftFormulasArray[p.QueueType][controlIndex], controlIndex.ToString().PadLeft(2, '0'), "S", content.ToString()));

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

		/// <summary>
		/// 資源釋放
		/// </summary>
		protected override void DisposeManaged()
		{
			if (LeftFormulasArray != null)
			{
				LeftFormulasArray.Clear();
			}
			if (RightFormulasArray != null)
			{
				RightFormulasArray.Clear();
			}
		}
	}
}