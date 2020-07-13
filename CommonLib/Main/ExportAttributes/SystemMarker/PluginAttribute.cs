using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib
{
	/// <summary>
	/// 自定義屬性(系統模塊識別號)
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class PluginAttribute : Attribute
	{
		/// <summary>
		/// 構造函數
		/// </summary>
		/// <param name="model">模塊識別號</param>
		/// <param name="classify">題型分類</param>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <param name="description">題型名稱</param>
		public PluginAttribute(SystemModelType model, LayoutSetting.Classify classify, string topicIdentifier, string description)
		{
			SystemModel = model;
			TopicIdentifier = topicIdentifier;
			Classify = classify;
			Description = description;
		}

		/// <summary>
		/// 題型名稱
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// 識別號
		/// </summary>
		public SystemModelType SystemModel
		{
			get;
			set;
		}

		/// <summary>
		/// 題型識別ID
		/// </summary>
		public string TopicIdentifier
		{
			get;
			set;
		}

		/// <summary>
		/// 題型分類
		/// </summary>
		public LayoutSetting.Classify Classify
		{
			get;
			set;
		}
	}
}