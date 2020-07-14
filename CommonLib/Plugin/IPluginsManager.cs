using System.Collections.Generic;
using System.IO;

namespace MyMathSheets.CommonLib.Plugin
{
	/// <summary>
	/// 插件管理接口
	/// </summary>
	public interface IPluginsManager
	{
		/// <summary>
		/// 插件信息初始化
		/// </summary>
		void Initialize();

		/// <summary>
		/// 檢索並獲取插件文件信息
		/// </summary>
		IList<FileInfo> SearchPlugins();
	}
}