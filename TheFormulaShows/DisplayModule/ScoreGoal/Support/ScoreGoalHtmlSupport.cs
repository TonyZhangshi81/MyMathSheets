using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.ScoreGoal.Main.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace MyMathSheets.TheFormulaShows.ScoreGoal.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport("ScoreGoal")]
	[Substitute(SubstituteType.Stylesheet, "<link href=\"../Content/ScoreGoal.css\" rel=\"stylesheet\" type=\"text/css\" />")]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.ScoreGoal.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "__goalsArrayHiddenControlId = 'hidBallsArray';MathSheets.ScoreGoal.ready('divBall', 'divGoaler');")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.ScoreGoal.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.ScoreGoal.theirPapers();")]
	public class ScoreGoalHtmlSupport : HtmlSupportBase<ScoreGoalParameter>
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";

		/// <summary>
		/// 球類列表
		/// </summary>
		private readonly List<BallType> BallsImageArray;

		/// <summary>
		/// 可拖動球類的初期坐標集合
		/// </summary>
		private readonly List<Coordinate> CoordinateArray;

		/// <summary>
		/// 構造體
		/// </summary>
		[ImportingConstructor]
		public ScoreGoalHtmlSupport()
		{
			// 球類圖片列表
			BallsImageArray = new List<BallType>()
			{
				BallType.Ball,
				BallType.Basketball,
				BallType.BeachBall,
				BallType.Bowling,
				BallType.Football,
				BallType.Golf,
				BallType.Rugby,
				BallType.Tennis,
				BallType.Volleyball,
				BallType.Bomb
			};

			// 可拖動球類圖片初期化坐標集合
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
			BallsImageArray = BallsImageArray.OrderBy(x => Guid.NewGuid()).ToList();
			CoordinateArray = CoordinateArray.OrderBy(x => Guid.NewGuid()).ToList();
		}

		/// <summary>
		/// 放置範圍上下限值設定
		/// </summary>
		/// <param name="ballsFormulas">球門位置</param>
		/// <returns></returns>
		private string GetBallsArray(Dictionary<Formula, int> ballsFormulas)
		{
			StringBuilder seatsList = new StringBuilder();
			ballsFormulas.ToList().ForEach(d => seatsList.AppendFormat("{0};", d.Value));
			seatsList.Length -= 1;

			return seatsList.ToString();
		}

		/// <summary>
		/// 射門得分HTML作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		public override string MakeHtmlContent(ScoreGoalParameter p)
		{
			if (p.Formulas.BallsFormulas.Count == 0)
			{
				return string.Empty;
			}

			StringBuilder html = new StringBuilder();
			StringBuilder easyuiPanelHtml = new StringBuilder();
			StringBuilder divBallsHtml = new StringBuilder();
			StringBuilder divGoalsHtml = new StringBuilder();

			int index = 0;
			// 繪製球類算式區
			p.Formulas.BallsFormulas.ToList().ForEach(d =>
			{
				var formula = d.Key;
				// 可拖動球類Div區域
				divBallsHtml.AppendLine(string.Format("<div id=\"divBall{0}\" class=\"divBall\" style=\"position: absolute; left: {1}px; top: {2}px;\" data-options=\"onDrag:MathSheets.ScoreGoal.onDrag, onStopDrag:MathSheets.ScoreGoal.onStopDrag\">", index, CoordinateArray[index].Left, CoordinateArray[index].Top));
				divBallsHtml.AppendLine(string.Format("<img src=\"../Content/image/sport/{0}.png\" width=40 height=40 data-toggle=\"tooltip\" title=\"{1}{2}{3}\">", BallsImageArray[index].ToString(), formula.LeftParameter, formula.Sign.ToOperationUnicode(), formula.RightParameter));
				divBallsHtml.AppendLine(string.Format("<input id=\"divBall{0}Input\" type=\"hidden\" />", index));
				divBallsHtml.AppendLine(string.Format("<input id=\"divBall{0}Result\" type=\"hidden\" value=\"ERROR\" />", index));
				divBallsHtml.AppendLine("</div>");

				index++;
			});

			// 球門區域HTML作成
			CreateGoalsHtml(divGoalsHtml, p.Formulas.GoalsFormulas);

			// 圖片移動區域
			easyuiPanelHtml.AppendLine("<div class=\"easyui-panel easyui-panel-ScoreGoal-ext\" >");
			// 可移動球類
			easyuiPanelHtml.AppendLine(divBallsHtml.ToString());
			// 球門區域
			easyuiPanelHtml.AppendLine(divGoalsHtml.ToString());
			// Div闭合
			easyuiPanelHtml.AppendLine("</div>");

			// 放置範圍上下限值設定
			html.AppendFormat("<input id=\"hidBallsArray\" type=\"hidden\" value=\"{0}\"/>", GetBallsArray(p.Formulas.BallsFormulas));
			// 圖片移動區域
			html.Append(easyuiPanelHtml.ToString());

			if (html.Length != 0)
			{
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, "ScoreGoal", "射門得分"));
			}
			return html.ToString();
		}

		/// <summary>
		/// 球門區域HTML作成
		/// </summary>
		/// <param name="divGoalsHtml">球門區域html</param>
		/// <param name="goalsFormulas">球門計算式</param>
		private void CreateGoalsHtml(StringBuilder divGoalsHtml, IList<Formula> goalsFormulas)
		{
			// 球門1
			divGoalsHtml.AppendLine("<div id=\"divGoaler0\" class=\"divGoaler divGoaler0\">");
			divGoalsHtml.AppendLine("<h5>");
			divGoalsHtml.AppendLine(string.Format("<span class=\"label\">{0}{1}{2}</span>", goalsFormulas[0].LeftParameter, goalsFormulas[0].Sign.ToOperationUnicode(), goalsFormulas[0].RightParameter));
			divGoalsHtml.AppendLine("<img src=\"../Content/image/sport/goalkeeper.png\" width=200 height=100 style=\"margin-top:15px;\" >");
			divGoalsHtml.AppendLine("</h5>");
			divGoalsHtml.AppendLine("</div>");

			// 比分顯示
			divGoalsHtml.AppendLine("<div id=\"divScore0\" class=\"divScore divScore0\">");
			divGoalsHtml.AppendLine("<h2><span id=\"spanHomeScore\"></span></h2>");
			divGoalsHtml.AppendLine("</div>");
			divGoalsHtml.AppendLine("<div id=\"divScore1\" class=\"divScore divScore1\">");
			divGoalsHtml.AppendLine("<h2>");
			divGoalsHtml.AppendLine("<span>:</span>");
			divGoalsHtml.AppendLine("</h2>");
			divGoalsHtml.AppendLine("</div>");
			divGoalsHtml.AppendLine("<div id=\"divScore2\" class=\"divScore divScore2\">");
			divGoalsHtml.AppendLine("<h2><span id=\"spanAwayScore\"></span></h2>");
			divGoalsHtml.AppendLine("</div>");

			// 球門2
			divGoalsHtml.AppendLine("<div id=\"divGoaler1\" class=\"divGoaler divGoaler1\">");
			divGoalsHtml.AppendLine("<h5>");
			divGoalsHtml.AppendLine(string.Format("<span class=\"label\">{0}{1}{2}</span>", goalsFormulas[1].LeftParameter, goalsFormulas[1].Sign.ToOperationUnicode(), goalsFormulas[1].RightParameter));
			divGoalsHtml.AppendLine("<img src=\"../Content/image/sport/goalkeeper.png\" width=200 height=100 style=\"margin-top:15px;\" >");
			divGoalsHtml.AppendLine("</h5>");
			divGoalsHtml.AppendLine("</div>");

			// 對錯顯示
			divGoalsHtml.AppendLine("<div class=\"divScoreGoalResultImg\">");
			divGoalsHtml.AppendLine("<div class=\"divCorrectOrFault-4\">");
			divGoalsHtml.AppendLine("<img id=\"imgOKScoreGoal\" src=\"../Content/image/correct.png\" class=\"imgCorrect-4\" />");
			divGoalsHtml.AppendLine("<img id=\"imgNoScoreGoal\" src=\"../Content/image/fault.png\" class=\"imgFault-4\" />");
			divGoalsHtml.AppendLine("</div>");
			divGoalsHtml.AppendLine("</div>");
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