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
		/// 自定義導出屬性
		/// </summary>
		/// <param name="layout">題型類別</param>
		/// <param name="identifiers">識別ID</param>
		public OperationAttribute(LayoutSetting.Preview layout, string identifiers = "") : base(typeof(OperationBase))
		{
			Layout = layout;
			Identifiers = identifiers;
		}

		/// <summary>
		/// 題型類別
		/// </summary>
		public LayoutSetting.Preview Layout
		{
			get;
			set;
		}
		/// <summary>
		/// 識別ID
		/// </summary>
		public string Identifiers
		{
			get;
			set;
		}
	}
}
