using MyMathSheets.CommonLib.Util;
using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	/// 運算符自定義導出屬性
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class CalculateAttribute : ExportAttribute
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sign"></param>
		public CalculateAttribute(SignOfOperation sign) : base(typeof(CalculateBase))
		{
			Sign = sign;
		}

		/// <summary>
		/// 
		/// </summary>
		public SignOfOperation Sign
		{
			get;
			set;
		}
	}
}
