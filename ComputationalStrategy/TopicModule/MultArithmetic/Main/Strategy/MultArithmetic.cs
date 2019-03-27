using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MultArithmetic.Item;
using MyMathSheets.ComputationalStrategy.MultArithmetic.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.MultArithmetic.Main.Strategy
{
	/// <summary>
	/// 多元四則運算題
	/// </summary>
	[Operation(LayoutSetting.Preview.MultArithmetic)]
	public class MultArithmetic : OperationBase
	{
		/// <summary>
		/// 方程式層數
		/// </summary>
		private int _multCount { get; set; }
		/// <summary>
		/// 計算式左右參數寄存棧（用於二叉樹的構建）
		/// </summary>
		private Stack<int> _stackParameter { get;set;}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			MultArithmeticParameter p = parameter as MultArithmeticParameter;

			// 算式作成
			MarkFormulaList(p, () => { return CommonUtil.GetRandomNumber(p.Signs.ToList()); });
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(MultArithmeticParameter p, Func<SignOfOperation> signFunc)
		{
			ICalculate strategy = null;

			_stackParameter = new Stack<int>();
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				_stackParameter.Clear();

				_multCount = 1;
				// 對指定運算符實例化
				strategy = CalculateManager(signFunc());
				// 答題參數作成
				MultArithmeticFormula tree = new MultArithmeticFormula
				{
					// 根計算式作成（第一層）
					Arithmetic = strategy.CreateFormula(new CalculateParameter()
					{
						MaximumLimit = p.MaximumLimit,
						QuestionType = p.QuestionType,
						MinimumLimit = 0
					})
				};

				// 注意先進後出原則（先推算左葉子計算式，再推算右葉子計算式）
				// 右葉子參數入棧（待出棧）
				_stackParameter.Push(tree.Arithmetic.RightParameter);
				// 左葉子參數入棧（待出棧）
				_stackParameter.Push(tree.Arithmetic.LeftParameter);

				while (_multCount <= 4)
				{
					// 構建多元計算式二叉樹結構
					CreateFormulaTree(p, tree, tree.Arithmetic.LeftParameter, tree.Arithmetic.RightParameter);




					// 層數++
					_multCount++;
				}
			}
		}

		/// <summary>
		/// 構建多元計算式二叉樹結構
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="tree">上一級計算式</param>
		/// <param name="leftParameter">作為結果值進行後續推算</param>
		/// <param name="rightParameter">作為結果值進行後續推算</param>
		/// <returns>葉子計算式</returns>
		private MultArithmeticFormula CreateFormulaTree(MultArithmeticParameter p, MultArithmeticFormula tree, int leftParameter, int rightParameter)
		{
			ICalculate strategy = null;

			tree.LeftArithmetic = new MultArithmeticFormula();
			strategy = CalculateManager(CommonUtil.GetRandomNumber(p.Signs.ToList()));
			// 左葉子
			tree.LeftArithmetic.Arithmetic = strategy.CreateFormulaWithAnswer(new CalculateParameter()
			{
				MaximumLimit = p.MaximumLimit,
				QuestionType = QuestionType.Default,
				MinimumLimit = 0
			}, leftParameter);


			tree.RightArithmetic = new MultArithmeticFormula();
			strategy = CalculateManager(CommonUtil.GetRandomNumber(p.Signs.ToList()));
			// 右葉子
			tree.RightArithmetic.Arithmetic = strategy.CreateFormulaWithAnswer(new CalculateParameter()
			{
				MaximumLimit = p.MaximumLimit,
				QuestionType = QuestionType.Default,
				MinimumLimit = 0
			}, rightParameter);

			return tree;
		}

		/// <summary>
		/// 構建多元計算式二叉樹結構
		/// </summary>
		/// <param name="tree">上一級計算式</param>
		/// <returns>葉子計算式</returns>
		private MultArithmeticFormula CreateRightFormulaTree(MultArithmeticFormula tree)
		{

			tree.RightArithmetic = new MultArithmeticFormula();






			return tree;
		}
	}
}
