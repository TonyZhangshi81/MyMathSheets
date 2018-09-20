﻿namespace MyMathSheets.MathSheetsSettingApp
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
			this.chkArithmetic = new System.Windows.Forms.CheckBox();
			this.chkEqualityComparison = new System.Windows.Forms.CheckBox();
			this.chkComputingConnection = new System.Windows.Forms.CheckBox();
			this.chkMathWordProblems = new System.Windows.Forms.CheckBox();
			this.flpPreview = new System.Windows.Forms.FlowLayoutPanel();
			this.chkFruitsLinkage = new System.Windows.Forms.CheckBox();
			this.chkFindNearestNumber = new System.Windows.Forms.CheckBox();
			this.chkCombinatorialEquation = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnSure
			// 
			this.btnSure.Location = new System.Drawing.Point(27, 370);
			this.btnSure.Name = "btnSure";
			this.btnSure.Size = new System.Drawing.Size(354, 23);
			this.btnSure.TabIndex = 0;
			this.btnSure.Text = "出題";
			this.btnSure.UseVisualStyleBackColor = true;
			this.btnSure.Click += new System.EventHandler(this.SureClick);
			// 
			// chkArithmetic
			// 
			this.chkArithmetic.AutoSize = true;
			this.chkArithmetic.Location = new System.Drawing.Point(27, 12);
			this.chkArithmetic.Name = "chkArithmetic";
			this.chkArithmetic.Size = new System.Drawing.Size(72, 16);
			this.chkArithmetic.TabIndex = 14;
			this.chkArithmetic.Text = "四則運算";
			this.chkArithmetic.UseVisualStyleBackColor = true;
			this.chkArithmetic.CheckedChanged += new System.EventHandler(this.ArithmeticCheckedChanged);
			// 
			// chkEqualityComparison
			// 
			this.chkEqualityComparison.AutoSize = true;
			this.chkEqualityComparison.Location = new System.Drawing.Point(132, 12);
			this.chkEqualityComparison.Name = "chkEqualityComparison";
			this.chkEqualityComparison.Size = new System.Drawing.Size(84, 16);
			this.chkEqualityComparison.TabIndex = 15;
			this.chkEqualityComparison.Text = "算式比大小";
			this.chkEqualityComparison.UseVisualStyleBackColor = true;
			this.chkEqualityComparison.CheckedChanged += new System.EventHandler(this.EqualityComparisonCheckedChanged);
			// 
			// chkComputingConnection
			// 
			this.chkComputingConnection.AutoSize = true;
			this.chkComputingConnection.Location = new System.Drawing.Point(254, 12);
			this.chkComputingConnection.Name = "chkComputingConnection";
			this.chkComputingConnection.Size = new System.Drawing.Size(72, 16);
			this.chkComputingConnection.TabIndex = 16;
			this.chkComputingConnection.Text = "等式接龍";
			this.chkComputingConnection.UseVisualStyleBackColor = true;
			this.chkComputingConnection.CheckedChanged += new System.EventHandler(this.ComputingConnectionCheckedChanged);
			// 
			// chkMathWordProblems
			// 
			this.chkMathWordProblems.AutoSize = true;
			this.chkMathWordProblems.Location = new System.Drawing.Point(27, 45);
			this.chkMathWordProblems.Name = "chkMathWordProblems";
			this.chkMathWordProblems.Size = new System.Drawing.Size(84, 16);
			this.chkMathWordProblems.TabIndex = 17;
			this.chkMathWordProblems.Text = "算式應用題";
			this.chkMathWordProblems.UseVisualStyleBackColor = true;
			this.chkMathWordProblems.CheckedChanged += new System.EventHandler(this.MathWordProblemsCheckedChanged);
			// 
			// flpPreview
			// 
			this.flpPreview.AutoScroll = true;
			this.flpPreview.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flpPreview.Location = new System.Drawing.Point(409, 12);
			this.flpPreview.Name = "flpPreview";
			this.flpPreview.Size = new System.Drawing.Size(511, 382);
			this.flpPreview.TabIndex = 18;
			this.flpPreview.WrapContents = false;
			// 
			// chkFruitsLinkage
			// 
			this.chkFruitsLinkage.AutoSize = true;
			this.chkFruitsLinkage.Location = new System.Drawing.Point(132, 45);
			this.chkFruitsLinkage.Name = "chkFruitsLinkage";
			this.chkFruitsLinkage.Size = new System.Drawing.Size(84, 16);
			this.chkFruitsLinkage.TabIndex = 19;
			this.chkFruitsLinkage.Text = "水果連連看";
			this.chkFruitsLinkage.UseVisualStyleBackColor = true;
			this.chkFruitsLinkage.CheckedChanged += new System.EventHandler(this.FruitsLinkageCheckedChanged);
			// 
			// chkFindNearestNumber
			// 
			this.chkFindNearestNumber.AutoSize = true;
			this.chkFindNearestNumber.Location = new System.Drawing.Point(254, 45);
			this.chkFindNearestNumber.Name = "chkFindNearestNumber";
			this.chkFindNearestNumber.Size = new System.Drawing.Size(108, 16);
			this.chkFindNearestNumber.TabIndex = 20;
			this.chkFindNearestNumber.Text = "找出最近的數字";
			this.chkFindNearestNumber.UseVisualStyleBackColor = true;
			this.chkFindNearestNumber.CheckedChanged += new System.EventHandler(this.FindNearestNumberCheckedChanged);
			// 
			// chkCombinatorialEquation
			// 
			this.chkCombinatorialEquation.AutoSize = true;
			this.chkCombinatorialEquation.Location = new System.Drawing.Point(27, 80);
			this.chkCombinatorialEquation.Name = "chkCombinatorialEquation";
			this.chkCombinatorialEquation.Size = new System.Drawing.Size(72, 16);
			this.chkCombinatorialEquation.TabIndex = 21;
			this.chkCombinatorialEquation.Text = "算式組合";
			this.chkCombinatorialEquation.UseVisualStyleBackColor = true;
			this.chkCombinatorialEquation.CheckedChanged += new System.EventHandler(this.CombinatorialEquationCheckedChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(27, 341);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(354, 23);
			this.button1.TabIndex = 22;
			this.button1.Text = "設置";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(932, 405);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.chkCombinatorialEquation);
			this.Controls.Add(this.chkFindNearestNumber);
			this.Controls.Add(this.chkFruitsLinkage);
			this.Controls.Add(this.flpPreview);
			this.Controls.Add(this.chkMathWordProblems);
			this.Controls.Add(this.chkComputingConnection);
			this.Controls.Add(this.chkEqualityComparison);
			this.Controls.Add(this.chkArithmetic);
			this.Controls.Add(this.btnSure);
			this.Name = "FrmMain";
			this.Text = "戀數學";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSure;
		private System.Windows.Forms.CheckBox chkArithmetic;
		private System.Windows.Forms.CheckBox chkEqualityComparison;
		private System.Windows.Forms.CheckBox chkComputingConnection;
		private System.Windows.Forms.CheckBox chkMathWordProblems;
		private System.Windows.Forms.FlowLayoutPanel flpPreview;
		private System.Windows.Forms.CheckBox chkFruitsLinkage;
		private System.Windows.Forms.CheckBox chkFindNearestNumber;
		private System.Windows.Forms.CheckBox chkCombinatorialEquation;
		private System.Windows.Forms.Button button1;
	}
}

