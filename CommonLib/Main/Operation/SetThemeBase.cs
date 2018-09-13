using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Main.ArithmeticStrategy;
using MyMathSheets.CommonLib.Util;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Operation
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class SetThemeBase<T> where T : new()
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
		private ICalculateFactory _calculateManager;

		/// <summary>
		/// 
		/// </summary>
		protected ICalculateFactory CalculateManager
		{
			get
			{
				if (_calculateManager == null)
				{
					_calculateManager = new CalculateFactory();
				}
				return _calculateManager;
			}
			set => _calculateManager = value;
		}

		/// <summary>
		/// 
		/// </summary>
		public T Formulas { get => _formulas; private set => _formulas = value; }

		/// <summary>
		/// 
		/// </summary>
		public SetThemeBase()
		{
			_formulas = new T();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit">运算结果最大限度值</param>
		/// <param name="numberOfQuestions">出题数量</param>
		public SetThemeBase(int maximumLimit, int numberOfQuestions)
			: this()
		{
			_maximumLimit = maximumLimit;
			_numberOfQuestions = numberOfQuestions;
		}

		/// <summary>
		/// 
		/// </summary>
		public abstract void MarkFormulaList();
	}
}
