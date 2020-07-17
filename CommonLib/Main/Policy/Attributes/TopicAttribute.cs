using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Policy.Attributes
{
	/// <summary>
	/// 運算符自定義導出屬性
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class TopicAttribute : ExportAttribute, ITogicMetaDataView
	{
		/// <summary>
		/// 自定義導出屬性
		/// </summary>
		/// <param name="topicIdentifier">題型識別</param>
		public TopicAttribute(string topicIdentifier) : base(typeof(TopicBase))
		{
			TopicIdentifier = topicIdentifier;
		}

		/// <summary>
		/// 題型識別
		/// </summary>
		public string TopicIdentifier
		{
			get;
			set;
		}
	}
}