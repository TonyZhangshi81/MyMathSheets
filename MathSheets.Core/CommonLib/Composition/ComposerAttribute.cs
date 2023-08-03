using System;
using System.Reflection;

namespace MyMathSheets.CommonLib.Composition
{
	/// <summary>
	/// 用於設定優先讀取<see cref="Assembly"/>的自定義屬性
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class ComposerAttribute : Attribute
	{
		/// <summary>
		/// 取得程序集名稱
		/// </summary>
		public AssemblyName Name
		{
			get;
			private set;
		}

		/// <summary>
		/// <see cref="ComposerAttribute" />的實例構造
		/// </summary>
		/// <param name="name">取得程序集名稱</param>
		public ComposerAttribute(string name)
		{
			Name = new AssemblyName(name);
		}
	}
}