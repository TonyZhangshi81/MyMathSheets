using System.Collections.Generic;
using System.IO;

namespace MyMathSheets.CommonLib.Plugin
{
	/// <summary>
	///
	/// </summary>
	public interface IPluginsManager
	{
		/// <summary>
		/// 插件信息初始化
		/// </summary>
		void Initialize();

		/// <summary>
		/// 檢索插件信息
		/// </summary>
		IList<FileInfo> SearchPlugins();
	}
}