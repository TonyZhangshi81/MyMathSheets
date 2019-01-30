﻿using MyMathSheets.CommonLib.Composition;
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
		private static Log log = Log.LogReady(typeof(FrmMain));

		/// <summary>
		/// 畫面處理類
		/// </summary>
		[Import(typeof(IMainProcess))]
		private IMainProcess _process { get; set; }

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
			log.Debug(MessageUtil.GetException(() => MsgResources.I0001A));

			this.Width = 920;
			this.Height = 490;

			// 題型縮略瀏覽初期化
			PreviewInit();
			// 歷屆題型試卷初期化顯示
			WorkPagesDisplay();

			// 創建題型選擇控件
			CreateQuestionCheckBoxList();
		}

		/// <summary>
		/// 歷屆題型試卷初期化顯示
		/// </summary>
		private void WorkPagesDisplay()
		{
			cmbWorkPages.DataSource = _process.GetWorkPageFiles();
		}

		/// <summary>
		/// 創建題型選擇控件
		/// </summary>
		private void CreateQuestionCheckBoxList()
		{
			int rowIndex = 0;
			int controlIndex = 0;
			// 題型分類集合
			List<LayoutSetting.Classify> classifyList = _process.ControlList.GroupBy(d => d.Classify).Select(d => d.Key).ToList();

			int locationY = 0;
			int rowCount = 0;
			classifyList.ForEach(c =>
			{
				GroupBox groubox = new GroupBox();
				groubox.Name = "groubox" + rowIndex.ToString();
				groubox.Width = 360;
				groubox.Text = c.ToClassifyName();
				groubox.Location = new Point(0, locationY);

				var controlList = _process.ControlList.Where(d => c == d.Classify).ToList();
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
				Image = (Bitmap)ImgResources.ResourceManager.GetObject(name.ToString()),
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
			if (cmbWorkPages.SelectedIndex > 0)
			{
				Process.Start(ConfigurationManager.AppSettings.Get("Preview"), "\"" + Path.GetFullPath(ConfigurationManager.AppSettings.Get("HtmlWork") + cmbWorkPages.SelectedValue.ToString()) + "\"");
				// 退出系統
				Environment.Exit(0);
			}

			// 選題情況
			if (!_process.ChooseCheck())
			{
				MessageBox.Show(this, "题型未选择");
				return;
			}

			log.Debug(MessageUtil.GetException(() => MsgResources.I0002A));
			// 出題按鍵點擊事件
			var destFileName = _process.SureClick();
			// 使用IE打開已作成的靜態頁面
			Process.Start(ConfigurationManager.AppSettings.Get("Preview"), "\"" + Path.GetFullPath(@destFileName) + "\"");
			// 退出系統
			Environment.Exit(0);
		}

		/// <summary>
		/// 題型縮略瀏覽初期化
		/// </summary>
		private void PreviewReflash()
		{
			flpPreview.Controls.Clear();

			// 瀏覽區域顯示
			_process.LayoutSettingPreviewList.ForEach(d => PictureIntoFlowLayoutPanel(d));
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
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, checkBox.Text));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, checkBox.Text));
			}
			_process.TopicCheckedChanged(checkBox.Checked, () => _process.ControlList.Where(d => d.ControlId.Equals(checkBox.Name)).First());

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
	}
}
