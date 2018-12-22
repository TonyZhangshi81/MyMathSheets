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
		static void Main(string[] args)
		{
			// 日誌配置初期化
			log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(@"log4net.config"));

			// 主程序啟動
			var program = new Program();
			program.Start(args);
		}
	}
}
