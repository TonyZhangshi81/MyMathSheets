using ComputationalStrategy.Item;
using Spring.Context.Support;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Main
{
	public class BuildOperation
	{
		/// <summary>
		/// 
		/// </summary>
		private Dictionary<string, ICalculatePattern> _cacheStrategy;
		/// <summary>
		/// 
		/// </summary>
		protected IList<Formula> _formulas;
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
		private IObjectFactory _operatorObjectFactory;
		/// <summary>
		/// 
		/// </summary>
		public IList<Formula> Formulas { get => _formulas; private set => _formulas = value; }

		/// <summary>
		/// 
		/// </summary>
		public BuildOperation()
		{
			_cacheStrategy = new Dictionary<string, ICalculatePattern>();
			_formulas = new List<Formula>();

			// 
			CreateOperatorObjectFactory();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="sign"></param>
		/// <param name="questionType"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
		public BuildOperation(FourOperationsType fourOperationsType, SignOfOperation sign, QuestionType questionType, int maximumLimit, int numberOfQuestions)
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
		public BuildOperation(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, QuestionType questionType, int maximumLimit, int numberOfQuestions)
		: this()
		{
			_fourOperationsType = fourOperationsType;
			_signs = signs;
			_questionType = questionType;
			_maximumLimit = maximumLimit;
			_numberOfQuestions = numberOfQuestions;
		}

		/// <summary>
		/// 
		/// </summary>
		private void CreateOperatorObjectFactory()
		{
			IResource input = new FileSystemResource(@"..\Config\Operator.xml");
			_operatorObjectFactory = new XmlObjectFactory(input);
		}

		/// <summary>
		/// 指定运算符获得相应的运算处理对象（实例）
		/// </summary>
		/// <param name="sign">运算符</param>
		/// <returns>运算处理对象（实例）</returns>
		private ICalculatePattern GetPatternInstance(SignOfOperation sign)
		{
			ICalculatePattern strategy = (ICalculatePattern)_operatorObjectFactory.GetObject(sign.ToString());
			if (!_cacheStrategy.ContainsKey(sign.ToString()))
			{
				_cacheStrategy.Add(sign.ToString(), strategy);
			}
			return _cacheStrategy[sign.ToString()];
		}


		/// <summary>
		/// 
		/// </summary>
		public void MarkFormulaList()
		{
			if (_fourOperationsType == FourOperationsType.Default)
			{
				return;
			}

			ICalculatePattern strategy = null;
			if (_fourOperationsType == FourOperationsType.Standard)
			{
				strategy = GetPatternInstance(_signs[0]);

				for (var i = 0; i < _numberOfQuestions; i++)
				{
					_formulas.Add(strategy.Make(_maximumLimit, _questionType));
				}
			}
			else
			{
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					RandomNumberComposition random = new RandomNumberComposition(0, _signs.Count);
					SignOfOperation sign = _signs[random.GetRandomNumber()];

					strategy = GetPatternInstance(sign);
					_formulas.Add(strategy.Make(_maximumLimit, _questionType));
				}
			}
		}
	}
}
