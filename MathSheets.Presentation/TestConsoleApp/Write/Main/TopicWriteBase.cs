using MyMathSheets.CommonLib.Main;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.TestConsoleApp.Write.Main
{
	/// <summary>
	/// 出題策略抽象類
	/// </summary>
	public abstract class TopicWriteBase<T> : ObjectBase, IConsoleWrite, IConsoleWrite<T>
		where T : TopicParameterBase
	{
		/// <summary>
		/// 策略作成
		/// </summary>
		/// <param name="param">参数对象</param>
		public virtual void Do(TopicParameterBase param)
		{
			Guard.ArgumentNotNull(param, "param");

			ConsoleFormulas((T)param);
		}

		/// <summary>
		/// 策略出題處理
		/// </summary>
		/// <param name="p">参数对象</param>
		public abstract void ConsoleFormulas(T p);
	}
}