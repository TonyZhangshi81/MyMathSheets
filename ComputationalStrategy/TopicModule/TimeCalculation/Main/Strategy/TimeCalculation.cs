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
	/// 時間計算
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
		private int GetStartTime(TimeCalculationParameter p, Time startTime)
		{
			Time time = new Time
			{
				// 小時數
				Hours = CommonUtil.GetRandomNumber(0, 24),
				// 分鐘數
				Minutes = p.FourOperationsType == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
							: _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)],
				// 秒數
				Seconds = CommonUtil.GetRandomNumber(0, 59)
			};
			startTime = time;
			// 時間轉換為秒數
			return time.ToSeconds();
		}

		/// <summary>
		/// 隨機取得開始時間(秒數)
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="elapsedTime">經過時間</param>
		/// <returns>經過時間(秒數)</returns>
		private int GetElapsedTime(TimeCalculationParameter p, Time elapsedTime)
		{
			Time time = new Time
			{
				// 小時數
				Hours = CommonUtil.GetRandomNumber(0, 24),
				// 分鐘數
				Minutes = p.FourOperationsType == FourOperationsType.Random ? CommonUtil.GetRandomNumber(0, 59)
							: _assignMinutes[CommonUtil.GetRandomNumber(HourDivision.IntegralPoint, HourDivision.ThreeQuarters)],
				// 秒數
				Seconds = CommonUtil.GetRandomNumber(0, 59)
			};
			elapsedTime = time;
			// 時間轉換為秒數
			return time.ToSeconds();
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
				// 開始時間
				Time startTime = new Time();
				// 結束時間
				Time endTime = new Time();
				// 經過時間
				Time elapsedTime = new Time();

				strategy = CalculateManager(signFunc());
				Formula formula = strategy.CreateFormula(new CalculateParameter()
				{
					MaximumLimit = GetStartTime(p, startTime),
					QuestionType = p.QuestionType,
					MinimumLimit = GetElapsedTime(p, elapsedTime)
				});
				endTime = formula.Answer.ToTime();



			}
		}


		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			TimeCalculationParameter p = parameter as TimeCalculationParameter;

			// 按照指定數量作成相應的數學計算式
			for (int i = 0; i < p.NumberOfQuestions; i++)
			{

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
			if (currentFormula.ElapsedTime.Hours == 0 && currentFormula.ElapsedTime.Minutes == 0 && currentFormula.ElapsedTime.Seconds == 0)
			{
				return true;
			}

			// 判斷當前算式是否已經出現過
			if (preFormulas.ToList().Any(d => d.ElapsedTime.Hours == currentFormula.ElapsedTime.Hours && d.ElapsedTime.Minutes == currentFormula.ElapsedTime.Minutes))
			{
				return true;
			}
			return false;
		}

	}
}
