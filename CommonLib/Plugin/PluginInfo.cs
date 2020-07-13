using MyMathSheets.CommonLib.Util;
using System;
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
			/// 題型名稱
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

			var subsection = file.Name.Split(new char[] { '.' }, StringSplitOptions.None);
			if (subsection.Length == 5)
			{
				// 題型名稱
				Description = subsection[(int)SectionType.Preview];
				// 題型分類
				Classify = (LayoutSetting.Classify)Enum.Parse(typeof(LayoutSetting.Classify), subsection[(int)SectionType.Classify]);
				// 題型識別ID
				TopicIdentifier = subsection[(int)SectionType.Preview];
				// 插件DLL的完整路徑
				FullName = file.FullName;
			}
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
		/// 題型識別ID
		/// </summary>
		public string TopicIdentifier
		{
			get;
			private set;
		}

		/// <summary>
		/// 插件DLL的完整路徑
		/// </summary>
		public string FullName
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