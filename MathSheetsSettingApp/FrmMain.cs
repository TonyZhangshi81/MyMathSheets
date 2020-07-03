using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.FromProcess;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.MathSheetsSettingApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyMathSheets.MathSheetsSettingApp
{
	/// <summary>
	///
	/// </summary>
	public partial class FrmMain : Form
	{
		/// <summary>
		/// 畫面處理類
		/// </summary>
		[Import(typeof(IMainProcess))]
		private MainProcess Process
		{
			get;
			set;
		}

		/// <summary>
		/// 畫面構造函數
		/// </summary>
		public FrmMain()
		{
			InitializeComponent();

			// 獲取HTML支援類Composer
			Composer composer = ComposerFactory.GetComporser(SystemModel.Common);
			composer.Compose(this);
		}

		/// <summary>
		/// 畫面初期化處理
		/// </summary>
		/// <param name="sender">事件發生者</param>
		/// <param name="e">事件處理</param>
		private void FormLoad(object sender, EventArgs e)
		{
			LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0001A));

			this.Owner.Hide();

			// 城市背景滾動效果初期化設定
			ScrollCityInit();

			// 題型縮略瀏覽初期化
			PreviewInit();

			// 歷屆題型試卷初期化顯示
			WorkPagesDisplay();

			// 創建題型選擇控件
			CreateQuestionCheckBoxList();

			LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0008A));
		}

		/// <summary>
		/// 歷屆題型試卷初期化顯示
		/// </summary>
		private void WorkPagesDisplay()
		{
			cmbWorkPages.DataSource = Process.GetWorkPageFiles();
		}

		/// <summary>
		/// 創建題型選擇控件
		/// </summary>
		private void CreateQuestionCheckBoxList()
		{
			int rowIndex = 0;
			int controlIndex = 0;
			// 題型分類集合
			List<LayoutSetting.Classify> classifyList = Process.ControlList.GroupBy(d => d.Classify).Select(d => d.Key).ToList();

			int locationY = 0;
			int rowCount = 0;
			classifyList.ForEach(c =>
			{
				GroupBox groubox = new GroupBox
				{
					Name = $"groubox{rowIndex}",
					Width = 360,
					Text = c.ToClassifyName(),
					Location = new Point(0, locationY)
				};

				var controlList = Process.ControlList.Where(d => c == d.Classify).ToList();
				if (controlList.Count > 0)
				{
					controlList.ForEach(d =>
					{
						CheckBox checkBox = new CheckBox
						{
							AutoSize = true,
							Location = new Point((10 + (d.IndexX - 1) * 120), (20 + (d.IndexY - 1) * 33)),
							Name = d.ControlId,
							Size = new Size(72, 16),
							Text = d.Title,
							TabIndex = controlIndex,
							UseVisualStyleBackColor = true
						};
						checkBox.CheckedChanged += new EventHandler(QuestionCheckedChanged);

						groubox.Controls.Add(checkBox);

						controlIndex++;
						rowCount = d.IndexY;
					});
				}
				groubox.Height = 15 + rowCount * 33;
				locationY += groubox.Height + 10;

				this.panel1.Controls.Add(groubox);
				rowIndex++;
			});
		}

		/// <summary>
		/// 題型縮略瀏覽初期化
		/// </summary>
		private void PreviewInit()
		{
			// 標題瀏覽
			PictureIntoFlowLayoutPanel(LayoutSetting.Preview.Title);
			// 答題結束瀏覽
			PictureIntoFlowLayoutPanel(LayoutSetting.Preview.Ready);
		}

		/// <summary>
		/// 添加題型模塊至瀏覽項目
		/// </summary>
		/// <param name="name">題型的名字</param>
		private void PictureIntoFlowLayoutPanel(LayoutSetting.Preview name)
		{
			PictureBox picBox = new PictureBox
			{
				// 獲取題型項目的縮略圖
				Image = (Bitmap)ImgResources.ResourceManager.GetObject(name.ToString(), CultureInfo.CurrentCulture),
				Tag = name.ToString(),
				SizeMode = PictureBoxSizeMode.StretchImage,
				Width = flpPreview.Width - 20,
				Height = 100
			};
			// 防止閃爍
			flpPreview.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(picBox, true, null);
			// 將題型縮略圖添加至瀏覽框
			flpPreview.Controls.Add(picBox);

			picBox.Margin = new Padding(0);
		}

		/// <summary>
		/// 出題按鍵點擊事件
		/// </summary>
		/// <param name="sender">事件發生者</param>
		/// <param name="e">事件處理</param>
		private void SureClick(object sender, EventArgs e)
		{
			TopMost = true;

			// 使用IE打開已作成的靜態頁面
			Process cmdProcess = new Process();
			// 命令
			cmdProcess.StartInfo.FileName = ConfigurationManager.AppSettings.Get("Preview");
			// 顯示DOC界面
			cmdProcess.StartInfo.CreateNoWindow = true;
			// 啟動Exited事件
			cmdProcess.EnableRaisingEvents = true;
			// 註冊進程結束事件
			//cmdProcess.Exited += new EventHandler((s, v) =>
			//{
			// 註冊進程結束事件
			//Environment.Exit(0);
			//});

			if (cmbWorkPages.SelectedIndex > 0)
			{
				// 參數
				cmdProcess.StartInfo.Arguments = ("\"" + Path.GetFullPath(ConfigurationManager.AppSettings.Get("HtmlWork") + cmbWorkPages.SelectedValue.ToString()) + "\"");
				cmdProcess.Start();

				cmdProcess.WaitForExit(5000);

				Environment.Exit(0);

				return;
			}

			// 選題情況
			if (!Process.ChooseCheck())
			{
				MessageBox.Show(this, MessageUtil.GetException(() => MsgResources.I0009A));
				return;
			}

			LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0002A));

			// 出題按鍵點擊事件
			var destFileName = Process.SureClick();

			// 參數
			cmdProcess.StartInfo.Arguments = ("\"" + Path.GetFullPath(@destFileName) + "\"");
			cmdProcess.Start();

			cmdProcess.WaitForExit(5000);

			Environment.Exit(0);
		}

		/// <summary>
		/// 題型縮略瀏覽初期化
		/// </summary>
		private void PreviewReflash()
		{
			flpPreview.Controls.Clear();

			// 瀏覽區域顯示
			Process.LayoutSettingPreviewList.ForEach(d => PictureIntoFlowLayoutPanel(d));
		}

		/// <summary>
		/// 題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void QuestionCheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0006A, checkBox.Text));
			}
			else
			{
				LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0007A, checkBox.Text));
			}

			Process.TopicCheckedChanged(checkBox.Checked,
					() => Process.ControlList.Where(d => d.ControlId.Equals(checkBox.Name, StringComparison.CurrentCultureIgnoreCase)).First());

			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 畫面關閉事件
		/// </summary>
		/// <param name="sender">關閉按鈕</param>
		/// <param name="e">關閉事件</param>
		private void CloseClick(object sender, EventArgs e)
		{
			// 退出系統
			Environment.Exit(0);
		}

		/// <summary>
		/// 城市背景滾動效果初期化設定
		/// </summary>
		private void ScrollCityInit()
		{
			CarMoveUnit = 4;
			CityMoveUnit = 4;
			IsInCity = true;

			picCity.Controls.Add(picCar);
			picCar.Location = new Point(30, 66);
			picCar.BackColor = Color.Transparent;

			panel2.Controls.Add(picCity);
			picCity.Location = new Point(0, 0);

			panel2.Controls.Add(picCityNext);
			picCityNext.Location = new Point(panel2.Width, 0);
		}

		/// <summary>
		/// 用於播放城市背景滾動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TimerTick(object sender, EventArgs e)
		{
			picCity.Location = new Point(picCity.Location.X - CityMoveUnit, picCity.Location.Y);
			picCityNext.Location = new Point(picCityNext.Location.X - CityMoveUnit, picCity.Location.Y);

			picCar.Location = new Point(picCar.Location.X + CarMoveUnit, picCar.Location.Y);

			// 小車在城市背景中所剩路程已不足80的時候，減速慢行
			if ((IsInCity && (picCity.Width - picCar.Location.X <= 120)) ||
				(!IsInCity && (picCityNext.Width - picCar.Location.X <= 120)))
			{
				CarMoveUnit = 2;
			}
			else if ((IsInCity && (picCity.Width - picCar.Location.X <= 250) && (picCity.Width - picCar.Location.X > 120)) ||
			   (!IsInCity && (picCityNext.Width - picCar.Location.X <= 250) && (picCityNext.Width - picCar.Location.X > 120)))
			{
				// 小車在城市背景中所剩路程已不足160的時候，勻速行駛
				CarMoveUnit = 4;
			}

			if (picCity.Location.X * -1 >= panel2.Width)
			{
				picCity.Controls.Remove(picCar);
				picCity.Location = new Point(panel2.Width, 0);

				IsInCity = false;
				picCityNext.Controls.Add(picCar);
				picCar.Location = new Point(-46, 66);
				picCar.BackColor = Color.Transparent;

				// 小車已經離開城市背景的時候,急速追趕
				CarMoveUnit = 8;
			}

			if (picCityNext.Location.X * -1 >= panel2.Width)
			{
				picCityNext.Controls.Remove(picCar);
				picCityNext.Location = new Point(panel2.Width, 0);

				IsInCity = true;
				picCity.Controls.Add(picCar);
				picCar.Location = new Point(-46, 66);
				picCar.BackColor = Color.Transparent;

				// 小車已經離開城市背景的時候,急速追趕
				CarMoveUnit = 8;
			}
		}

		/// <summary>
		/// 小車是否在城市背景中
		/// </summary>
		private bool IsInCity { get; set; }

		/// <summary>
		/// 小車移動單位
		/// </summary>
		private int CarMoveUnit { get; set; }

		/// <summary>
		/// 城市背景移動單位
		/// </summary>
		private int CityMoveUnit { get; set; }
	}
}