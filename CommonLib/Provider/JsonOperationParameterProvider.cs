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
		/// 
		/// </summary>
		/// <param name="identifier"></param>
		public override ParameterBase Initialize(string identifier)
		{
			List<ParameterBase> allProblems = null;
			// 读取资料库
			using (System.IO.StreamReader file = System.IO.File.OpenText(Argument.ToString()))
			{
				allProblems = JsonConvert.DeserializeObject<List<ParameterBase>>(file.ReadToEnd());
				//allProblems = JsonExtension.GetObjectByJson<List<ParameterBase>>(file.ReadToEnd());
			};

			return allProblems.ToList().Where(d => d.Identifier.Equals(identifier)).First();
		}
	}
}
