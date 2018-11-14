using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Item;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Strategy
{
	/// <summary>
	/// 認識貨幣題型
	/// </summary>
	[Operation(LayoutSetting.Preview.LearnCurrency)]
	public class LearnCurrency : OperationBase
	{
		/// <summary>
		/// 存儲貨幣轉換的實現方法集合
		/// </summary>
		private readonly Dictionary<CurrencyTransform, Action<LearnCurrencyFormula, QuestionType>> _currencys =
			new Dictionary<CurrencyTransform, Action<LearnCurrencyFormula, QuestionType>>();

		/// <summary>
		/// 構造函數
		/// </summary>
		public LearnCurrency()
		{
			// 元轉換為角
			_currencys[CurrencyTransform.Y2J] = YuanConvertToJiao;
			// 元轉換為分
			_currencys[CurrencyTransform.Y2F] = YuanConvertToFen;
		}
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			LearnCurrencyParameter p = parameter as LearnCurrencyParameter;

			// 標準題型（指定單個轉換單位）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 單一的貨幣轉換類型
				CurrencyTransform type = (CurrencyTransform)p.Types[0];

				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					LearnCurrencyFormula formula = new LearnCurrencyFormula();
					if (_currencys.TryGetValue(type, out Action<LearnCurrencyFormula, QuestionType> currency))
					{
						currency(formula, p.QuestionType);
					}
					else
					{
						throw new ArgumentException(MessageUtil.GetException(() => MsgResources.E0001L, type.ToString()));
					}

					p.Formulas.Add(formula);
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 單一的貨幣轉換類型
					CurrencyTransform type = GetRandomCurrencyTransform(p.Types);
					LearnCurrencyFormula formula = new LearnCurrencyFormula();
					if (_currencys.TryGetValue(type, out Action<LearnCurrencyFormula, QuestionType> currency))
					{
						currency(formula, p.QuestionType);
					}
					else
					{
						throw new ArgumentException(MessageUtil.GetException(() => MsgResources.E0001L, type.ToString()));
					}
					p.Formulas.Add(formula);
				}
			}
		}

		/// <summary>
		/// 隨機獲取貨幣轉換集合中的一個轉換類型
		/// </summary>
		/// <param name="types">貨幣轉換集合（題型配置）</param>
		/// <returns>轉換類型</returns>
		private CurrencyTransform GetRandomCurrencyTransform(int[] types)
		{
			int index = CommonUtil.GetRandomNumber(1, types.Count());
			return (CurrencyTransform)types[index];
		}

		/// <summary>
		/// 元轉換為角
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(元或者角)</param>
		protected virtual void YuanConvertToJiao(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得元數量級
			int yuan = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為角的換算
			int jiao = yuan * 10;
			// 填空項目編排
			GapFilling gap = GapFilling.Right;
			if (type == QuestionType.GapFilling)
			{
				// 隨機編排填空項目(是角還是元)
				gap = (GapFilling)CommonUtil.GetRandomNumber((int)GapFilling.Left, (int)GapFilling.Right);
			}
			// 結果對象設置并返回
			// 元單位
			formula.YuanUnit = yuan;
			// 角單位
			formula.JiaoUnit = jiao;
			// 填空項目(元或者角)
			formula.Gap = gap;
		}

		/// <summary>
		/// 元轉換為分
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(元或者分)</param>
		protected virtual void YuanConvertToFen(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得元數量級
			int yuan = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為分的換算
			int fen = yuan * 100;
			// 填空項目編排
			GapFilling gap = GapFilling.Right;
			if (type == QuestionType.GapFilling)
			{
				// 隨機編排填空項目(是分還是元)
				gap = (GapFilling)CommonUtil.GetRandomNumber((int)GapFilling.Left, (int)GapFilling.Right);
			}
			// 結果對象設置并返回
			// 元單位
			formula.YuanUnit = yuan;
			// 分單位
			formula.FenUnit = fen;
			// 填空項目(元或者分)
			formula.Gap = gap;
		}





	}
}
