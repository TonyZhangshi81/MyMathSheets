using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Item;
using MyMathSheets.ComputationalStrategy.Main.Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyMathSheets.TheFormulaShows;
using MyMathSheets.TheFormulaShows.Attributes;
using MyMathSheets.TheFormulaShows.Support;

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
			Dictionary<int, string> kvTopic = new Dictionary<int, string>
			{
				{ 1, "标准" },
				{ 2, "填空" }
			};
			BindingSource bsTopic = new BindingSource
			{
				DataSource = kvTopic
			};

			this.cmbTopic.DataSource = bsTopic;
			this.cmbTopic.ValueMember = "Key";
			this.cmbTopic.DisplayMember = "Value";

			Dictionary<int, string> kvComplexity = new Dictionary<int, string>
			{
				{ 20, "简单" },
				{ 50, "中等" },
				{ 100, "困难" }
			};
			BindingSource bsComplexity = new BindingSource
			{
				DataSource = kvComplexity
			};

			this.cmbComplexity.DataSource = bsComplexity;
			this.cmbTopic.ValueMember = "Key";
			this.cmbTopic.DisplayMember = "Value";

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
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StandardCheckedChanged(object sender, EventArgs e)
		{
			this.chkAdition.Checked = true;
			this.chkSubtraction.Checked = false;
			this.chkMultiplication.Checked = false;
			this.chkDivision.Checked = false;

			this.chkAdition.Enabled = true;
			this.chkSubtraction.Enabled = true;
			this.chkMultiplication.Enabled = true;
			this.chkDivision.Enabled = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MixtureCheckedChanged(object sender, EventArgs e)
		{
			this.chkAdition.Checked = true;
			this.chkSubtraction.Checked = true;
			this.chkMultiplication.Checked = true;
			this.chkDivision.Checked = true;

			//this.chkAdition.Enabled = false;
			//this.chkSubtraction.Enabled = false;
			//this.chkMultiplication.Enabled = false;
			//this.chkDivision.Enabled = false;
		}
		/// <summary>
		/// 
		/// </summary>
		private QuestionType _questionType
		{
			get
			{
				return (QuestionType)(Convert.ToInt32(this.cmbTopic.SelectedValue));
			}
		}
		/// <summary>
		/// 
		/// </summary>
		private int _maximumLimit
		{
			get
			{
				return Convert.ToInt32(((KeyValuePair<int, string>)this.cmbComplexity.SelectedValue).Key);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		private int _numberOfQuestions
		{
			get
			{
				return Convert.ToInt32(this.tbxNumberOf.Text);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		private FourOperationsType _fourOperationsType
		{
			get
			{
				return radStandard.Checked ? FourOperationsType.Standard : FourOperationsType.Random;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		private IList<SignOfOperation> _signs
		{
			get
			{
				var tmp = new List<SignOfOperation>();
				if (chkAdition.Checked)
				{
					tmp.Add(SignOfOperation.Plus);
				}
				if (chkDivision.Checked)
				{
					tmp.Add(SignOfOperation.Division);
				}
				if (chkMultiplication.Checked)
				{
					tmp.Add(SignOfOperation.Multiple);
				}
				if (chkSubtraction.Checked)
				{
					tmp.Add(SignOfOperation.Subtraction);
				}

				return tmp;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SureClick(object sender, EventArgs e)
		{
			if (!chkAdition.Checked && !chkDivision.Checked && !chkMultiplication.Checked && !chkSubtraction.Checked)
			{
				MessageBox.Show(this, "運算符未指定");
				return;
			}

			string sourceFileName = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings.Get("Template"));
			string destFileName = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings.Get("HtmlWork") + string.Format("HTMLPage_{0}.html", DateTime.Now.ToString("HHmmssfff")));
			File.Copy(sourceFileName, destFileName);

			StringBuilder htmlTemplate = new StringBuilder();
			htmlTemplate.Append(File.ReadAllText(destFileName, Encoding.UTF8));

			foreach (KeyValuePair<string, Dictionary<string, string>> d in _htmlMaps)
			{
				foreach (KeyValuePair<string, string> m in d.Value)
				{
					htmlTemplate.Replace(m.Key, m.Value);
				}
			}

			File.WriteAllText(destFileName, htmlTemplate.ToString(), Encoding.UTF8);

			System.Diagnostics.Process.Start(@"IExplore.exe", Path.GetFullPath(destFileName));

			Environment.Exit(0);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AditionCheckedChanged(object sender, EventArgs e)
		{
			if (this.radStandard.Checked && this.chkAdition.Checked)
			{
				this.chkSubtraction.Checked = false;
				this.chkDivision.Checked = false;
				this.chkMultiplication.Checked = false;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SubtractionCheckedChanged(object sender, EventArgs e)
		{
			if (this.radStandard.Checked && this.chkSubtraction.Checked)
			{
				this.chkAdition.Checked = false;
				this.chkDivision.Checked = false;
				this.chkMultiplication.Checked = false;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MultiplicationCheckedChanged(object sender, EventArgs e)
		{
			if (this.radStandard.Checked && this.chkMultiplication.Checked)
			{
				this.chkAdition.Checked = false;
				this.chkDivision.Checked = false;
				this.chkSubtraction.Checked = false;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DivisionCheckedChanged(object sender, EventArgs e)
		{
			if (this.radStandard.Checked && this.chkDivision.Checked)
			{
				this.chkAdition.Checked = false;
				this.chkMultiplication.Checked = false;
				this.chkSubtraction.Checked = false;
			}
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
			// 答題結束瀏覽
			PictureIntoFlowLayoutPanel(LayoutSetting.Preview.Ready);
		}

		/// <summary>
		/// 獲取指定題型的瀏覽所在位置
		/// </summary>
		/// <param name="viewName"></param>
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
		/// 四則運算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ArithmeticCheckedChanged(object sender, EventArgs e)
		{
			if (chkArithmetic.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<Formula>, Arithmetic> work = new MakeHtml<List<Formula>, Arithmetic>(_fourOperationsType, _signs, _questionType, 30, 20);
				work.Structure();
				htmlMaps.Add("<!--ARITHMETIC-->", work.GetHtmlStatement());

				Type type = typeof(ArithmeticHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add(LayoutSetting.Preview.Arithmetic.ToString(), htmlMaps);
			}
			else
			{
				_htmlMaps.Remove(LayoutSetting.Preview.Arithmetic.ToString());
			}
			PreviewReflash();
		}

		/// <summary>
		/// 等式比大小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EqualityComparisonCheckedChanged(object sender, EventArgs e)
		{
			if (chkEqualityComparison.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<EqualityFormula>, EqualityComparison> work = new MakeHtml<List<EqualityFormula>, EqualityComparison>(_fourOperationsType, _signs, QuestionType.Standard, 30, 8);
				work.Structure();
				htmlMaps.Add("<!--EQUALITYCOMPARISON-->", work.GetHtmlStatement());

				Type type = typeof(EqualityComparisonHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add(LayoutSetting.Preview.EqualityComparison.ToString(), htmlMaps);
			}
			else
			{
				_htmlMaps.Remove(LayoutSetting.Preview.EqualityComparison.ToString());
			}
			PreviewReflash();
		}
		/// <summary>
		/// 等式接龍
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ComputingConnectionCheckedChanged(object sender, EventArgs e)
		{
			if (chkComputingConnection.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<ConnectionFormula>, ComputingConnection> work = new MakeHtml<List<ConnectionFormula>, ComputingConnection>(_fourOperationsType, _signs, QuestionType.Standard, 30, 3);
				work.Structure();
				htmlMaps.Add("<!--COMPUTINGCONNECTION-->", work.GetHtmlStatement());

				Type type = typeof(ComputingConnectionHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add(LayoutSetting.Preview.ComputingConnection.ToString(), htmlMaps);
			}
			else
			{
				_htmlMaps.Remove(LayoutSetting.Preview.ComputingConnection.ToString());
			}
			PreviewReflash();
		}

		/// <summary>
		/// 算式應用題
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MathWordProblemsCheckedChanged(object sender, EventArgs e)
		{
			if (chkMathWordProblems.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<MathWordProblemsFormula>, MathWordProblems> work = new MakeHtml<List<MathWordProblemsFormula>, MathWordProblems>(_fourOperationsType, _signs, QuestionType.Default, 30, 3);
				work.Structure();
				htmlMaps.Add("<!--MATHWORDPROBLEMS-->", work.GetHtmlStatement());

				Type type = typeof(MathWordProblemsHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add(LayoutSetting.Preview.MathWordProblems.ToString(), htmlMaps);
			}
			else
			{
				_htmlMaps.Remove(LayoutSetting.Preview.MathWordProblems.ToString());
			}
			PreviewReflash();
		}

		/// <summary>
		/// 水果連連看
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FruitsLinkageCheckedChanged(object sender, EventArgs e)
		{
			if (chkFruitsLinkage.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<FruitsLinkageFormula, FruitsLinkage> work = new MakeHtml<FruitsLinkageFormula, FruitsLinkage>(_fourOperationsType, _signs, QuestionType.Default, 50, 10);
				work.Structure();
				htmlMaps.Add("<!--FRUITSLINKAGE-->", work.GetHtmlStatement());

				Type type = typeof(FruitsLinkageHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add(LayoutSetting.Preview.FruitsLinkage.ToString(), htmlMaps);
			}
			else
			{
				_htmlMaps.Remove(LayoutSetting.Preview.FruitsLinkage.ToString());
			}
			PreviewReflash();
		}

		/// <summary>
		/// 找出最近的數字
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FindNearestNumberCheckedChanged(object sender, EventArgs e)
		{
			if (chkFindNearestNumber.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<EqualityFormula>, FindNearestNumber> work = new MakeHtml<List<EqualityFormula>, FindNearestNumber>(_fourOperationsType, _signs, QuestionType.GapFilling, 50, 8);
				work.Structure();
				htmlMaps.Add("<!--FINDNEARESTNUMBER-->", work.GetHtmlStatement());

				Type type = typeof(FindNearestNumberHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add(LayoutSetting.Preview.FindNearestNumber.ToString(), htmlMaps);
			}
			else
			{
				_htmlMaps.Remove(LayoutSetting.Preview.FindNearestNumber.ToString());
			}
			PreviewReflash();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CombinatorialEquationCheckedChanged(object sender, EventArgs e)
		{
			if (chkCombinatorialEquation.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<CombinatorialFormula>, CombinatorialEquation> work = new MakeHtml<List<CombinatorialFormula>, CombinatorialEquation>(50, 2);
				work.Structure();
				htmlMaps.Add("<!--COMBINATORIALEQUATION-->", work.GetHtmlStatement());

				Type type = typeof(CombinatorialEquationHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add(LayoutSetting.Preview.CombinatorialEquation.ToString(), htmlMaps);
			}
			else
			{
				_htmlMaps.Remove(LayoutSetting.Preview.CombinatorialEquation.ToString());
			}
			PreviewReflash();
		}
	}
}
