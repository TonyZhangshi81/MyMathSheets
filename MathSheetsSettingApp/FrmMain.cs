using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Main.OperationStrategy.Parameters;
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
				Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject(name.ToString()),
				Tag = name.ToString(),
				SizeMode = PictureBoxSizeMode.StretchImage,
				Width = flpPreview.Width - 20,
				Height = 100
			};
			// 將題型縮略圖添加至瀏覽框
			flpPreview.Controls.Add(picBox);
			// 防止閃爍
			flpPreview.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(picBox, true, null);

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
				// 替換HTML模板中的預留內容（HTML、JS注入操作）
				foreach (KeyValuePair<string, string> m in d.Value)
				{
					htmlTemplate.Replace(m.Key, m.Value);
				}
			}
			// 保存至靜態頁面
			File.WriteAllText(destFileName, htmlTemplate.ToString(), Encoding.UTF8);

			if (chkIsPreview.Checked)
			{
				// 使用IE打開已作成的靜態頁面
				System.Diagnostics.Process.Start(@"chrome.exe", Path.GetFullPath(destFileName));
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

			// 標題瀏覽
			PictureIntoFlowLayoutPanel(LayoutSetting.Preview.Title);
			// 四則運算瀏覽
			if (chkArithmetic.Checked)
			{
				PictureIntoFlowLayoutPanel(LayoutSetting.Preview.Arithmetic);
			}
			// 運算比大小瀏覽
			if (chkEqualityComparison.Checked)
			{
				PictureIntoFlowLayoutPanel(LayoutSetting.Preview.EqualityComparison);
			}
			// 等式接龍瀏覽
			if (chkComputingConnection.Checked)
			{
				PictureIntoFlowLayoutPanel(LayoutSetting.Preview.ComputingConnection);
			}
			// 算式應用題瀏覽
			if (chkMathWordProblems.Checked)
			{
				PictureIntoFlowLayoutPanel(LayoutSetting.Preview.MathWordProblems);
			}
			// 水果連連看
			if (chkFruitsLinkage.Checked)
			{
				PictureIntoFlowLayoutPanel(LayoutSetting.Preview.FruitsLinkage);
			}
			// 水果連連看
			if (chkFindNearestNumber.Checked)
			{
				PictureIntoFlowLayoutPanel(LayoutSetting.Preview.FindNearestNumber);
			}
			// 運算組合
			if (chkCombinatorialEquation.Checked)
			{
				PictureIntoFlowLayoutPanel(LayoutSetting.Preview.CombinatorialEquation);
			}
			// 射門得分
			if (chkScoreGoal.Checked)
			{
				PictureIntoFlowLayoutPanel(LayoutSetting.Preview.ScoreGoal);
			}
			// 比多少
			if (chkHowMuchMore.Checked)
			{
				PictureIntoFlowLayoutPanel(LayoutSetting.Preview.HowMuchMore);
			}
			// 答題結束瀏覽
			PictureIntoFlowLayoutPanel(LayoutSetting.Preview.Ready);
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
				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.EqualityComparison.ToString());
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
				// 題型移除
				_htmlMaps.Remove(LayoutSetting.Preview.HowMuchMore.ToString());
				_selectedTopic--;
			}
			// 刷新題型預覽區域
			PreviewReflash();
		}
	}
}
