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
			this.panel1 = new System.Windows.Forms.Panel();
			this.cmbWorkPages = new System.Windows.Forms.ComboBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnSure
			// 
			this.btnSure.Location = new System.Drawing.Point(261, 371);
			this.btnSure.Name = "btnSure";
			this.btnSure.Size = new System.Drawing.Size(57, 23);
			this.btnSure.TabIndex = 0;
			this.btnSure.Text = "出题";
			this.btnSure.UseVisualStyleBackColor = true;
			this.btnSure.Click += new System.EventHandler(this.SureClick);
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
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(27, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(354, 292);
			this.panel1.TabIndex = 28;
			// 
			// cmbWorkPages
			// 
			this.cmbWorkPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbWorkPages.FormattingEnabled = true;
			this.cmbWorkPages.Location = new System.Drawing.Point(62, 373);
			this.cmbWorkPages.Margin = new System.Windows.Forms.Padding(2);
			this.cmbWorkPages.Name = "cmbWorkPages";
			this.cmbWorkPages.Size = new System.Drawing.Size(189, 20);
			this.cmbWorkPages.TabIndex = 29;
			this.cmbWorkPages.SelectedIndexChanged += new System.EventHandler(this.WorkPagesSelectedIndexChanged);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(324, 370);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(57, 23);
			this.btnClose.TabIndex = 30;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.CloseClick);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(856, 405);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.cmbWorkPages);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.flpPreview);
			this.Controls.Add(this.btnSure);
			this.Name = "FrmMain";
			this.Text = "一起做练习";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSure;
		private System.Windows.Forms.FlowLayoutPanel flpPreview;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cmbWorkPages;
		private System.Windows.Forms.Button btnClose;
	}
}

