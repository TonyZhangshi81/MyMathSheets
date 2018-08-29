﻿using ComputationalStrategy.Item;
using Spring.Objects.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Main
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class SetThemeOperationBase<T> where T : new()
	{
		/// <summary>
		/// 
		/// </summary>
		protected T _formulas;
		/// <summary>
		/// 
		/// </summary>
		protected Dictionary<string, ICalculatePattern> _cacheStrategy;
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
		public T Formulas { get => _formulas; private set => _formulas = value; }

		/// <summary>
		/// 
		/// </summary>
		public SetThemeOperationBase()
		{
			_formulas = new T();
		}

		/// <summary>
		/// 
		/// </summary>
		public abstract void MarkFormulaList();
	}
}
