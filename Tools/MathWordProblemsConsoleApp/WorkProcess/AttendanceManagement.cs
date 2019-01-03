using MyMathSheets.MathWordProblemsConsoleApp.Ext;
using MyMathSheets.MathWordProblemsConsoleApp.Util;
using MyMathSheets.MathWordProblemsConsoleApp.WorkProcess.Item;
using System.Collections.Generic;
using System.IO;

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
		/// <param name="workbook">SHEET對象</param>
		public AttendanceManagement(SpireXls xls)
		{
			_xls = xls;
		}

		/// <summary>
		/// 讀取文件并轉換為特定類（應用題題型類）
		/// </summary>
		public void SetMathWordProblemsJson()
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
					Content = _xls.GetRangeText(string.Format("C{0}", rowIndex)),
					Verify = _xls.GetRangeText(string.Format("D{0}", rowIndex)),
					Unit = _xls.GetRangeText(string.Format("E{0}", rowIndex))
				});
			}
			// JSON轉換處理
			string content = list.GetJsonByObject();
			// 寫文件（作成json文件）
			Write(content);
		}

		/// <summary>
		/// 寫文件（作成json文件）
		/// </summary>
		/// <param name="content"></param>
		private void Write(string content)
		{
			using (FileStream fs = new FileStream(Path.GetFullPath(@"..\App_Data\output\Problems.json"), FileMode.Create))
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
