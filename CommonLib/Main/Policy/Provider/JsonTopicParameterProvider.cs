using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMathSheets.CommonLib.Main.Provider
{
	/// <summary>
	/// 取得計算式初期參數的配置信息（從JSON中取得參數值）
	/// </summary>
	[TopicParameterProvider("json")]
	public class JsonTopicParameterProvider : TopicParameterProvider
	{
		/// <summary>
		/// 參數初期化
		/// </summary>
		/// <param name="identifier">參數標識ID（如果沒有指定參數標識，則默認返回當前參數序列的第一個參數項目）</param>
		/// <exception cref="ArgumentNullException"><paramref name="identifier"/>為NULL的情況</exception>
		/// <param name="replenishArgument">補充參數</param>
		public override TopicParameterBase Initialize(string identifier, out Dictionary<string, string> replenishArgument)
		{
			Guard.ArgumentNotNull(identifier, "identifier");

			// 參數識別ID(topicIdentifier + "::" + identifier)
			string[] identifiers = identifier.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);

			// 獲取所有參數配置集合
			List<TopicParameterBase> allProblems = GetAllProblems(identifiers[0]);
			// 獲取補充參數
			replenishArgument = GetReplenishArgument(identifiers[0]);

			// 如果沒有指定題型編號，則默認使用當前參數序列的第一個參數項目中的題型編號
			if (string.IsNullOrEmpty(identifiers[1]))
			{
				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0032L, allProblems[0].TopicArgumentsIdentifier));
				return allProblems[0];
			}

			// 指定的題型編號不存在的情況下,默認選用現有題型參數集中的第一個參數項目
			List<TopicParameterBase> list = allProblems.ToList()
										 .Where(d => d.TopicArgumentsIdentifier.EndsWith(identifiers[1], StringComparison.CurrentCultureIgnoreCase))
										 .ToList();
			if (list.Count == 0)
			{
				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0033L, identifiers[1], allProblems[0].TopicArgumentsIdentifier));
				return allProblems[0];
			}

			// 按照指定參數標識返回相應的參數項目
			return list.First();
		}

		/// <summary>
		/// 獲取補充參數
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <returns>補充參數對象</returns>
		private Dictionary<string, string> GetReplenishArgument(string topicIdentifier)
		{
			// 未配置補充參數設置
			if(ReplenishArgument == null)
			{
				return null;
			}

			// 當前題型中沒有配置補充參數
			if(ReplenishArgument.TryGetValue(topicIdentifier, out Dictionary<string, string> replenishArgument))
			{
				return replenishArgument;
			}
			return null;
		}

		/// <summary>
		/// 獲取所有參數配置集合
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <returns>參數配置集合</returns>
		private List<TopicParameterBase> GetAllProblems(string topicIdentifier)
		{
			// 獲取參數配置文件（json文）的路徑（含文件名）
			string jsonFilePath = GetJsonFilePath(topicIdentifier);
			List<TopicParameterBase> allProblems = null;
			// 读取资料库
			using (StreamReader file = File.OpenText(jsonFilePath))
			{
				allProblems = JsonConvert.DeserializeObject<List<TopicParameterBase>>(file.ReadToEnd());
			};

			return allProblems;
		}

		/// <summary>
		/// 獲取參數配置文件（json文）的路徑（含文件名）
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <returns>配置文件路徑</returns>
		private string GetJsonFilePath(string topicIdentifier)
		{
			var path = Argument.ToString();
			// 相對路徑的對應
			if (path.StartsWith("~/") || path.StartsWith("~\\"))
			{
				path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\'), path.Substring(2));
			}
			string jsonFilePath = Path.Combine(path, $"{topicIdentifier}.json");

			return jsonFilePath;
		}
	}
}