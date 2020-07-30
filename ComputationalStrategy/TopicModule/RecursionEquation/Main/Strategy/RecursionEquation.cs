using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Main.VirtualHelper;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Arithmetic.Main.Helper;
using MyMathSheets.ComputationalStrategy.RecursionEquation.Item;
using MyMathSheets.ComputationalStrategy.RecursionEquation.Main.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyMathSheets.ComputationalStrategy.RecursionEquation.Main.Strategy
{
	/// <summary>
	/// 遞等式計算
	/// </summary>
	[Topic("RecursionEquation")]
	public class RecursionEquation : TopicBase<RecursionEquationParameter>
	{
		/// <summary>
		/// 遞等式計算的實現方法集合
		/// </summary>
		private readonly Dictionary<TopicType, Action<IList<RecursionEquationFormula>>> _calculations = new Dictionary<TopicType, Action<IList<RecursionEquationFormula>>>();

		/// <summary>
		/// <see cref="RecursionEquation"/>構造函數
		/// </summary>
		[ImportingConstructor]
		public RecursionEquation()
		{
			// 遞等式計算[A+B+C]
			_calculations[TopicType.CleverA] = CleverA;
			// 遞等式計算[A-(B-C)]
			_calculations[TopicType.CleverB] = CleverB;
			// 遞等式計算[A-(B+C)]
			_calculations[TopicType.CleverC] = CleverC;
			// 遞等式計算[A+(B-C)]
			_calculations[TopicType.CleverD] = CleverD;
			// 遞等式計算[A+(B+C)]
			_calculations[TopicType.CleverE] = CleverE;
			// 遞等式計算[A+B-C]
			_calculations[TopicType.CleverF] = CleverF;
			// 遞等式計算[A-B+C]
			_calculations[TopicType.CleverG] = CleverG;
			// 遞等式計算[A-B-C]
			_calculations[TopicType.CleverH] = CleverH;
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		public override void MarkFormulaList(RecursionEquationParameter p)
		{
			// 算式作成
			MarkFormulaList(p, () =>
			{
				// 隨機子題型
				return CommonUtil.GetRandomNumber(p.TopicTypes);
			});

			// 智能提示作成
			VirtualHelperBase<RecursionEquationFormula> helper = new RecursionEquationDialogue();
			p.BrainpowerHint = helper.CreateHelperDialogue(p.Formulas);
		}

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="topicTypeFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(RecursionEquationParameter p, Func<TopicType> topicTypeFunc)
		{
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				if (_calculations.TryGetValue(topicTypeFunc(), out Action<IList<RecursionEquationFormula>> calculation))
				{
					calculation(p.Formulas);
				}
			}
		}

		/// <summary>
		/// 遞等式計算通用處理
		/// </summary>
		/// <param name="type">題型</param>
		/// <param name="expressFormat">計算表達式</param>
		/// <param name="getArguments">參數處理邏輯</param>
		/// <param name="formulas">計算式作成</param>
		private void CleverStartegy(TopicType type, string expressFormat, Func<int[]> getArguments, IList<RecursionEquationFormula> formulas)
		{
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;

			RecursionEquationFormula RecursionEquation = new RecursionEquationFormula
			{
				Type = type,
				ConfixFormulas = new List<Formula>(),
				Answer = new List<int>()
			};

			while (1 == 1)
			{
				int[] factors = getArguments();

				StringBuilder express = new StringBuilder();
				var answer = factors[3];
				express.AppendFormat(CultureInfo.CurrentCulture, expressFormat, factors[0], factors[1], factors[2]);

				// 計算式推導
				var calc = new ExpressArithmeticUtil();
				if (!calc.IsResult(express.ToString(), out int result))
				{
					defeated++;
					continue;
				}
				if (result != answer)
				{
					defeated++;
					continue;
				}
				// 加入推導出計算式集合
				calc.Formulas.ToList().ForEach(f => RecursionEquation.ConfixFormulas.Add(f));
				RecursionEquation.Answer.Add(answer);
				defeated = 0;

				break;
			}

			if (RecursionEquation != null)
			{
				formulas.Add(RecursionEquation);
			}
		}

		#region 遞等式計算

		/// <summary>
		/// 遞等式計算[A-B-C]
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 68-44-18 -> 68-18-44
		/// </remarks>
		protected virtual void CleverH(IList<RecursionEquationFormula> formulas)
		{
			CleverStartegy(TopicType.CleverH, "{0}-{1}-{2}",
				() =>
				{
					// (A-C)參數差值（獲取三位數(整十位或者整百位)）
					var diff = CommonUtil.GetRandomNumber(160, 350, _ => _ % 10 == 0);
					// 參數C (*增加參數B的取值範圍) eg: 162-(44+38)
					var argumentC = CommonUtil.GetRandomNumber(50, 150);
					// 另一個隨機取值方案（整十數湊百）eg: 160+(44-40)
					if (1 == CommonUtil.GetRandomNumber(1, 2))
					{
						diff = CommonUtil.GetRandomNumber(160, 350, _ => _ % 100 == 0);
						argumentC = CommonUtil.GetRandomNumber(50, 150, _ => _ % 10 == 0 && _ % 100 != 0);
					}
					// 參數A
					var argumentA = diff + argumentC;
					// 參數B
					var argumentB = CommonUtil.GetRandomNumber(argumentC, diff,
																	b => b != argumentC
																			&& (argumentA - b) % 10 != 0
																			&& (argumentC + b) % 10 != 0);
					// 結果值
					var answer = diff - argumentB;

					// 隨機排列計算式各參數（打亂參數B,C 位置）
					SetNewGuidFactors(out List<int> factors1, argumentB, argumentC);
					return new int[] { argumentA, factors1[0], factors1[1], answer };
				}, formulas);
		}

		/// <summary>
		/// 遞等式計算[A-B+C]
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 68-44+12 -> 68+12-44
		/// </remarks>
		protected virtual void CleverG(IList<RecursionEquationFormula> formulas)
		{
			CleverStartegy(TopicType.CleverG, "{0}-{1}+{2}",
				() =>
				{
					// (A+C)參數合計（獲取三位數(整十位或者整百位)）
					var argumentSum = CommonUtil.GetRandomNumber(300, 550, _ => _ % 10 == 0);
					// 參數C (*增加參數B的取值範圍) eg: 162-44+38
					var argumentC = CommonUtil.GetRandomNumber(100, 250, _ => _ % 100 != 0);
					// 另一個隨機取值方案（整十數湊百）eg: 160-44+40)
					if (1 == CommonUtil.GetRandomNumber(1, 2))
					{
						argumentSum = CommonUtil.GetRandomNumber(300, 550, _ => _ % 100 == 0);
						argumentC = CommonUtil.GetRandomNumber(100, 250, _ => _ % 10 == 0 && _ % 100 != 0);
					}
					// 參數A
					var argumentA = argumentSum - argumentC;
					if (argumentA % 100 == 0)
					{
						var diff = CommonUtil.GetRandomNumber(1, 100, _ => _ % 10 == 0);
						argumentA += diff;
						argumentSum += diff;
					}
					// 參數B
					var argumentB = CommonUtil.GetRandomNumber(50, argumentA, b => b != argumentC && (b + argumentA) % 10 != 0);
					// 結果值
					var answer = argumentSum - argumentB;

					// 參數不能隨便交換位置
					return new int[] { argumentA, argumentB, argumentC, answer };
				}, formulas);
		}

		/// <summary>
		/// 遞等式計算[A+B-C]
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 68+44-18 -> 68+44-18 -> 68-18+44
		/// </remarks>
		protected virtual void CleverF(IList<RecursionEquationFormula> formulas)
		{
			CleverStartegy(TopicType.CleverF, "{0}+{1}-{2}",
				() =>
				{
					// (A-C)參數差值（獲取三位數(整十位或者整百位)）
					var diff = CommonUtil.GetRandomNumber(160, 350, _ => _ % 10 == 0);
					// 參數C (*增加參數B的取值範圍) eg: 162+44-62
					var argumentC = CommonUtil.GetRandomNumber(50, 150);
					// 另一個隨機取值方案（整十數湊百）eg: 160+44-60
					if (1 == CommonUtil.GetRandomNumber(1, 2))
					{
						diff = CommonUtil.GetRandomNumber(160, 350, _ => _ % 100 == 0);
						argumentC = CommonUtil.GetRandomNumber(50, 150, _ => _ % 10 == 0 && _ % 100 != 0);
					}
					// 參數A
					var argumentA = diff + argumentC;
					// 參數B
					var argumentB = CommonUtil.GetRandomNumber(argumentC, diff, b => b != argumentC && (argumentA - b) % 10 != 0);
					// 結果值
					var answer = diff + argumentB;

					// 參數不能隨便交換位置
					return new int[] { argumentA, argumentB, argumentC, answer };
				}, formulas);
		}

		/// <summary>
		/// 遞等式計算[A+(B+C)]
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 68+(44+12) -> 68+44+12 -> 68+12+44
		/// </remarks>
		protected virtual void CleverE(IList<RecursionEquationFormula> formulas)
		{
			CleverStartegy(TopicType.CleverE, "{0}+({1}+{2})",
				() =>
				{
					// (A+C)參數合計（獲取三位數(整十位或者整百位)）
					var argumentSum = CommonUtil.GetRandomNumber(300, 550, _ => _ % 10 == 0);
					// 參數C (*增加參數B的取值範圍)
					var argumentC = CommonUtil.GetRandomNumber(100, 250, _ => _ % 10 != 0);
					// 另一個隨機取值方案（整十數湊百）
					if (1 == CommonUtil.GetRandomNumber(1, 2))
					{
						argumentSum = CommonUtil.GetRandomNumber(300, 550, _ => _ % 100 == 0);
						argumentC = CommonUtil.GetRandomNumber(100, 250, _ => _ % 10 == 0 && _ % 100 != 0);
					}

					// 參數A
					var argumentA = argumentSum - argumentC;
					if (argumentA % 100 == 0)
					{
						var diff = CommonUtil.GetRandomNumber(1, 100, _ => _ % 10 == 0);
						argumentA += diff;
						argumentSum += diff;
					}
					// 參數B
					var argumentB = CommonUtil.GetRandomNumber(50, 250, b => b != argumentC && (b + argumentA) % 10 != 0 && (b + argumentC) % 10 != 0);
					// 結果值
					var answer = argumentSum + argumentB;

					// 隨機排列計算式各參數（打亂參數A,C 位置）
					SetNewGuidFactors(out List<int> factors1, argumentA, argumentC);
					SetNewGuidFactors(out List<int> factors2, factors1[1], argumentB);
					return new int[] { factors1[0], factors2[0], factors2[1], answer };
				}, formulas);
		}

		/// <summary>
		/// 遞等式計算[A+(B-C)]
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 62+(44-12) -> 62+44-12 -> 62-12+44
		/// </remarks>
		protected virtual void CleverD(IList<RecursionEquationFormula> formulas)
		{
			CleverStartegy(TopicType.CleverD, "{0}+({1}-{2})",
				() =>
				{
					// (A-C)參數差值（獲取三位數(整十位或者整百位)）
					var diff = CommonUtil.GetRandomNumber(160, 350, _ => _ % 10 == 0);
					// 參數C (*增加參數B的取值範圍) eg: 162+(44-62)
					var argumentC = CommonUtil.GetRandomNumber(50, 150);
					// 另一個隨機取值方案（整十數湊百）eg: 160+(44-60)
					if (1 == CommonUtil.GetRandomNumber(1, 2))
					{
						diff = CommonUtil.GetRandomNumber(160, 350, _ => _ % 100 == 0);
						argumentC = CommonUtil.GetRandomNumber(50, 150, _ => _ % 10 == 0 && _ % 100 != 0);
					}
					// 參數A
					var argumentA = diff + argumentC;
					// 參數B
					var argumentB = CommonUtil.GetRandomNumber(argumentC, diff, b => b != argumentC && (argumentA - b) % 10 != 0);
					// 結果值
					var answer = diff + argumentB;

					// 參數不能隨便交換位置
					return new int[] { argumentA, argumentB, argumentC, answer };
				}, formulas);
		}

		/// <summary>
		/// 遞等式計算[A-(B+C)]
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 62-(44+12) -> 62-44-12
		/// </remarks>
		protected virtual void CleverC(IList<RecursionEquationFormula> formulas)
		{
			CleverStartegy(TopicType.CleverC, "{0}-({1}+{2})",
				() =>
				{
					// (A-C)參數差值（獲取三位數(整十位或者整百位)）
					var diff = CommonUtil.GetRandomNumber(160, 350, _ => _ % 10 == 0);
					// 參數C (*增加參數B的取值範圍) eg: 162-(44+38)
					var argumentC = CommonUtil.GetRandomNumber(50, 150);
					// 另一個隨機取值方案（整十數湊百）eg: 160+(44-40)
					if (1 == CommonUtil.GetRandomNumber(1, 2))
					{
						diff = CommonUtil.GetRandomNumber(160, 350, _ => _ % 100 == 0);
						argumentC = CommonUtil.GetRandomNumber(50, 150, _ => _ % 10 == 0 && _ % 100 != 0);
					}
					// 參數A
					var argumentA = diff + argumentC;
					// 參數B
					var argumentB = CommonUtil.GetRandomNumber(argumentC, diff, b => b != argumentC && (argumentA - b) % 10 != 0);
					// 結果值
					var answer = diff - argumentB;

					// 參數不能隨便交換位置
					return new int[] { argumentA, argumentB, argumentC, answer };
				}, formulas);
		}

		/// <summary>
		/// 遞等式計算[A-(B-C)]
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 68-(44-12) -> 68-44+12 -> 68+12-44
		/// </remarks>
		protected virtual void CleverB(IList<RecursionEquationFormula> formulas)
		{
			CleverStartegy(TopicType.CleverB, "{0}-({1}-{2})",
				() =>
				{
					// (A+C)參數合計（獲取三位數(整十位或者整百位)）
					var argumentSum = CommonUtil.GetRandomNumber(300, 550, _ => _ % 10 == 0);
					// 參數C (*增加參數B的取值範圍) eg: 162-(44-62)
					var argumentC = CommonUtil.GetRandomNumber(100, 250, _ => _ % 100 != 0);
					// 另一個隨機取值方案（整十數湊百）eg: 160-(44-60)
					if (1 == CommonUtil.GetRandomNumber(1, 2))
					{
						argumentSum = CommonUtil.GetRandomNumber(300, 550, _ => _ % 100 == 0);
						argumentC = CommonUtil.GetRandomNumber(100, 250, _ => _ % 10 == 0 && _ % 100 != 0);
					}
					// 參數A
					var argumentA = argumentSum - argumentC;
					if (argumentA % 100 == 0)
					{
						var diff = CommonUtil.GetRandomNumber(1, 100, _ => _ % 10 == 0);
						argumentA += diff;
						argumentSum += diff;
					}
					// 參數B
					var argumentB = CommonUtil.GetRandomNumber(50, argumentA, b => b != argumentC && (b + argumentA) % 10 != 0);
					// 結果值
					var answer = argumentSum - argumentB;

					// 參數不能隨便交換位置
					return new int[] { argumentA, argumentB, argumentC, answer };
				}, formulas);
		}

		/// <summary>
		/// 遞等式計算[A+B+C]
		/// </summary>
		/// <param name="formulas">計算式作成</param>
		/// <remarks>
		/// 22+44+56 -> 22+(44+56)
		/// </remarks>
		protected virtual void CleverA(IList<RecursionEquationFormula> formulas)
		{
			CleverStartegy(TopicType.CleverA, "{0}+{1}+{2}",
				() =>
				{
					// 參數A
					var argumentA = CommonUtil.GetRandomNumber(50, 300);
					// 參數合計（獲取三位數(整數十位或者整百數)）
					var argumentSum = CommonUtil.GetRandomNumber(100, 500, _ => _ % 10 == 0);
					// 參數B
					var argumentB = CommonUtil.GetRandomNumber(100, argumentSum,
																	b => b != argumentA
																		&& b != argumentSum - argumentA
																		&& b % 100 != 0);
					// 參數C
					var argumentC = argumentSum - argumentB;
					// 避免AB或者AC相加和為整十數
					if ((argumentA + argumentC) % 10 == 0 || (argumentA + argumentB) % 10 == 0)
					{
						argumentA -= 1;
					}
					// 結果值
					var answer = argumentA + argumentSum;

					// 隨機排列計算式各參數（打亂參數位置）
					SetNewGuidFactors(out List<int> factors, argumentA, argumentB, argumentC);
					factors.Add(answer);

					return factors.ToArray();
				}, formulas);
		}

		#endregion 遞等式計算

		/// <summary>
		/// 隨機排列計算式各參數
		/// </summary>
		/// <param name="factors">參數列</param>
		/// <param name="arguments">參數集合</param>
		/// <remarks>
		/// 5 * 10 + 2 * 5
		/// 計算式中的參數位置可以有以下情況：
		/// 10 * 5 + 2 * 5
		/// 5 * 10 + 5 * 2
		/// 等等，顧需要隨機打亂其中的擺放順序
		/// 注：共有因數必須兩邊都有
		/// </remarks>
		private void SetNewGuidFactors(out List<int> factors, params int[] arguments)
		{
			// 隨機排序
			factors = arguments.OrderBy(c => Guid.NewGuid()).ToList();
		}
	}
}