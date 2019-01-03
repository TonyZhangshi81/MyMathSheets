using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Item;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Strategy
{
	/// <summary>
	/// 應用題策略
	/// </summary>
	[Operation(LayoutSetting.Preview.MathWordProblems)]
	public class MathWordProblems : OperationBase
	{
		/// <summary>
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 3;
		/// <summary>
		/// 應用題庫文件所在路徑
		/// </summary>
		private const string PROBLEMS_JSON_FILE_PATH = @"..\Config\Problems.json";

		/// <summary>
		/// 出題資料庫
		/// </summary>
		private List<Problems> _allProblems { get; set; }

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(MathWordProblemsParameter p, Func<SignOfOperation> signFunc)
		{
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;

			ICalculate strategy = null;
			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				SignOfOperation sign = signFunc();
				List<Problems> signProblems = GetProblemsBySign(sign);
				// 題庫中的數量比指定的出題數少的情況
				if (signProblems.Count == 0)
				{
					continue;
				}

				// 指定單個運算符實例
				strategy = CalculateManager(sign);
				// 計算式作成
				MarkFormulas(p, strategy, signProblems);
				// 判定是否需要反推并重新作成計算式
				if (CheckIsNeedInverseMethod(p.Formulas.Last().ProblemFormula))
				{
					p.Formulas.Remove(p.Formulas.Last());

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
			}
		}

		/// <summary>
		/// 題型作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			MathWordProblemsParameter p = parameter as MathWordProblemsParameter;

			// 讀取出題資料庫
			GetAllProblemsFromResource();

			ICalculate strategy = null;
			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				// 單一的運算符類型
				MarkFormulaList(p, () => { return p.Signs[0]; });
			}
			else
			{
				// 混合題型（加減乘除運算符實例隨機抽取）
				MarkFormulaList(p, () => { return CommonUtil.GetRandomNumber(p.Signs.ToList()); });
			}
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：x或y參數為0
		/// </remarks>
		/// <param name="currentFormula">當前算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(Formula currentFormula)
		{
			// x或y參數為0
			if (currentFormula.LeftParameter == 0 || currentFormula.RightParameter == 0 || currentFormula.Answer == 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 計算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="strategy">四則運算符實例</param>
		/// <param name="signProblems">指定運算符的出題資料庫</param>
		private void MarkFormulas(MathWordProblemsParameter p, ICalculate strategy, List<Problems> signProblems)
		{
			// 資料庫中的應用題隨機取得（不重複）
			Problems problem = GetRandomProblemsIndex(signProblems);
			// 計算式作成
			Formula formula = strategy.CreateFormula(new CalculateParameter()
			{
				MaximumLimit = p.MaximumLimit,
				QuestionType = p.QuestionType,
				MinimumLimit = 1
			});
			// 答題矯正(當答題中未出現x或y值時)
			AnswerCorrect(problem, formula);

			p.Formulas.Add(new MathWordProblemsFormula()
			{
				// 運算式
				ProblemFormula = formula,
				// 單位
				Unit = problem.Unit ?? string.Empty,
				// 應用題文字內容
				MathWordProblem = problem.Content.Replace("x", formula.LeftParameter.ToString())
													.Replace("y", formula.RightParameter.ToString()),
				// 標準答案
				Verify = problem.Verify.Replace("x", formula.LeftParameter.ToString())
													.Replace("y", formula.RightParameter.ToString())
													.Replace("n", formula.Answer.ToString())
			});
		}

		/// <summary>
		/// 答題矯正(當答題中未出現x或y值時)
		/// </summary>
		/// <param name="problem">資料庫</param>
		/// <param name="formula">計算式</param>
		/// <remarks>
		/// 基於計算式的推斷,當資料庫中出現x+x=n或者y+y=n的情況時,需要對計算式進行矯正,以匹配資料庫中的答案等式
		/// </remarks>
		private void AnswerCorrect(Problems problem, Formula formula)
		{
			// 中括號中的內容
			Regex rgx = new Regex(@"(?i)(?<=\[)(.*)(?=\])");
			if (rgx.IsMatch(problem.Content))
			{
				// 中括號內的表達式取得
				var expression = rgx.Match(problem.Content).Value;
				var left = expression.Substring(0, 1);
				var expValue = expression.Substring(2);
				// 如果是減法表達式
				if (expression.IndexOf("-") >= 0)
				{
					if ("x".Equals(left))
					{
						formula.LeftParameter -= Convert.ToInt32(expValue);
						formula.Answer -= Convert.ToInt32(expValue);
					}
					else if ("y".Equals(left))
					{
						formula.RightParameter -= Convert.ToInt32(expValue);
						formula.Answer += Convert.ToInt32(expValue);
					}
				}
				else
				{
					// 加法表達式
					if ("x".Equals(left))
					{
						formula.LeftParameter += Convert.ToInt32(expValue);
					}
					else if ("y".Equals(left))
					{
						formula.RightParameter += Convert.ToInt32(expValue);
					}
					formula.Answer += Convert.ToInt32(expValue);
				}
				// 表達式替換為一個參數符號(eg: [x+1] => x)
				problem.Content = problem.Content.Replace(string.Format("[{0}]", rgx.Match(problem.Content).Value), left);
			}
			// 答題結果中不存在x參數
			if (problem.Verify.IndexOf("x") < 0)
			{
				formula.LeftParameter = formula.RightParameter;
				formula.Answer = formula.RightParameter * 2;
			}
			// 答題結果中不存在y參數
			if (problem.Verify.IndexOf("y") < 0)
			{
				formula.RightParameter = formula.LeftParameter;
				formula.Answer = formula.LeftParameter * 2;
			}
		}

		/// <summary>
		/// 讀取資料庫
		/// </summary>
		private void GetAllProblemsFromResource()
		{
			// 讀取資料庫
			using (System.IO.StreamReader file = System.IO.File.OpenText(PROBLEMS_JSON_FILE_PATH))
			{
				_allProblems = JsonExtension.GetObjectByJson<List<Problems>>(file.ReadToEnd());
			};
		}

		/// <summary>
		/// 隨機獲取資料庫中的題目（不可重複）
		/// </summary>
		/// <param name="signsProblems">指定運算符的應用題</param>
		/// <returns>被提取的應用題</returns>
		private Problems GetRandomProblemsIndex(List<Problems> signsProblems)
		{
			Problems problem = signsProblems[CommonUtil.GetRandomNumber(0, signsProblems.Count - 1)];
			// 從指定運算符的資料庫中刪除已抽取的題目
			signsProblems.Remove(problem);
			// 從總資料庫中刪除已抽取的題目
			_allProblems.Remove(_allProblems.Where(d => d.ID.Equals(problem.ID)).First());

			return problem;
		}

		/// <summary>
		/// 獲取指定運算符出題資料庫
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>出題資料庫</returns>
		private List<Problems> GetProblemsBySign(SignOfOperation sign)
		{
			return _allProblems.Where(d => d.ID.Substring(0, 1).Equals(sign.OperationToID())).ToList();
		}
	}
}
