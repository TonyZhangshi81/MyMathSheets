using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
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
	[Operation(LayoutSetting.Preview.LearnLengthUnit)]
	public class LearnLengthUnit : OperationBase
	{
		/// <summary>
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 3;

		/// <summary>
		/// 存儲長度轉換的實現方法集合
		/// </summary>
		private readonly Dictionary<CurrencyTransform, Action<LearnLengthUnitFormula, QuestionType>> _currencys =
			new Dictionary<CurrencyTransform, Action<LearnLengthUnitFormula, QuestionType>>();

		/// <summary>
		/// 構造函數
		/// </summary>
		public LearnLengthUnit()
		{
			/*
			// 元轉換為角
			_currencys[CurrencyTransform.Y2J] = YuanConvertToJiao;
			// 元轉換為分
			_currencys[CurrencyTransform.Y2F] = YuanConvertToFen;
			// 角轉換為元
			_currencys[CurrencyTransform.J2Y] = JiaoConvertToYuan;
			// 角轉換為分
			_currencys[CurrencyTransform.J2F] = JiaoConvertToFen;
			// 角轉換為元分
			_currencys[CurrencyTransform.J2YF] = JiaoConvertToYuanFen;
			// 分轉換為元
			_currencys[CurrencyTransform.F2Y] = FenConvertToYuan;
			// 分轉換為角
			_currencys[CurrencyTransform.F2J] = FenConvertToJiao;
			// 分轉換為元角
			_currencys[CurrencyTransform.F2YJ] = FenConvertToYuanJiao;

			// 角轉換為元（有剩餘）
			_currencys[CurrencyTransform.J2YExt] = JiaoConvertToYuanExt;
			// 分轉換為元角（有剩餘）
			_currencys[CurrencyTransform.F2YJExt] = FenConvertToYuanJiaoExt;
			*/
		}
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			LearnLengthUnitParameter p = parameter as LearnLengthUnitParameter;

			/*
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;
			// 標準題型（指定單個轉換單位）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 單一的長度轉換類型
				CurrencyTransform type = (CurrencyTransform)p.Types[0];

				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					LearnLengthUnitFormula formula = new LearnLengthUnitFormula() { CurrencyTransformType = type };
					if (_currencys.TryGetValue(type, out Action<LearnLengthUnitFormula, QuestionType> currency))
					{
						currency(formula, p.QuestionType);
					}
					else
					{
						throw new ArgumentException(MessageUtil.GetException(() => MsgResources.E0001L, type.ToString()));
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
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 單一的長度轉換類型
					CurrencyTransform type = GetRandomCurrencyTransform(p.Types);
					LearnLengthUnitFormula formula = new LearnLengthUnitFormula() { CurrencyTransformType = type };
					if (_currencys.TryGetValue(type, out Action<LearnLengthUnitFormula, QuestionType> currency))
					{
						currency(formula, p.QuestionType);
					}
					else
					{
						throw new ArgumentException(MessageUtil.GetException(() => MsgResources.E0001L, type.ToString()));
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
			*/
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
