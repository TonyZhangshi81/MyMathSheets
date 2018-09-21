using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 等式接龍題型
	/// </summary>
	[Operation(LayoutSetting.Preview.ComputingConnection)]
	public class ComputingConnection : OperationBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public override void MarkFormulaList(ParameterBase parameter)
		{
			ComputingConnectionParameter p = parameter as ComputingConnectionParameter;

			ICalculate strategy = null;
			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 考虑乘除法接龙答题结果会超出限制,所以只随机加减法策略
				if (p.Signs[0] == SignOfOperation.Division || p.Signs[0] == SignOfOperation.Multiple)
				{
					return;
				}

				// 指定單個運算符實例
				strategy = CalculateManager(p.Signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					Formula previousFormula = null;
					IList<Formula> formulas = new List<Formula>();
					// 随机获取接龙层数
					int sectionNumber = GetSectionNumber();
					for (var j = 0; j < sectionNumber; j++)
					{
						Formula formula = strategy.CreateFormula(p.MaximumLimit, previousFormula, p.QuestionType);
						if (CheckIsNeedInverseMethod(p.Formulas, formula))
						{
							i--;
							continue;
						}
						previousFormula = formula;

						formulas.Add(formula);
					}
					p.Formulas.Add(new ConnectionFormula() { ConfixFormulas = formulas, ConfixNumber = formulas.Count });
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					Formula previousFormula = null;
					IList<Formula> formulas = new List<Formula>();

					// 随机获取接龙层数
					int sectionNumber = GetSectionNumber();
					for (var j = 0; j < sectionNumber; j++)
					{
						RandomNumberComposition random = new RandomNumberComposition(0, (int)SignOfOperation.Subtraction);
						// 混合題型（加減運算符實例隨機抽取） 注意:考虑乘除法接龙答题结果会超出限制,所以只随机加减法策略
						SignOfOperation sign = p.Signs[random.GetRandomNumber()];
						// 對四則運算符實例進行cache管理
						strategy = CalculateManager(sign);

						Formula formula = strategy.CreateFormula(p.MaximumLimit, previousFormula, p.QuestionType);

						if (CheckIsNeedInverseMethod(p.Formulas, formula))
						{
							i--;
							continue;
						}

						previousFormula = formula;

						formulas.Add(formula);
					}
					p.Formulas.Add(new ConnectionFormula() { ConfixFormulas = formulas, ConfixNumber = formulas.Count });
				}
			}
		}

		/// <summary>
		/// 连等式总节数(预设3~5随机抽取)
		/// </summary>
		/// <returns></returns>
		private int GetSectionNumber()
		{
			RandomNumberComposition number = new RandomNumberComposition(3, 5);
			return number.GetRandomNumber();
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：算式存在一致
		/// 情況2：全零的情況
		/// </remarks>
		/// <param name="preFormulas">已得到的算式</param>
		/// <param name="currentFormula">當前算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(IList<ConnectionFormula> preFormulas, Formula currentFormula)
		{
			// 全零的情況
			if (currentFormula.LeftParameter == 0 && currentFormula.RightParameter == 0 && currentFormula.Answer == 0)
			{
				return true;
			}
			// 算式存在一致
			preFormulas.ToList().Any(d => {
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
