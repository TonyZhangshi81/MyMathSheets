using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.TheFormulaShows.Properties;
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
		private static Log log = Log.LogReady(typeof(T));

		/// <summary>
		/// HTML支援類注入配置文件所在路徑
		/// </summary>
		private const string HTML_SUPPORT_RESOURCE_NAME = @"..\Config\HtmlSupport.xml";

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

		IObjectFactory _supportObjectFactory;

		/// <summary>
		/// 指定题型大分类获得相应的题型HTML处理对象（实例）
		/// </summary>
		/// <param name="preview">题型大分类</param>
		/// <returns>题型HTML处理对象（实例）</returns>
		protected IMakeHtml<T> GetHtmlSupportInstance(LayoutSetting.Preview preview)
		{
			if (_supportObjectFactory == null)
			{
				// spring对象工厂实例作成（设定文件导入）
				IResource input = new FileSystemResource(HTML_SUPPORT_RESOURCE_NAME);
				_supportObjectFactory = new XmlObjectFactory(input);

				log.Debug(MessageUtil.GetException(() => MsgResources.I0001F));
			}

			log.Debug(MessageUtil.GetException(() => MsgResources.I0002F, preview.ToString()));

			IMakeHtml<T> support = (IMakeHtml<T>)_supportObjectFactory.GetObject(preview.ToString());

			log.Debug(MessageUtil.GetException(() => MsgResources.I0003F, preview.ToString()));

			return support;
		}

		/// <summary>
		/// HTML模板替換內容作成
		/// </summary>
		/// <returns>模板替換內容</returns>
		public string GetHtmlStatement(LayoutSetting.Preview preview, T formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0004F));

			IMakeHtml<T> support = GetHtmlSupportInstance(preview);

			return support.MakeHtml(formulas);
		}
	}
}
