using MyMathSheets.CommonLib.Main.OperationStrategy;
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
		/// <param name="preview">題型種類</param>
		/// <param name="args">調試用參數</param>
		public static void ConsoleFormulas(string preview, string args)
		{
			ParameterBase parameter = OperationStrategyHelper.Instance.Structure(preview, args);

			var writer = FormulasConsolerFactory.Instance.CreateConsoleWriter(preview);
			writer.ConsoleFormulas(parameter);
		}
	}
}