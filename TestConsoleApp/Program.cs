﻿using MyMathSheets.CommonLib.Plugin;

namespace MyMathSheets.TestConsoleApp
{
	/// <summary>
	///
	/// </summary>
	public class Program : ProgramBase
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="args"></param>
		private static void Main(string[] args)
		{
			PluginHelper.GetManager().Initialize();

			// 主程序啟動
			var program = new Program();
			program.Start(args);
		}
	}
}