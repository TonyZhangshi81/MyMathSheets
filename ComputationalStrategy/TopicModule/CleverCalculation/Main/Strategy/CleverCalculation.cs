using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Main.Helper;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Item;
using MyMathSheets.ComputationalStrategy.CleverCalculation.Main.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyMathSheets.ComputationalStrategy.CleverCalculation.Main.Strategy
{
	/// <summary>
	/// 巧算
	/// </summary>
	[Topic("CleverCalculation")]
	public class CleverCalculation : TopicBase<CleverCalculationParameter>
	{
		/// <summary>
		/// 巧算的實現方法集合
		/// </summary>
		private readonly Dictionary<TopicType, Action<IList<CleverCalculationFormula>>> _calculations = new Dictionary<TopicType, Action<IList<CleverCalculationFormula>>>();

		/// <summary>
		/// <see cref="CleverCalculation"/>構造函數
		/// </summary>
		[ImportingConstructor]
		public CleverCalculation()
		{
			// 加法巧算
			_calculations[TopicType.Plus] = CleverWithPlus;
			// 減法巧算
			_calculations[TopicType.Subtraction] = CleverWithSubtraction;
			// 乘法巧算
			_calculations[TopicType.Multiple] = CleverWithMultiple;
			// 混合題型巧算(拆解)
			_calculations[TopicType.SyntheticUnknit] = CleverWithSyntheticUnknit;
			// 混合題型巧算(合併)
			_calculations[TopicType.SyntheticCombine] = CleverWithSyntheticCombine;
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="topicTypeFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(CleverCalculationParameter p, Func<TopicType> topicTypeFunc)
		{
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				if (_calculations.TryGetValue(topicTypeFunc(), out Action<IList<CleverCalculationFormula>> calculation))
				{
					calculation(p.Formulas);
				}
			}
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		public override void MarkFormulaList(CleverCalculationParameter p)
		{
			// 算式作成
			MarkFormulaList(p, () => { return (TopicType)CommonUtil.GetRandomNumber(p.TopicTypes.ToList()); });

			// 智能提示作成
			VirtualHelperBase<CleverCalculationFormula> helper = new CleverCalculationDialogue();
			p.BrainpowerHint = helper.CreateHelperDialogue(p.Formulas);
		}

		/// <summary>
		/// 加法巧算
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 計算式構成樣例： 57 + 78 = (?) + (?) = (?)
		/// 結果列：135
		/// </remarks>
		protected virtual void CleverWithPlus(IList<CleverCalculationFormula> formulas)
		{
			CleverCalculationFormula cleverCalculation = new CleverCalculationFormula
			{
				Type = TopicType.Plus,
				ConfixFormulas = new List<Formula>(),
				Answer = new List<int>()
			};

			// 獲取兩個隨機非整數
			var left = CommonUtil.GetRandomNumber(10, 99, _ => _ % 10 != 0);
			var right = CommonUtil.GetRandomNumber(10, 99, _ => _ % 10 != 0 && _ != left);
			var answer = left + right;
			cleverCalculation.ConfixFormulas.Add(new Formula(left, right, answer, SignOfOperation.Plus));
			cleverCalculation.Answer.Add(answer);

			// 數字按照位數分解
			int[] lefts = new int[2] { left / 10, left % 10 };
			int[] rights = new int[2] { right / 10, right % 10 };
			// 距離整數的差異值
			int leftDiff = lefts[1] >= 5 ? 10 - lefts[1] : lefts[1];
			int rightDiff = rights[1] >= 5 ? 10 - rights[1] : rights[1];

			// eg: 58 + 57 => 60 + 59
			// eg: 58 + 12 => 60 + 10
			// eg: 55 + 25 => 60 + 20 || 50 + 30
			if (lefts[1] >= 5 && (leftDiff < rightDiff || lefts[1] + rights[1] == 10))
			{
				// 左邊的值向上化整
				cleverCalculation.ConfixFormulas.Add(new Formula(left + leftDiff, right - leftDiff, answer, SignOfOperation.Plus));
				// 左右兩邊的值向上化整
				if (lefts[1] == 5 && rights[1] == 5)
				{
					cleverCalculation.ConfixFormulas.Add(new Formula(left - leftDiff, right + leftDiff, answer, SignOfOperation.Plus));
				}
			}
			// eg: 58 + 48 => 60 + 46 || 56 + 50
			else if (lefts[1] > 5 && lefts[1] == rights[1])
			{
				// 左右兩邊的值向上化整
				cleverCalculation.ConfixFormulas.Add(new Formula(left + leftDiff, right - leftDiff, answer, SignOfOperation.Plus));
				cleverCalculation.ConfixFormulas.Add(new Formula(left - rightDiff, right + rightDiff, answer, SignOfOperation.Plus));
			}
			// eg: 57 + 69 => 56 + 70
			// eg: 57 + 61 => 58 + 60
			// eg: 55 + 21 => 56 + 20
			// eg: 55 + 28 => 57 + 30
			else if (lefts[1] >= 5 && leftDiff > rightDiff)
			{
				if (rights[1] >= 5)
				{
					// 右邊的值向上化整
					cleverCalculation.ConfixFormulas.Add(new Formula(left - rightDiff, right + rightDiff, answer, SignOfOperation.Plus));
				}
				else
				{
					// 右邊的值向下化整
					cleverCalculation.ConfixFormulas.Add(new Formula(left + rightDiff, right - rightDiff, answer, SignOfOperation.Plus));
				}
			}
			// eg: 51 + 52 => 50 + 53
			// eg: 51 + 59 => 50 + 60
			// eg: 55 + 59 => 54 + 60
			else if (lefts[1] <= 5 && (leftDiff < rightDiff || lefts[1] + rights[1] == 10))
			{
				// 左邊的值向下化整
				cleverCalculation.ConfixFormulas.Add(new Formula(left - lefts[1], right + lefts[1], answer, SignOfOperation.Plus));
			}
			// eg: 51 + 41 => 50 + 42 || 52 + 40
			else if (lefts[1] < 5 && lefts[1] == rights[1])
			{
				// 左右兩邊的值向下化整
				cleverCalculation.ConfixFormulas.Add(new Formula(left - lefts[1], right + lefts[1], answer, SignOfOperation.Plus));
				cleverCalculation.ConfixFormulas.Add(new Formula(left + lefts[1], right - rights[1], answer, SignOfOperation.Plus));
			}
			// eg: 54 + 61 => 55 + 60
			// eg: 54 + 69 => 53 + 70
			// eg: 55 + 33 => 58 + 30
			else if (lefts[1] <= 5 && leftDiff > rightDiff)
			{
				if (rights[1] >= 5)
				{
					// 右邊的值向上化整
					cleverCalculation.ConfixFormulas.Add(new Formula(left - rightDiff, right + rightDiff, answer, SignOfOperation.Plus));
				}
				else
				{
					// 右邊的值向下化整
					cleverCalculation.ConfixFormulas.Add(new Formula(left + rights[1], right - rights[1], answer, SignOfOperation.Plus));
				}
			}
			// eg: 55 + 25 => 50 + 30 || 60 + 20
			else if (lefts[1] == 5 && rights[1] == 5)
			{
				// 左右兩邊的值均可向下或者向下化整
				cleverCalculation.ConfixFormulas.Add(new Formula(left - 5, right + 5, answer, SignOfOperation.Plus));
				cleverCalculation.ConfixFormulas.Add(new Formula(left + 5, right - 5, answer, SignOfOperation.Plus));
			}

			formulas.Add(cleverCalculation);
		}

		/// <summary>
		/// 減法巧算
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		protected virtual void CleverWithSubtraction(IList<CleverCalculationFormula> formulas)
		{
			CleverCalculationFormula cleverCalculation = new CleverCalculationFormula
			{
				Type = TopicType.Plus,
				ConfixFormulas = new List<Formula>(),
				Answer = new List<int>()
			};

			// 獲取兩個隨機非整數
			// 減數的個位是大於等於3的兩位數
			var right = CommonUtil.GetRandomNumber(20, 60, _ => _ % 10 != 0 && _ % 10 >= 3);
			// 被減數需要滿足借位計算
			var left = CommonUtil.GetRandomNumber(50, 99, _ => _ % 10 != 0 && _ > right && _ % 10 < right % 10);
			var answer = left - right;
			cleverCalculation.ConfixFormulas.Add(new Formula(left, right, answer, SignOfOperation.Subtraction));
			cleverCalculation.Answer.Add(answer);

			// 數字按照位數分解
			int[] lefts = new int[2] { left / 10, left % 10 };
			int[] rights = new int[2] { right / 10, right % 10 };
			// 距離整數的差異值
			int leftDiff = lefts[1] >= 5 ? 10 - lefts[1] : lefts[1];
			int rightDiff = rights[1] >= 5 ? 10 - rights[1] : rights[1];

			// eg: 51 - 19 = 50 - 18 || 52 - 20
			if (lefts[1] + rights[1] == 10)
			{
				cleverCalculation.ConfixFormulas.Add(new Formula(left - leftDiff, right - leftDiff, answer, SignOfOperation.Subtraction));
				cleverCalculation.ConfixFormulas.Add(new Formula(left + rightDiff, right + rightDiff, answer, SignOfOperation.Subtraction));
			}
			// eg: 51 - 12 = 50 - 11
			else if (leftDiff < rightDiff)
			{
				cleverCalculation.ConfixFormulas.Add(new Formula(left - leftDiff, right - leftDiff, answer, SignOfOperation.Subtraction));
			}
			// eg: 53 - 19 = 54 - 20
			else if (leftDiff > rightDiff)
			{
				cleverCalculation.ConfixFormulas.Add(new Formula(left + rightDiff, right + rightDiff, answer, SignOfOperation.Subtraction));
			}

			formulas.Add(cleverCalculation);
		}

		/// <summary>
		/// 乘法巧算
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 計算式構成樣例： 36 = (?) X 6 = (?) X 2 = 4 X (?)
		/// 結果列：6, 18, 9
		/// </remarks>
		protected virtual void CleverWithMultiple(IList<CleverCalculationFormula> formulas)
		{
			CleverCalculationFormula cleverCalculation = new CleverCalculationFormula
			{
				Type = TopicType.Multiple,
				ConfixFormulas = new List<Formula>(),
				Answer = new List<int>()
			};

			// 乘法算式序列（取得隨機數後執行Check回調預判其是否可用）
			var list = GetMultipleSyntagmaticOrdering(formulas);

			int seq = 1;
			// 從乘法算式序列中隨機選擇3個算式（不重複）
			while (seq <= 3)
			{
				var formula = CommonUtil.GetRandomNumber(list);
				cleverCalculation.Answer.Add(formula.Gap == GapFilling.Left ? formula.LeftParameter : formula.RightParameter);
				cleverCalculation.ConfixFormulas.Add(formula);
				list.Remove(formula);

				seq++;
			}

			formulas.Add(cleverCalculation);
		}

		/// <summary>
		/// 獲取隨機值的乘法序列集合
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <returns>乘法序列集合</returns>
		/// <remarks>
		/// 36 => [1*36,2*18,3*12,4*9,6*6]
		/// 當左邊因數大於右邊因數時停止搜集
		/// </remarks>
		private List<Formula> GetMultipleSyntagmaticOrdering(IList<CleverCalculationFormula> formulas)
		{
			List<Formula> list = new List<Formula>();

			// 隨機數取得後的回調函數
			var answerRandomCallback = new Func<int, bool>(answer =>
			{
				return !formulas.ToList().Any(d => d.ConfixFormulas.Any(dd => dd.Answer == answer));
			});

			while (1 == 1)
			{
				list.Clear();

				// 獲取一個隨機數
				int answer = CommonUtil.GetRandomNumber(10, 99, answerRandomCallback);

				var left = 2;
				while (left <= answer)
				{
					if (answer % left == 0)
					{
						int right = answer / left;
						if (left > right)
						{
							break;
						}

						var formula = new Formula(left, right, answer, SignOfOperation.Multiple, () => { return CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Right); });
						LogUtil.LogCalculate(formula);
						list.Add(formula);
					}
					left++;
				}

				if (list.Count >= 4)
				{
					break;
				}
			}

			return list;
		}

		/// <summary>
		/// 綜合題型巧算（拆解）
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		protected virtual void CleverWithSyntheticUnknit(IList<CleverCalculationFormula> formulas)
		{
			CleverCalculationFormula cleverCalculation = new CleverCalculationFormula
			{
				Type = TopicType.SyntheticUnknit,
				ConfixFormulas = new List<Formula>(),
				Answer = new List<int>()
			};

			while (1 == 1)
			{
				cleverCalculation.ConfixFormulas.Clear();
				cleverCalculation.Answer.Clear();

				// 獲取一個待拆解的非整數(不包含各位為5的數)
				int unknitValue = CommonUtil.GetRandomNumber(10, 60, _ => _ % 10 != 0 && _ % 10 != 5);
				// 獲取一個10以內的因數
				int factor = CommonUtil.GetRandomNumber(3, 9, _ => _ != unknitValue % 10);

				// 數字按照位數分解
				int[] unknits = new int[2] { unknitValue / 10, unknitValue % 10 };
				// 距離整數的差異值
				int unknitDiff = unknits[1] >= 5 ? 10 - unknits[1] : unknits[1];

				// 結果值
				int answer = unknitValue * factor;
				// 用於設定待拆解因數的位置
				LeftOrRight leftOrRight = CommonUtil.GetRandomNumber(LeftOrRight.Left, LeftOrRight.Right);
				cleverCalculation.Answer.Add(answer);
				if (leftOrRight == LeftOrRight.Left)
				{
					cleverCalculation.ConfixFormulas.Add(new Formula(unknitValue, factor, answer, SignOfOperation.Multiple));
				}
				else
				{
					cleverCalculation.ConfixFormulas.Add(new Formula(factor, unknitValue, answer, SignOfOperation.Multiple));
				}

				StringBuilder express = new StringBuilder();
				// 加法拆解
				if (unknits[1] < 5)
				{
					// eg: 12 * 5 = 10 * 5 + 2 * 5
					express.AppendFormat(CultureInfo.CurrentCulture, "{0}*{1}+{2}*{3}", unknits[0], factor, unknits[1], factor);
				}
				// 減法拆解
				else
				{
					// eg: 18 * 5 = 20 * 5 - 2 * 5
					express.AppendFormat(CultureInfo.CurrentCulture, "{0}*{1}-{2}*{3}", unknitValue + unknitDiff, factor, unknitDiff, factor);
				}

				// 計算式推導
				var calc = new ExpressArithmeticUtil();
				int result;
				if (!calc.IsResult(express.ToString(), out result))
				{
					continue;
				}
				if (result != answer)
				{
					continue;
				}
				// 加入推導出計算式集合
				calc.Formulas.ToList().ForEach(f => cleverCalculation.ConfixFormulas.Add(f));

				break;
			}

			formulas.Add(cleverCalculation);
		}

		/// <summary>
		/// 綜合題型巧算（合併）
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		protected virtual void CleverWithSyntheticCombine(IList<CleverCalculationFormula> formulas)
		{
		}
	}
}