using MyMathSheets.CommonLib.Util;
using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 運算符參數自定義導出屬性
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class OperationParameterAttribute : ExportAttribute, IOperationMetaDataView
	{
		/// <summary>
		/// 自定義導出屬性
		/// </summary>
		/// <param name="layout">題型類別</param>
		/// <param name="identifiers">識別ID</param>
		public OperationParameterAttribute(LayoutSetting.Preview layout, string identifiers = "") : base(typeof(ParameterBase))
		{
			Layout = layout;
			Identifiers = identifiers;
		}

		/// <summary>
		/// 
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
