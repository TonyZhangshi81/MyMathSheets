namespace MyMathSheets.MathSheetsSettingApp
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
			this.chkScoreGoal = new System.Windows.Forms.CheckBox();
			this.chkHowMuchMore = new System.Windows.Forms.CheckBox();
			this.chkIsPreview = new System.Windows.Forms.CheckBox();
			this.chkFindTheLaw = new System.Windows.Forms.CheckBox();
			this.chkNumericSorting = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// btnSure
			// 
			this.btnSure.Location = new System.Drawing.Point(76, 370);
			this.btnSure.Name = "btnSure";
			this.btnSure.Size = new System.Drawing.Size(305, 23);
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
			// chkScoreGoal
			// 
			this.chkScoreGoal.AutoSize = true;
			this.chkScoreGoal.Location = new System.Drawing.Point(132, 80);
			this.chkScoreGoal.Name = "chkScoreGoal";
			this.chkScoreGoal.Size = new System.Drawing.Size(72, 16);
			this.chkScoreGoal.TabIndex = 23;
			this.chkScoreGoal.Text = "射門得分";
			this.chkScoreGoal.UseVisualStyleBackColor = true;
			// 
			// chkHowMuchMore
			// 
			this.chkHowMuchMore.AutoSize = true;
			this.chkHowMuchMore.Location = new System.Drawing.Point(254, 80);
			this.chkHowMuchMore.Name = "chkHowMuchMore";
			this.chkHowMuchMore.Size = new System.Drawing.Size(60, 16);
			this.chkHowMuchMore.TabIndex = 24;
			this.chkHowMuchMore.Text = "比多少";
			this.chkHowMuchMore.UseVisualStyleBackColor = true;
			// 
			// chkIsPreview
			// 
			this.chkIsPreview.AutoSize = true;
			this.chkIsPreview.Location = new System.Drawing.Point(27, 374);
			this.chkIsPreview.Name = "chkIsPreview";
			this.chkIsPreview.Size = new System.Drawing.Size(48, 16);
			this.chkIsPreview.TabIndex = 25;
			this.chkIsPreview.Text = "瀏覽";
			this.chkIsPreview.UseVisualStyleBackColor = true;
			// 
			// chkFindTheLaw
			// 
			this.chkFindTheLaw.AutoSize = true;
			this.chkFindTheLaw.Location = new System.Drawing.Point(27, 114);
			this.chkFindTheLaw.Name = "chkFindTheLaw";
			this.chkFindTheLaw.Size = new System.Drawing.Size(60, 16);
			this.chkFindTheLaw.TabIndex = 26;
			this.chkFindTheLaw.Text = "找規律";
			this.chkFindTheLaw.UseVisualStyleBackColor = true;
			// 
			// chkNumericSorting
			// 
			this.chkNumericSorting.AutoSize = true;
			this.chkNumericSorting.Location = new System.Drawing.Point(132, 114);
			this.chkNumericSorting.Name = "chkNumericSorting";
			this.chkNumericSorting.Size = new System.Drawing.Size(72, 16);
			this.chkNumericSorting.TabIndex = 27;
			this.chkNumericSorting.Text = "數字排序";
			this.chkNumericSorting.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(27, 144);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(354, 178);
			this.panel1.TabIndex = 28;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(932, 405);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.chkNumericSorting);
			this.Controls.Add(this.chkFindTheLaw);
			this.Controls.Add(this.chkIsPreview);
			this.Controls.Add(this.chkHowMuchMore);
			this.Controls.Add(this.chkScoreGoal);
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
		private System.Windows.Forms.CheckBox chkScoreGoal;
		private System.Windows.Forms.CheckBox chkHowMuchMore;
		private System.Windows.Forms.CheckBox chkIsPreview;
		private System.Windows.Forms.CheckBox chkFindTheLaw;
		private System.Windows.Forms.CheckBox chkNumericSorting;
		private System.Windows.Forms.Panel panel1;
	}
}

