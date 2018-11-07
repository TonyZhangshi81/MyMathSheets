using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Main.Provider;
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
		public override ParameterBase Initialize(string identifier = "")
		{
			List<ParameterBase> allProblems = null;
			// 读取资料库
			using (System.IO.StreamReader file = System.IO.File.OpenText(Argument.ToString()))
			{
				allProblems = JsonConvert.DeserializeObject<List<ParameterBase>>(file.ReadToEnd());
			};
			// 如果沒有指定參數標識，則默認返回當前參數序列的第一個參數項目
			if (string.IsNullOrEmpty(identifier))
			{
				return allProblems[0];
			}
			// 按照指定參數標識返回相應的參數項目
			return allProblems.ToList().Where(d => d.Identifier.Equals(identifier)).First();
		}
	}
}
