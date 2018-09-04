using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MathWordProblemsConsoleApp.Ext
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
		/// 
		/// </summary>
		/// <param name="cellName"></param>
		/// <param name="value"></param>
		/// <param name="style"></param>
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
