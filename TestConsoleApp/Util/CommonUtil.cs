using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.TestConsoleApp
{
	/// <summary>
	/// 
	/// </summary>
	public static class CommonUtil
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="parameter"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		public static string GetValue(GapFilling item, int parameter, GapFilling gap)
		{
			return item == gap ? string.Format("[{0}]", parameter) : parameter.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="parameter"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		public static string GetValue(GapFilling item, string parameter, GapFilling gap)
		{
			return item == gap ? string.Format("[{0}]", parameter) : parameter;
		}

		/// <summary>
		/// 題型測試結果輸出
		/// </summary>
		/// <typeparam name="T">各題型類的輸出結果類型參數</typeparam>
		/// <param name="preview">題型種類</param>
		/// <param name="formulas">各題型類的輸出結果對象</param>
		public static void ConsoleFormulas<T>(LayoutSetting.Preview preview, T formulas)
		{
			var writer = FormulasConsolerFactory.Instance.CreateConsoleWriter(preview, formulas);
			writer.ConsoleFormulas(formulas);
		}
	}
}
