using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.TheFormulaShows.MathUpright.Support
{
	/// <summary>
	/// 題型模板支援類
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.MathUpright)]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.MathUpright.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.MathUpright.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.MathUpright.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.MathUpright.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.MathUpright.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.MathUpright.printAfterSetting();")]
	public class MathUprightHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span class=\"span-strategy-name\">{1}</span></h4></div><hr class=\"hr-Ext\" />";
		/// <summary>
		/// 等號HTML模板
		/// </summary>
		private const string EQUALTO_HTML_FORMAT = "<span class=\"label\">{0}</span>";
		/// <summary>
		/// LABEL標籤HTML模板
		/// </summary>
		private const string LABEL_HTML_FORMAT = "<span class=\"label\">{0}</span>";

		/// <summary>
		/// 題型HTML模板作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		/// <returns>題型HTML模板信息</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{

			return string.Empty;
		}
	}
}
