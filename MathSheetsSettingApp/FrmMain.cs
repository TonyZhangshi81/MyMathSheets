using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
using MyMathSheets.MathSheetsSettingApp.Properties;
using MyMathSheets.TheFormulaShows;
using MyMathSheets.TheFormulaShows.Attributes;
using MyMathSheets.TheFormulaShows.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
		/// 
		/// </summary>
		public FrmMain()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		private Dictionary<string, Dictionary<string, string>> _htmlMaps = new Dictionary<string, Dictionary<string, string>>();
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
		/// 選題情況
		/// </summary>
		private int _selectedTopic = 0;
		/// <summary>
		/// 出題按鍵點擊事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SureClick(object sender, EventArgs e)
		{
			if (_selectedTopic == 0)
			{
				MessageBox.Show(this, "運算符未指定");
				return;
			}

			log.Debug(MessageUtil.GetException(() => MsgResources.I0002A));

			// HTML模板存放路徑
			string sourceFileName = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings.Get("Template"));
			// 靜態頁面作成后存放的路徑（文件名：日期時間形式）
			string destFileName = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings.Get("HtmlWork") + string.Format("{0}.html", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
			// 文件移動
			File.Copy(sourceFileName, destFileName);

			StringBuilder htmlTemplate = new StringBuilder();
			// 讀取HTML模板內容
			htmlTemplate.Append(File.ReadAllText(destFileName, Encoding.UTF8));
			// 遍歷已選擇的題型
			foreach (KeyValuePair<string, Dictionary<string, string>> d in _htmlMaps)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0003A, d.Key));

				// 替換HTML模板中的預留內容（HTML、JS注入操作）
				foreach (KeyValuePair<string, string> m in d.Value)
				{
					log.Debug(MessageUtil.GetException(() => MsgResources.I0004A, m.Key));

					htmlTemplate.Replace(m.Key, m.Value);
				}
			}

			log.Debug(MessageUtil.GetException(() => MsgResources.I0005A));

			// 保存至靜態頁面
			File.WriteAllText(destFileName, htmlTemplate.ToString(), Encoding.UTF8);

			if (chkIsPreview.Checked)
			{
				// 使用IE打開已作成的靜態頁面
				System.Diagnostics.Process.Start(@"chrome.exe", "\"" + Path.GetFullPath(@destFileName) + "\"");
			}
			// 退出系統
			Environment.Exit(0);
		}

		/// <summary>
		/// 題型預覽列表
		/// </summary>
		List<LayoutSetting.Preview> _layoutSettingPreviewList;
		/// <summary>
		/// 題型預覽列表設置
		/// </summary>
		/// <param name="name">題型名稱</param>
		private void SetLayoutSettingPreviewList(LayoutSetting.Preview name)
		{
			// 初期化
			if(_layoutSettingPreviewList == null)
			{
				_layoutSettingPreviewList = new List<LayoutSetting.Preview>();
				// 標題區
				_layoutSettingPreviewList.Add(LayoutSetting.Preview.Title);
				// 答題區
				_layoutSettingPreviewList.Add(LayoutSetting.Preview.Ready);
			}
			// 如果列表中不存在，則添加在答題區之前
			if (!_layoutSettingPreviewList.Any(d => d == name))
			{
				_layoutSettingPreviewList.Insert(_layoutSettingPreviewList.Count - 1, name);
			}
		}

		/// <summary>
		/// 題型縮略瀏覽初期化
		/// </summary>
		private void PreviewReflash()
		{
			flpPreview.Controls.Clear();

			// 瀏覽區域顯示
			_layoutSettingPreviewList.ForEach(d => PictureIntoFlowLayoutPanel(d));
		}

		/// <summary>
		/// 獲取指定題型的瀏覽所在位置
		/// </summary>
		/// <param name="viewName">題型種類</param>
		/// <returns></returns>
		private int GetPreviewItemIndexof(LayoutSetting.Preview viewName)
		{
			int index = 0;
			foreach (PictureBox pictureBox in flpPreview.Controls)
			{
				if (pictureBox.Tag.ToString().Equals(viewName.ToString()))
				{
					index = flpPreview.Controls.GetChildIndex(pictureBox);
					break;
				}
			}
			return index;
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

				// 題型預覽添加
				SetLayoutSettingPreviewList(LayoutSetting.Preview.Arithmetic);

				// 添加題型
				_selectedTopic++;

				MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
				ArithmeticParameter acParameter = (ArithmeticParameter)work.Structure(LayoutSetting.Preview.Arithmetic, "AC001");

				Dictionary<string, string> htmlMaps = new Dictionary<string, string>
				{
					{ "<!--ARITHMETIC-->", work.GetHtmlStatement(LayoutSetting.Preview.Arithmetic, acParameter) }
				};
				// JS模板內容替換
				MarkJavaScriptReplaceContent(typeof(ArithmeticHtmlSupport), htmlMaps);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(LayoutSetting.Preview.Arithmetic.ToString(), htmlMaps);
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "四則運算"));

				// 題型預覽移除
				_layoutSettingPreviewList.Remove(LayoutSetting.Preview.Arithmetic);

				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.Arithmetic.ToString());
				_selectedTopic--;
			}
			// 刷新題型預覽區域
			PreviewReflash();
		}

		/// <summary>
		/// JS模板內容替換
		/// </summary>
		/// <param name="type">題型HTML支持類(類型)</param>
		/// <param name="htmlMaps">替換標籤以及喜歡內容</param>
		private void MarkJavaScriptReplaceContent(Type type, Dictionary<string, string> htmlMaps)
		{
			object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
			attribute.ToList().ForEach(d =>
			{
				var attr = (SubstituteAttribute)d;
				htmlMaps.Add(attr.Source, attr.Target);
			});
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

				// 題型預覽添加
				SetLayoutSettingPreviewList(LayoutSetting.Preview.EqualityComparison);

				// 添加題型
				_selectedTopic++;
				MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
				EqualityComparisonParameter ecParameter = (EqualityComparisonParameter)work.Structure(LayoutSetting.Preview.EqualityComparison, "EC001");

				Dictionary<string, string> htmlMaps = new Dictionary<string, string>
				{
					{ "<!--EQUALITYCOMPARISON-->", work.GetHtmlStatement(LayoutSetting.Preview.EqualityComparison, ecParameter) }
				};
				// JS模板內容替換
				MarkJavaScriptReplaceContent(typeof(EqualityComparisonHtmlSupport), htmlMaps);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(LayoutSetting.Preview.EqualityComparison.ToString(), htmlMaps);
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "等式比大小"));

				// 題型預覽移除
				_layoutSettingPreviewList.Remove(LayoutSetting.Preview.EqualityComparison);

				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.EqualityComparison.ToString());
				_selectedTopic--;
			}
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

				// 題型預覽添加
				SetLayoutSettingPreviewList(LayoutSetting.Preview.ComputingConnection);

				// 添加題型
				_selectedTopic++;

				MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
				ComputingConnectionParameter ccParameter = (ComputingConnectionParameter)work.Structure(LayoutSetting.Preview.ComputingConnection, "CC001");

				Dictionary<string, string> htmlMaps = new Dictionary<string, string>
				{
					{ "<!--COMPUTINGCONNECTION-->", work.GetHtmlStatement(LayoutSetting.Preview.ComputingConnection, ccParameter) }
				};
				// JS模板內容替換
				MarkJavaScriptReplaceContent(typeof(ComputingConnectionHtmlSupport), htmlMaps);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(LayoutSetting.Preview.ComputingConnection.ToString(), htmlMaps);
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "等式接龍"));

				// 題型預覽移除
				_layoutSettingPreviewList.Remove(LayoutSetting.Preview.ComputingConnection);

				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.ComputingConnection.ToString());
				_selectedTopic--;
			}
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

				// 題型預覽添加
				SetLayoutSettingPreviewList(LayoutSetting.Preview.MathWordProblems);

				// 添加題型
				_selectedTopic++;

				MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
				MathWordProblemsParameter mpParameter = (MathWordProblemsParameter)work.Structure(LayoutSetting.Preview.MathWordProblems, "MP001");

				Dictionary<string, string> htmlMaps = new Dictionary<string, string>
				{
					{ "<!--MATHWORDPROBLEMS-->", work.GetHtmlStatement(LayoutSetting.Preview.MathWordProblems, mpParameter) }
				};
				// JS模板內容替換
				MarkJavaScriptReplaceContent(typeof(MathWordProblemsHtmlSupport), htmlMaps);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(LayoutSetting.Preview.MathWordProblems.ToString(), htmlMaps);
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "算式應用題"));

				// 題型預覽移除
				_layoutSettingPreviewList.Remove(LayoutSetting.Preview.MathWordProblems);

				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.MathWordProblems.ToString());
				_selectedTopic--;
			}
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

				// 題型預覽添加
				SetLayoutSettingPreviewList(LayoutSetting.Preview.FruitsLinkage);

				// 添加題型
				_selectedTopic++;

				MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
				FruitsLinkageParameter flParameter = (FruitsLinkageParameter)work.Structure(LayoutSetting.Preview.FruitsLinkage, "FL001");

				Dictionary<string, string> htmlMaps = new Dictionary<string, string>
				{
					{ "<!--FRUITSLINKAGE-->", work.GetHtmlStatement(LayoutSetting.Preview.FruitsLinkage, flParameter) }
				};
				// JS模板內容替換
				MarkJavaScriptReplaceContent(typeof(FruitsLinkageHtmlSupport), htmlMaps);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(LayoutSetting.Preview.FruitsLinkage.ToString(), htmlMaps);
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "水果連連看"));

				// 題型預覽移除
				_layoutSettingPreviewList.Remove(LayoutSetting.Preview.FruitsLinkage);

				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.FruitsLinkage.ToString());
				_selectedTopic--;
			}
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

				// 題型預覽添加
				SetLayoutSettingPreviewList(LayoutSetting.Preview.FindNearestNumber);

				// 添加題型
				_selectedTopic++;

				MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
				FindNearestNumberParameter fnParameter = (FindNearestNumberParameter)work.Structure(LayoutSetting.Preview.FindNearestNumber, "FN001");

				Dictionary<string, string> htmlMaps = new Dictionary<string, string>
				{
					{ "<!--FINDNEARESTNUMBER-->", work.GetHtmlStatement(LayoutSetting.Preview.FindNearestNumber, fnParameter) }
				};
				// JS模板內容替換
				MarkJavaScriptReplaceContent(typeof(FindNearestNumberHtmlSupport), htmlMaps);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(LayoutSetting.Preview.FindNearestNumber.ToString(), htmlMaps);
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "找出最近的數字"));

				// 題型預覽移除
				_layoutSettingPreviewList.Remove(LayoutSetting.Preview.FindNearestNumber);

				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.FindNearestNumber.ToString());
				_selectedTopic--;
			}
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

				// 題型預覽添加
				SetLayoutSettingPreviewList(LayoutSetting.Preview.CombinatorialEquation);

				// 添加題型
				_selectedTopic++;

				MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
				CombinatorialEquationParameter ceParameter = (CombinatorialEquationParameter)work.Structure(LayoutSetting.Preview.CombinatorialEquation, "CE001");

				Dictionary<string, string> htmlMaps = new Dictionary<string, string>
				{
					{ "<!--COMBINATORIALEQUATION-->", work.GetHtmlStatement(LayoutSetting.Preview.CombinatorialEquation, ceParameter) }
				};
				// JS模板內容替換
				MarkJavaScriptReplaceContent(typeof(CombinatorialEquationHtmlSupport), htmlMaps);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(LayoutSetting.Preview.CombinatorialEquation.ToString(), htmlMaps);
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "算式組合"));

				// 題型預覽移除
				_layoutSettingPreviewList.Remove(LayoutSetting.Preview.CombinatorialEquation);

				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.CombinatorialEquation.ToString());
				_selectedTopic--;
			}
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

				// 題型預覽添加
				SetLayoutSettingPreviewList(LayoutSetting.Preview.ScoreGoal);

				// 添加題型
				_selectedTopic++;

				MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
				ScoreGoalParameter sgParameter = (ScoreGoalParameter)work.Structure(LayoutSetting.Preview.ScoreGoal, "SG001");

				Dictionary<string, string> htmlMaps = new Dictionary<string, string>
				{
					{ "<!--SCOREGOAL-->", work.GetHtmlStatement(LayoutSetting.Preview.ScoreGoal, sgParameter) }
				};
				// JS模板內容替換
				MarkJavaScriptReplaceContent(typeof(ScoreGoalHtmlSupport), htmlMaps);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(LayoutSetting.Preview.ScoreGoal.ToString(), htmlMaps);
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "射門得分"));

				// 題型預覽移除
				_layoutSettingPreviewList.Remove(LayoutSetting.Preview.ScoreGoal);

				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.ScoreGoal.ToString());
				_selectedTopic--;
			}
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

				// 題型預覽添加
				SetLayoutSettingPreviewList(LayoutSetting.Preview.HowMuchMore);

				// 添加題型
				_selectedTopic++;

				MakeHtml<ParameterBase> work = new MakeHtml<ParameterBase>();
				HowMuchMoreParameter hmmParameter = (HowMuchMoreParameter)work.Structure(LayoutSetting.Preview.HowMuchMore, "HMM001");

				Dictionary<string, string> htmlMaps = new Dictionary<string, string>
				{
					{ "<!--HOWMUCHMORE-->", work.GetHtmlStatement(LayoutSetting.Preview.HowMuchMore, hmmParameter) }
				};
				// JS模板內容替換
				MarkJavaScriptReplaceContent(typeof(HowMuchMoreHtmlSupport), htmlMaps);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(LayoutSetting.Preview.HowMuchMore.ToString(), htmlMaps);
			}
			else
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0007A, "比多少"));

				// 題型預覽移除
				_layoutSettingPreviewList.Remove(LayoutSetting.Preview.HowMuchMore);

				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.HowMuchMore.ToString());
				_selectedTopic--;
			}
			// 刷新題型預覽區域
			PreviewReflash();
		}
	}
}
