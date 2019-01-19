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
			this.flpPreview = new System.Windows.Forms.FlowLayoutPanel();
			this.chkIsPreview = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cmbWorkPages = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnSure
			// 
			this.btnSure.Location = new System.Drawing.Point(114, 555);
			this.btnSure.Margin = new System.Windows.Forms.Padding(4);
			this.btnSure.Name = "btnSure";
			this.btnSure.Size = new System.Drawing.Size(458, 34);
			this.btnSure.TabIndex = 0;
			this.btnSure.Text = "出題";
			this.btnSure.UseVisualStyleBackColor = true;
			this.btnSure.Click += new System.EventHandler(this.SureClick);
			// 
			// flpPreview
			// 
			this.flpPreview.AutoScroll = true;
			this.flpPreview.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flpPreview.Location = new System.Drawing.Point(614, 18);
			this.flpPreview.Margin = new System.Windows.Forms.Padding(4);
			this.flpPreview.Name = "flpPreview";
			this.flpPreview.Size = new System.Drawing.Size(766, 573);
			this.flpPreview.TabIndex = 18;
			this.flpPreview.WrapContents = false;
			// 
			// chkIsPreview
			// 
			this.chkIsPreview.AutoSize = true;
			this.chkIsPreview.Location = new System.Drawing.Point(40, 561);
			this.chkIsPreview.Margin = new System.Windows.Forms.Padding(4);
			this.chkIsPreview.Name = "chkIsPreview";
			this.chkIsPreview.Size = new System.Drawing.Size(70, 22);
			this.chkIsPreview.TabIndex = 25;
			this.chkIsPreview.Text = "瀏覽";
			this.chkIsPreview.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(40, 18);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(531, 438);
			this.panel1.TabIndex = 28;
			// 
			// cmbWorkPages
			// 
			this.cmbWorkPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbWorkPages.FormattingEnabled = true;
			this.cmbWorkPages.Location = new System.Drawing.Point(290, 510);
			this.cmbWorkPages.Name = "cmbWorkPages";
			this.cmbWorkPages.Size = new System.Drawing.Size(282, 26);
			this.cmbWorkPages.TabIndex = 29;
			this.cmbWorkPages.SelectedIndexChanged += new System.EventHandler(this.WorkPagesSelectedIndexChanged);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1398, 608);
			this.Controls.Add(this.cmbWorkPages);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.chkIsPreview);
			this.Controls.Add(this.flpPreview);
			this.Controls.Add(this.btnSure);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "FrmMain";
			this.Text = "戀數學";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSure;
		private System.Windows.Forms.FlowLayoutPanel flpPreview;
		private System.Windows.Forms.CheckBox chkIsPreview;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cmbWorkPages;
	}
}

