using MyMathSheets.MathWordProblemsConsoleApp.Ext;
using MyMathSheets.MathWordProblemsConsoleApp.Util;
using MyMathSheets.MathWordProblemsConsoleApp.WorkProcess.Item;
using System.Collections.Generic;
using System.IO;

namespace MyMathSheets.MathWordProblemsConsoleApp.WorkProcess
{
	/// <summary>
	/// 
	/// </summary>
	public class AttendanceManagement
	{
		/// <summary>
		/// 
		/// </summary>
		private SpireXls _xls { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="workbook"></param>
		public AttendanceManagement(SpireXls xls)
		{
			_xls = xls;
		}

		/// <summary>
		/// 
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

				list.Add(new MathWordProblems()
				{
					ID = _xls.GetRangeText(string.Format("B{0}", rowIndex)),
					Content = _xls.GetRangeText(string.Format("C{0}", rowIndex)),
					Verify = _xls.GetRangeText(string.Format("D{0}", rowIndex))
				});
			}

			string content = list.GetJsonByObject();

			Write(content);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="content"></param>
		private void Write(string content)
		{
			using (FileStream fs = new FileStream(Path.GetFullPath(@"..\..\..\ComputationalStrategy\Config\Problems.json"), FileMode.Create))
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
