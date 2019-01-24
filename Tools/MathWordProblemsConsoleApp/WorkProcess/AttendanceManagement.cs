﻿using MyMathSheets.MathWordProblemsConsoleApp.Ext;
using MyMathSheets.MathWordProblemsConsoleApp.Util;
using MyMathSheets.MathWordProblemsConsoleApp.Util.Security;
using MyMathSheets.MathWordProblemsConsoleApp.WorkProcess.Item;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMathSheets.MathWordProblemsConsoleApp.WorkProcess
{
	/// <summary>
	/// XLS文件內容轉JSON類
	/// </summary>
	public class AttendanceManagement
	{
		/// <summary>
		/// xls公共處理類（當掐活動的SHEET對象）
		/// </summary>
		private SpireXls _xls { get; set; }
		
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="xls">SHEET對象</param>
		public AttendanceManagement(SpireXls xls)
		{
			_xls = xls;
		}

		/// <summary>
		/// 讀取文件并轉換為特定類（填空題題型類）
		/// </summary>
		/// <param name="jsonFileName">新作成的JSON文件名</param>
		public void SetGapFillingProblemsJson(string jsonFileName)
		{
			List<GapFillingProblems> list = new List<GapFillingProblems>();

			var rowIndex = 2;
			while (true)
			{
				rowIndex++;
				if (string.IsNullOrWhiteSpace(_xls.GetRangeText(string.Format("B{0}", rowIndex))))
				{
					break;
				}

				if("off".Equals(_xls.GetRangeText(string.Format("A{0}", rowIndex))))
				{
					continue;
				}

				// 構造
				list.Add(new GapFillingProblems()
				{
					ID = _xls.GetRangeText(string.Format("B{0}", rowIndex)),
					// 題型內容壓縮加密
					Content = ZipHelper.GZipCompressString(_xls.GetRangeText(string.Format("C{0}", rowIndex))),
					Level = Convert.ToInt16(_xls.GetRangeText(string.Format("AD{0}", rowIndex))),
					Parameters = new List<string>() {
						_xls.GetRangeText(string.Format("D{0}", rowIndex)),
						_xls.GetRangeText(string.Format("E{0}", rowIndex)),
						_xls.GetRangeText(string.Format("F{0}", rowIndex)),
						_xls.GetRangeText(string.Format("G{0}", rowIndex)),
						_xls.GetRangeText(string.Format("H{0}", rowIndex)),
						_xls.GetRangeText(string.Format("I{0}", rowIndex)),
						_xls.GetRangeText(string.Format("J{0}", rowIndex)),
						_xls.GetRangeText(string.Format("K{0}", rowIndex)),
						_xls.GetRangeText(string.Format("L{0}", rowIndex)),
						_xls.GetRangeText(string.Format("M{0}", rowIndex)),
					},
					Answers = new List<string>() {
						_xls.GetRangeText(string.Format("N{0}", rowIndex), true),
						_xls.GetRangeText(string.Format("O{0}", rowIndex), true),
						_xls.GetRangeText(string.Format("P{0}", rowIndex), true),
						_xls.GetRangeText(string.Format("Q{0}", rowIndex), true),
						_xls.GetRangeText(string.Format("R{0}", rowIndex), true),
						_xls.GetRangeText(string.Format("S{0}", rowIndex), true),
						_xls.GetRangeText(string.Format("T{0}", rowIndex), true),
						_xls.GetRangeText(string.Format("U{0}", rowIndex), true),
						_xls.GetRangeText(string.Format("V{0}", rowIndex), true),
						_xls.GetRangeText(string.Format("W{0}", rowIndex), true)
					}
				});

				Console.Write(".");
			}
			Console.WriteLine();
			// JSON轉換處理
			string content = list.GetJsonByObject();
			// 寫文件（作成json文件）
			Write(content, jsonFileName);
		}

		/// <summary>
		/// 讀取文件并轉換為特定類（應用題題型類）
		/// </summary>
		/// <param name="jsonFileName">新作成的JSON文件名</param>
		public void SetMathWordProblemsJson(string jsonFileName)
		{
			List<MathWordProblems> list = new List<MathWordProblems>();

			var rowIndex = 2;
			while (true)
			{
				rowIndex++;
				if (string.IsNullOrWhiteSpace(_xls.GetRangeText(string.Format("B{0}", rowIndex))))
				{
					break;
				}

				// 構造
				list.Add(new MathWordProblems()
				{
					ID = _xls.GetRangeText(string.Format("B{0}", rowIndex)),
					// 題型內容壓縮加密
					Content = ZipHelper.GZipCompressString(_xls.GetRangeText(string.Format("C{0}", rowIndex))),
					Sign = Convert.ToInt16(_xls.GetRangeText(string.Format("N{0}", rowIndex))),
					Level = Convert.ToInt16(_xls.GetRangeText(string.Format("AA{0}", rowIndex))),
					Parameters = new List<string>() {
						_xls.GetRangeText(string.Format("D{0}", rowIndex)),
						_xls.GetRangeText(string.Format("E{0}", rowIndex)),
						_xls.GetRangeText(string.Format("F{0}", rowIndex)),
						_xls.GetRangeText(string.Format("G{0}", rowIndex)),
						_xls.GetRangeText(string.Format("H{0}", rowIndex)),
						_xls.GetRangeText(string.Format("I{0}", rowIndex)),
						_xls.GetRangeText(string.Format("J{0}", rowIndex)),
						_xls.GetRangeText(string.Format("K{0}", rowIndex)),
						_xls.GetRangeText(string.Format("L{0}", rowIndex)),
						_xls.GetRangeText(string.Format("M{0}", rowIndex)),
					},
					Answers = new List<string>() {
						GetAnswer(string.Format("P{0}", rowIndex)),
						GetAnswer(string.Format("Q{0}", rowIndex)),
						GetAnswer(string.Format("R{0}", rowIndex)),
						GetAnswer(string.Format("S{0}", rowIndex)),
						GetAnswer(string.Format("T{0}", rowIndex))
					},
					Unit = _xls.GetRangeText(string.Format("O{0}", rowIndex))
				});

				Console.Write(".");
			}
			Console.WriteLine();
			// JSON轉換處理
			string content = list.GetJsonByObject();
			// 寫文件（作成json文件）
			Write(content, jsonFileName);
		}

		/// <summary>
		/// 獲取公式答案
		/// </summary>
		/// <param name="cellName">單元格位置</param>
		/// <returns>公式答案(eg: D1+E1 => 9+1=10)</returns>
		private string GetAnswer(string cellName)
		{
			string formula = _xls.GetFormula(cellName);
			if (string.IsNullOrEmpty(formula))
			{
				return string.Empty;
			}

			// 公式中的坐標取得
			List<string> cellNameList = formula.Split(new char[4] { '+', '-', '*', '/' }).ToList();
			cellNameList.ForEach(d => {
				// 遍歷坐標并取得相應的值，替換至公式中
				formula = formula.Replace(d, _xls.GetRangeText(d));
			});
			// 公式結果取得并添加在末尾
			formula += string.Format("={0}", _xls.GetRangeText(cellName));
			return Base64.EncodeBase64(formula);
		}

		/// <summary>
		/// 寫文件（作成json文件）
		/// </summary>
		/// <param name="content">寫入文件的內容</param>
		/// <param name="jsonFileName">新作成的JSON文件名</param>
		private void Write(string content, string jsonFileName)
		{
			using (FileStream fs = new FileStream(Path.GetFullPath(string.Format(@"..\App_Data\output\{0}.json", jsonFileName)), FileMode.Create))
			{
				// 获得字节数组
				byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
				// 开始写入
				fs.Write(data, 0, data.Length);
				// 清空缓冲区、关闭流
				fs.Flush();
			}
		}
	}
}
