using MyMathSheets.CommonLib.OperationStrategy.Provider;
using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Provider
{
	/// <summary>
	/// 自定義導出屬性
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ParameterProviderAttribute : ExportAttribute, IParameterProviderMetaDataView
	{
		/// <summary>
		///
		/// </summary>
		public ParameterProviderAttribute(string name) : base(typeof(OperationParameterProvider))
		{
			Name = name;
		}

		/// <summary>
		/// Provider名
		/// </summary>
		public string Name
		{
			get;
			set;
		}
	}
}