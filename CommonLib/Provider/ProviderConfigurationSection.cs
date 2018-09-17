using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Provider
{
	/// <summary>
	/// 
	/// </summary>
	public class ProviderConfigurationSection : ConfigurationSection
	{
		/// <summary>
		/// 
		/// </summary>
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base["providers"];

		/// <summary>
		/// 
		/// </summary>
		[ConfigurationProperty("defaultProvider", DefaultValue = "JsonOperationParameterProvider")]
		public string DefaultProvider
		{
			get => (string)base["defaultProvider"];
			set => base["defaultProvider"] = value;
		}

	}
}
