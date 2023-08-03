namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	/// NLOG日誌處理適配器
	/// </summary>
	public class NLogLoggerFactoryAdapter : Common.Logging.NLog.NLogLoggerFactoryAdapter
	{
		/// <summary>
		/// 適配器構造處理（適配器自定義屬性讀取）
		/// </summary>
		/// <param name="properties">屬性</param>
		public NLogLoggerFactoryAdapter(Common.Logging.Configuration.NameValueCollection properties)
			: base(properties)
		{
		}

		/// <summary>
		/// 創建日誌處理實例
		/// </summary>
		/// <param name="name">日誌名稱</param>
		/// <returns>日誌處理實例<see cref="NLogLogger"/></returns>
		protected override Common.Logging.ILog CreateLogger(string name)
		{
			return new NLogLogger(NLog.LogManager.GetLogger(name));
		}
	}
}