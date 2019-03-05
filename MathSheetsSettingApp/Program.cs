using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.MathSheetsSettingApp.Properties;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MyMathSheets.MathSheetsSettingApp
{
	/// <summary>
	/// 
	/// </summary>
	static class Program
	{
		private static Log log = Log.LogReady(typeof(Program));

		/// <summary>
		/// 
		/// </summary>
		[STAThread]
		static void Main()
		{
			// 日誌配置初期化
			log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(@"log4net.config"));

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(ConsoleMain_UnhandledException);
		
			Application.Run(new FrmModelLoad());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			try
			{
				Exception ex = e.Exception;

				log.Debug(MessageUtil.GetException(() => MsgResources.E0001A), ex);

				MessageBox.Show(MsgResources.E0001A);
			}
			finally
			{
				Application.Exit();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static private void ConsoleMain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
		{
			try
			{
				Exception ex = (Exception)e.ExceptionObject;

				log.Debug(MessageUtil.GetException(() => MsgResources.E0001A), ex);

				MessageBox.Show(MsgResources.E0001A);
			}
			finally
			{
				Application.Exit();
			}
		}
	}
}
