using CommonLib.Util;
using ComputationalStrategy.Main.Operation;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;
using System;
using System.Collections.Generic;
using TheFormulaShows.Support;

namespace TheFormulaShows
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class MakeHtml<T, F> where T : new()
	{
		/// <summary>
		/// 
		/// </summary>
		protected T _formulas;
		/// <summary>
		/// 题型（标准、随机填空）
		/// </summary>
		private readonly QuestionType _questionType;
		/// <summary>
		/// 在四则运算标准题下指定运算法（加减乘除）
		/// </summary>
		private readonly IList<SignOfOperation> _signs;
		/// <summary>
		/// 四则运算类型（标准、随机出题）
		/// </summary>
		private readonly FourOperationsType _fourOperationsType;
		/// <summary>
		/// 运算结果最大限度值
		/// </summary>
		private readonly int _maximumLimit;
		/// <summary>
		/// 出题数量
		/// </summary>
		private readonly int _numberOfQuestions;
		/// <summary>
		/// 
		/// </summary>
		public T Formulas { get => _formulas; private set => _formulas = value; }
		/// <summary>
		/// 
		/// </summary>
		private SetThemeBase<T> main;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="sign"></param>
		/// <param name="questionType"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
		public MakeHtml(FourOperationsType fourOperationsType, SignOfOperation sign, QuestionType questionType, int maximumLimit, int numberOfQuestions)
			: this(fourOperationsType, new List<SignOfOperation>(), questionType, maximumLimit, numberOfQuestions)
		{
			_signs.Add(sign);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="signs"></param>
		/// <param name="questionType"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
		public MakeHtml(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, QuestionType questionType, int maximumLimit, int numberOfQuestions)
		{
			_fourOperationsType = fourOperationsType;
			_signs = signs;
			_questionType = questionType;
			_maximumLimit = maximumLimit;
			_numberOfQuestions = numberOfQuestions;
		}

		/// <summary>
		/// 指定题型大分类获得相应的题型HTML处理对象（实例）
		/// </summary>
		/// <param name="name">题型大分类</param>
		/// <returns>题型HTML处理对象（实例）</returns>
		protected IMakeHtml<T> GetHtmlSupportInstance(string name)
		{
			// spring对象工厂实例作成（设定文件导入）
			IResource input = new FileSystemResource(@"..\Config\HtmlSupport.xml");
			IObjectFactory supportObjectFactory = new XmlObjectFactory(input);

			IMakeHtml<T> support = (IMakeHtml<T>)supportObjectFactory.GetObject(name);
			return support;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private F GetHtmlSupportInstance()
		{
			Type type = typeof(F);

			object[] arguments = null;
			if (type.Equals(typeof(Arithmetic)))
			{
				// 构造函数的参数 
				arguments = new object[5] { _fourOperationsType, _signs, _questionType, _maximumLimit, _numberOfQuestions };
			}
			else if (type.Equals(typeof(EqualityComparison)))
			{
				// 构造函数的参数 
				arguments = new object[4] { _fourOperationsType, _signs, _maximumLimit, _numberOfQuestions };
			}
			else if (type.Equals(typeof(ComputingConnection)))
			{
				// 构造函数的参数 
				arguments = new object[4] { _fourOperationsType, _signs, _maximumLimit, _numberOfQuestions };
			}
			else if (type.Equals(typeof(MathWordProblems)))
			{
				// 构造函数的参数 
				arguments = new object[4] { _fourOperationsType, _signs, _maximumLimit, _numberOfQuestions };
			}

			// 用Activator的CreateInstance静态方法，生成新对象 
			F instance = (F)Activator.CreateInstance(type, arguments);

			return instance;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Structure()
		{
			main = GetHtmlSupportInstance() as SetThemeBase<T>;
			main.MarkFormulaList();
			_formulas = main.Formulas;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetHtmlStatement()
		{
			Type type = typeof(F);

			IMakeHtml<T> support = GetHtmlSupportInstance(type.Name);
			return support.MakeHtml(_formulas);
		}
	}
}
