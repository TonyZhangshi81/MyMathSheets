using MyMathSheets.TestConsoleApp.Write.Main;
using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.TestConsoleApp.Write.Attributes
{
	/// <summary>
	///
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class TopicWriteAttribute : ExportAttribute, ITogicWriteMetaDataView
	{
		/// <summary>
		/// 自定義導出屬性
		/// </summary>
		/// <param name="topicIdentifier">題型識別</param>
		public TopicWriteAttribute(string topicIdentifier) : base(typeof(IConsoleWrite))
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