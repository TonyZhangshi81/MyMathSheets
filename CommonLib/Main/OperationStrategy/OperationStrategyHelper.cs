using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public class OperationStrategyHelper
	{
		/// <summary>
		/// 定義一個靜態變量來保存類的實例
		/// </summary>
		private static OperationStrategyHelper _instance;

		/// <summary>
		/// 定義一個標識確保線程同步
		/// </summary>
		private static readonly object locker = new object();

		/// <summary>
		/// 
		/// </summary>
		private OperationHelper _helper;

		/// <summary>
		/// 
		/// </summary>
		protected OperationHelper Helper => _helper ?? (_helper = new OperationHelper());


		/// <summary>
		/// 定義私有構造函數，使外界不能創建該類實例
		/// </summary>
		private OperationStrategyHelper()
		{
		}

		/// <summary>
		/// 定義公有屬性提供一個全局訪問點
		/// </summary>
		public static OperationStrategyHelper Instance
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
							_instance = new OperationStrategyHelper();
						}
					}
				}
				return _instance;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public ParameterBase Structure(LayoutSetting.Preview preview, string identifier)
		{
			return Helper.Structure(preview, identifier);
		}
	}
}
