using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnLengthUnit.Item;
using MyMathSheets.ComputationalStrategy.LearnLengthUnit.Main.Parameters;
using MyMathSheets.ComputationalStrategy.LearnLengthUnit.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.LearnLengthUnit.Main.Strategy
{
	/// <summary>
	/// 認識長度題型
	/// </summary>
	[Topic("LearnLengthUnit")]
	public class LearnLengthUnit : TopicBase
	{
		/// <summary>
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 3;

		/// <summary>
		/// 存儲長度轉換的實現方法集合
		/// </summary>
		private readonly Dictionary<LengthUnitTransformType, Action<LearnLengthUnitFormula, QuestionType>> _currencys =
			new Dictionary<LengthUnitTransformType, Action<LearnLengthUnitFormula, QuestionType>>();

		/// <summary>
		/// 構造函數
		/// </summary>
		public LearnLengthUnit()
		{
			// 米轉換為分米
			_currencys[LengthUnitTransformType.M2D] = MeterConvertToDecimetre;
			// 米轉換為釐米
			_currencys[LengthUnitTransformType.M2C] = MeterConvertToCentimeter;
			// 米轉換為毫米
			_currencys[LengthUnitTransformType.M2MM] = MeterConvertToMillimeter;
			// 分米轉換為米
			_currencys[LengthUnitTransformType.D2M] = DecimetreConvertToMeter;
			// 分米轉換為釐米
			_currencys[LengthUnitTransformType.D2C] = DecimetreConvertToCentimeter;
			// 分米轉換為毫米
			_currencys[LengthUnitTransformType.D2MM] = DecimetreConvertToMillimeter;
			// 分米到米分米
			_currencys[LengthUnitTransformType.D2MExt] = DecimetreConvertToMeterExt;
			// 分米到米釐米
			_currencys[LengthUnitTransformType.D2MC] = DecimetreConvertToMeterCentimeter;
			// 釐米到米
			_currencys[LengthUnitTransformType.C2M] = CentimeterConvertToMeter;
			// 釐米到分米
			_currencys[LengthUnitTransformType.C2D] = CentimeterConvertToDecimetre;
			// 釐米到毫米
			_currencys[LengthUnitTransformType.C2MM] = CentimeterConvertToMillimeter;
			// 釐米到米分米
			_currencys[LengthUnitTransformType.C2MD] = CentimeterConvertToMeterDecimetre;
			// 釐米到分米毫米
			_currencys[LengthUnitTransformType.C2DMM] = CentimeterConvertToDecimetreMillimeter;
			// 釐米到米分米釐米
			_currencys[LengthUnitTransformType.C2MDExt] = CentimeterConvertToMeterDecimetreExt;
			// 毫米到米
			_currencys[LengthUnitTransformType.MM2M] = MillimeterConvertToMeter;
			// 毫米到分米
			_currencys[LengthUnitTransformType.MM2D] = MillimeterConvertToDecimetre;
			// 毫米到釐米
			_currencys[LengthUnitTransformType.MM2C] = MillimeterConvertToCentimeter;
			// 毫米到米分米
			_currencys[LengthUnitTransformType.MM2MD] = MillimeterConvertToMeterDecimetre;
			// 毫米到米分米釐米
			_currencys[LengthUnitTransformType.MM2MDC] = MillimeterConvertToMeterDecimetreCentimeter;
			// 毫米到分米釐米
			_currencys[LengthUnitTransformType.MM2DC] = MillimeterConvertToDecimetreCentimeter;
			// 毫米到米釐米
			_currencys[LengthUnitTransformType.MM2MC] = MillimeterConvertToMeterCentimeter;
			// 毫米轉換為米分米釐米毫米
			_currencys[LengthUnitTransformType.MM2MDCExt] = MillimeterConvertToMeterDecimetreCentimeterExt;
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="lengthUnitFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(LearnLengthUnitParameter p, Func<LengthUnitTransformType> lengthUnitFunc)
		{
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 單一的長度轉換類型
				LengthUnitTransformType type = lengthUnitFunc();

				LearnLengthUnitFormula formula = new LearnLengthUnitFormula() { LengthUnitTransType = type };
				if (_currencys.TryGetValue(type, out Action<LearnLengthUnitFormula, QuestionType> currency))
				{
					currency(formula, p.QuestionType);
				}
				else
				{
					throw new ArgumentException(MessageUtil.GetMessage(() => MsgResources.E0001L, type.ToString()));
				}

				if (CheckIsNeedInverseMethod(p.Formulas, formula))
				{
					defeated++;
					// 如果大於兩次則認為此題無法作成繼續下一題
					if (defeated == INVERSE_NUMBER)
					{
						// 當前反推判定次數復原
						defeated = 0;
						continue;
					}
					i--;
					continue;
				}
				p.Formulas.Add(formula);
			}
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(TopicParameterBase parameter)
		{
			LearnLengthUnitParameter p = parameter as LearnLengthUnitParameter;

			// 標準題型（指定單個轉換單位）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 單一的長度轉換類型
				MarkFormulaList(p, () => { return (LengthUnitTransformType)p.Types[0]; });
			}
			else
			{
				// 隨機獲取長度轉換集合中的一個轉換類型
				MarkFormulaList(p, () => { return (LengthUnitTransformType)CommonUtil.GetRandomNumber(p.Types.ToList()); });
			}
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：完全一致
		/// </remarks>
		/// <param name="preFormulas">已得到的算式</param>
		/// <param name="currentFormula">當前算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(IList<LearnLengthUnitFormula> preFormulas, LearnLengthUnitFormula currentFormula)
		{
			// 判斷當前算式是否已經出現過
			if (preFormulas.ToList().Any(d => d.LengthUnitItme.Meter == currentFormula.LengthUnitItme.Meter
				&& d.LengthUnitItme.Decimetre == currentFormula.LengthUnitItme.Decimetre
				&& d.LengthUnitItme.Centimeter == currentFormula.LengthUnitItme.Centimeter
				&& d.LengthUnitItme.Millimeter == currentFormula.LengthUnitItme.Millimeter
				&& d.LengthUnitTransType == currentFormula.LengthUnitTransType))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 米轉換為分米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(米或者分米)</param>
		protected virtual void MeterConvertToDecimetre(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為分米的換算
			int decimetre = meter * 10;
			// 隨機編排填空項目(是米還是分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 米單位
				Meter = meter,
				// 分米單位
				Decimetre = decimetre
			};
			// 填空項目(米或者分米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 米轉換為釐米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(米或者釐米)</param>
		protected virtual void MeterConvertToCentimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為釐米的換算
			int centimeter = meter * 100;
			// 隨機編排填空項目(是米還是釐米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 米單位
				Meter = meter,
				// 釐米單位
				Centimeter = centimeter
			};
			// 填空項目(米或者釐米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 米轉換為毫米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(米或者毫米)</param>
		protected virtual void MeterConvertToMillimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為毫米的換算
			int millimeter = meter * 1000;
			// 隨機編排填空項目(是米還是毫米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 米單位
				Meter = meter,
				// 毫米單位
				Millimeter = millimeter
			};
			// 填空項目(米或者毫米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 分米轉換為米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(米或者分米)</param>
		protected virtual void DecimetreConvertToMeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(1, 10) * 10;
			// 轉換為米的換算
			int meter = decimetre / 10;
			// 隨機編排填空項目(是米還是分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 分米單位
				Decimetre = decimetre,
				// 米單位
				Meter = meter
			};
			// 填空項目(米或者分米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 分米到釐米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(米或者釐米)</param>
		protected virtual void DecimetreConvertToCentimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為釐米的換算
			int entimeter = decimetre * 10;
			// 隨機編排填空項目(是釐米還是分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 分米單位
				Decimetre = decimetre,
				// 釐米單位
				Centimeter = entimeter
			};
			// 填空項目(米或者釐米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 分米轉換為毫米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(分米或者毫米)</param>
		protected virtual void DecimetreConvertToMillimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為毫米的換算
			int millimeter = decimetre * 100;
			// 隨機編排填空項目(是分米還是毫米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 分米單位
				Decimetre = decimetre,
				// 毫米單位
				Millimeter = millimeter
			};
			// 填空項目(分米或者毫米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 分米到米分米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(分米或者米分米)</param>
		protected virtual void DecimetreConvertToMeterExt(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得分米數量級
			int remainderDecimetre = CommonUtil.GetRandomNumber(1, 9);
			// 隨機編排填空項目(是分米還是米分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 分米單位
				Decimetre = meter * 10 + remainderDecimetre,
				// 米單位
				Meter = meter
			};
			// 剩餘的分米
			formula.RemainderDecimetre = remainderDecimetre;
			// 填空項目(分米或者米、分米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 分米到米釐米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(分米或者米釐米)</param>
		protected virtual void DecimetreConvertToMeterCentimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(11, 99);
			// 轉換為米的換算
			int meter = decimetre / 10;
			// 轉換為釐米的換算
			int centimeter = decimetre % 10 * 10;
			// 隨機編排填空項目(是分米還是米分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 分米單位
				Decimetre = decimetre,
				// 米單位
				Meter = meter,
				// 釐米單位
				Centimeter = centimeter
			};
			// 填空項目(分米或者米、釐米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 釐米轉換為米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(釐米或者米)</param>
		protected virtual void CentimeterConvertToMeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(1, 10) * 100;
			// 轉換為米的換算
			int meter = centimeter / 100;
			// 隨機編排填空項目(是釐米還是米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 釐米單位
				Centimeter = centimeter,
				// 米單位
				Meter = meter
			};
			// 填空項目(釐米或者米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 釐米轉換為分米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(釐米或者分米)</param>
		protected virtual void CentimeterConvertToDecimetre(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(1, 10) * 10;
			// 轉換為分米的換算
			int decimetre = centimeter / 10;
			// 隨機編排填空項目(是釐米還是分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 釐米單位
				Centimeter = centimeter,
				// 分米單位
				Decimetre = decimetre
			};
			// 填空項目(釐米或者分米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 釐米轉換為毫米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(釐米或者毫米)</param>
		protected virtual void CentimeterConvertToMillimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為毫米的換算
			int millimeter = centimeter * 10;
			// 隨機編排填空項目(是釐米還是毫米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 釐米單位
				Centimeter = centimeter,
				// 毫米單位
				Millimeter = millimeter
			};
			// 填空項目(釐米或者毫米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 釐米轉換為米分米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(釐米或者米分米)</param>
		protected virtual void CentimeterConvertToMeterDecimetre(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(11, 99) * 10;
			// 轉換為米的換算
			int meter = centimeter / 100;
			// 轉換為分米的換算
			int decimetre = centimeter % 100;
			// 隨機編排填空項目(是釐米還是米分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 釐米單位
				Centimeter = centimeter,
				// 米單位
				Meter = meter,
				// 分米單位
				Decimetre = decimetre
			};
			// 填空項目(釐米或者米分米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 釐米轉換為分米毫米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(釐米或者分米毫米)</param>
		protected virtual void CentimeterConvertToDecimetreMillimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(11, 99);
			// 轉換為分米的換算
			int decimetre = centimeter / 10;
			// 轉換為毫米的換算
			int millimeter = centimeter % 10 * 10;
			// 隨機編排填空項目(是釐米還是分米毫米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 釐米單位
				Centimeter = centimeter,
				// 分米單位
				Decimetre = decimetre,
				// 毫米單位
				Millimeter = millimeter
			};
			// 填空項目(釐米或者分米毫米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 釐米轉換為米分米釐米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(釐米或者米分米釐米)</param>
		protected virtual void CentimeterConvertToMeterDecimetreExt(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得釐米數量級
			int remainderCentimeter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機編排填空項目(是釐米還是米分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 釐米單位
				Centimeter = meter * 100 + decimetre * 10 + remainderCentimeter,
				// 米單位
				Meter = meter,
				// 分米單位
				Decimetre = decimetre
			};
			// 剩餘的釐米
			formula.RemainderCentimeter = remainderCentimeter;
			// 填空項目(釐米或者米分米釐米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 毫米轉換為米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(毫米或者米)</param>
		protected virtual void MillimeterConvertToMeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 10);
			// 隨機編排填空項目(是毫米還是米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 毫米單位
				Millimeter = meter * 1000,
				// 米單位
				Meter = meter
			};
			// 填空項目(毫米或者米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 毫米轉換為分米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(毫米或者分米)</param>
		protected virtual void MillimeterConvertToDecimetre(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(1, 10);
			// 隨機編排填空項目(是毫米還是分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 毫米單位
				Millimeter = decimetre * 100,
				// 分米單位
				Decimetre = decimetre
			};
			// 填空項目(毫米或者分米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 毫米轉換為釐米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(毫米或者釐米)</param>
		protected virtual void MillimeterConvertToCentimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(1, 10);
			// 隨機編排填空項目(是毫米還是釐米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 毫米單位
				Millimeter = centimeter * 10,
				// 釐米單位
				Centimeter = centimeter
			};
			// 填空項目(毫米或者釐米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 毫米轉換為米分米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(毫米或者米分米)</param>
		protected virtual void MillimeterConvertToMeterDecimetre(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(1, 9);
			// 隨機編排填空項目(是毫米還是米分米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 毫米單位
				Millimeter = meter * 1000 + decimetre * 100,
				// 米單位
				Meter = meter,
				// 分米單位
				Decimetre = decimetre
			};
			// 填空項目(毫米或者米分米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 毫米轉換為米分米釐米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(毫米或者米分米釐米)</param>
		protected virtual void MillimeterConvertToMeterDecimetreCentimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機編排填空項目(是毫米還是米分米釐米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 毫米單位
				Millimeter = meter * 1000 + decimetre * 100 + centimeter * 10,
				// 米單位
				Meter = meter,
				// 分米單位
				Decimetre = decimetre,
				// 釐米單位
				Centimeter = centimeter
			};
			// 填空項目(毫米或者米分米釐米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 毫米轉換為分米釐米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(毫米或者分米釐米)</param>
		protected virtual void MillimeterConvertToDecimetreCentimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機編排填空項目(是毫米還是分米釐米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 毫米單位
				Millimeter = decimetre * 100 + centimeter * 10,
				// 分米單位
				Decimetre = decimetre,
				// 釐米單位
				Centimeter = centimeter
			};
			// 填空項目(毫米或者分米釐米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 毫米轉換為米釐米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(毫米或者米釐米)</param>
		protected virtual void MillimeterConvertToMeterCentimeter(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機編排填空項目(是毫米還是米釐米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 毫米單位
				Millimeter = meter * 1000 + centimeter * 10,
				// 米單位
				Meter = meter,
				// 釐米單位
				Centimeter = centimeter
			};
			// 填空項目(毫米或者米釐米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 毫米轉換為米分米釐米毫米
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(毫米或者米分米釐米毫米)</param>
		protected virtual void MillimeterConvertToMeterDecimetreCentimeterExt(LearnLengthUnitFormula formula, QuestionType type)
		{
			// 隨機取得米數量級
			int meter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得分米數量級
			int decimetre = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得釐米數量級
			int centimeter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機取得釐米數量級
			int remainderMillimeter = CommonUtil.GetRandomNumber(1, 9);
			// 隨機編排填空項目(是毫米還是米分米釐米毫米)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.LengthUnitItme = new LengthUnit()
			{
				// 毫米單位
				Millimeter = meter * 1000 + decimetre * 100 + centimeter * 10 + remainderMillimeter,
				// 米單位
				Meter = meter,
				// 分米單位
				Decimetre = decimetre,
				// 釐米單位
				Centimeter = centimeter
			};
			// 剩餘的毫米
			formula.RemainderMillimeter = remainderMillimeter;
			// 填空項目(毫米或者米分米釐米毫米)
			formula.Gap = gap;
		}

		/// <summary>
		/// 隨機編排填空項目
		/// </summary>
		/// <param name="type">是否隨機填空</param>
		/// <returns>填空項目類型</returns>
		private GapFilling GetRandomGapFilling(QuestionType type)
		{
			GapFilling gap = GapFilling.Right;
			if (type == QuestionType.GapFilling)
			{
				// 隨機編排填空項目(是分還是元)
				gap = CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Right);
			}
			return gap;
		}
	}
}