using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Policy
{
	/// <summary>
	/// 出題策略抽象類
	/// </summary>
	public abstract class TopicBase<T> : ObjectBase, ITopic, ITopic<T>
		where T : TopicParameterBase
	{
		/// <summary>
		/// 運算符工廠注入點
		/// </summary>
		[Import(typeof(IArithmeticFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
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
			return Helper.CreateArithmeticInstance(sign);
		}

		/// <summary>
		/// 策略作成
		/// </summary>
		/// <param name="param">参数对象</param>
		public virtual void Build(TopicParameterBase param)
		{
			Guard.ArgumentNotNull(param, "param");
			if (!typeof(T).IsAssignableFrom(param.GetType()))
			{
				throw CreateInvalidParameterException(typeof(T));
			}

			var p = (T)param;

			PreMarkFormulaList(p);

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
				PostMarkFormulaList(p);
			}
		}

		/// <summary>
		/// 參數模板類型不正確
		/// </summary>
		/// <param name="expectedType">參數模板類型</param>
		/// <returns>異常對象</returns>
		protected Exception CreateInvalidParameterException(Type expectedType)
		{
			Guard.ArgumentNotNull(expectedType, "expectedType");
			throw new ArgumentException(MessageUtil.GetMessage(() => MsgResources.E0045L, expectedType.FullName));
		}

		/// <summary>
		/// 策略出題之後的處理工作
		/// </summary>
		/// <param name="p">参数对象</param>
		protected virtual void PostMarkFormulaList(T p)
		{
		}

		/// <summary>
		/// 策略出題之前的準備工作
		/// </summary>
		/// <param name="p">参数对象</param>
		protected virtual void PreMarkFormulaList(T p)
		{
		}

		/// <summary>
		/// 策略出題處理
		/// </summary>
		/// <param name="p">参数对象</param>
		public abstract void MarkFormulaList(T p);
	}
}