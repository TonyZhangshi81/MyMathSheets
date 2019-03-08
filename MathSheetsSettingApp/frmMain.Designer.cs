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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.btnSure = new System.Windows.Forms.Button();
			this.flpPreview = new System.Windows.Forms.FlowLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cmbWorkPages = new System.Windows.Forms.ComboBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.picCity = new System.Windows.Forms.PictureBox();
			this.picCar = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel2 = new System.Windows.Forms.Panel();
			this.picCityNext = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picCity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picCar)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picCityNext)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSure
			// 
			this.btnSure.BackColor = System.Drawing.Color.SpringGreen;
			this.btnSure.Location = new System.Drawing.Point(270, 466);
			this.btnSure.Name = "btnSure";
			this.btnSure.Size = new System.Drawing.Size(57, 23);
			this.btnSure.TabIndex = 0;
			this.btnSure.Text = "出题";
			this.btnSure.UseVisualStyleBackColor = false;
			this.btnSure.Click += new System.EventHandler(this.SureClick);
			// 
			// flpPreview
			// 
			this.flpPreview.AutoScroll = true;
			this.flpPreview.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flpPreview.Location = new System.Drawing.Point(395, 12);
			this.flpPreview.Name = "flpPreview";
			this.flpPreview.Size = new System.Drawing.Size(511, 476);
			this.flpPreview.TabIndex = 18;
			this.flpPreview.WrapContents = false;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(23, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(367, 384);
			this.panel1.TabIndex = 28;
			// 
			// cmbWorkPages
			// 
			this.cmbWorkPages.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.cmbWorkPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbWorkPages.FormattingEnabled = true;
			this.cmbWorkPages.Location = new System.Drawing.Point(71, 468);
			this.cmbWorkPages.Margin = new System.Windows.Forms.Padding(2);
			this.cmbWorkPages.Name = "cmbWorkPages";
			this.cmbWorkPages.Size = new System.Drawing.Size(189, 20);
			this.cmbWorkPages.TabIndex = 29;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.NavajoWhite;
			this.btnClose.Location = new System.Drawing.Point(333, 465);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(57, 23);
			this.btnClose.TabIndex = 30;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.CloseClick);
			// 
			// picCity
			// 
			this.picCity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picCity.Image = ((System.Drawing.Image)(resources.GetObject("picCity.Image")));
			this.picCity.Location = new System.Drawing.Point(0, 0);
			this.picCity.Name = "picCity";
			this.picCity.Size = new System.Drawing.Size(367, 88);
			this.picCity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picCity.TabIndex = 31;
			this.picCity.TabStop = false;
			// 
			// picCar
			// 
			this.picCar.BackColor = System.Drawing.Color.Transparent;
			this.picCar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picCar.Image = ((System.Drawing.Image)(resources.GetObject("picCar.Image")));
			this.picCar.Location = new System.Drawing.Point(17, 67);
			this.picCar.Name = "picCar";
			this.picCar.Size = new System.Drawing.Size(30, 20);
			this.picCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picCar.TabIndex = 32;
			this.picCar.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.TimerTick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.picCityNext);
			this.panel2.Controls.Add(this.picCity);
			this.panel2.Controls.Add(this.picCar);
			this.panel2.Location = new System.Drawing.Point(23, 372);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(367, 88);
			this.panel2.TabIndex = 33;
			// 
			// picCityNext
			// 
			this.picCityNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picCityNext.Image = ((System.Drawing.Image)(resources.GetObject("picCityNext.Image")));
			this.picCityNext.Location = new System.Drawing.Point(3, -3);
			this.picCityNext.Name = "picCityNext";
			this.picCityNext.Size = new System.Drawing.Size(367, 88);
			this.picCityNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picCityNext.TabIndex = 33;
			this.picCityNext.TabStop = false;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(904, 500);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.btnSure);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.cmbWorkPages);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.flpPreview);
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmMain";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "一起数学";
			this.Load += new System.EventHandler(this.FormLoad);
			((System.ComponentModel.ISupportInitialize)(this.picCity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picCar)).EndInit();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picCityNext)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSure;
		private System.Windows.Forms.FlowLayoutPanel flpPreview;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cmbWorkPages;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.PictureBox picCity;
		private System.Windows.Forms.PictureBox picCar;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.PictureBox picCityNext;
	}
}

