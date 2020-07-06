using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Item;
using MyMathSheets.ComputationalStrategy.GapFillingProblems.Main.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.GapFillingProblems.Main.Strategy
{
	/// <summary>
	/// 填空題策略
	/// </summary>
	[Operation("GapFillingProblems")]
	public class GapFillingProblems : OperationBase
	{
		/// <summary>
		/// 填空題庫文件所在路徑
		/// </summary>
		private const string PROBLEMS_JSON_FILE_PATH = @"..\Config\GapFillingProblemsLibrary.json";

		/// <summary>
		/// 出題資料庫
		/// </summary>
		private List<GapFillingProblemsLibrary> _allProblems;

		/// <summary>
		/// 題型作成
		/// </summary>
		/// <param name="parameter">題型參數</param>
		protected override void MarkFormulaList(ParameterBase parameter)
		{
			GapFillingProblemsParameter p = parameter as GapFillingProblemsParameter;

			// 讀取出題資料庫
			GetAllProblemsFromResource(p.Levels);
			// 題庫為空的情況
			if (_allProblems.Count == 0)
			{
				return;
			}

			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				if (_allProblems.Count == 0)
				{
					break;
				}

				// 隨機從題庫中提取題目(不重複)
				GapFillingProblemsLibrary problem = CommonUtil.GetRandomNumber(_allProblems);
				// 資料庫中刪除已抽取的題目
				_allProblems.Remove(problem);

				// 題型作成
				p.Formulas.Add(new GapFillingProblemsFormula()
				{
					// 填空題內容
					GapFillingProblem = ZipHelper.GZipDecompressString(problem.Content),
					// 級別難度
					Level = problem.Level,
					// 參數集合
					Parameters = problem.Parameters,
					// 答案集合
					Answers = problem.Answers.Select(d => d = Base64.DecodeBase64String(d)).ToList()
				});
			}
		}

		/// <summary>
		/// 讀取資料庫
		/// </summary>
		/// <param name="levels">等級選擇</param>
		private void GetAllProblemsFromResource(int[] levels)
		{
			// 讀取資料庫
			using (System.IO.StreamReader file = System.IO.File.OpenText(PROBLEMS_JSON_FILE_PATH))
			{
				_allProblems = JsonExtension.GetObjectByJson<List<GapFillingProblemsLibrary>>(file.ReadToEnd()).Where(d => Array.IndexOf(levels, d.Level) >= 0).ToList();
			};
		}
	}
}