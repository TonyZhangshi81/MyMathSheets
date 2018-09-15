using MyMathSheets.CommonLib.Util;
using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 運算符自定義導出屬性
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class OperationAttribute : ExportAttribute, IOperationMetaDataView
	{
		/// <summary>
		/// 
		/// </summary>
		public OperationAttribute(LayoutSetting.Preview layout) : base()
		{
			Layout = layout;
		}

		/// <summary>
		/// 
		/// </summary>
		public LayoutSetting.Preview Layout
		{
			get;
			set;
		}
	}
}
