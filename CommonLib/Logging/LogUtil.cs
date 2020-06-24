using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System.Text;

namespace MyMathSheets.CommonLib.Logging
{
	/// <summary>
	///
	/// </summary>
	public static class LogUtil
	{
		/// <summary>
		/// 日誌處理：計算式作成處理開始
		/// </summary>
		/// <param name="identifier">識別ID</param>
		public static void LogBeginFormulaList(string identifier)
		{
			var log = Log.LogReady(typeof(OperationBase));

			log.Debug(MessageUtil.GetException(() => MsgResources.I0034L, identifier));
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="formula"></param>
		public static void LogFormula(Formula formula)
		{
			var log = Log.LogReady(typeof(OperationBase));

			StringBuilder context = new StringBuilder();
			context.Append(formula.LeftParameter);
			context.Append(formula.Sign.ToOperationString());
			context.Append(formula.RightParameter);
			context.Append(SignOfCompare.Equal.ToSignOfCompareString());
			context.Append(formula.Answer);

			log.Debug(context.ToString());
		}

		/// <summary>
		/// 日誌處理：計算式作成處理結束
		/// </summary>
		/// <param name="identifier">識別ID</param>
		public static void LogEndFormulaList(string identifier)
		{
			var log = Log.LogReady(typeof(OperationBase));

			log.Debug(MessageUtil.GetException(() => MsgResources.I0035L, identifier));
		}
	}
}