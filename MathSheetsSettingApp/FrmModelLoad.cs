using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyMathSheets.MathSheetsSettingApp
{
	/// <summary>
	/// 模塊加載進程顯示窗口
	/// </summary>
	public partial class FrmModelLoad : Form
	{
		/// <summary>
		/// 日誌作成
		/// </summary>
		private static Log log = Log.LogReady(typeof(FrmModelLoad));

		/// <summary>
		/// 單位步長
		/// </summary>
		private decimal UnitStep { get; set; }
		/// <summary>
		/// 模塊總件數
		/// </summary>
		private int ModelCount { get; set; }

		/// <summary>
		/// 窗口初期化
		/// </summary>
		public FrmModelLoad()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 模塊加載事件訂閱（獲取預加載的模塊總件數）
		/// </summary>
		/// <param name="modelCount">模塊總件數</param>
		private void SearchModelEvent(int modelCount)
		{
			// 創建一個線程用來描畫圖片的起始位置
			MethodInvoker invoker = () =>
			{
				ModelCount = modelCount;
				UnitStep = picCity.Width / modelCount;
				picCar.Location = new Point(0, 174);
			};

			// 禁止跨線程直接訪問控件
			// 一個控件的InvokeRequired值為true時，說明有一個創建它以外的線程想訪問它
			// 此時，必須使用控件的Invoke方法來將調用封送到適當的線程
			if (picCar.InvokeRequired)
			{
				picCar.Invoke(invoker);
			}
			else
			{
				invoker();
			}
		}

		/// <summary>
		/// 模塊加載事件訂閱（每加載一個模塊就會響應一次該事件）
		/// </summary>
		/// <param name="current">當前加載的序號</param>
		private void ModelLoadEvent(int current)
		{
			// 創建一個線程用來完成圖片移動（最終打開工作界面窗口）
			MethodInvoker invoker = () =>
			{
				// 最後一個動畫跳過以避免因界面描畫線程阻塞產生車尾的殘影
				if (ModelCount - current > 1)
				{
					// 小車移動
					picCar.Location = new Point(picCar.Location.X + Convert.ToInt32(Math.Floor(UnitStep)), 174);
				}

				// 當最後一個模塊加載完畢的時候
				if (current == ModelCount)
				{
					timer1.Stop();
					// 工作界面窗口顯示
					var frmMain = new FrmMain();
					frmMain.ShowDialog(this);
					// 關閉模塊加載進程顯示窗口（父畫面）
					this.Close();
				}
			};

			if (picCar.InvokeRequired)
			{
				picCar.Invoke(invoker);
			}
			else
			{
				invoker();
			}
		}

		/// <summary>
		/// 窗口加載事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmModelLoad_Load(object sender, EventArgs e)
		{
			picCity.Controls.Add(picCar);
			picCity.Controls.Add(picPlane);

			picCar.Location = new Point(0, 174);
			picCar.BackColor = Color.Transparent;
			picPlane.Location = new Point(picCity.Width - 60, 30);
			picPlane.BackColor = Color.Transparent;

			timer1.Start();

			ComposerFactory.ModelLoadEvent += new ComposerFactory.ModelLoadEventHandler(ModelLoadEvent);
			ComposerFactory.SearchModelEvent += new ComposerFactory.SearchModelEventHandler(SearchModelEvent);

			// 異步處理
			Action handler = new Action(ComposerFactory.Init);
			handler.BeginInvoke(ar => handler.EndInvoke(ar), null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TimerTick(object sender, EventArgs e)
		{
			//double x, y;
			//_number += 5;
			//x = 10 * Math.Sin(_number);
			//y = 10 * Math.Cos(_number);
			picPlane.Location = new Point(picPlane.Location.X - 8, 30);
		}
	}
}
