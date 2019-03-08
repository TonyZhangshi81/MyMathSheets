using System.Drawing;

namespace MyMathSheets.MathSheetsSettingApp
{
	partial class FrmModelLoad
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModelLoad));
			this.picCity = new System.Windows.Forms.PictureBox();
			this.picCar = new System.Windows.Forms.PictureBox();
			this.picPlane = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.picCity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picCar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picPlane)).BeginInit();
			this.SuspendLayout();
			// 
			// picCity
			// 
			this.picCity.Image = ((System.Drawing.Image)(resources.GetObject("picCity.Image")));
			this.picCity.Location = new System.Drawing.Point(0, -28);
			this.picCity.Name = "picCity";
			this.picCity.Size = new System.Drawing.Size(496, 208);
			this.picCity.TabIndex = 5;
			this.picCity.TabStop = false;
			// 
			// picCar
			// 
			this.picCar.BackColor = System.Drawing.Color.Transparent;
			this.picCar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picCar.Image = ((System.Drawing.Image)(resources.GetObject("picCar.Image")));
			this.picCar.Location = new System.Drawing.Point(0, 146);
			this.picCar.Name = "picCar";
			this.picCar.Size = new System.Drawing.Size(50, 32);
			this.picCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picCar.TabIndex = 6;
			this.picCar.TabStop = false;
			// 
			// picPlane
			// 
			this.picPlane.BackColor = System.Drawing.Color.Transparent;
			this.picPlane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picPlane.Image = ((System.Drawing.Image)(resources.GetObject("picPlane.Image")));
			this.picPlane.Location = new System.Drawing.Point(26, 41);
			this.picPlane.Name = "picPlane";
			this.picPlane.Size = new System.Drawing.Size(24, 20);
			this.picPlane.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picPlane.TabIndex = 7;
			this.picPlane.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.TimerTick);
			// 
			// FrmModelLoad
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(502, 186);
			this.ControlBox = false;
			this.Controls.Add(this.picPlane);
			this.Controls.Add(this.picCar);
			this.Controls.Add(this.picCity);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmModelLoad";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TransparencyKey = System.Drawing.Color.White;
			this.Load += new System.EventHandler(this.FrmModelLoad_Load);
			((System.ComponentModel.ISupportInitialize)(this.picCity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picCar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picPlane)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox picCity;
		private System.Windows.Forms.PictureBox picCar;
		private System.Windows.Forms.PictureBox picPlane;
		private System.Windows.Forms.Timer timer1;
	}
}