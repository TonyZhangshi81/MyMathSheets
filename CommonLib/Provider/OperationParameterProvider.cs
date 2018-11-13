using MyMathSheets.CommonLib.Main.OperationStrategy;

namespace MyMathSheets.CommonLib.Provider
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class OperationParameterProvider
	{
		/// <summary>
		/// 共通參數
		/// </summary>
		public object Argument { get; set; }

		/// <summary>
		/// 獲得Parameter
		/// </summary>
		/// <param name="identifier"></param>
		/// <returns></returns>
		public abstract ParameterBase Initialize(string identifier);
	}
}
