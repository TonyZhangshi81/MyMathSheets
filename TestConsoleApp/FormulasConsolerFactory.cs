using MyMathSheets.CommonLib.Util;
using MyMathSheets.TestConsoleApp.Write;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;

namespace MyMathSheets.TestConsoleApp
{
	/// <summary>
	/// 各計算式輸出類的實例工廠
	/// </summary>
	public class FormulasConsolerFactory
	{
		/// <summary>
		/// 輸出類注入配置文件所在路徑
		/// </summary>
		private const string CONSOLE_FORMULAS_XML_RESOURCE_NAME = @"..\Config\ConsoleFormulas.xml";

		/// <summary>
		/// 定義一個靜態變量來保存類的實例
		/// </summary>
		private static FormulasConsolerFactory _instance;

		/// <summary>
		/// 定義一個標識確保線程同步
		/// </summary>
		private static readonly object locker = new object();

		/// <summary>
		/// 定義私有構造函數，使外界不能創建該類實例
		/// </summary>
		private FormulasConsolerFactory()
		{
		}

		/// <summary>
		/// 定義公有屬性提供一個全局訪問點
		/// </summary>
		public static FormulasConsolerFactory Instance
		{
			get
			{
				// 當第一個線程運行到這裡的時候，此時會對locker對象加鎖
				// 當第二個線程運行該方法的時候，首先檢測到locker對象加鎖狀態，該線程就會掛起等待第一個線程解鎖
				// lock語句運行完后（即線程運行完之後）會對該對象解鎖
				// 雙重鎖定只需要一個判斷就可以完成
				if (_instance == null)
				{
					lock (locker)
					{
						// 如果類的實例不存在則創建，否則直接返回
						if (_instance == null)
						{
							_instance = new FormulasConsolerFactory();
						}
					}
				}
				return _instance;
			}
		}

		/// <summary>
		/// spring對象工廠
		/// </summary>
		IObjectFactory _objectFactory;

		/// <summary>
		/// spring對象工廠實例作成
		/// </summary>
		/// <param name="preview">設定題型</param>
		/// <param name="formulas">題型輸出結果</param>
		public IConsoleWrite<T> CreateConsoleWriter<T>(LayoutSetting.Preview preview, T formulas)
		{
			if (_objectFactory == null)
			{
				// 設定文件導入
				IResource input = new FileSystemResource(CONSOLE_FORMULAS_XML_RESOURCE_NAME);
				_objectFactory = new XmlObjectFactory(input);
			}
			// 創建對象實例并返回
			IConsoleWrite<T> writer = _objectFactory.GetObject(preview.ToString()) as IConsoleWrite<T>;
			return writer;
		}
	}
}
