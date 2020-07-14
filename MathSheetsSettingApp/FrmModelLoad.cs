using MyMathSheets.CommonLib.Plugin;
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
		/// 單位步長
		/// </summary>
		private decimal _unitStep;

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
		/// <param name="sender"></param>
		/// <param name="modelCount">模塊總件數</param>
		private void ModelPreLoadEvent(object sender, int modelCount)
		{
			// 創建一個線程用來描畫圖片的起始位置
			MethodInvoker invoker = () =>
			{
				_unitStep = picCity.Width / (modelCount + 1);
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
		/// <param name="sender"></param>
		/// <param name="current">當前加載的序號</param>
		private void ModelLoadingEvent(object sender, int current)
		{
			// 創建一個線程用來完成圖片移動（最終打開工作界面窗口）
			MethodInvoker invoker =
				() =>
				{
					// 小車移動
					picCar.Location = new Point(picCar.Location.X + Convert.ToInt32(Math.Floor(_unitStep)), 174);
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
		/// 模塊加載完畢後的事件訂閱（關閉模塊加載進程顯示窗口）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="current"></param>
		private void ModelLoadCompleteEvent(object sender, int current)
		{
			MethodInvoker invoker =
				() =>
				{
					timer1.Stop();
					// 工作界面窗口顯示
					var frmMain = new FrmMain();
					frmMain.ShowDialog(this);
					// 關閉模塊加載進程顯示窗口（父畫面）
					this.Close();
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
			picCity.Controls.Add(picWeatherWind);
			picWeatherWind.Visible = false;

			picCar.Location = new Point(0, 174);
			picCar.BackColor = Color.Transparent;
			picPlane.Location = new Point(picCity.Width - 60, 30);
			picPlane.BackColor = Color.Transparent;
			picWeatherWind.Location = new Point(picCity.Width - 430, 30);
			picWeatherWind.BackColor = Color.Transparent;

			timer1.Start();

			PluginHelper helper = new PluginHelper();
			PluginsManagerBase pluginsManage = helper.GetManager();
			pluginsManage.ModelLoading += new PluginsManagerBase.ModelLoadingEventHandler(ModelLoadingEvent);
			pluginsManage.ModelLoadComplete += new PluginsManagerBase.ModelLoadCompleteEventHandler(ModelLoadCompleteEvent);
			pluginsManage.ModelPreLoad += new PluginsManagerBase.ModelPreLoadEventHandler(ModelPreLoadEvent);

			// 異步處理
			Action handler = new Action(pluginsManage.Initialize);
			handler.BeginInvoke(ar => handler.EndInvoke(ar), null);
		}

		/// <summary>
		/// 計時器（開場紙飛機動畫）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TimerTick(object sender, EventArgs e)
		{
			int locationY;
			// 使用餘弦函數計算弧度
			double y = Math.Cos(Math.PI * picPlane.Location.X / 180.0);
			// 正負值計算y軸的相對偏移量（20是小飛機的高度）
			if (y < 0)
			{
				locationY = (int)((y * -1 * 90) * 0.7) + 20;
			}
			else
			{
				locationY = (int)((y * 90) * 0.7) + 20;
				if (locationY < 30)
				{
					locationY = 25;
					picWeatherWind.Visible = true;
				}
			}
			// 位置移動
			picPlane.Location = new Point(picPlane.Location.X - 2, locationY);
		}
	}
}