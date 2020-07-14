using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Main.Provider;

namespace MyMathSheets.CommonLib.OperationStrategy.Provider
{
	/// <summary>
	/// 取得計算式初期參數的配置信息（從畫面中取得參數值）
	/// </summary>
	[ParameterProvider("form")]
	public class FormOperationParameterProvider : OperationParameterProvider
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="identifier"></param>
		public override ParameterBase Initialize(string identifier)
		{
			return new ParameterBase();
		}
	}
}