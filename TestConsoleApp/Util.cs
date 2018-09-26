using MyMathSheets.CommonLib.Util;
using MyMathSheets.TestConsoleApp.Write;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;

namespace MyMathSheets.TestConsoleApp
{
	/// <summary>
	/// 
	/// </summary>
	public static class Util
	{
		/// <summary>
		/// 輸出類注入配置文件所在路徑
		/// </summary>
		private const string CONSOLE_FORMULAS_XML_RESOURCE_NAME = @"..\Config\ConsoleFormulas.xml";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="parameter"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		public static string GetValue(GapFilling item, int parameter, GapFilling gap)
		{
			return item == gap ? string.Format("({0})", parameter) : parameter.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		static IObjectFactory _objectFactory;
		/// <summary>
		/// spring对象工厂实例作成（设定文件导入）
		/// </summary>
		/// <param name="preview"></param>
		/// <param name="formulas"></param>
		public static void CreateOperatorObjectFactory<T>(LayoutSetting.Preview preview, T formulas)
		{
			if (_objectFactory == null)
			{
				// 设定文件导入
				IResource input = new FileSystemResource(CONSOLE_FORMULAS_XML_RESOURCE_NAME);
				_objectFactory = new XmlObjectFactory(input);
			}

			IConsoleWrite<T> instance = _objectFactory.GetObject(preview.ToString()) as IConsoleWrite<T>;
			instance.ConsoleFormulas(formulas);
		}
	}
}
