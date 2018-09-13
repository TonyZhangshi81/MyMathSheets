using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.Main.ArithmeticStrategy;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;
using System.Collections.Generic;

namespace MyMathSheets.ComputationalStrategy.Main.Operation
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
		/// 
		/// </summary>
		private IObjectFactory _operatorObjectFactory;
		/// <summary>
		/// 
		/// </summary>
		private readonly Dictionary<string, ICalculatePattern> _cacheStrategy;
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
		public SetThemeBase()
		{
			_formulas = new T();

			_cacheStrategy = new Dictionary<string, ICalculatePattern>();

			// spring对象工厂实例作成（设定文件导入）
			CreateOperatorObjectFactory();
		}

		/// <summary>
		/// spring对象工厂实例作成（设定文件导入）
		/// </summary>
		private void CreateOperatorObjectFactory()
		{
			// 设定文件导入
			IResource input = new FileSystemResource(@"..\Config\Operator.xml");
			_operatorObjectFactory = new XmlObjectFactory(input);
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

		/// <summary>
		/// 指定运算符获得相应的运算处理对象（实例）
		/// </summary>
		/// <param name="sign">运算符</param>
		/// <returns>运算处理对象（实例）</returns>
		protected ICalculatePattern GetPatternInstance(SignOfOperation sign)
		{
			ICalculatePattern strategy = (ICalculatePattern)_operatorObjectFactory.GetObject(sign.ToString());
			if (!_cacheStrategy.ContainsKey(sign.ToString()))
			{
				_cacheStrategy.Add(sign.ToString(), strategy);
			}
			return _cacheStrategy[sign.ToString()];
		}
	}
}
