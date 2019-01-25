using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.LearnLengthUnit.Item;
using MyMathSheets.ComputationalStrategy.LearnLengthUnit.Main.Parameters;
using System.Text;

namespace MyMathSheets.TheFormulaShows.LearnLengthUnit.Support
{
	/// <summary>
	/// 根據題型輸出結果作成HTML模板信息
	/// </summary>
	[HtmlSupport(LayoutSetting.Preview.LearnLengthUnit)]
	[Substitute(SubstituteType.Script, "<script src=\"../Scripts/Ext/MathSheets.LearnLengthUnit.js\" charset=\"utf-8\"></script>")]
	[Substitute(SubstituteType.ReadyEvent, "MathSheets.LearnLengthUnit.ready();")]
	[Substitute(SubstituteType.MakeCorrectionsEvent, "fault += MathSheets.LearnLengthUnit.makeCorrections();")]
	[Substitute(SubstituteType.TheirPapersEvent, "MathSheets.LearnLengthUnit.theirPapers();")]
	[Substitute(SubstituteType.PrintSettingEvent, "MathSheets.LearnLengthUnit.printSetting();")]
	[Substitute(SubstituteType.PrintAfterSettingEvent, "MathSheets.LearnLengthUnit.printAfterSetting();")]
	public class LearnLengthUnitHtmlSupport : HtmlSupportBase
	{
		/// <summary>
		/// 標題HTML模板
		/// </summary>
		private const string PAGE_HEADER_HTML_FORMAT = "<br/><div class=\"page-header\"><h4 id=\"mathSheet{0}\"><img src=\"../Content/image/homework.png\" width=\"30\" height=\"30\" /><span style=\"padding: 8px\">{1}</span></h4></div><hr />";

		private const string INPUT_HTML_FORMAT = "<input id=\"inputLlu{0}{1}\" type=\"text\" placeholder=\" ?? \" class=\"form-control input-addBorder-2\" disabled=\"disabled\" onkeyup=\"if(!/^\\d+$/.test(this.value)) this.value='';\" />";
		private const string UNIT_HTML_FORMAT = "<span class=\"label p-2\">{0}</span>";
		private const string SPAN_HTML_FORMAT = "<span class=\"label\">{0}</span>";
		private const string EQUALTO_HTML = "<span class=\"label\">&#61;</span>";

		/// <summary>
		/// 答案列表(eg: 1,5,12,10,4,8,1.....)
		/// </summary>
		private StringBuilder _answers;

		/// <summary>
		/// 根據題型輸出結果作成HTML模板信息
		/// </summary>
		/// <param name="parameter">題型輸出結果</param>
		/// <returns>HTML模板信息</returns>
		protected override string MakeHtmlStatement(ParameterBase parameter)
		{
			LearnLengthUnitParameter p = parameter as LearnLengthUnitParameter;

			if (p.Formulas.Count == 0)
			{
				return string.Empty;
			}

			int numberOfColumns = 0;
			bool isRowHtmlClosed = false;
			_answers = new StringBuilder();

			// 輸入框控件ID
			int controlIndex = 0;
			StringBuilder html = new StringBuilder();
			StringBuilder rowHtml = new StringBuilder();
			StringBuilder colHtml = new StringBuilder();
			foreach (LearnLengthUnitFormula item in p.Formulas)
			{
				isRowHtmlClosed = false;
				colHtml.AppendLine("<div class=\"col-md-6 form-inline\">");
				colHtml.AppendLine("<h5>");
				colHtml.Append(GetHtml(item, controlIndex));
				colHtml.AppendLine("</h5>");
				colHtml.AppendLine("<div class=\"divCorrectOrFault-1\">");
				colHtml.AppendLine(string.Format("<img id=\"imgOKLearnLengthUnit{0}\" src=\"../Content/image/correct.png\" class=\"imgCorrect-1\" />", controlIndex));
				colHtml.AppendLine(string.Format("<img id=\"imgNoLearnLengthUnit{0}\" src=\"../Content/image/fault.png\" class=\"imgFault-1\" />", controlIndex));
				colHtml.AppendLine("</div>");
				colHtml.AppendLine("</div>");

				controlIndex++;
				numberOfColumns++;
				if (numberOfColumns == 2)
				{
					rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
					rowHtml.Append(colHtml.ToString());
					rowHtml.AppendLine("</div>");

					html.Append(rowHtml);

					isRowHtmlClosed = true;
					numberOfColumns = 0;
					rowHtml.Length = 0;
					colHtml.Length = 0;
				}
			}

			if (!isRowHtmlClosed)
			{
				rowHtml.AppendLine("<div class=\"row text-center row-margin-top\">");
				rowHtml.Append(colHtml.ToString());
				rowHtml.AppendLine("</div>");

				html.Append(rowHtml);
			}

			if (html.Length != 0)
			{
				// 題型標題
				html.Insert(0, string.Format(PAGE_HEADER_HTML_FORMAT, LayoutSetting.Preview.LearnLengthUnit.ToString(), LayoutSetting.Preview.LearnLengthUnit.ToComputationalStrategyName()));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回米單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetMeterHtml(LearnLengthUnitFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.LengthUnitItme.Meter.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.METER_UNIT));
			}
			else
			{
				html.AppendFormat(SPAN_HTML_FORMAT, item.LengthUnitItme.Meter).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.METER_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回分米單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetDecimetreHtml(LearnLengthUnitFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.LengthUnitItme.Decimetre.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.DECIMETRE_UNIT));
			}
			else
			{
				html.AppendFormat(SPAN_HTML_FORMAT, item.LengthUnitItme.Decimetre).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.DECIMETRE_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回釐米單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetCentimeterHtml(LearnLengthUnitFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.LengthUnitItme.Centimeter.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.CENTIMETER_UNIT));
			}
			else
			{
				html.AppendFormat(SPAN_HTML_FORMAT, item.LengthUnitItme.Centimeter).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.CENTIMETER_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回毫米單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetMillimeterHtml(LearnLengthUnitFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.LengthUnitItme.Millimeter.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.MILLIMETER_UNIT));
			}
			else
			{
				html.AppendFormat(SPAN_HTML_FORMAT, item.LengthUnitItme.Millimeter).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.MILLIMETER_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回分米單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetRemainderDecimetreHtml(LearnLengthUnitFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.RemainderDecimetre.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.DECIMETRE_UNIT));
			}
			else
			{
				// 考慮剩餘分米的輸出
				html.AppendFormat(SPAN_HTML_FORMAT, item.RemainderDecimetre.Value).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.DECIMETRE_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回釐米單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetRemainderCentimeterHtml(LearnLengthUnitFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.RemainderCentimeter.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.CENTIMETER_UNIT));
			}
			else
			{
				// 考慮剩餘釐米的輸出
				html.AppendFormat(SPAN_HTML_FORMAT, item.RemainderCentimeter.Value).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.CENTIMETER_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 返回毫米單位的HTML信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID(主)</param>
		/// <param name="isInput">是否為可輸入項目</param>
		/// <param name="childIndex">輸入框控件ID(子)</param>
		/// <returns>HTML信息</returns>
		private string GetRemainderMillimeterHtml(LearnLengthUnitFormula item, int controlIndex, bool isInput, string childIndex = "S0")
		{
			StringBuilder html = new StringBuilder();

			if (isInput)
			{
				_answers.AppendFormat("{0};", Base64.EncodeBase64(item.RemainderMillimeter.Value.ToString()));
				html.AppendFormat(INPUT_HTML_FORMAT, controlIndex, childIndex).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.MILLIMETER_UNIT));
			}
			else
			{
				// 考慮剩餘毫米的輸出
				html.AppendFormat(SPAN_HTML_FORMAT, item.RemainderMillimeter.Value).AppendLine(string.Format(UNIT_HTML_FORMAT, Consts.MILLIMETER_UNIT));
			}

			return html.ToString();
		}

		/// <summary>
		/// 根據題型輸出結果作成HTML模板信息
		/// </summary>
		/// <param name="item">題型輸出結果</param>
		/// <param name="controlIndex">輸入框控件ID</param>
		/// <returns>HTML模板信息</returns>
		private string GetHtml(LearnLengthUnitFormula item, int controlIndex)
		{
			_answers.Length = 0;
			StringBuilder html = new StringBuilder();

			switch (item.LengthUnitTransformType)
			{
				// 米到分米
				case LengthUnitTransform.M2D:
					html.Append(GetMeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 米到釐米
				case LengthUnitTransform.M2C:
					html.Append(GetMeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetCentimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 米到毫米
				case LengthUnitTransform.M2MM:
					html.Append(GetMeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMillimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 分米到米
				case LengthUnitTransform.D2M:
					html.Append(GetDecimetreHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 分米到釐米
				case LengthUnitTransform.D2C:
					html.Append(GetDecimetreHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetCentimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 分米到毫米
				case LengthUnitTransform.D2MM:
					html.Append(GetDecimetreHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMillimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 分米到米分米（有剩餘）
				case LengthUnitTransform.D2MExt:
					html.Append(GetDecimetreHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetRemainderDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					break;

				// 分米到米釐米
				case LengthUnitTransform.D2MC:
					html.Append(GetDecimetreHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetCentimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					break;

				// 釐米到米
				case LengthUnitTransform.C2M:
					html.Append(GetCentimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 釐米到分米
				case LengthUnitTransform.C2D:
					html.Append(GetCentimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 釐米到毫米
				case LengthUnitTransform.C2MM:
					html.Append(GetCentimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMillimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 釐米到米分米
				case LengthUnitTransform.C2MD:
					html.Append(GetCentimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					break;

				// 釐米到分米毫米
				case LengthUnitTransform.C2DMM:
					html.Append(GetCentimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetMillimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					break;

				// 釐米到米分米釐米（有剩餘）
				case LengthUnitTransform.C2MDExt:
					html.Append(GetCentimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					html.Append(GetRemainderCentimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S2"));
					break;

				// 毫米到分米
				case LengthUnitTransform.MM2D:
					html.Append(GetMillimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 毫米到釐米
				case LengthUnitTransform.MM2C:
					html.Append(GetMillimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetCentimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					break;

				// 毫米到米分米
				case LengthUnitTransform.MM2MD:
					html.Append(GetMillimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					break;

				// 毫米到米分米釐米
				case LengthUnitTransform.MM2MDC:
					html.Append(GetMillimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					html.Append(GetCentimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S2"));
					break;

				// 毫米到分米釐米
				case LengthUnitTransform.MM2DC:
					html.Append(GetMillimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetCentimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					break;

				// 毫米到米釐米
				case LengthUnitTransform.MM2MC:
					html.Append(GetMillimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetCentimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					break;

				// 毫米到米分米釐米毫米（有剩餘）
				case LengthUnitTransform.MM2MDCExt:
					html.Append(GetMillimeterHtml(item, controlIndex, (item.Gap == GapFilling.Left)));
					html.AppendLine(EQUALTO_HTML);
					html.Append(GetMeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left)));
					html.Append(GetDecimetreHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S1"));
					html.Append(GetCentimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S2"));
					html.Append(GetRemainderMillimeterHtml(item, controlIndex, !(item.Gap == GapFilling.Left), "S3"));
					break;
			}

			// 答案列表
			_answers.Length -= 1;
			html.AppendLine(string.Format("<input id=\"hidLluAnswer{0}\" type=\"hidden\" value=\"{1}\" />", controlIndex, _answers.ToString()));

			return html.ToString();
		}
	}
}
