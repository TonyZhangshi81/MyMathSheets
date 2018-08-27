using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheFormulaShows;

namespace MathSheetsSettingApp
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();
		}

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

		private void StandardCheckedChanged(object sender, EventArgs e)
		{
			this.chkAdition.Checked = true;
			this.chkDivision.Checked = false;
			this.chkMultiplication.Checked = false;
			this.chkSubtraction.Checked = false;

			this.chkAdition.Enabled = true;
			this.chkDivision.Enabled = false;
			this.chkMultiplication.Enabled = false;
			this.chkSubtraction.Enabled = false;
		}

		private void MixtureCheckedChanged(object sender, EventArgs e)
		{
			this.chkAdition.Checked = true;
			this.chkDivision.Checked = true;
			this.chkMultiplication.Checked = true;
			this.chkSubtraction.Checked = true;

			this.chkAdition.Enabled = false;
			this.chkDivision.Enabled = false;
			this.chkMultiplication.Enabled = false;
			this.chkSubtraction.Enabled = false;
		}

		private void SureClick(object sender, EventArgs e)
		{
			if(!this.chkAdition.Checked && !this.chkDivision.Checked && !this.chkMultiplication.Checked && !this.chkSubtraction.Checked)
			{
				MessageBox.Show(this, "加减乘除未指定！");
				return;
			}

			MakeHtmlBase work = new Arithmetic();
			work.MaximumLimit = Convert.ToInt32(((KeyValuePair<int, string>)this.cmbComplexity.SelectedValue).Key);
			work.NumberOf = Convert.ToInt32(this.tbxNumberOf.Text);
			work.QType = (QuestionTypes)(Convert.ToInt32(this.cmbTopic.SelectedValue));
			work.Structure();

			string html = work.MakeHtml();

			var sourceFileName = System.Configuration.ConfigurationManager.AppSettings.Get("Template");
			var destFileName = System.Configuration.ConfigurationManager.AppSettings.Get("HtmlWork") + string.Format("HTMLPage_{0}.html", DateTime.Now.ToString("HHmmssfff"));
			File.Copy(sourceFileName, destFileName);

			var index = 0;
			string[] allTextLines = File.ReadAllLines(destFileName, Encoding.UTF8);
			allTextLines.ToList().ForEach(d =>
			{
				index++;
				if (d.IndexOf("<!--ARITHMETIC-->") >= 0)
				{
					allTextLines[index] = html;
				}
			});
			File.WriteAllLines(destFileName, allTextLines, Encoding.Unicode);

			System.Diagnostics.Process.Start(@"IExplore.exe", destFileName);
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
	}
}
