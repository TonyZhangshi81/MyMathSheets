using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.Policy
{
	/// <summary>
	/// 用於計算式策略實例取得的HEPLER類
	/// </summary>
	public class TopicHelper
	{
		/// <summary>
		///
		/// </summary>
		private readonly ITopicFactory _topicFactory;

		/// <summary>
		/// 實例化
		/// </summary>
		/// <param name="topicFactory"></param>
		public TopicHelper(ITopicFactory topicFactory)
		{
			Guard.ArgumentNotNull(topicFactory, "topicFactory");

			_topicFactory = topicFactory;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <param name="topicNumber">參數編號（如果沒有指定參數編號，則默認返回當前參數序列的第一個參數項目）</param>
		/// <returns></returns>
		public TopicParameterBase Structure(string topicIdentifier, string topicNumber)
		{
			// 題型實例
			using (ITopic instance = _topicFactory.CreateTopicInstance(topicIdentifier))
			{
				// 計算式所需參數
				TopicParameterBase parameter = _topicFactory.CreateTopicParameterInstance(topicIdentifier, topicNumber);

				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0006L));

				// 根據參數構建題型
				instance.Build(parameter);

				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0007L));

				return parameter;
			}
		}
	}
}