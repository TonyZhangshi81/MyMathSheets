namespace MathSheetsSettingApp
{
	partial class FrmMain
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSure = new System.Windows.Forms.Button();
			this.chkAdition = new System.Windows.Forms.CheckBox();
			this.chkSubtraction = new System.Windows.Forms.CheckBox();
			this.chkMultiplication = new System.Windows.Forms.CheckBox();
			this.chkDivision = new System.Windows.Forms.CheckBox();
			this.radStandard = new System.Windows.Forms.RadioButton();
			this.radMixture = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmbTopic = new System.Windows.Forms.ComboBox();
			this.lblTopic = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tbxNumberOf = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbComplexity = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSure
			// 
			this.btnSure.Location = new System.Drawing.Point(22, 196);
			this.btnSure.Name = "btnSure";
			this.btnSure.Size = new System.Drawing.Size(354, 23);
			this.btnSure.TabIndex = 0;
			this.btnSure.Text = "出题";
			this.btnSure.UseVisualStyleBackColor = true;
			this.btnSure.Click += new System.EventHandler(this.SureClick);
			// 
			// chkAdition
			// 
			this.chkAdition.AutoSize = true;
			this.chkAdition.Checked = true;
			this.chkAdition.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAdition.Location = new System.Drawing.Point(160, 18);
			this.chkAdition.Name = "chkAdition";
			this.chkAdition.Size = new System.Drawing.Size(48, 16);
			this.chkAdition.TabIndex = 1;
			this.chkAdition.Text = "加法";
			this.chkAdition.UseVisualStyleBackColor = true;
			this.chkAdition.CheckedChanged += new System.EventHandler(this.chkAdition_CheckedChanged);
			// 
			// chkSubtraction
			// 
			this.chkSubtraction.AutoSize = true;
			this.chkSubtraction.Location = new System.Drawing.Point(246, 18);
			this.chkSubtraction.Name = "chkSubtraction";
			this.chkSubtraction.Size = new System.Drawing.Size(48, 16);
			this.chkSubtraction.TabIndex = 2;
			this.chkSubtraction.Text = "减法";
			this.chkSubtraction.UseVisualStyleBackColor = true;
			this.chkSubtraction.CheckedChanged += new System.EventHandler(this.chkSubtraction_CheckedChanged);
			// 
			// chkMultiplication
			// 
			this.chkMultiplication.AutoSize = true;
			this.chkMultiplication.Enabled = false;
			this.chkMultiplication.Location = new System.Drawing.Point(160, 40);
			this.chkMultiplication.Name = "chkMultiplication";
			this.chkMultiplication.Size = new System.Drawing.Size(48, 16);
			this.chkMultiplication.TabIndex = 3;
			this.chkMultiplication.Text = "乘法";
			this.chkMultiplication.UseVisualStyleBackColor = true;
			this.chkMultiplication.CheckedChanged += new System.EventHandler(this.chkMultiplication_CheckedChanged);
			// 
			// chkDivision
			// 
			this.chkDivision.AutoSize = true;
			this.chkDivision.Enabled = false;
			this.chkDivision.Location = new System.Drawing.Point(246, 40);
			this.chkDivision.Name = "chkDivision";
			this.chkDivision.Size = new System.Drawing.Size(48, 16);
			this.chkDivision.TabIndex = 4;
			this.chkDivision.Text = "除法";
			this.chkDivision.UseVisualStyleBackColor = true;
			this.chkDivision.CheckedChanged += new System.EventHandler(this.chkDivision_CheckedChanged);
			// 
			// radStandard
			// 
			this.radStandard.AutoSize = true;
			this.radStandard.Checked = true;
			this.radStandard.Location = new System.Drawing.Point(16, 18);
			this.radStandard.Name = "radStandard";
			this.radStandard.Size = new System.Drawing.Size(59, 16);
			this.radStandard.TabIndex = 5;
			this.radStandard.TabStop = true;
			this.radStandard.Text = "标准题";
			this.radStandard.UseVisualStyleBackColor = true;
			this.radStandard.CheckedChanged += new System.EventHandler(this.StandardCheckedChanged);
			// 
			// radMixture
			// 
			this.radMixture.AutoSize = true;
			this.radMixture.Enabled = false;
			this.radMixture.Location = new System.Drawing.Point(16, 40);
			this.radMixture.Name = "radMixture";
			this.radMixture.Size = new System.Drawing.Size(71, 16);
			this.radMixture.TabIndex = 6;
			this.radMixture.Text = "混合计算";
			this.radMixture.UseVisualStyleBackColor = true;
			this.radMixture.CheckedChanged += new System.EventHandler(this.MixtureCheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radStandard);
			this.groupBox1.Controls.Add(this.chkDivision);
			this.groupBox1.Controls.Add(this.radMixture);
			this.groupBox1.Controls.Add(this.chkMultiplication);
			this.groupBox1.Controls.Add(this.chkAdition);
			this.groupBox1.Controls.Add(this.chkSubtraction);
			this.groupBox1.Location = new System.Drawing.Point(22, 114);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(354, 66);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			// 
			// cmbTopic
			// 
			this.cmbTopic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTopic.FormattingEnabled = true;
			this.cmbTopic.Location = new System.Drawing.Point(109, 21);
			this.cmbTopic.Name = "cmbTopic";
			this.cmbTopic.Size = new System.Drawing.Size(100, 20);
			this.cmbTopic.TabIndex = 8;
			// 
			// lblTopic
			// 
			this.lblTopic.AutoSize = true;
			this.lblTopic.Location = new System.Drawing.Point(37, 24);
			this.lblTopic.Name = "lblTopic";
			this.lblTopic.Size = new System.Drawing.Size(41, 12);
			this.lblTopic.TabIndex = 9;
			this.lblTopic.Text = "题型：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(38, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 10;
			this.label1.Text = "数量：";
			// 
			// tbxNumberOf
			// 
			this.tbxNumberOf.Location = new System.Drawing.Point(109, 50);
			this.tbxNumberOf.Name = "tbxNumberOf";
			this.tbxNumberOf.Size = new System.Drawing.Size(100, 21);
			this.tbxNumberOf.TabIndex = 11;
			this.tbxNumberOf.Text = "35";
			this.tbxNumberOf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(39, 81);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "等级：";
			// 
			// cmbComplexity
			// 
			this.cmbComplexity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbComplexity.FormattingEnabled = true;
			this.cmbComplexity.Location = new System.Drawing.Point(109, 78);
			this.cmbComplexity.Name = "cmbComplexity";
			this.cmbComplexity.Size = new System.Drawing.Size(100, 20);
			this.cmbComplexity.TabIndex = 13;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(399, 240);
			this.Controls.Add(this.cmbComplexity);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbxNumberOf);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblTopic);
			this.Controls.Add(this.cmbTopic);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSure);
			this.Name = "FrmMain";
			this.Text = "出题设置";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSure;
		private System.Windows.Forms.CheckBox chkAdition;
		private System.Windows.Forms.CheckBox chkSubtraction;
		private System.Windows.Forms.CheckBox chkMultiplication;
		private System.Windows.Forms.CheckBox chkDivision;
		private System.Windows.Forms.RadioButton radStandard;
		private System.Windows.Forms.RadioButton radMixture;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmbTopic;
		private System.Windows.Forms.Label lblTopic;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbxNumberOf;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbComplexity;
	}
}

