using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Util;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Policy
{
	/// <summary>
	/// 出題策略抽象類
	/// </summary>
	public abstract class TopicBase : ObjectBase, ITopic
	{
		/// <summary>
		/// 運算符工廠注入點
		/// </summary>
		[Import(typeof(IArithmeticFactory))]
		private IArithmeticFactory CalculateFactory
		{
			get;
			set;
		}

		/// <summary>
		/// 運算符支持類
		/// </summary>
		private ArithmeticHelper _helper;

		/// <summary>
		/// 運算符支持類
		/// </summary>
		private ArithmeticHelper Helper
		{
			get
			{
				if (_helper == null)
				{
					_helper = new ArithmeticHelper(CalculateFactory);
				}

				return _helper;
			}
		}

		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		protected IArithmetic CalculateManager(SignOfOperation sign)
		{
			return Helper.CreateCalculateInstance(sign);
		}

		/// <summary>
		/// 策略作成
		/// </summary>
		/// <param name="p">参数对象</param>
		public virtual void Build(TopicParameterBase p)
		{
			PreExecute(p);

			try
			{
				MarkFormulaList(p);
			}
			catch
			{
				throw;
			}
			finally
			{
				PostExecute(p);
			}
		}

		/// <summary>
		/// 計算式作成後的處理
		/// </summary>
		/// <param name="p">参数对象</param>
		public virtual void PostExecute(TopicParameterBase p)
		{
		}

		/// <summary>
		/// 計算式作成前的處理
		/// </summary>
		/// <param name="p">参数对象</param>
		public virtual void PreExecute(TopicParameterBase p)
		{
		}

		/// <summary>
		/// 計算式作成處理
		/// </summary>
		/// <param name="p">参数对象</param>
		protected abstract void MarkFormulaList(TopicParameterBase p);
	}
}