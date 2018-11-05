using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.FromProcess;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.MathSheetsSettingApp.Properties;
using System;
using System.ComponentModel.Composition;
using System.IO;
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
		private void Form1_Load(object sender, EventArgs e)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0001A));

			// 題型縮略瀏覽初期化
			PreviewInit();
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
				Image = (System.Drawing.Bitmap)ImgResources.ResourceManager.GetObject(name.ToString()),
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
			// 選題情況
			if (!_process.ChooseCheck())
			{
				MessageBox.Show(this, "運算符未指定");
				return;
			}

			log.Debug(MessageUtil.GetException(() => MsgResources.I0002A));
			// 出題按鍵點擊事件
			var destFileName = _process.SureClick();
			// 是否瀏覽
			if (chkIsPreview.Checked)
			{
				// 使用IE打開已作成的靜態頁面
				System.Diagnostics.Process.Start(@"chrome.exe", "\"" + Path.GetFullPath(@destFileName) + "\"");
			}
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
		/// 四則運算題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void ArithmeticCheckedChanged(object sender, EventArgs e)
		{
			if (chkArithmetic.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "四則運算"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "四則運算"));
			}
			_process.TopicCheckedChanged(chkArithmetic.Checked, LayoutSetting.Preview.Arithmetic, "AC001");

			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 等式比大小題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void EqualityComparisonCheckedChanged(object sender, EventArgs e)
		{
			if (chkEqualityComparison.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "等式比大小"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "等式比大小"));
			}
			_process.TopicCheckedChanged(chkEqualityComparison.Checked, LayoutSetting.Preview.EqualityComparison, "EC001");
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 等式接龍題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void ComputingConnectionCheckedChanged(object sender, EventArgs e)
		{
			if (chkComputingConnection.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "等式接龍"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "等式接龍"));
			}
			_process.TopicCheckedChanged(chkComputingConnection.Checked, LayoutSetting.Preview.ComputingConnection, "CC001");
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 算式應用題題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void MathWordProblemsCheckedChanged(object sender, EventArgs e)
		{
			if (chkMathWordProblems.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "算式應用題"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "算式應用題"));
			}
			_process.TopicCheckedChanged(chkMathWordProblems.Checked, LayoutSetting.Preview.MathWordProblems, "MP001");
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 水果連連看題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void FruitsLinkageCheckedChanged(object sender, EventArgs e)
		{
			if (chkFruitsLinkage.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "水果連連看"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "水果連連看"));
			}
			_process.TopicCheckedChanged(chkFruitsLinkage.Checked, LayoutSetting.Preview.FruitsLinkage, "FL001");
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 找出最近的數字題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void FindNearestNumberCheckedChanged(object sender, EventArgs e)
		{
			if (chkFindNearestNumber.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "找出最近的數字"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "找出最近的數字"));
			}
			_process.TopicCheckedChanged(chkFindNearestNumber.Checked, LayoutSetting.Preview.FindNearestNumber, "FN001");
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 算式組合題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void CombinatorialEquationCheckedChanged(object sender, EventArgs e)
		{
			if (chkCombinatorialEquation.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "算式組合"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "算式組合"));
			}
			_process.TopicCheckedChanged(chkCombinatorialEquation.Checked, LayoutSetting.Preview.CombinatorialEquation, "CE001");
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 射門得分題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void ScoreGoalCheckedChanged(object sender, EventArgs e)
		{
			if (chkScoreGoal.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "射門得分"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "射門得分"));
			}
			_process.TopicCheckedChanged(chkScoreGoal.Checked, LayoutSetting.Preview.ScoreGoal, "SG001");
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 比多少題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void HowMuchMoreCheckedChanged(object sender, EventArgs e)
		{
			if (chkHowMuchMore.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "比多少"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "比多少"));
			}
			_process.TopicCheckedChanged(chkHowMuchMore.Checked, LayoutSetting.Preview.HowMuchMore, "HMM001");
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 找規律題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void FindTheLawCheckedChanged(object sender, EventArgs e)
		{
			if (chkFindTheLaw.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "找規律"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "找規律"));
			}
			_process.TopicCheckedChanged(chkFindTheLaw.Checked, LayoutSetting.Preview.FindTheLaw, "FTL001");
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// 數字排序題型選擇事件
		/// </summary>
		/// <param name="sender">選擇框</param>
		/// <param name="e">選擇事件</param>
		private void NumericSortingCheckedChanged(object sender, EventArgs e)
		{
			if (chkNumericSorting.Checked)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0006A, "數字排序"));
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "數字排序"));
			}
			_process.TopicCheckedChanged(chkNumericSorting.Checked, LayoutSetting.Preview.NumericSorting, "NS001");
			// 刷新題型預覽區域
			PreviewReflash();
		}
	}
}
