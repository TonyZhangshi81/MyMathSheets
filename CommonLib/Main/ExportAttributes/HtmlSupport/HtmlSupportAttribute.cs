using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類自定義導出屬性
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class HtmlSupportAttribute : ExportAttribute, IHtmlSupportMetaDataView
	{
		/// <summary>
		/// 自定義導出屬性
		/// </summary>
		/// <param name="layout">題型類別</param>
		public HtmlSupportAttribute(string layout) : base(typeof(HtmlSupportBase))
		{
			Layout = layout;
		}

		/// <summary>
		/// 題型類別
		/// </summary>
		public string Layout
		{
			get;
			set;
		}
	}
}