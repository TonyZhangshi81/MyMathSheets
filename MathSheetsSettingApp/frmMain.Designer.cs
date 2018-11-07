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
			this.button1 = new System.Windows.Forms.Button();
			this.chkIsPreview = new System.Windows.Forms.CheckBox();
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
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(27, 341);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(354, 23);
			this.button1.TabIndex = 22;
			this.button1.Text = "設置";
			this.button1.UseVisualStyleBackColor = true;
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
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(27, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(354, 292);
			this.panel1.TabIndex = 28;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(932, 405);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.chkIsPreview);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.flpPreview);
			this.Controls.Add(this.btnSure);
			this.Name = "FrmMain";
			this.Text = "戀數學";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSure;
		private System.Windows.Forms.FlowLayoutPanel flpPreview;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox chkIsPreview;
		private System.Windows.Forms.Panel panel1;
	}
}

