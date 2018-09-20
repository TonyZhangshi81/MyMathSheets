using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.TheFormulaShows.Support;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;

namespace MyMathSheets.TheFormulaShows
{
	/// <summary>
	/// 
	/// </summary>
	public class MakeHtml<T> where T : ParameterBase
	{
		/// <summary>
		/// 
		/// </summary>
		private OperationHelper _helper;

		/// <summary>
		/// 
		/// </summary>
		protected OperationHelper Helper => _helper ?? (_helper = new OperationHelper());

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public T Structure(LayoutSetting.Preview preview, string identifier)
		{
			return Helper.Structure(preview, identifier) as T;
		}

		/// <summary>
		/// 指定题型大分类获得相应的题型HTML处理对象（实例）
		/// </summary>
		/// <param name="preview">题型大分类</param>
		/// <returns>题型HTML处理对象（实例）</returns>
		protected IMakeHtml<T> GetHtmlSupportInstance(LayoutSetting.Preview preview)
		{
			// spring对象工厂实例作成（设定文件导入）
			IResource input = new FileSystemResource(@"..\Config\HtmlSupport.xml");
			IObjectFactory supportObjectFactory = new XmlObjectFactory(input);

			IMakeHtml<T> support = (IMakeHtml<T>)supportObjectFactory.GetObject(preview.ToString());
			return support;
		}

		/// <summary>
		/// HTML模板替換內容作成
		/// </summary>
		/// <returns>模板替換內容</returns>
		public string GetHtmlStatement(LayoutSetting.Preview preview, T formulas)
		{
			IMakeHtml<T> support = GetHtmlSupportInstance(preview);

			return support.MakeHtml(formulas);
		}
	}
}
