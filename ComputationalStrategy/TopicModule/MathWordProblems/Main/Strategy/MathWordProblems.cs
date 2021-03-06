﻿using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.CommonLib.Util.Security;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Item;
using MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.MathWordProblems.Main.Strategy
{
	/// <summary>
	/// 應用題策略
	/// </summary>
	[Topic("MathWordProblems")]
	public class MathWordProblems : TopicBase<MathWordProblemsParameter>
	{
		/// <summary>
		/// 反推判定次數（如果大於兩次則認為此題無法作成繼續下一題）
		/// </summary>
		private const int INVERSE_NUMBER = 3;

		/// <summary>
		/// 應用題庫文件所在路徑
		/// </summary>
		private const string PROBLEMS_JSON_FILE_PATH = @"..\Config\MathWordProblemsLibrary.json";

		/// <summary>
		/// 出題資料庫
		/// </summary>
		private List<Problems> _allProblems;

		/// <summary>
		/// 算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signFunc">運算符取得用的表達式</param>
		private void MarkFormulaList(MathWordProblemsParameter p, Func<SignOfOperation> signFunc)
		{
			// 當前反推判定次數（一次推算內次數累加）
			int defeated = 0;

			// 按照指定數量作成相應的數學計算式
			for (var i = 0; i < p.NumberOfQuestions; i++)
			{
				SignOfOperation sign = signFunc();
				List<Problems> signProblems = GetProblemsBySign(sign, p.Levels);
				// 題庫中的數量比指定的出題數少的情況
				if (signProblems.Count == 0)
				{
					continue;
				}

				// 計算式作成
				MarkFormulas(p, signProblems);
				// 判定是否需要反推并重新作成計算式
				if (CheckIsNeedInverseMethod(p.Formulas.Last()))
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
		/// <param name="p">題型參數</param>
		public override void MarkFormulaList(MathWordProblemsParameter p)
		{
			// 讀取出題資料庫
			GetAllProblemsFromResource(p);
			// 算式作成
			MarkFormulaList(p, () => { return CommonUtil.GetRandomNumber(p.Signs.ToList()); });
		}

		/// <summary>
		/// 判定是否需要反推并重新作成計算式
		/// </summary>
		/// <remarks>
		/// 情況1：計算式中存在0
		/// </remarks>
		/// <param name="currentFormula">當前算式</param>
		/// <returns>需要反推：true  正常情況: false</returns>
		private bool CheckIsNeedInverseMethod(MathWordProblemsFormula currentFormula)
		{
			if (currentFormula.Answers.Any(d => d.IndexOf('0') >= 0))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 計算式作成
		/// </summary>
		/// <param name="p">題型參數</param>
		/// <param name="signProblems">指定運算符的出題資料庫</param>
		private void MarkFormulas(MathWordProblemsParameter p, List<Problems> signProblems)
		{
			// 資料庫中的應用題隨機取得（不重複）
			Problems problem = GetRandomProblemsIndex(signProblems);
			// 題型列表作成
			p.Formulas.Add(new MathWordProblemsFormula()
			{
				// 單位
				Unit = problem.Unit ?? string.Empty,
				// 應用題文字內容
				MathWordProblem = ZipHelper.GZipDecompressString(problem.Content),
				// 標準答案
				Answers = problem.Answers.Select(d => d = Base64.DecodeBase64String(d)).ToList()
			});
		}

		/// <summary>
		/// 讀取資料庫
		/// </summary>
		/// <param name="p">題型參數</param>
		private void GetAllProblemsFromResource(MathWordProblemsParameter p)
		{
			var path = p.ProblemsJsonFilePath;
			// 未設定時使用默認配置路徑
			if (string.IsNullOrWhiteSpace(path))
			{
				path = PROBLEMS_JSON_FILE_PATH;
			}
			// 相對路徑的對應
			if (path.StartsWith("~/") || path.StartsWith("~\\"))
			{
				path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\'), path.Substring(2));
			}

			// 讀取資料庫
			using (StreamReader file = File.OpenText(path))
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
			Problems problem = CommonUtil.GetRandomNumber(signsProblems);
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
		/// <param name="levels">等級選擇</param>
		/// <returns>出題資料庫</returns>
		private List<Problems> GetProblemsBySign(SignOfOperation sign, int[] levels)
		{
			return _allProblems.Where(d => d.Sign == (int)sign && Array.IndexOf(levels, d.Level) >= 0).ToList();
		}
	}
}