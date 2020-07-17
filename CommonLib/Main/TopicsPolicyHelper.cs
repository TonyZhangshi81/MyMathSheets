using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Main.Policy;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main
{
	/// <summary>
	///
	/// </summary>
	public class TopicsPolicyHelper
	{
		/// <summary>
		/// 定義一個靜態變量來保存類的實例
		/// </summary>
		private static TopicsPolicyHelper _instance;

		/// <summary>
		/// 定義一個標識確保線程同步
		/// </summary>
		private static readonly object locker = new object();

		/// <summary>
		/// 計算式工廠注入點（对象共享注入）
		/// </summary>
		[Import(typeof(ITopicFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
		public ITopicFactory TopicFactory
		{
			get;
			set;
		}

		/// <summary>
		///
		/// </summary>
		private TopicHelper _helper;

		/// <summary>
		///
		/// </summary>
		protected TopicHelper Helper
		{
			get
			{
				if (_helper == null)
				{
					_helper = new TopicHelper(TopicFactory);
				}

				return _helper;
			}
		}

		/// <summary>
		/// 定義私有構造函數，使外界不能創建該類實例
		/// </summary>
		private TopicsPolicyHelper()
		{
			ComposerFactory.GetComporser(this.GetType().Assembly).Compose(this);
		}

		/// <summary>
		/// 定義公有屬性提供一個全局訪問點
		/// </summary>
		public static TopicsPolicyHelper Instance
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
							_instance = new TopicsPolicyHelper();
						}
					}
				}
				return _instance;
			}
		}

		/// <summary>
		/// 題型構築并返回結果
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <param name="topicNumber">參數編號（如果沒有指定參數編號，則默認返回當前參數序列的第一個參數項目）</param>
		public TopicParameterBase Structure(string topicIdentifier, string topicNumber)
		{
			return Helper.Structure(topicIdentifier, topicNumber);
		}
	}
}