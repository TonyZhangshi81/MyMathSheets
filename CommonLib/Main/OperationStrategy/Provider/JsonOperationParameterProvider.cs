using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Main.Provider;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.OperationStrategy.Provider;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMathSheets.CommonLi.OperationStrategy.Provider
{
	/// <summary>
	/// 取得計算式初期參數的配置信息（從JSON中取得參數值）
	/// </summary>
	[ParameterProvider("json")]
	public class JsonOperationParameterProvider : OperationParameterProvider
	{
		/// <summary>
		/// 參數初期化
		/// </summary>
		/// <param name="identifier">參數標識ID（如果沒有指定參數標識，則默認返回當前參數序列的第一個參數項目）</param>
		/// <exception cref="ArgumentNullException"><paramref name="identifier"/>為NULL的情況</exception>
		public override ParameterBase Initialize(string identifier)
		{
			Guard.ArgumentNotNull(identifier, "identifier");

			// 參數識別ID(topicIdentifier + "::" + identifier)
			string[] identifiers = identifier.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);

			string jsonFilePath = Path.Combine(Argument.ToString(), $"{identifiers[0]}.json");

			List<ParameterBase> allProblems = null;
			// 读取资料库
			using (StreamReader file = File.OpenText(jsonFilePath))
			{
				allProblems = JsonConvert.DeserializeObject<List<ParameterBase>>(file.ReadToEnd());
			};

			// 如果沒有指定題型編號，則默認使用當前參數序列的第一個參數項目中的題型編號
			if (string.IsNullOrEmpty(identifiers[1]))
			{
				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0032L, allProblems[0].TopicArgumentsIdentifier));
				return allProblems[0];
			}

			// 指定的題型編號不存在的情況下,默認選用現有題型參數集中的第一個參數項目
			List<ParameterBase> list = allProblems.ToList()
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
	}
}