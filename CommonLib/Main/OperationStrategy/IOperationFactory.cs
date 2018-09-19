using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 計算式策略對象生產工廠接口類
	/// </summary>
	public interface IOperationFactory
	{
		/// <summary>
		/// 對指定計算式策略實例化
		/// </summary>
		/// <param name="preview">策略種類</param>
		/// <returns>策略實例</returns>
		IOperation CreateOperationInstance(LayoutSetting.Preview preview);

		/// <summary>
		/// 對指定計算式策略所需參數的對象實例化
		/// </summary>
		/// <param name="identifier">參數識別ID</param>
		/// <returns>對象實例</returns>
		ParameterBase CreateOperationParameterInstance(string identifier);
	}
}
