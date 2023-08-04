using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.FruitsLinkage.Main.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.FruitsLinkage.Support
{
    /// <summary>
    /// 題型模板支援類
    /// </summary>
    [HtmlSupport("FruitsLinkage")]
    [Substitute(SubstituteType.Stylesheet, "<link href=\"../Content/css/FruitsLinkage.css\" rel=\"stylesheet\" type=\"text/css\" />")]
    [Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.FruitsLinkage.js\" charset=\"utf-8\"></script>")]
    [Substitute(SubstituteType.ReadyEvent, "__fruitsArrayHiddenControlId = 'hidFruitsArray';MathSheets.FruitsLinkage.ready('divFruitDrag', 'divContainer');")]
    [Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.FruitsLinkage.makeCorrections();")]
    [Substitute(SubstituteType.TheirPapersEvent, "MathSheets.FruitsLinkage.theirPapers();")]
    public class FruitsLinkageHtmlSupport : HtmlSupportBase<FruitsLinkageParameter>
    {
        /// <summary>
        /// 標題HTML模板
        /// </summary>
        private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

        /// <summary>
        /// 水果序號列表
        /// </summary>
        private readonly List<FruitType> FruitsArray;

        /// <summary>
        /// 可拖動水果的初期坐標集合
        /// </summary>
        private readonly List<Coordinate> CoordinateArray;

        /// <summary>
        /// 構造體
        /// </summary>
        [ImportingConstructor]
        public FruitsLinkageHtmlSupport()
        {
            // 水果圖片列表
            FruitsArray = new List<FruitType>()
            {
                FruitType.Apple,
                FruitType.Apricot,
                FruitType.Banana,
                FruitType.Cherry,
                FruitType.Grape,
                FruitType.Hamimelon,
                FruitType.Orange,
                FruitType.Peach,
                FruitType.Pear,
                FruitType.Strawberry,
                FruitType.Watermelon
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
        /// <param name="p">相關計算式</param>
        /// <returns>HTML語句</returns>
        public override string MakeHtmlContent(FruitsLinkageParameter p)
        {
            if (p.Formulas.FruitsFormulas.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder html = new StringBuilder();
            StringBuilder easyuiPanelHtml = new StringBuilder();
            StringBuilder divDragHtml = new StringBuilder();
            StringBuilder divContainerHtml = new StringBuilder();

            // 隨機排序
            var fruitsArray = FruitsArray.OrderBy(x => Guid.NewGuid()).ToList();
            var coordinateArray = CoordinateArray.OrderBy(x => Guid.NewGuid()).ToList();

            int index = 0;
            // 繪製水果算是區
            p.Formulas.FruitsFormulas.ToList().ForEach(d =>
            {
                // 可拖動水果Div區域
                divDragHtml.AppendLine(string.Format("<div id=\"divFruitDrag{0}\" class=\"divFruitDrag\" style=\"position: absolute; left: {1}px; top: {2}px;\" data-options=\"onDrag:MathSheets.FruitsLinkage.onDrag, onStopDrag:MathSheets.FruitsLinkage.onStopDrag\">", index, coordinateArray[index].Left, coordinateArray[index].Top));
                divDragHtml.AppendLine(string.Format("<img src=\"../Content/image/fruits/{0}.png\" width=\"40\" height=\"40\" data-toggle=\"tooltip\" title=\"{1}{2}{3}\">", fruitsArray[index].ToString(), d.LeftParameter, d.Sign.ToOperationUnicode(), d.RightParameter));
                divDragHtml.AppendLine(string.Format("<input id=\"divFruitDrag{0}Input\" type=\"hidden\" />", index));
                divDragHtml.AppendLine(string.Format("<input id=\"divFruitDrag{0}Result\" type=\"hidden\" value=\"ERROR\" />", index));
                divDragHtml.AppendLine("</div>");

                index++;
            });

            int seat = 0;
            p.Formulas.Sort.ToList().ForEach(d =>
            {
                // 容器對象
                Formula container = p.Formulas.ContainersFormulas[d];
                // 容器Div區域
                divContainerHtml.AppendLine(string.Format("<div id=\"divContainer{0}\" class=\"divContainer divSeat{1}\">", seat, seat));
                divContainerHtml.AppendLine("<h5>");
                divContainerHtml.AppendLine(string.Format("<span class=\"label\">{0}{1}{2}</span>", container.LeftParameter, container.Sign.ToOperationUnicode(), container.RightParameter));
                divContainerHtml.AppendLine("<img src=\"../Content/image/fruits/table.png\" width=78 height=75 style=\"margin-top:-20px;\">");
                divContainerHtml.AppendLine("</h5>");
                divContainerHtml.AppendLine("<div class=\"divCorrectOrFault-2\">");
                divContainerHtml.AppendLine(string.Format("<img id=\"imgOKFruitsLinkage{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-2\" />", seat));
                divContainerHtml.AppendLine(string.Format("<img id=\"imgNoFruitsLinkage{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-2\" />", seat));
                divContainerHtml.AppendLine("</div>");
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
            // 圖片移動區域
            html.Append(easyuiPanelHtml.ToString());

            if (html.Length != 0)
            {
                html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "FruitsLinkage", "水果連連看"));
            }

            return html.ToString();
        }
    }

    /// <summary>
    /// 可拖動的水果初期坐標類
    /// </summary>
    public sealed class Coordinate
    {
        /// <summary>
        ///
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Top { get; set; }
    }
}