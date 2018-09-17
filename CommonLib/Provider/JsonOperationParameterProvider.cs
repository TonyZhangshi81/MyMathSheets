using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Util;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;

namespace MyMathSheets.CommonLib.Provider
{
	/// <summary>
	/// 取得計算式初期參數的配置信息
	/// </summary>
	public class JsonOperationParameterProvider : OperationParameterProvider
	{
		/// <summary>
		/// json文件路徑
		/// </summary>
		private string _jsonPath;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		/// <param name="identifier"></param>
		public override void Set(ParameterBase parameter, string identifier)
		{
			List<ParameterBase> allProblems = null;
			// 读取资料库
			using (System.IO.StreamReader file = System.IO.File.OpenText(_jsonPath))
			{
				allProblems = JsonExtension.GetObjectByJson<List<ParameterBase>>(file.ReadToEnd());
			};

			parameter = allProblems.ToList().Where(d => d.Identifier.Equals(identifier)).First();
		}

		/// <summary>
		/// 初始化提供程序。
		/// </summary>
		/// <param name="name">该提供程序的友好名称。</param>
		/// <param name="config">名称/值对的集合，表示在配置中为该提供程序指定的、提供程序特定的属性。</param>
		public override void Initialize(string name, NameValueCollection config)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "OperationParameterProvider";
			}

			if (null == config)
			{
				throw new ArgumentException("config參數不能為null");
			}

			string sectionName = config["settingName"];
			if (sectionName == null || sectionName.Length < 1)
			{
				throw new ProviderException("settingName屬性缺少或者為空");
			}

			base.Initialize(name, config);

			_jsonPath = ConfigurationManager.ConnectionStrings[sectionName].ElementInformation.Properties["Path"].Value.ToString();
			if (string.IsNullOrWhiteSpace(_jsonPath))
			{
				throw new ProviderException("找不到[" + sectionName + "]所指定的配置信息");
			}
		}
	}
}
