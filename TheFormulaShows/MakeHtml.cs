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
		/// <param name="parameter"></param>
		public T Structure(LayoutSetting.Preview preview, string identifier)
		{
			return Helper.Structure(preview, identifier) as T;
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
