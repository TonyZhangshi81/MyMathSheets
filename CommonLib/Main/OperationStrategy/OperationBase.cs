using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class OperationBase<T> where T : new()
	{
		/// <summary>
		/// 
		/// </summary>
		protected T _formulas;
		/// <summary>
		/// 题型（标准、随机填空）
		/// </summary>
		protected QuestionType _questionType;
		/// <summary>
		/// 在四则运算标准题下指定运算法（加减乘除）
		/// </summary>
		protected IList<SignOfOperation> _signs;
		/// <summary>
		/// 四则运算类型（标准、随机出题）
		/// </summary>
		protected FourOperationsType _fourOperationsType;
		/// <summary>
		/// 运算结果最大限度值
		/// </summary>
		protected int _maximumLimit;
		/// <summary>
		/// 出题数量
		/// </summary>
		protected int _numberOfQuestions;


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
		public T Formulas { get => _formulas; private set => _formulas = value; }

		/// <summary>
		/// 
		/// </summary>
		public OperationBase()
		{
			_formulas = new T();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit">运算结果最大限度值</param>
		/// <param name="numberOfQuestions">出题数量</param>
		public OperationBase(int maximumLimit, int numberOfQuestions)
			: this()
		{
			_maximumLimit = maximumLimit;
			_numberOfQuestions = numberOfQuestions;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public abstract void MarkFormulaList(ParameterBase parameter = null);
	}
}
