using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.TheFormulaShows
{
	/// <summary>
	/// 
	/// </summary>
	public class MakeHtml<T> where T : ParameterBase
	{
		/// <summary>
		/// 
		/// </summary>
		private OperationHelper _helper;

		/// <summary>
		/// 
		/// </summary>
		protected OperationHelper Helper => _helper ?? (_helper = new OperationHelper());

		/// <summary>
		/// 
		/// </summary>
		public void Structure(T parameter)
		{
			IOperation instance = Helper.CreateOperationInstance(LayoutSetting.Preview.Arithmetic);
			instance.MarkFormulaList(parameter);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetHtmlStatement()
		{
			//Type type = typeof(F);

			//IMakeHtml<T> support = GetHtmlSupportInstance(type.Name);
			//return support.MakeHtml(_formulas);
			return string.Empty;
		}
	}
}
