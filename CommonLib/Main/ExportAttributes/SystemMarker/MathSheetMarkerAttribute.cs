using MyMathSheets.CommonLib.Util;
using System;
using System.Diagnostics;
using System.IO;

namespace MyMathSheets.CommonLib
{
	/// <summary>
	/// 自定義屬性(系統模塊識別號)
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class MathSheetMarkerAttribute : Attribute
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="fileInfo">插件</param>
		public MathSheetMarkerAttribute(FileInfo fileInfo)
		{
			Guard.ArgumentNotNull(fileInfo, "fileInfo");

			this.Description = FileVersionInfo.GetVersionInfo(fileInfo.FullName).Comments;
		}

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="model">模塊識別號</param>
		public MathSheetMarkerAttribute(SystemModelType model)
		{
			this.SystemModel = model;
			this.Preview = string.Empty;
			this.Classify = LayoutSetting.Classify.Default;
			this.Description = string.Empty;
		}

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="model">模塊識別號</param>
		/// <param name="classify">題型分類</param>
		/// <param name="preview">子模塊識別號（題型模塊化對應）</param>
		/// <param name="description">題型名稱</param>
		public MathSheetMarkerAttribute(SystemModelType model, LayoutSetting.Classify classify, string preview, string description)
		{
			this.SystemModel = model;
			this.Preview = preview;
			this.Classify = classify;
			this.Description = description;
		}

		/// <summary>
		/// 題型名稱
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// 識別號
		/// </summary>
		public SystemModelType SystemModel
		{
			get;
			set;
		}

		/// <summary>
		/// 子模塊識別號（題型模塊化對應）
		/// </summary>
		public string Preview
		{
			get;
			set;
		}

		/// <summary>
		/// 題型分類
		/// </summary>
		public LayoutSetting.Classify Classify
		{
			get;
			set;
		}
	}
}