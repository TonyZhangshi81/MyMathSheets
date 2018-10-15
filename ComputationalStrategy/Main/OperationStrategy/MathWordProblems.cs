using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.Main.OperationStrategy
{
	/// <summary>
	/// 應用題策略
	/// </summary>
	[Operation(LayoutSetting.Preview.MathWordProblems)]
	public class MathWordProblems : OperationBase
	{
		/// <summary>
		/// 應用題庫文件所在路徑
		/// </summary>
		private const string PROBLEMS_JSON_FILE_PATH = @"..\Config\Problems.json";

		/// <summary>
		/// 出题资料库
		/// </summary>
		private List<Problems> _allProblems { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public override void MarkFormulaList(ParameterBase parameter)
		{
			MathWordProblemsParameter p = parameter as MathWordProblemsParameter;

			// 读取出题资料库
			GetAllProblemsFromResource();

			ICalculate strategy = null;
			// 標準題型（指定單個運算符）
			if (p.FourOperationsType == FourOperationsType.Standard)
			{
				List<Problems> signProblems = GetProblemsBySign(p.Signs[0]);
				// 题库中的数量比指定的出题数少的情况
				if (signProblems.Count == 0)
				{
					return;
				}

				// 指定單個運算符實例
				strategy = CalculateManager(p.Signs[0]);
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					// 計算式作成
					MarkFormulas(p, strategy, signProblems);
					// 判定是否需要反推并重新作成計算式
					if (CheckIsNeedInverseMethod(p.Formulas.Last().ProblemFormula))
					{
						i--;
						p.Formulas.Remove(p.Formulas.Last());
						continue;
					}
				}
			}
			else
			{
				// 按照指定數量作成相應的數學計算式
				for (var i = 0; i < p.NumberOfQuestions; i++)
				{
					RandomNumberComposition random = new RandomNumberComposition(0, p.Signs.Count - 1);
					// 混合題型（加減乘除運算符實例隨機抽取）
					SignOfOperation sign = p.Signs[random.GetRandomNumber()];
					// 對四則運算符實例進行cache管理
					strategy = CalculateManager(sign);

					List<Problems> signProblems = GetProblemsBySign(sign);
					// 题库中的数量比指定的出题数少的情况
					if (signProblems.Count == 0)
					{
						i--;
						continue;
					}

					// 計算式作成
					MarkFormulas(p, strategy, signProblems);

					// 判定是否需要反推并重新作成計算式
					if (CheckIsNeedInverseMethod(p.Formulas.Last().ProblemFormula))
					{
						i--;
						p.Formulas.Remove(p.Formulas.Last());
						continue;
					}
				}
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
			if (currentFormula.LeftParameter == 0 || currentFormula.RightParameter == 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 計算式作成
		/// </summary>
		/// <param name="strategy">四則運算符實例</param>
		/// <param name="signProblems">指定运算符的出题资源库</param>
		private void MarkFormulas(MathWordProblemsParameter p, ICalculate strategy, List<Problems> signProblems)
		{
			// 资料库中应用题随机取得（不重复）
			Problems problem = GetRandomProblemsIndex(signProblems);
			// 計算式作成
			Formula formula = strategy.CreateFormula(p.MaximumLimit, p.QuestionType, 1);
			// 答題矯正(當答題中未出現x或y值時)
			AnswerCorrect(problem, formula);

			p.Formulas.Add(new MathWordProblemsFormula()
			{
				// 运算式
				ProblemFormula = formula,
				// 应用题文字内容
				MathWordProblem = problem.Content.Replace("x", formula.LeftParameter.ToString())
													.Replace("y", formula.RightParameter.ToString()),
				// 标准答案
				Verify = problem.Verify.Replace("x", formula.LeftParameter.ToString())
													.Replace("y", formula.RightParameter.ToString())
													.Replace("n", formula.Answer.ToString())
			});
		}

		/// <summary>
		/// 答題矯正(當答題中未出現x或y值時)
		/// </summary>
		/// <param name="problem">资料库</param>
		/// <param name="formula">計算式</param>
		/// <remarks>
		/// 基於計算式的推斷,當資料庫中出現x+x=n或者y+y=n的情況時,需要對計算式進行矯正,以匹配資料庫中的答案等式
		/// </remarks>
		private void AnswerCorrect(Problems problem, Formula formula)
		{
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
		/// 读取资料库
		/// </summary>
		private void GetAllProblemsFromResource()
		{
			// 读取资料库
			using (System.IO.StreamReader file = System.IO.File.OpenText(PROBLEMS_JSON_FILE_PATH))
			{
				_allProblems = JsonExtension.GetObjectByJson<List<Problems>>(file.ReadToEnd());
			};
		}

		/// <summary>
		/// 随机获取资料库中的题目（不可重复）
		/// </summary>
		/// <param name="signsProblems">指定运算符资料库</param>
		/// <returns>被抽取的题目</returns>
		private Problems GetRandomProblemsIndex(List<Problems> signsProblems)
		{
			RandomNumberComposition random = new RandomNumberComposition(0, signsProblems.Count - 1);
			Problems problem = signsProblems[random.GetRandomNumber()];
			// 从指定运算符的资源库中删除已抽取的题目
			signsProblems.Remove(problem);
			// 从总资源库中删除已抽取的题目
			_allProblems.Remove(_allProblems.Where(d => d.ID.Equals(problem.ID)).First());

			return problem;
		}

		/// <summary>
		/// 获取指定运算符出题资料库
		/// </summary>
		/// <param name="sign">运算符</param>
		/// <returns>出题资料库</returns>
		private List<Problems> GetProblemsBySign(SignOfOperation sign)
		{
			return _allProblems.Where(d => d.ID.Substring(0, 1).Equals(sign.OperationToID())).ToList();
		}
	}
}
