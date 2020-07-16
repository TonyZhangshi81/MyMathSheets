using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CurrencyLinkage.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.CurrencyLinkage.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("CurrencyLinkage")]
	[Substitute(SubstituteType.Stylesheet, "<link href=\"../Content/CurrencyLinkage.css\" rel=\"stylesheet\" type=\"text/css\" />")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.CurrencyLinkage.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.CurrencyLinkage.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.CurrencyLinkage.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.CurrencyLinkage.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.CurrencyLinkage.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.CurrencyLinkage.printAfterSetting();")]
	public class CurrencyLinkageHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 商品圖片列表
		/// </summary>
		private readonly List<ShopType> ShopsArray;

		/// <summary>
		/// 左側坐標列表（限定5個坐標）
		/// </summary>
		private readonly Dictionary<DivQueueType, List<string>> LeftCurrencysArray;

		/// <summary>
		/// 右側坐標列表（限定5個坐標）
		/// </summary>
		private readonly Dictionary<DivQueueType, List<string>> RightCurrencysArray;

		/// <summary>
		/// 答案列表
		/// </summary>
		private readonly StringBuilder _answerString;

		/// <summary>
		/// 初期化參數
		/// </summary>
		private readonly StringBuilder _hidCurrencyInitSettings;

		/// <summary>
		/// 構造體
		/// </summary>
		public CurrencyLinkageHtmlSupport()
		{
			LeftCurrencysArray = new Dictionary<DivQueueType, List<string>>
			{
				// 左側計算式坐標列表(縱向連線)
				[DivQueueType.Lengthways] = new List<string>()
				{
					"left: 30px; top:10px;",
					"left: 30px; top:110px;",
					"left: 30px; top:210px;",
					"left: 30px; top:310px;",
					"left: 30px; top:410px;"
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

			RightCurrencysArray = new Dictionary<DivQueueType, List<string>>
			{
				// 右側計算式坐標列表(縱向連線)
				[DivQueueType.Lengthways] = new List<string>()
				{
					"left: 250px; top:10px;",
					"left: 250px; top:110px;",
					"left: 250px; top:210px;",
					"left: 250px; top:310px;",
					"left: 250px; top:410px;"
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

			// 商品圖片列表
			ShopsArray = new List<ShopType>()
			{
				ShopType.Mittens,
				ShopType.Book,
				ShopType.Christmas,
				ShopType.Hat,
				ShopType.Pencil,
				ShopType.Rubber,
				ShopType.RubiksCube,
				ShopType.Ruler,
				ShopType.Schoolbag,
				ShopType.Shirt,
				ShopType.Slipper,
				ShopType.Umbrella
			};
			// 隨機排序
			ShopsArray = ShopsArray.OrderBy(x => Guid.NewGuid()).ToList();

			_answerString = new StringBuilder();
			_hidCurrencyInitSettings = new StringBuilder();
		}

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		protected override string MakeHtmlStatement(TopicParameterBase parameter)
		{
			CurrencyLinkageParameter p = parameter as CurrencyLinkageParameter;

			if (p.Currencys.LeftCurrencys.Count == 0)
			{
				return string.Empty;
			}

			StringBuilder html = new StringBuilder();

			html.AppendLine(string.Format("<div class=\"row text-center row-margin-top {0}\">", p.QueueType == DivQueueType.Lengthways ? "drawLine-panel-currency-lengthways" : "drawLine-panel-currency-crosswise"));
			html.Append(GetSvgHtml(p));
			html.Append(GetLeftCurrencysHtml(p));
			html.Append(GetRightCurrencysHtml(p));
			html.Append(GetInitSettingsHtml(p));
			html.AppendLine("</div>");
			html.AppendLine(string.Format("<div class=\"{0}\">", p.QueueType == DivQueueType.Lengthways ? "divCorrectOrFault-lengthways" : "divCorrectOrFault-crosswise"));
			html.AppendLine(string.Format("<img id=\"imgOKCurrencyLinkage\" src=\"../Content/image/correct.png\" class=\"{0}\" />", p.QueueType == DivQueueType.Lengthways ? "OKCurrencyLinkage-lengthways" : "OKCurrencyLinkage-crosswise"));
			html.AppendLine(string.Format("<img id=\"imgNoCurrencyLinkage\" src=\"../Content/image/fault.png\" class=\"{0}\" />", p.QueueType == DivQueueType.Lengthways ? "NoCurrencyLinkage-lengthways" : "NoCurrencyLinkage-crosswise"));
			html.AppendLine("</div>");

			html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "CurrencyLinkage", "認識價格"));

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
		/// 右側(下面)HTML模板
		/// </summary>
		private const string RIGHT_CURRENCY_HTML_FORMAT = "<h6><span class=\"label\">{0}</span></h6>";

		/// <summary>
		/// 左側(上面)HTML模板
		/// </summary>
		private const string LEFT_CURRENCY_HTML_FORMAT = "<h6><span class=\"label\">{0}</span></h6>";

		/// <summary>
		/// 右側(下面)貨幣圖片
		/// </summary>
		private const string RIGHT_IMAGE_HTML = "<img src=\"../Content/image/shop/Money.png\" width=\"60\" height=\"60\" />";

		/// <summary>
		/// 左側(上面)隨機抽取商品圖片
		/// </summary>
		private const string LEFT_IMAGE_HTML_FORMAT = "<img src=\"../Content/image/shop/{0}.png\" width=\"60\" height=\"60\" />";

		/// <summary>
		/// 選擇控件HTML模板
		/// </summary>
		private const string CHECKBOX_HTML_FORMAT = "<input type=\"checkbox\" id=\"chkdivCl{0}{1}\" style=\"display: none;\" />";

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
		/// 右側(下面)貨幣HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>右側(下面)貨幣HTML</returns>
		private string GetRightCurrencysHtml(CurrencyLinkageParameter p)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder content = new StringBuilder();

			p.Currencys.Sort.ToList().ForEach(i =>
			{
				content.Length = 0;

				int currency = p.Currencys.RightCurrencys[i];

				// 右側(下面)貨幣圖片
				content.AppendLine(RIGHT_IMAGE_HTML);
				// 右側(下面)HTML模板
				content.AppendLine(string.Format(RIGHT_CURRENCY_HTML_FORMAT, currency.IntToCurrency().CurrencyToString()));
				// 選擇控件HTML模板
				content.AppendLine(string.Format(CHECKBOX_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0'), "E"));
				// 起始點（結束點）DIV的HTML模板
				html.AppendLine(string.Format(DIVDRAWLINE_HTML_FORMAT, RightCurrencysArray[p.QueueType][controlIndex], controlIndex.ToString().PadLeft(2, '0'), "E", content.ToString()));

				controlIndex++;
			});

			return html.ToString();
		}

		/// <summary>
		/// 初期化參數HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>初期化參數HTML</returns>
		private string GetInitSettingsHtml(CurrencyLinkageParameter p)
		{
			StringBuilder html = new StringBuilder();

			if (p.QueueType == DivQueueType.Lengthways)
			{
				int divLastLastIndex = p.Currencys.RightCurrencys.Count - 1;
				_hidCurrencyInitSettings.AppendFormat("#divCl00S,#divCl00E,#divCl{0}E,{1},#svgCl01", divLastLastIndex.ToString().PadLeft(2, '0'), (int)p.QueueType);
			}
			else
			{
				int divLastIndex = p.Currencys.LeftCurrencys.Count - 1;
				_hidCurrencyInitSettings.AppendFormat("#divCl00S,#divCl{0}S,#divCl00E,{1},#svgCl01", divLastIndex.ToString().PadLeft(2, '0'), (int)p.QueueType);
			}

			html.AppendLine("<input type=\"hidden\" id=\"hidCurrencySelectedS\" />");
			html.AppendLine("<input type=\"hidden\" id=\"hidCurrencySelectedE\" />");
			html.AppendLine(string.Format(INIT_SETTINGS_HTML_FORMAT, _hidCurrencyInitSettings.ToString()));
			html.AppendLine(string.Format(ANSWER_HTML_FORMAT, _answerString.ToString()));

			return html.ToString();
		}

		/// <summary>
		/// 左側(上面)HTML模板作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>左側計算式HTML</returns>
		private string GetLeftCurrencysHtml(CurrencyLinkageParameter p)
		{
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder content = new StringBuilder();

			p.Currencys.LeftCurrencys.ToList().ForEach(d =>
			{
				content.Length = 0;

				// 左側(上面)隨機商品圖片
				content.AppendLine(string.Format(LEFT_IMAGE_HTML_FORMAT, ShopsArray[controlIndex].ToString()));
				// 左側(上面)價格HTML模板
				content.AppendLine(string.Format(LEFT_CURRENCY_HTML_FORMAT, d.ToString("0.00")));
				// 選擇控件HTML模板
				content.AppendLine(string.Format(CHECKBOX_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0'), "S"));
				// 起始點（結束點）DIV的線型名稱模板
				content.AppendLine(string.Format(DIV_LINE_HTML_FORMAT, controlIndex.ToString().PadLeft(2, '0')));
				// 起始點（結束點）DIV的HTML模板
				html.AppendLine(string.Format(DIVDRAWLINE_HTML_FORMAT, LeftCurrencysArray[p.QueueType][controlIndex], controlIndex.ToString().PadLeft(2, '0'), "S", content.ToString()));

				int seat = p.Currencys.Seats[controlIndex];
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
			p.Currencys.LeftCurrencys.ToList().ForEach(d =>
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