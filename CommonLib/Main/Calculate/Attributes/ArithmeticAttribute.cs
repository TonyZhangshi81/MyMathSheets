using MyMathSheets.CommonLib.Util;
using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Calculate.Attributes
{
	/// <summary>
	/// 運算符自定義導出屬性
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ArithmeticAttribute : ExportAttribute
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="sign"></param>
		public ArithmeticAttribute(SignOfOperation sign) : base(typeof(ArithmeticBase))
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