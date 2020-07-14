using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Plugin;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Plugin
{
	/// <summary>
	/// 插件管理類
	/// </summary>
	public class PluginsManager : PluginsManagerBase
	{
		/// <summary>
		/// 文件名檢索用關鍵字
		/// </summary>
		private const string SEARCH_PATTERN = "MyMathSheets.ComputationalStrategy.*.dll";

		/// <summary>
		/// 模塊
		/// </summary>
		private static readonly string[] ExcludeAssemblies =
			new[]
				{
					"MyMathSheets.CommonLib.dll",
					"MyMathSheets.BasicOperationsLib.dll"
				};

		/// <summary>
		/// 檢索並獲取插件文件信息
		/// </summary>
		/// <returns>插件文件信息集合</returns>
		public override IList<FileInfo> SearchPlugins()
		{
			var fileList = new List<FileInfo>();

			DirectoryInfo theFolder = new DirectoryInfo(Configuration.Current.ApplicationRootPath);
			// 遍歷文件
			foreach (var fi in from _ in theFolder.GetFiles(SEARCH_PATTERN)
							   where !ExcludeAssemblies.Contains(_.Name)
							   orderby _.Name.Length descending
							   select _)
			{
				fileList.Add(fi);
			}

			return fileList;
		}
	}
}