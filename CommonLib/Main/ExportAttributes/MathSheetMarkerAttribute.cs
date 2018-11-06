using MyMathSheets.CommonLib.Util;
using System;

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
		/// <param name="id">模塊識別號</param>
		/// <param name="preview">子模塊識別號（題型模塊化對應）</param>
		/// <param name="description">程序集描述</param>
		/// <param name="identifier">題型參數書別號</param>
		public MathSheetMarkerAttribute(SystemModel id, LayoutSetting.Preview preview = LayoutSetting.Preview.Null, string description = "", string identifier = "")
        {
            this.SystemId = id;
			this.Preview = preview;
			this.Description = description;
			this.Identifier = identifier;
		}

		/// <summary>
		/// 題型參數書別號
		/// </summary>
		public string Identifier
		{
			get; set;
		}

		/// <summary>
		/// 程序集描述
		/// </summary>
		public string Description
		{
			get;set;
		}

		/// <summary>
		/// 識別號
		/// </summary>
		public SystemModel SystemId
        {
            get;
            set;
        }

		/// <summary>
		/// 子模塊識別號（題型模塊化對應）
		/// </summary>
		public LayoutSetting.Preview Preview
		{
			get;
			set;
		}
	}
}
