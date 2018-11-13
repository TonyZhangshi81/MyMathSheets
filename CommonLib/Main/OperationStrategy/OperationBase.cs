using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class OperationBase : IOperation
	{
		private static Log log = Log.LogReady(typeof(OperationBase));

		/// <summary>
		/// 
		/// </summary>
		private CalculateHelper _helper;

		/// <summary>
		/// 
		/// </summary>
		protected CalculateHelper Helper => _helper ?? (_helper = new CalculateHelper());

		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		protected ICalculate CalculateManager(SignOfOperation sign)
		{
			return Helper.CreateCalculateInstance(sign);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public virtual void Build(ParameterBase parameter)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0006L));

			MarkFormulaList(parameter);

			log.Debug(MessageUtil.GetException(() => MsgResources.I0007L));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		protected abstract void MarkFormulaList(ParameterBase parameter);
	}
}
