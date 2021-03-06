﻿using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Policy.Attributes
{
	/// <summary>
	/// 運算符參數自定義導出屬性
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class TopicParameterAttribute : ExportAttribute, ITogicMetaDataView
	{
		/// <summary>
		/// 自定義導出屬性
		/// </summary>
		/// <param name="topicIdentifier">題型識別</param>
		public TopicParameterAttribute(string topicIdentifier) : base(typeof(TopicParameterBase))
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