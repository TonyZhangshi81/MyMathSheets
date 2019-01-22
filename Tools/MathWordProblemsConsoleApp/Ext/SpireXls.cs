using Spire.Xls;
using System;

namespace MyMathSheets.MathWordProblemsConsoleApp.Ext
{
	/// <summary>
	/// Excel read simple processing
	/// </summary>
	public class SpireXls : ISpireExt, IDisposable
	{
		/// <summary>
		/// 
		/// </summary>
		Workbook _workBook { get; set; }

		/// <summary>
		/// 
		/// </summary>
		private Worksheet _workSheet;
		/// <summary>
		/// 
		/// </summary>
		public Worksheet Sheet
		{
			get
			{
				if (_workSheet == null)
				{
					_workSheet = _workBook.ActiveSheet;
					if (_workSheet == null)
					{
						_workSheet = _workBook.Worksheets[0];
						_workBook.Worksheets[0].Activate();
					}
				}
				return _workSheet;
			}
			set
			{
				_workSheet = value;
			}
		}

		/// <summary>
		/// The constructor
		/// </summary>
		public SpireXls()
		{
			_workBook = new Workbook();
		}

		#region Load the Excel file

		/// <summary>
		/// Load an existing Excel file cannot be loaded by the password protected file
		/// </summary>
		/// <param name="fileName">The file name</param>
		public void Load(string fileName)
		{
			_workBook.LoadFromFile(fileName, ExcelVersion.Version2007);
		}

		#endregion

		/// <summary>
		/// 返回指定單元格的值
		/// </summary>
		/// <param name="cellName">單元格位置</param>
		/// <returns>單元格的值</returns>
		public string GetRangeText(string cellName)
		{
			CellRange cell = Sheet.Range[cellName];
			if (cell.HasFormula)
			{
				Object value = cell.FormulaValue;
				cell.Clear(ExcelClearOptions.ClearContent);
				cell.Value2 = value;
			}
			return Sheet.Range[cellName].Value;
		}

		/// <summary>
		/// 返回指定單元格的公式
		/// </summary>
		/// <param name="cellName">單元格位置</param>
		/// <returns>單元格的公式</returns>
		public string GetFormula(string cellName)
		{
			CellRange cell = Sheet.Range[cellName];
			if (cell.HasFormula)
			{
				return cell.Formula.Substring(1);
			}
			return string.Empty;
		}

		#region Release resources

		/// <summary>
		/// Release resources
		/// </summary>
		public void Dispose(Workbook workbook) => _workBook.Dispose();

		/// <summary>
		/// Release resources
		/// </summary>
		public void Dispose()
		{
			if (_workBook != null)
			{
				Dispose(_workBook);
			}
		}

		#endregion
	}
}
