using MyMathSheets.CommonLib.Main.Item;
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
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 3;

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
		}
		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			LearnCurrencyParameter p = parameter as LearnCurrencyParameter;

			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;
			// 標準題型（指定單個轉換單位）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 單一的貨幣轉換類型
				CurrencyTransform type = (CurrencyTransform)p.Types[0];

				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					LearnCurrencyFormula formula = new LearnCurrencyFormula() { CurrencyTransformType = type };
					if (_currencys.TryGetValue(type, out Action<LearnCurrencyFormula, QuestionType> currency))
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
					// 單一的貨幣轉換類型
					CurrencyTransform type = GetRandomCurrencyTransform(p.Types);
					LearnCurrencyFormula formula = new LearnCurrencyFormula() { CurrencyTransformType = type };
					if (_currencys.TryGetValue(type, out Action<LearnCurrencyFormula, QuestionType> currency))
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
		private bool CheckIsNeedInverseMethod(IList<LearnCurrencyFormula> preFormulas, LearnCurrencyFormula currentFormula)
		{
			// 判斷當前算式是否已經出現過
			if (preFormulas.ToList().Any(d => d.CurrencyUnit.Yuan == currentFormula.CurrencyUnit.Yuan
				&& d.CurrencyUnit.Jiao == currentFormula.CurrencyUnit.Jiao
				&& d.CurrencyUnit.Fen == currentFormula.CurrencyUnit.Fen
				&& d.CurrencyTransformType == currentFormula.CurrencyTransformType))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 隨機獲取貨幣轉換集合中的一個轉換類型
		/// </summary>
		/// <param name="types">貨幣轉換集合（題型配置）</param>
		/// <returns>轉換類型</returns>
		private CurrencyTransform GetRandomCurrencyTransform(int[] types)
		{
			int index = CommonUtil.GetRandomNumber(0, types.Count() - 1);
			return (CurrencyTransform)types[index];
		}

		/// <summary>
		/// 分轉換為元角（有剩餘）
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(分或者元角)</param>
		protected virtual void FenConvertToYuanJiaoExt(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得分數量級
			int fen = CommonUtil.GetRandomNumber(1, 1000);
			// 如果沒有產生分的餘量，那麼變換題型為分轉元角
			if (fen % 10 == 0)
			{
				// 題型變換
				formula.CurrencyTransformType = CurrencyTransform.F2YJ;
				// 分轉換為元角
				FenConvertToYuanJiao(formula, type);
				return;
			}
			else if (fen % 100 == 0)
			{
				// 題型變換
				formula.CurrencyTransformType = CurrencyTransform.F2Y;
				// 分轉換為元
				FenConvertToYuan(formula, type);
				return;
			}

			// 轉換為元的換算
			int yuan = fen / 100;
			// 轉換為角的換算
			int jiao = fen % 100 / 10;
			// 隨機編排填空項目(是角還是分)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 元單位
				Yuan = yuan,
				// 角單位
				Jiao = jiao,
				// 分單位
				Fen = fen
			};
			// 剩餘的分
			formula.RemainderFen = fen % 10;
			// 填空項目(分或者角)
			formula.Gap = gap;
		}

		/// <summary>
		/// 分轉換為元角
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(分或者元角)</param>
		protected virtual void FenConvertToYuanJiao(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得分數量級
			int fen = CommonUtil.GetRandomNumber(1, 100) * 10;
			// 轉換為元的換算
			int yuan = fen / 100;
			// 轉換為角的換算
			int jiao = fen % 100 / 10;
			// 隨機編排填空項目(是角還是分)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 元單位
				Yuan = yuan,
				// 角單位
				Jiao = jiao,
				// 分單位
				Fen = fen
			};
			// 填空項目(分或者角)
			formula.Gap = gap;
		}

		/// <summary>
		/// 分轉換為角
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(分或者角)</param>
		protected virtual void FenConvertToJiao(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得分數量級
			int fen = CommonUtil.GetRandomNumber(1, 10) * 10;
			// 轉換為角的換算
			int jiao = fen / 10;
			// 隨機編排填空項目(是角還是分)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 角單位
				Jiao = jiao,
				// 分單位
				Fen = fen
			};
			// 填空項目(分或者角)
			formula.Gap = gap;
		}

		/// <summary>
		/// 分轉換為元
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(分或者元)</param>
		protected virtual void FenConvertToYuan(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得分數量級
			int fen = CommonUtil.GetRandomNumber(1, 10) * 100;
			// 轉換為元的換算
			int yuan = fen / 100;
			// 隨機編排填空項目(是角還是分)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 元單位
				Yuan = yuan,
				// 分單位
				Fen = fen
			};
			// 填空項目(分或者元)
			formula.Gap = gap;
		}

		/// <summary>
		/// 角轉換為元分
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(元分或者角)</param>
		protected virtual void JiaoConvertToYuanFen(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得角數量級
			int jiao = CommonUtil.GetRandomNumber(1, 100);
			// 轉換為元的換算
			int yuan = Convert.ToInt32(Math.Floor(Convert.ToDecimal(jiao) / 10.0m));
			// 轉換為分的換算
			int fen = (jiao % 10) * 10;
			// 隨機編排填空項目(是角還是分)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 元單位
				Yuan = yuan,
				// 角單位
				Jiao = jiao,
				// 分單位
				Fen = fen
			};
			// 填空項目(分或者角)
			formula.Gap = gap;
		}

		/// <summary>
		/// 角轉換為分
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(分或者角)</param>
		protected virtual void JiaoConvertToFen(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得角數量級
			int jiao = CommonUtil.GetRandomNumber(1, 10);
			// 轉換為分的換算
			int fen = jiao * 10;
			// 隨機編排填空項目(是角還是分)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 角單位
				Jiao = jiao,
				// 分單位
				Fen = fen
			};
			// 填空項目(分或者角)
			formula.Gap = gap;
		}

		/// <summary>
		/// 角轉換為元（有剩餘）
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(元或者角)</param>
		protected virtual void JiaoConvertToYuanExt(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得角數量級
			int jiao = CommonUtil.GetRandomNumber(10, 100);
			// 如果沒有產生角的餘量，那麼變換題型為角轉元
			if (jiao % 10 == 0)
			{
				// 題型變換
				formula.CurrencyTransformType = CurrencyTransform.J2Y;
				// 角轉換為元
				JiaoConvertToYuan(formula, type);
				return;
			}

			// 轉換為元的換算
			int yuan = jiao / 10;
			// 隨機編排填空項目(是角還是元)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 元單位
				Yuan = yuan,
				// 角單位
				Jiao = jiao
			};
			// 剩餘的角
			formula.RemainderJiao = jiao % 10;
			// 填空項目(角或者元、角)
			formula.Gap = gap;
		}

		/// <summary>
		/// 角轉換為元
		/// </summary>
		/// <param name="formula">計算式作成</param>
		/// <param name="type">填空項目選擇(元或者角)</param>
		protected virtual void JiaoConvertToYuan(LearnCurrencyFormula formula, QuestionType type)
		{
			// 隨機取得角數量級
			int jiao = CommonUtil.GetRandomNumber(1, 10) * 10;
			// 轉換為元的換算
			int yuan = jiao / 10;
			// 隨機編排填空項目(是角還是元)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 元單位
				Yuan = yuan,
				// 角單位
				Jiao = jiao
			};
			// 填空項目(角或者元)
			formula.Gap = gap;
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
			// 隨機編排填空項目(是角還是元)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 元單位
				Yuan = yuan,
				// 角單位
				Jiao = jiao
			};
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
			// 隨機編排填空項目(是分還是元)
			GapFilling gap = GetRandomGapFilling(type);
			// 結果對象設置并返回
			formula.CurrencyUnit = new Currency()
			{
				// 元單位
				Yuan = yuan,
				// 分單位
				Fen = fen
			};
			// 填空項目(元或者分)
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
