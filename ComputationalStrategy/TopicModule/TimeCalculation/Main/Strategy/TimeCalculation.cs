using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Item;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Main.Parameters;
using MyMathSheets.ComputationalStrategy.TimeCalculation.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.TimeCalculation.Main.Strategy
{
	/// <summary>
	/// 時鐘學習板
	/// </summary>
	[Operation(LayoutSetting.Preview.TimeCalculation)]
	public class TimeCalculation : OperationBase
	{
		/// <summary>
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 3;

		/// <summary>
		/// 指定分鐘數值
		/// </summary>
		private readonly Dictionary<HourDivision, int> _assignMinutes;

		/// <summary>
		/// 存儲貨幣轉換的實現方法集合
		/// </summary>
		private readonly Dictionary<TimeIntervalType, Action<TimeCalculationFormula, FourOperationsType>> _timeIntervals =
			new Dictionary<TimeIntervalType, Action<TimeCalculationFormula, FourOperationsType>>();

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

			// 時間段類型-午夜
			_timeIntervals[TimeIntervalType.Midnight] = MidnightTimeInterval;
			// 時間段類型-凌晨
			_timeIntervals[TimeIntervalType.WeeHours] = WeeHoursTimeInterval;
			// 時間段類型-上午
			_timeIntervals[TimeIntervalType.Forenoon] = ForenoonTimeInterval;
			// 時間段類型-中午
			_timeIntervals[TimeIntervalType.Nooning] = NooningTimeInterval;
			// 時間段類型-下午
			_timeIntervals[TimeIntervalType.Afternoon] = AfternoonTimeInterval;
			// 時間段類型-晚上
			_timeIntervals[TimeIntervalType.Night] = NightTimeInterval;
			// 時間段類型-深夜
			_timeIntervals[TimeIntervalType.LateNight] = LateNightTimeInterval;
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			TimeCalculationParameter p = parameter as TimeCalculationParameter;

			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 隨機獲取時間段類型中的一個類型
				TimeIntervalType type = CommonUtil.GetRandomNumber(TimeIntervalType.Midnight, TimeIntervalType.LateNight);
				TimeCalculationFormula formula = new TimeCalculationFormula() { TimeInterval = type };
				if (_timeIntervals.TryGetValue(type, out Action<TimeCalculationFormula, FourOperationsType> timeInterval))
				{
					timeInterval(formula, p.FourOperationsType);
					if (p.IsShowSeconds)
					{
						formula.Seconds = 0;
					}
				}
				else
				{
					throw new ArgumentException(MessageUtil.GetException(() => MsgResources.E0002L, type.ToString()));
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
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1:完全一致
		/// 情況2:時分秒全零
		/// </remarks>
		/// <param name="preFormulas">已得到的算式</param>
		/// <param name="currentFormula">當前算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(IList<TimeCalculationFormula> preFormulas, TimeCalculationFormula currentFormula)
		{
			// 防止出現全零的情況
			if (currentFormula.Hours == 0 && currentFormula.Minutes == 0 && currentFormula.Seconds == 0)
			{
				return true;
			}

			// 判斷當前算式是否已經出現過
			if (preFormulas.ToList().Any(d => d.Hours == currentFormula.Hours && d.Minutes == currentFormula.Minutes))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 設定計時制（AM/PM）
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		private void SetTimeType(TimeCalculationFormula formula)
		{
			if (!formula.Hours.HasValue || !formula.Minutes.HasValue)
			{
				throw new ArgumentNullException(MessageUtil.GetException(() => MsgResources.E0001L));
			}

			// 上午[00:01~12:00]
			if ((0 < formula.Hours && formula.Hours < 12)
				|| (formula.Hours == 12 && formula.Minutes == 0)
				|| (formula.Hours == 0 && formula.Minutes >= 1))
			{
				formula.TimeType = TimeSystem.AM;
			}
			else
			{
				formula.TimeType = TimeSystem.PM;
			}
		}

		/// <summary>
		/// 時間段類型-午夜
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void MidnightTimeInterval(TimeCalculationFormula formula, FourOperationsType type)
		{
			// 午夜[0:XX]
			formula.Hours = 0;
			// 分鐘數值
			formula.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)];
			// 秒數值
			formula.Seconds = CommonUtil.GetRandomNumber(0, 59);
			// 設定計時制（AM/PM）
			SetTimeType(formula);
		}

		/// <summary>
		/// 時間段類型-凌晨
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void WeeHoursTimeInterval(TimeCalculationFormula formula, FourOperationsType type)
		{
			// 凌晨[1:XX~5:XX]
			formula.Hours = CommonUtil.GetRandomNumber(1, 5);
			// 分鐘數值
			formula.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)];
			// 秒數值
			formula.Seconds = CommonUtil.GetRandomNumber(0, 59);
			// 設定計時制（AM/PM）
			SetTimeType(formula);
		}

		/// <summary>
		/// 時間段類型-上午
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void ForenoonTimeInterval(TimeCalculationFormula formula, FourOperationsType type)
		{
			// 上午[6:XX~11:XX]
			formula.Hours = CommonUtil.GetRandomNumber(6, 11);
			// 分鐘數值
			formula.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)];
			// 秒數值
			formula.Seconds = CommonUtil.GetRandomNumber(0, 59);
			// 設定計時制（AM/PM）
			SetTimeType(formula);
		}

		/// <summary>
		/// 時間段類型-中午
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void NooningTimeInterval(TimeCalculationFormula formula, FourOperationsType type)
		{
			// 中午[12:XX]
			formula.Hours = 12;
			// 分鐘數值
			formula.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)];
			// 秒數值
			formula.Seconds = CommonUtil.GetRandomNumber(0, 59);
			// 設定計時制（AM/PM）
			SetTimeType(formula);
		}

		/// <summary>
		/// 時間段類型-下午
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void AfternoonTimeInterval(TimeCalculationFormula formula, FourOperationsType type)
		{
			// 下午[13:XX~18:XX]
			formula.Hours = CommonUtil.GetRandomNumber(13, 18);
			// 分鐘數值
			formula.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)];
			// 秒數值
			formula.Seconds = CommonUtil.GetRandomNumber(0, 59);
			// 設定計時制（AM/PM）
			SetTimeType(formula);
		}

		/// <summary>
		/// 時間段類型-晚上
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void NightTimeInterval(TimeCalculationFormula formula, FourOperationsType type)
		{
			// 晚上[19:XX~21:XX]
			formula.Hours = CommonUtil.GetRandomNumber(19, 21);
			// 分鐘數值
			formula.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)];
			// 秒數值
			formula.Seconds = CommonUtil.GetRandomNumber(0, 59);
			// 設定計時制（AM/PM）
			SetTimeType(formula);
		}

		/// <summary>
		/// 時間段類型-深夜
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void LateNightTimeInterval(TimeCalculationFormula formula, FourOperationsType type)
		{
			// 深夜[22:XX~23:XX]
			formula.Hours = CommonUtil.GetRandomNumber(22, 23);
			// 分鐘數值
			formula.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)];
			// 秒數值
			formula.Seconds = CommonUtil.GetRandomNumber(0, 59);
			// 設定計時制（AM/PM）
			SetTimeType(formula);
		}
	}
}
