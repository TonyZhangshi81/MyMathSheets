using System.Collections.Specialized;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	/// 
	/// </summary>
	public class NLogLoggerFactoryAdapter : Common.Logging.Factory.AbstractCachingLoggerFactoryAdapter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="collection"></param>
		public NLogLoggerFactoryAdapter(NameValueCollection properties)
			: base(true)
		{
			string configType = properties["configType"];
			string configFile = properties["configFile"];

			switch (configType)
			{
				case "File":
					NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(configFile);
					break;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		protected override Common.Logging.ILog CreateLogger(string name)
		{
			return new NLogger(name);
		}
	}
}
