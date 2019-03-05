using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using System;
using System.Windows.Forms;

namespace MyMathSheets.MathSheetsSettingApp
{
	/// <summary>
	/// 
	/// </summary>
	public partial class FrmModelLoad : Form
	{
		private static Log log = Log.LogReady(typeof(FrmModelLoad));

		/// <summary>
		/// 
		/// </summary>
		public FrmModelLoad()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="modelCount"></param>
		private void SearchModelEvent(int modelCount)
		{
			MethodInvoker invoker = () =>
			{
				progressBar.Value = 0;
				progressBar.Minimum = 0;
				progressBar.Maximum = modelCount;
			};

			if (progressBar.InvokeRequired)
			{
				progressBar.Invoke(invoker);
			}
			else
			{
				invoker();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="current"></param>
		private void ModelLoadEvent(int current)
		{
			MethodInvoker invoker = () =>
			{
				progressBar.Value = current;
				if (progressBar.Value == progressBar.Maximum)
				{
					var frmMain = new FrmMain();
					frmMain.ShowDialog(this);
					this.Close();
				}
			};

			if (progressBar.InvokeRequired)
			{
				progressBar.Invoke(invoker);
			}
			else
			{
				invoker();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmModelLoad_Load(object sender, EventArgs e)
		{
			ComposerFactory.ModelLoadEvent += new ComposerFactory.ModelLoadEventHandler(ModelLoadEvent);
			ComposerFactory.SearchModelEvent += new ComposerFactory.SearchModelEventHandler(SearchModelEvent);

			// 異步處理
			Action handler = new Action(ComposerFactory.Init);
			handler.BeginInvoke(ar => handler.EndInvoke(ar), null);
		}
	}
}
