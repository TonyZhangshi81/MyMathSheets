using ComputationalStrategy.Item;
using ComputationalStrategy.Main.Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TheFormulaShows;
using TheFormulaShows.Attributes;
using TheFormulaShows.Support;

namespace MathSheetsSettingApp
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

			this.chkAdition.Enabled = false;
			this.chkSubtraction.Enabled = false;
			this.chkMultiplication.Enabled = false;
			this.chkDivision.Enabled = false;
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
				MessageBox.Show(this, "加减乘除未指定！");
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
		/// <param name="item"></param>
		/// <param name="parameter"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		private string GetValue(GapFilling item, int parameter, GapFilling gap)
		{
			if (item == gap)
			{
				return string.Empty.PadLeft(2);
			}
			return parameter.ToString();
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
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ArithmeticCheckedChanged(object sender, EventArgs e)
		{
			if (chkArithmetic.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<Formula>, Arithmetic> work = new MakeHtml<List<Formula>, Arithmetic>(_fourOperationsType, _signs, _questionType, _maximumLimit, _numberOfQuestions);
				work.Structure();
				htmlMaps.Add("<!--ARITHMETIC-->", work.GetHtmlStatement());

				Type type = typeof(ArithmeticHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add("ARITHMETIC", htmlMaps);
			}
			else
			{
				_htmlMaps.Remove("ARITHMETIC");
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EqualityComparisonCheckedChanged(object sender, EventArgs e)
		{
			if (chkEqualityComparison.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<EqualityFormula>, EqualityComparison> work = new MakeHtml<List<EqualityFormula>, EqualityComparison>(_fourOperationsType, new List<SignOfOperation> { SignOfOperation.Plus, SignOfOperation.Subtraction }, QuestionType.Standard, _maximumLimit, _numberOfQuestions);
				work.Structure();
				htmlMaps.Add("<!--EQUALITYCOMPARISON-->", work.GetHtmlStatement());

				Type type = typeof(EqualityComparisonHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add("EQUALITYCOMPARISON", htmlMaps);
			}
			else
			{
				_htmlMaps.Remove("EQUALITYCOMPARISON");
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ComputingConnectionCheckedChanged(object sender, EventArgs e)
		{
			if (chkComputingConnection.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<ConnectionFormula>, ComputingConnection> work = new MakeHtml<List<ConnectionFormula>, ComputingConnection>(_fourOperationsType, new List<SignOfOperation> { SignOfOperation.Plus, SignOfOperation.Subtraction }, QuestionType.Standard, _maximumLimit, _numberOfQuestions);
				work.Structure();
				htmlMaps.Add("<!--COMPUTINGCONNECTION-->", work.GetHtmlStatement());

				Type type = typeof(ComputingConnectionHtmlSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add("COMPUTINGCONNECTION", htmlMaps);
			}
			else
			{
				_htmlMaps.Remove("COMPUTINGCONNECTION");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MathWordProblemsCheckedChanged(object sender, EventArgs e)
		{
			if (chkMathWordProblems.Checked)
			{
				Dictionary<string, string> htmlMaps = new Dictionary<string, string>();
				MakeHtml<List<MathWordProblemsFormula>, MathWordProblems> work = new MakeHtml<List<MathWordProblemsFormula>, MathWordProblems>(_fourOperationsType, _signs, QuestionType.Default, _maximumLimit, _numberOfQuestions);
				work.Structure();
				htmlMaps.Add("<!--MATHWORDPROBLEMS-->", work.GetHtmlStatement());

				Type type = typeof(MathWordProblemsSupport);
				object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
				attribute.ToList().ForEach(d =>
				{
					var attr = (SubstituteAttribute)d;
					htmlMaps.Add(attr.Source, attr.Target);
				});

				_htmlMaps.Add("MATHWORDPROBLEMS", htmlMaps);
			}
			else
			{
				_htmlMaps.Remove("MATHWORDPROBLEMS");
			}
		}
	}
}
