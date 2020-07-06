using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.SchoolClock.Item;
using MyMathSheets.ComputationalStrategy.SchoolClock.Main.Parameters;
using MyMathSheets.ComputationalStrategy.SchoolClock.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.SchoolClock.Main.Strategy
{
	/// <summary>
	/// 時鐘學習板
	/// </summary>
	[Operation("SchoolClock")]
	public class SchoolClock : OperationBase
	{
		/// <summary>
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 3;

		/// <summary>
		/// 指定分鐘數值
		/// </summary>
		private readonly Dictionary<HourDivisionType, int> _assignMinutes;

		/// <summary>
		/// 存儲貨幣轉換的實現方法集合
		/// </summary>
		private readonly Dictionary<TimeIntervalType, Action<SchoolClockFormula, FourOperationsType>> _timeIntervals =
			new Dictionary<TimeIntervalType, Action<SchoolClockFormula, FourOperationsType>>();

		/// <summary>
		/// 構造函數
		/// </summary>
		public SchoolClock()
		{
			// 指定分鐘數值（0、15、30、45分鐘）
			_assignMinutes = new Dictionary<HourDivisionType, int>() {
				{ HourDivisionType.IntegralPoint, 0 },
				{ HourDivisionType.Quarter, 15 },
				{ HourDivisionType.Half, 30 },
				{ HourDivisionType.ThreeQuarters, 45 }
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
			SchoolClockParameter p = parameter as SchoolClockParameter;

			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				// 隨機獲取時間段類型中的一個類型
				TimeIntervalType type = CommonUtil.GetRandomNumber(TimeIntervalType.Midnight, TimeIntervalType.LateNight);
				SchoolClockFormula formula = new SchoolClockFormula
				{
					LatestTime = new TimeType() { Hours = 0, Minutes = 0, Seconds = 0 }
				};
				if (_timeIntervals.TryGetValue(type, out Action<SchoolClockFormula, FourOperationsType> timeInterval))
				{
					timeInterval(formula, p.FourOperationsType);
					if (p.IsShowSeconds)
					{
						formula.LatestTime.Seconds = 0;
					}
				}
				else
				{
					throw new ArgumentException(MessageUtil.GetMessage(() => MsgResources.E0002L, type.ToString()));
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
		private bool CheckIsNeedInverseMethod(IList<SchoolClockFormula> preFormulas, SchoolClockFormula currentFormula)
		{
			// 防止出現全零的情況
			if (currentFormula.LatestTime.Hours == 0 && currentFormula.LatestTime.Minutes == 0 && currentFormula.LatestTime.Seconds == 0)
			{
				return true;
			}

			// 判斷當前算式是否已經出現過
			if (preFormulas.ToList().Any(d => d.LatestTime.Hours == currentFormula.LatestTime.Hours && d.LatestTime.Minutes == currentFormula.LatestTime.Minutes))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 時間段類型-午夜
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void MidnightTimeInterval(SchoolClockFormula formula, FourOperationsType type)
		{
			// 午夜[0:XX]
			formula.LatestTime.Hours = 0;
			// 分鐘數值
			formula.LatestTime.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivisionType.IntegralPoint, HourDivisionType.ThreeQuarters)];
			// 秒數值
			formula.LatestTime.Seconds = CommonUtil.GetRandomNumber(0, 59);
		}

		/// <summary>
		/// 時間段類型-凌晨
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void WeeHoursTimeInterval(SchoolClockFormula formula, FourOperationsType type)
		{
			// 凌晨[1:XX~5:XX]
			formula.LatestTime.Hours = CommonUtil.GetRandomNumber(1, 5);
			// 分鐘數值
			formula.LatestTime.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivisionType.IntegralPoint, HourDivisionType.ThreeQuarters)];
			// 秒數值
			formula.LatestTime.Seconds = CommonUtil.GetRandomNumber(0, 59);
		}

		/// <summary>
		/// 時間段類型-上午
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void ForenoonTimeInterval(SchoolClockFormula formula, FourOperationsType type)
		{
			// 上午[6:XX~11:XX]
			formula.LatestTime.Hours = CommonUtil.GetRandomNumber(6, 11);
			// 分鐘數值
			formula.LatestTime.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivisionType.IntegralPoint, HourDivisionType.ThreeQuarters)];
			// 秒數值
			formula.LatestTime.Seconds = CommonUtil.GetRandomNumber(0, 59);
		}

		/// <summary>
		/// 時間段類型-中午
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void NooningTimeInterval(SchoolClockFormula formula, FourOperationsType type)
		{
			// 中午[12:XX]
			formula.LatestTime.Hours = 12;
			// 分鐘數值
			formula.LatestTime.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivisionType.IntegralPoint, HourDivisionType.ThreeQuarters)];
			// 秒數值
			formula.LatestTime.Seconds = CommonUtil.GetRandomNumber(0, 59);
		}

		/// <summary>
		/// 時間段類型-下午
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void AfternoonTimeInterval(SchoolClockFormula formula, FourOperationsType type)
		{
			// 下午[13:XX~18:XX]
			formula.LatestTime.Hours = CommonUtil.GetRandomNumber(13, 18);
			// 分鐘數值
			formula.LatestTime.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivisionType.IntegralPoint, HourDivisionType.ThreeQuarters)];
			// 秒數值
			formula.LatestTime.Seconds = CommonUtil.GetRandomNumber(0, 59);
		}

		/// <summary>
		/// 時間段類型-晚上
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void NightTimeInterval(SchoolClockFormula formula, FourOperationsType type)
		{
			// 晚上[19:XX~21:XX]
			formula.LatestTime.Hours = CommonUtil.GetRandomNumber(19, 21);
			// 分鐘數值
			formula.LatestTime.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivisionType.IntegralPoint, HourDivisionType.ThreeQuarters)];
			// 秒數值
			formula.LatestTime.Seconds = CommonUtil.GetRandomNumber(0, 59);
		}

		/// <summary>
		/// 時間段類型-深夜
		/// </summary>
		/// <param name="formula">答題結果參數</param>
		/// <param name="type">指定分鐘數值</param>
		private void LateNightTimeInterval(SchoolClockFormula formula, FourOperationsType type)
		{
			// 深夜[22:XX~23:XX]
			formula.LatestTime.Hours = CommonUtil.GetRandomNumber(22, 23);
			// 分鐘數值
			formula.LatestTime.Minutes = type == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
										: _assignMinutes[CommonUtil.GetRandomNumber(HourDivisionType.IntegralPoint, HourDivisionType.ThreeQuarters)];
			// 秒數值
			formula.LatestTime.Seconds = CommonUtil.GetRandomNumber(0, 59);
		}
	}
}