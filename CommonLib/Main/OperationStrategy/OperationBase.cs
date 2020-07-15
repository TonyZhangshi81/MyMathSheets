using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class OperationBase : ObjectBase, IOperation
	{
		/// <summary>
		///
		/// </summary>
		private CalculateHelper _helper;

		/// <summary>
		///
		/// </summary>
		protected CalculateHelper Helper
		{
			get
			{
				if (_helper == null)
				{
					_helper = new CalculateHelper();
				}

				return _helper;
			}
		}

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
			PreExecute(parameter);

			try
			{
				MarkFormulaList(parameter);
			}
			catch
			{
				throw;
			}
			finally
			{
				PostExecute(parameter);
			}
		}

		/// <summary>
		/// 計算式作成後的處理
		/// </summary>
		/// <param name="p"></param>
		public virtual void PostExecute(ParameterBase p)
		{
		}

		/// <summary>
		/// 計算式作成前的處理
		/// </summary>
		/// <param name="p"></param>
		/// <exception cref="ArgumentNullException"><paramref name="p"/>為NULL的情況</exception>
		/// <exception cref="ArgumentNullException"><paramref name="p.Identifier"/>為NULL的情況</exception>
		public virtual void PreExecute(ParameterBase p)
		{
		}

		/// <summary>
		/// 計算式作成處理
		/// </summary>
		/// <param name="parameter"></param>
		protected abstract void MarkFormulaList(ParameterBase parameter);
	}
}