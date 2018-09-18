using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using MyMathSheets.CommonLib.Main.Provider;

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
				allProblems = JsonExtension.GetObjectByJson<List<ParameterBase>>(file.ReadToEnd());
			};

			return allProblems.ToList().Where(d => d.Identifier.Equals(identifier)).First();
		}
	}
}
