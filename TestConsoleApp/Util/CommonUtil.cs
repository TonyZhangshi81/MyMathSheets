﻿using MyMathSheets.CommonLib.Main;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.TestConsoleApp.Write.Main;

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
		public static string GetValue<T>(GapFilling item, T parameter, GapFilling gap)
		{
			return item == gap ? string.Format("[{0}]", parameter) : parameter.ToString();
		}

		/// <summary>
		/// 題型測試結果輸出
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <param name="args">調試用參數</param>
		public static void ConsoleFormulas(string topicIdentifier, string args)
		{
			TopicParameterBase parameter = PolicyHelper.Instance.Structure(topicIdentifier, args);

			FormulasConsolerFactory formulasConsoler = new FormulasConsolerFactory();
			var writer = formulasConsoler.CreateConsoleWriter(topicIdentifier);
			writer.Do(parameter);
		}
	}
}