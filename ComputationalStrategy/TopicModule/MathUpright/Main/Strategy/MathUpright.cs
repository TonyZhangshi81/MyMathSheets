using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MathUpright.Item;
using MyMathSheets.ComputationalStrategy.MathUpright.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.MathUpright.Main.Strategy
{
	/// <summary>
	/// 豎式計算題
	/// </summary>
	[Operation("MathUpright")]
	public class MathUpright : TopicBase
	{
		/// <summary>
		/// 填空的位置
		/// </summary>
		private readonly List<int> FillPositions;

		/// <summary>
		/// 反推判定次數（如果大於八次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 8;

		/// <summary>
		/// 參數初期化
		/// </summary>
		/// <remarks>
		/// 位置信息：
		/// 14 -> [1]5 + 2[3] = 38
		/// 23 -> 1[5] + [2]3 = 38
		/// 16 -> [1]5 + 23 = 3[8]
		/// 25 -> 1[5] + 23 = [3]8
		/// 36 -> 15 + [2]3 = 3[8]
		/// 45 -> 15 + 2[3] = [3]8
		/// 56 -> 15 + 23 = [3][8]
		/// </remarks>
		public MathUpright()
		{
			// 位置信息
			FillPositions = new List<int>() { 14, 23, 16, 25, 36, 45, 56 };
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(MathUprightParameter p, Func<SignOfOperation> signFunc)
		{
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;

			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				IArithmetic strategy = CalculateManager(signFunc());
				// 運算式作成
				Formula formula = strategy.CreateFormula(new ArithmeticParameter()
				{
					MaximumLimit = p.MaximumLimit,
					QuestionType = p.QuestionType,
					MinimumLimit = 1
				});

				// 判定是否需要反推并重新作成計算式
				if (CheckIsNeedInverseMethod(p, formula))
				{
					defeated++;
					// 如果大於八次則認為此題無法作成繼續下一題
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
				p.Formulas.Add(new MathUprightFormula
				{
					Arithmetic = formula,
					// 如果是標準題型則將等式結果項目作為填空項目，以外的情況則隨機產生一個填空項目位置
					FillPosition = (p.QuestionType == QuestionType.Standard) ? FillPositions.Last() : CommonUtil.GetRandomNumber(FillPositions),
					// 計算式數據鏈作成
					FormulaDataLink = CreatFormulaDataLink(formula)
				});

				defeated = 0;
			}
		}

		/// <summary>
		/// 計算式數據鏈作成
		/// </summary>
		/// <param name="formula">計算式</param>
		/// <returns>計算式數據鏈</returns>
		/// <remarks>
		/// 數據鏈構成：
		/// 15+23=38 -> [1,5,2,3,3,8]
		/// 23-4=19 -> [2,3,null,4,1,9]
		/// </remarks>
		private List<int?> CreatFormulaDataLink(Formula formula)
		{
			List<int?> link = new List<int?>
			{
				// 第一位
				(formula.LeftParameter >= 10) ? formula.LeftParameter / 10 : (int?)null,
				// 第二位
				formula.LeftParameter % 10,
				// 第三位
				(formula.RightParameter >= 10) ? formula.RightParameter / 10 : (int?)null,
				// 第四位
				formula.RightParameter % 10,
				// 第五位
				(formula.Answer >= 10) ? formula.Answer / 10 : (int?)null,
				// 第六位
				formula.Answer % 10
			};

			return link;
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(TopicParameterBase parameter)
		{
			MathUprightParameter p = parameter as MathUprightParameter;

			// 算式作成
			MarkFormulaList(p, () => { return CommonUtil.GetRandomNumber(p.Signs.ToList()); });
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：算式存在一致
		/// 情況2：等式左邊的兩個數字都是一位數
		/// 情況3：全零的情況
		/// </remarks>
		/// <param name="p">已得到的算式</param>
		/// <param name="currentFormula">當前算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(MathUprightParameter p, Formula currentFormula)
		{
			// 全零的情況
			if (currentFormula.LeftParameter == 0 || currentFormula.RightParameter == 0 || currentFormula.Answer == 0)
			{
				return true;
			}

			// 等式左邊的兩個數字必須有一個是兩位數
			if (currentFormula.LeftParameter < 10 || currentFormula.RightParameter < 10 || (currentFormula.Sign == SignOfOperation.Subtraction && currentFormula.Answer < 10))
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