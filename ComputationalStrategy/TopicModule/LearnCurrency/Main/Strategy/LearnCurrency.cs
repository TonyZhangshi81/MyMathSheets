using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Item;
using MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Parameters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyMathSheets.ComputationalStrategy.LearnCurrency.Main.Strategy
{
	/// <summary>
	/// 認識貨幣題型
	/// </summary>
	[Operation(LayoutSetting.Preview.LearnCurrency)]
	public class LearnCurrency : OperationBase
	{
		/// <summary>
		/// 存儲貨幣轉換的實現方法
		/// </summary>
		private readonly ConcurrentDictionary<CurrencyTransform, Expression<Func<LearnCurrencyFormula>>> Currencys;

		/// <summary>
		/// 構造函數
		/// </summary>
		public LearnCurrency()
		{
			Currencys = new ConcurrentDictionary<CurrencyTransform, Expression<Func<LearnCurrencyFormula>>>();
			//Currencys.GetOrAdd(CurrencyTransform.Y2J, )
		}
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			LearnCurrencyParameter p = parameter as LearnCurrencyParameter;

			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{

			}

		}

		/// <summary>
		/// 元轉換為角
		/// </summary>
		/// <param name="type"></param>
		private LearnCurrencyFormula YuanConvertToJiao(QuestionType type)
		{
			// 隨機取得元數量級
			int yuan = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為角的換算
			int jiao = yuan * 10;
			// 填空項目編排
			GapFilling gap = GapFilling.Left;
			if (type == QuestionType.GapFilling)
			{
				// 隨機編排填空項目(是角還是元)
				gap = (GapFilling)CommonUtil.GetRandomNumber((int)GapFilling.Left, (int)GapFilling.Right);
			}
			// 結果對象設置并返回
			LearnCurrencyFormula formula = new LearnCurrencyFormula()
			{
				// 元單位
				YuanUnit = yuan,
				// 角單位
				JiaoUnit = jiao,
				// 填空項目(元或者角)
				Gap = gap
			};

			return formula;
		}







	}
}
