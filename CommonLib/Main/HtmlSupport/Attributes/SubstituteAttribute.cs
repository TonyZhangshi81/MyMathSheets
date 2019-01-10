using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib.Main.HtmlSupport.Attributes
{
	/// <summary>
	/// 自定義HTML內容注入屬性
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class SubstituteAttribute : Attribute
	{
		/// <summary>
		/// 注入點
		/// </summary>
		private readonly SubstituteType _source;
		/// <summary>
		/// 注入信息
		/// </summary>
		private readonly string _target;
		/// <summary>
		/// 取得注入點
		/// </summary>
		public SubstituteType Source
		{
			get { return _source; }
		}
		/// <summary>
		/// 獲取注入信息
		/// </summary>
		public string Target
		{
			get { return _target; }
		}
		/// <summary>
		/// 自定義HTML內容注入屬性構造函數
		/// </summary>
		/// <param name="source">注入點</param>
		/// <param name="target">取得注入點</param>
		public SubstituteAttribute(SubstituteType source, string target)
		{
			_source = source;
			_target = target;
		}
	}

	/// <summary>
	/// 屬性結構對象
	/// </summary>
	public class Substitute
	{
		/// <summary>
		/// 注入點
		/// </summary>
		public SubstituteType Souce { get; set; }
		/// <summary>
		/// 取得注入點
		/// </summary>
		public string Target { get; set; }
	}
}
