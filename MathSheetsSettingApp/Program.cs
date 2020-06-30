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
	internal static class Program
	{
		/// <summary>
		///
		/// </summary>
		[STAThread]
		private static void Main()
		{
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

				LogUtil.LogError(MessageUtil.GetException(() => MsgResources.E0001A), ex);

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

				LogUtil.LogError(MessageUtil.GetException(() => MsgResources.E0001A), ex);

				MessageBox.Show(MsgResources.E0001A);
			}
			finally
			{
				Application.Exit();
			}
		}
	}
}