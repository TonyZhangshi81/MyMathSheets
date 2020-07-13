﻿using System;
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
		public OperationParameterAttribute(string layout) : base(typeof(ParameterBase))
		{
			Layout = layout;
		}

		/// <summary>
		/// 題型種類
		/// </summary>
		public string Layout
		{
			get;
			set;
		}
	}
}