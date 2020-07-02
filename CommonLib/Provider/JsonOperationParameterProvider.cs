using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Main.Provider;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.CommonLib.Provider
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
		public override ParameterBase Initialize(string identifier)
		{
			Guard.ArgumentNotNull(identifier, "identifier");

			// 識別號(preview + "::" + identifier)
			string[] identifiers = identifier.Split(':');

			string jsonFilePath = $@"{Argument}\{identifiers[0]}.json";

			List<ParameterBase> allProblems = null;
			// 读取资料库
			using (System.IO.StreamReader file = System.IO.File.OpenText(jsonFilePath))
			{
				allProblems = JsonConvert.DeserializeObject<List<ParameterBase>>(file.ReadToEnd());
			};


			// 如果沒有指定題型編號，則默認使用當前參數序列的第一個參數項目中的題型編號
			if (string.IsNullOrEmpty(identifiers[2]))
			{
				LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0032L, allProblems[0].Identifier));
				return allProblems[0];
			}

			// 指定的題型編號不存在的情況下,默認選用現有題型參數集中的第一個參數項目
			List<ParameterBase> list = allProblems.ToList()
										 .Where(d => d.Identifier.Equals(identifiers[2]))
										 .ToList();
			if (list.Count == 0)
			{
				LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0033L, identifiers[2], allProblems[0].Identifier));
				return allProblems[0];
			}

			// 按照指定參數標識返回相應的參數項目
			return list.First();
		}
	}
}