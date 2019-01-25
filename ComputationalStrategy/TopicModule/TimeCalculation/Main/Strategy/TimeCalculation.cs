using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Item;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.TimeCalculation.Main.Strategy
{
	/// <summary>
	/// 時間運算
	/// </summary>
	[Operation(LayoutSetting.Preview.TimeCalculation)]
	public class TimeCalculation : OperationBase
	{
		/// <summary>
		/// 指定分鐘數值
		/// </summary>
		private readonly Dictionary<HourDivision, int> _assignMinutes;

		/// <summary>
		/// 構造函數
		/// </summary>
		public TimeCalculation()
		{
			// 指定分鐘數值（0、15、30、45分鐘）
			_assignMinutes = new Dictionary<HourDivision, int>() {
				{ HourDivision.IntegralPoint, 0 },
				{ HourDivision.Quarter, 15 },
				{ HourDivision.Half, 30 },
				{ HourDivision.ThreeQuarters, 45 }
			};
		}

		/// <summary>
		/// 隨機取得開始時間(秒數)
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="startTime">開始時間</param>
		/// <returns>開始時間(秒數)</returns>
		private int GetStartTime(TimeCalculationParameter p, out Time startTime)
		{
			startTime = new Time
			{
				// 小時數
				Hours = CommonUtil.GetRandomNumber(0, 23),
				// 分鐘數（減少難度而暫定的處理 -> 指定分鐘數值（0、15、30、45分鐘））
				Minutes = _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)],
				// 秒數
				Seconds = (p.IsShowSeconds) ? 0 : CommonUtil.GetRandomNumber(0, 59)
			};
			// 時間轉換為秒數
			return startTime.ToSeconds();
		}

		/// <summary>
		/// 隨機取得經過時間(秒數)
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="elapsedTime">經過時間</param>
		/// <returns>經過時間(秒數)</returns>
		private int GetElapsedTime(TimeCalculationParameter p, out Time elapsedTime)
		{
			elapsedTime = new Time
			{
				// 小時數
				Hours = CommonUtil.GetRandomNumber(p.ElapsedHours.ToList()),
				// 分鐘數
				Minutes = p.IsEssignMinutes ? _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)]
							: CommonUtil.GetRandomNumber(0, 59),
				// 秒數
				Seconds = (p.IsShowSeconds) ? 0 : CommonUtil.GetRandomNumber(0, 59)
			};
			// 時間轉換為秒數
			return elapsedTime.ToSeconds();
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(TimeCalculationParameter p, Func<SignOfOperation> signFunc)
		{
			ICalculate strategy = null;
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 結束時間
				Time endTime;
				SignOfOperation sign = signFunc();
				// 對指定運算符實例化(時間計算:之前和之後)
				strategy = CalculateManager(sign);
				Formula formula = strategy.CreateFormula(new CalculateParameter()
				{
					// 開始時間
					MaximumLimit = GetStartTime(p, out Time startTime),
					// 結束時間
					MinimumLimit = GetElapsedTime(p, out Time elapsedTime)
				});
				endTime = formula.Answer.ToTime();

				TimeCalculationFormula timeCalculationFormula = new TimeCalculationFormula()
				{
					ElapsedTime = elapsedTime,
					StartTime = startTime,
					EndTime = endTime,
					Sign = sign,
					// 對在等式中的三個數值隨機產生填空項（用於填空題型）
					Gap = (p.QuestionType == QuestionType.GapFilling) ? CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Answer) : GapFilling.Answer
				};
				// 判定是否需要反推并重新作成計算式
				if (CheckIsNeedInverseMethod(p.Formulas, timeCalculationFormula))
				{
					i--;
					continue;
				}
				// 計算式作成
				p.Formulas.Add(timeCalculationFormula);
			}
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			TimeCalculationParameter p = parameter as TimeCalculationParameter;

			// 標準題型
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 算式作成（指定單個運算符實例）
				MarkFormulaList(p, () => { return p.Signs[0]; });
			}
			else
			{
				// 算式作成（之前之後運算符實例隨機抽取）
				MarkFormulaList(p, () => { return CommonUtil.GetRandomNumber(p.Signs.ToList()); });
			}
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1:時分秒全零
		/// </remarks>
		/// <param name="preFormulas">已得到的算式</param>
		/// <param name="currentFormula">當前算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(IList<TimeCalculationFormula> preFormulas, TimeCalculationFormula currentFormula)
		{
			// 防止出現全零的情況
			if (currentFormula.ElapsedTime.Hours == 0 && currentFormula.ElapsedTime.Minutes == 0 && currentFormula.ElapsedTime.Seconds == 0)
			{
				return true;
			}
			return false;
		}

	}
}
