using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.ComputingConnection.Item;
using MyMathSheets.ComputationalStrategy.ComputingConnection.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.ComputingConnection.Main.Strategy
{
	/// <summary>
	/// 等式接龍題型（當前版本實現了加減法接龍）
	/// </summary>
	[Operation("ComputingConnection")]
	public class ComputingConnection : TopicBase
	{
		/// <summary>
		/// 反推判定次數（如果大於五次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 5;

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(ComputingConnectionParameter p, Func<SignOfOperation> signFunc)
		{
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;

			// 按照指定數量作成相應的數學計算式
			for (int i = 0; i < p.NumberOfQuestions; i++)
			{
				Formula previousFormula = null;
				IList<Formula> formulas = new List<Formula>();
				// 随机获取接龙层数
				for (int j = 0; j < p.SectionNumber; j++)
				{
					// 指定運算符實例作成
					IArithmetic strategy = CalculateManager(signFunc());
					Formula formula = strategy.CreateFormula(new ArithmeticParameter()
					{
						MaximumLimit = p.MaximumLimit,
						QuestionType = QuestionType.Default,
						MinimumLimit = 0
					}, previousFormula);

					if (CheckIsNeedInverseMethod(p.Formulas, formula, p.MaximumLimit))
					{
						defeated++;
						// 如果大於五次則認為此題無法作成繼續下一題
						if (defeated == INVERSE_NUMBER)
						{
							// 當前反推判定次數復原
							defeated = 0;
							continue;
						}
						j--;
						continue;
					}
					previousFormula = formula;

					formulas.Add(formula);
				}
				// 當前反推判定次數復原
				defeated = 0;

				p.Formulas.Add(new ConnectionFormula() { ConfixFormulas = formulas, ConfixNumber = formulas.Count });
			}
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(TopicParameterBase parameter)
		{
			ComputingConnectionParameter p = parameter as ComputingConnectionParameter;

			// 算式作成
			MarkFormulaList(p, () => { return CommonUtil.GetRandomNumber(p.Signs.ToList()); });
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：算式存在一致
		/// 情況2：有零的情況
		/// 情況3：大於等於設定的計算結果最大值情況
		/// 情況4：等於設定的計算結果最小值情況
		/// </remarks>
		/// <param name="preFormulas">已得到的算式</param>
		/// <param name="currentFormula">當前算式</param>
		/// <param name="maximumLimit">計算結果最大值</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(IList<ConnectionFormula> preFormulas, Formula currentFormula, int maximumLimit)
		{
			// 等於設定的計算結果最小值情況
			if (currentFormula.Answer == 0)
			{
				return true;
			}

			// 大於等於設定的計算結果最大值情況
			if (currentFormula.Answer >= maximumLimit)
			{
				return true;
			}

			// 有零的情況
			if (currentFormula.LeftParameter == 0 || currentFormula.RightParameter == 0 || currentFormula.Answer == 0)
			{
				return true;
			}
			// 算式存在一致
			preFormulas.ToList().Any(d =>
			{
				if (d.ConfixFormulas.ToList().Any(m => m.LeftParameter == currentFormula.LeftParameter
					&& m.RightParameter == currentFormula.RightParameter
					&& m.Answer == currentFormula.Answer
					&& m.Sign == currentFormula.Sign))
				{
					return true;
				};
				return false;
			});
			return false;
		}
	}
}