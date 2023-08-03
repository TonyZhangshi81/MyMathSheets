using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Main.Helper;
using MyMathSheets.ComputationalStrategy.ArithmeticOperations.Item;
using MyMathSheets.ComputationalStrategy.ArithmeticOperations.Main.Parameters;
using System;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Arithmetic.Main.Strategy
{
	/// <summary>
	/// 四則遠算題
	/// </summary>
	[Topic("ArithmeticOperations")]
	public class ArithmeticOperations : TopicBase<ArithmeticOperationsParameter>
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
		private void MarkFormulaList(ArithmeticOperationsParameter p, Func<SignOfOperation> signFunc)
		{
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;

			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				IArithmetic strategy = CalculateManager(signFunc());
				Formula formula = strategy.CreateFormula(new ArithmeticParameter()
				{
					MaximumLimit = p.MaximumLimit,
					QuestionType = p.QuestionType,
					MinimumLimit = 0,
					LeftScope = p.LeftScope,
					RightScope = p.RightScope
				});

				// 填空項目設定
				formula.Gap = CommonUtil.GetRandomNumber(GapFilling.Left, GapFilling.Right);

				// 判定是否需要反推并重新作成計算式
				if (CheckIsNeedInverseMethod(p, formula))
				{
					defeated++;
					// 如果大於五次則認為此題無法作成繼續下一題
					if (defeated == INVERSE_NUMBER)
					{
						// 當前反推判定次數復原
						defeated = 0;
						continue;
					}
					i--;
					continue;
				}

				// 計算式作成
				p.Formulas.Add(new ArithmeticOperationsFormula
				{
					// 四則運算式
					Arithmetic = formula,
					// 等式值是不是出現在右邊（如果未指定則隨機產生）
					AnswerIsRight = p.AnswerIsRight ?? IsRight
				});

				defeated = 0;
			}
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		public override void MarkFormulaList(ArithmeticOperationsParameter p)
		{
			// 算式作成
			MarkFormulaList(p, () => { return CommonUtil.GetRandomNumber(p.Signs.ToList()); });

			// 智能提示作成
			VirtualHelperBase<ArithmeticOperationsFormula> helper = new ArithmeticOperationsDialogue();
			p.BrainpowerHint = helper.CreateHelperDialogue(p.Formulas);
		}

		/// <summary>
		/// 等式值是不是出現在左邊
		/// </summary>
		public bool IsRight => CommonUtil.GetRandomNumber(LeftOrRight.Left, LeftOrRight.Right) == LeftOrRight.Right;

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：算式存在一致
		/// 情況2：全零的情況
		/// 情況3：多級計算時，第二級計算式結果不能為零
		/// </remarks>
		/// <param name="p">已得到的算式</param>
		/// <param name="currentFormula">當前算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(ArithmeticOperationsParameter p, Formula currentFormula)
		{
			if (currentFormula.IsNoSolution)
			{
				return true;
			}

			// 全零的情況
			if (currentFormula.LeftParameter == 0 || currentFormula.RightParameter == 0 || currentFormula.Answer == 0)
			{
				return true;
			}

			// 判斷當前算式是否已經出現過
			if (p.Formulas.ToList().Any(d => d.Arithmetic.LeftParameter == currentFormula.LeftParameter
				&& d.Arithmetic.RightParameter == currentFormula.RightParameter
				&& d.Arithmetic.Answer == currentFormula.Answer
				&& d.Arithmetic.Sign == currentFormula.Sign))
			{
				return true;
			}
			return false;
		}
	}
}