using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using MyMathSheets.TheFormulaShows.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.Support
{
	/// <summary>
	/// 
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.FruitsLinkage)]
	[Substitute("<!--FRUITSLINKAGESCRIPT-->", "<script src=\"../Scripts/Ext/MathSheets.FruitsLinkage.js\" charset=\"utf-8\"></script>")]
	[Substitute("//<!--FRUITSLINKAGEREADY-->", "__fruitsArrayHiddenControlId = 'hidFruitsArray';MathSheets.FruitsLinkage.ready('divFruitDrag', 'divContainer');")]
	[Substitute("//<!--FRUITSLINKAGEMAKECORRECTIONS-->", "fault += MathSheets.FruitsLinkage.makeCorrections();")]
	[Substitute("//<!--FRUITSLINKAGETHEIRPAPERS-->", "MathSheets.FruitsLinkage.theirPapers();")]
	public class FruitsLinkageHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 水果序號列表
		/// </summary>
		private readonly List<Fruits> FruitsArray;
		/// <summary>
		/// 可拖動水果的初期坐標集合
		/// </summary>
		private readonly List<Coordinate> CoordinateArray;
		/// <summary>
		/// 構造體
		/// </summary>
		public FruitsLinkageHtmlSupport()
		{
			// 水果圖片列表
			FruitsArray = new List<Fruits>()
			{
				Fruits.Apple,
				Fruits.Apricot,
				Fruits.Banana,
				Fruits.Cherry,
				Fruits.Grape,
				Fruits.Hamimelon,
				Fruits.Orange,
				Fruits.Peach,
				Fruits.Pear,
				Fruits.Strawberry,
				Fruits.Watermelon
			};

			// 可拖動水果圖片初期化坐標集合
			CoordinateArray = new List<Coordinate>()
			{
				new Coordinate(){ Left = 115, Top = 130 },
				new Coordinate(){ Left = 130, Top = 35 },
				new Coordinate(){ Left = 15, Top = 20 },
				new Coordinate(){ Left = 80, Top = 75 },
				new Coordinate(){ Left = 70, Top = 175 },
				new Coordinate(){ Left = 140, Top = 230 },
				new Coordinate(){ Left = 170, Top = 90 },
				new Coordinate(){ Left = 10, Top = 90 },
				new Coordinate(){ Left = 5, Top = 180 },
				new Coordinate(){ Left = 185, Top = 165 }
			};

			// 隨機排序
			FruitsArray = FruitsArray.OrderBy(x => Guid.NewGuid()).ToList();
			CoordinateArray = CoordinateArray.OrderBy(x => Guid.NewGuid()).ToList();
		}
		/// <summary>
		/// 放置範圍上下限值設定
		/// </summary>
		/// <param name="seats">座位編號</param>
		/// <returns></returns>
		private string GetFruitsArray(IList<int> seats)
		{
			StringBuilder seatsList = new StringBuilder();
			seats.ToList().ForEach(d => seatsList.AppendFormat("{0},", d));
			seatsList.Length -= 1;

			return seatsList.ToString();
		}

		/// <summary>
		/// 水果連連看HTML作成
		/// </summary>
		/// <param name="parameter">相關計算式</param>
		/// <returns>HTML語句</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			FruitsLinkageParameter p = parameter as FruitsLinkageParameter;

			if (p.Formulas.FruitsFormulas.Count == 0)
			{
				return string.Empty;
			}

			StringBuilder html = new StringBuilder();
			StringBuilder fruitsFormulasLeftHtml = new StringBuilder();
			StringBuilder fruitsFormulasRightHtml = new StringBuilder();
			StringBuilder easyuiPanelHtml = new StringBuilder();
			StringBuilder divDragHtml = new StringBuilder();
			StringBuilder divContainerHtml = new StringBuilder();

			int index = 0;
			// 繪製水果算是區
			p.Formulas.FruitsFormulas.ToList().ForEach(d =>
			{
				if (index >= 5)
				{
					// 水果算式对半显示(以5件为界限)
					if (index == 5)
					{
						fruitsFormulasRightHtml.AppendLine("<div class=\"row text-center row-margin-top fruitsFormula-right\" >");
					}
					// 顯示水果算式Div區域(后5件)
					CreateFruitsFormulasHtml(fruitsFormulasRightHtml, d, index);
				}
				else
				{
					if (index == 0)
					{
						fruitsFormulasLeftHtml.AppendLine("<div class=\"row text-center row-margin-top fruitsFormula-left\" >");
					}
					// 顯示水果算式Div區域(前5件)
					CreateFruitsFormulasHtml(fruitsFormulasLeftHtml, d, index);
				}

				// 可拖動水果Div區域
				divDragHtml.AppendLine(string.Format("<div id=\"divFruitDrag{0}\" class=\"divFruitDrag\" style=\"position: absolute; left: {1}px; top: {2}px;\" data-options=\"onDrag:MathSheets.FruitsLinkage.onDrag, onStopDrag:MathSheets.FruitsLinkage.onStopDrag\">", index, CoordinateArray[index].Left, CoordinateArray[index].Top));
				divDragHtml.AppendLine(string.Format("<img src=\"../Content/image/fruits/{0}.png\" width=40 height=40>", FruitsArray[index].ToString()));
				divDragHtml.AppendLine(string.Format("<input id=\"divFruitDrag{0}Input\" type=\"hidden\" />", index));
				divDragHtml.AppendLine(string.Format("<input id=\"divFruitDrag{0}Result\" type=\"hidden\" value=\"ERROR\" />", index));
				divDragHtml.AppendLine("</div>");

				index++;
			});
			// Div闭合(件數一定會存在)
			fruitsFormulasLeftHtml.AppendLine("</div>");
			// 5件及以上的情况
			if (fruitsFormulasRightHtml.Length != 0)
			{
				// Div闭合
				fruitsFormulasRightHtml.AppendLine("</div>");
			}

			int seat = 0;
			p.Formulas.Sort.ToList().ForEach(d =>
			{
				// 容器對象
				Formula container = p.Formulas.ContainersFormulas[d];
				// 容器Div區域
				divContainerHtml.AppendLine(string.Format("<div id=\"divContainer{0}\" class=\"divContainer divSeat{1}\">", seat, seat));
				divContainerHtml.AppendLine("<h5>");
				divContainerHtml.AppendLine(string.Format("<span class=\"label\">{0}{1}{2}</span>", container.LeftParameter, container.Sign.ToOperationString(), container.RightParameter));
				divContainerHtml.AppendLine("<img src=\"../Content/image/table.png\" width=78 height=20 style=\"margin-top:35px;\">");
				divContainerHtml.AppendLine(string.Format("<img id=\"imgOKFruitsLinkage{0}\" src=\"../Content/image/correct.png\" style=\"width: 20px; height: 20px; display: none; \" />", seat));
				divContainerHtml.AppendLine(string.Format("<img id=\"imgNoFruitsLinkage{0}\" src=\"../Content/image/fault.png\" style=\"width: 20px; height: 20px; display: none; \" />", seat));
				divContainerHtml.AppendLine("</h5>");
				divContainerHtml.AppendLine("</div>");

				seat++;
			});

			// 圖片移動區域
			easyuiPanelHtml.AppendLine("<div class=\"easyui-panel easyui-panel-ext\" >");
			// 可移動水果
			easyuiPanelHtml.AppendLine(divDragHtml.ToString());
			// 容器
			easyuiPanelHtml.AppendLine(divContainerHtml.ToString());
			easyuiPanelHtml.AppendLine("</div>");

			// 放置範圍上下限值設定
			html.AppendFormat("<input id=\"hidFruitsArray\" type=\"hidden\" value=\"{0}\"/>", GetFruitsArray(p.Formulas.Seats));
			// 繪製水果算式區
			html.Append(fruitsFormulasLeftHtml.ToString());
			html.Append(fruitsFormulasRightHtml.ToString());
			// 圖片移動區域
			html.Append(easyuiPanelHtml.ToString());

			if (html.Length != 0)
			{
				html.Insert(0, "<br/><div class=\"page-header\"><h4><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">水果連連看</span></h4></div><hr />");
			}

			return html.ToString();
		}

		/// <summary>
		/// 顯示水果算式Div區域
		/// </summary>
		/// <param name="fruitsFormulasHtml">html編輯</param>
		/// <param name="formula">計算式</param>
		/// <param name="index">當前記錄件數索引</param>
		private void CreateFruitsFormulasHtml(StringBuilder fruitsFormulasHtml, Formula formula, int index)
		{
			fruitsFormulasHtml.AppendLine("<div class=\"form-inline-ext\">");
			fruitsFormulasHtml.AppendLine("<h5>");
			fruitsFormulasHtml.AppendLine(string.Format("<img src=\"../Content/image/fruits/{0}.png\" class=\"fruitsImg\" width=25 height=25>", FruitsArray[index].ToString()));
			fruitsFormulasHtml.AppendLine(string.Format("<span class=\"label\">{0}{1}{2}</span>", formula.LeftParameter, formula.Sign.ToOperationString(), formula.RightParameter));
			fruitsFormulasHtml.AppendLine("</h5>");
			fruitsFormulasHtml.AppendLine("</div>");
		}
	}

	/// <summary>
	/// 可拖動的水果初期坐標類
	/// </summary>
	public sealed class Coordinate
	{
		public int Left { get; set; }
		public int Top { get; set; }
	}
}
