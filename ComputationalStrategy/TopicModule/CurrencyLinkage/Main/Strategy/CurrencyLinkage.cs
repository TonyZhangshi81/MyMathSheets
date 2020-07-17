using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.CurrencyLinkage.Item;
using MyMathSheets.ComputationalStrategy.CurrencyLinkage.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.CurrencyLinkage.Main.Strategy
{
	/// <summary>
	/// 認識價格題型構築
	/// </summary>
	[Topic("CurrencyLinkage")]
	public class CurrencyLinkage : TopicBase
	{
		/// <summary>
		/// 題型構築
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(TopicParameterBase parameter)
		{
			CurrencyLinkageParameter p = parameter as CurrencyLinkageParameter;

			// 認識價格對象實例
			CurrencyLinkageFormula CurrencyLinkageFormula = new CurrencyLinkageFormula();

			// 左邊(上面)價格實例
			IList<decimal> leftCurrencys = new List<decimal>();
			// 右邊(下面)價格實例
			IList<int> rightCurrencys = new List<int>();
			// 正確的位置序號
			IList<int> seats = new List<int>();

			// (題型數量最多10個)
			p.NumberOfQuestions = (p.NumberOfQuestions > 10) ? 10 : p.NumberOfQuestions;
			// 按照指定數量作成相應的數學計算式
			for (var seatIndex = 0; seatIndex < p.NumberOfQuestions; seatIndex++)
			{
				int currency = CommonUtil.GetRandomNumber(1, 999);
				// 左邊(上面)價格作成
				leftCurrencys.Add(currency / 100.0m);
				// 右邊(下面)價格作成
				rightCurrencys.Add(currency);
				// 容器座位號
				seats.Add(seatIndex);
			}
			CurrencyLinkageFormula.LeftCurrencys = leftCurrencys;
			CurrencyLinkageFormula.RightCurrencys = rightCurrencys;
			CurrencyLinkageFormula.Sort = seats.OrderBy(x => Guid.NewGuid()).ToList();
			CurrencyLinkageFormula.Seats = GetNewSeats(CurrencyLinkageFormula.Sort);

			p.Currencys = CurrencyLinkageFormula;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="seats"></param>
		/// <returns></returns>
		private IList<int> GetNewSeats(IList<int> seats)
		{
			IList<int> newSeats = new List<int>();
			for (int number = 0; number < seats.Count; number++)
			{
				newSeats.Add(seats.IndexOf(number));
			}
			return newSeats;
		}
	}
}