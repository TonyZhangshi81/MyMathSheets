using MyMathSheets.CommonLib.Util;
using System;
using System.Diagnostics;
using System.IO;

namespace MyMathSheets.CommonLib.Plugin
{
	/// <summary>
	/// 插件信息
	/// </summary>
	public class PluginInfo
	{
		/// <summary>
		/// 分段分類
		/// </summary>
		private enum SectionType : int
		{
			/// <summary>
			/// 命名空間
			/// </summary>
			Namespace = 0,

			/// <summary>
			/// 系統模塊
			/// </summary>
			SystemModelType,

			/// <summary>
			/// 題型分類
			/// </summary>
			Classify,

			/// <summary>
			/// 子模塊識別號
			/// </summary>
			Preview
		}

		/// <summary>
		/// <see cref="PluginInfo"/>的構造函數
		/// </summary>
		/// <param name="file">插件DLL</param>
		public PluginInfo(FileInfo file)
		{
			Guard.ArgumentNotNull(file, "file");

			// 題型名稱
			Description = FileVersionInfo.GetVersionInfo(file.FullName).Comments;

			var subsection = file.Name.Split(new char[] { '.' }, StringSplitOptions.None);
			// 系統功能模塊區分
			SystemModel = (SystemModelType)Enum.Parse(typeof(SystemModelType), subsection[(int)SectionType.SystemModelType]);
			// 題型分類
			Classify = (LayoutSetting.Classify)Enum.Parse(typeof(LayoutSetting.Classify), subsection[(int)SectionType.Classify]);
			// 子模塊識別號
			Preview = subsection[(int)SectionType.Preview];

			// 文件的完整目录
			FullName = file.FullName;
		}

		/// <summary>
		/// 获取目录或文件的完整目录
		/// </summary>
		public string FullName
		{
			get;
			private set;
		}

		/// <summary>
		/// 題型名稱
		/// </summary>
		public string Description
		{
			get;
			private set;
		}

		/// <summary>
		/// 識別號
		/// </summary>
		public SystemModelType SystemModel
		{
			get;
			private set;
		}

		/// <summary>
		/// 子模塊識別號（題型模塊化對應）
		/// </summary>
		public string Preview
		{
			get;
			private set;
		}

		/// <summary>
		/// 題型分類
		/// </summary>
		public LayoutSetting.Classify Classify
		{
			get;
			private set;
		}
	}
}