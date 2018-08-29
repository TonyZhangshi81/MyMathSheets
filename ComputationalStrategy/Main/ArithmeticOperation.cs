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
	public class ArithmeticOperation : SetThemeOperationBase<List<Formula>>
	{
		/// <summary>
		/// 
		/// </summary>
		private IObjectFactory _operatorObjectFactory;

		/// <summary>
		/// 
		/// </summary>
		public ArithmeticOperation() : base()
		{
			_cacheStrategy = new Dictionary<string, ICalculatePattern>();

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
		public ArithmeticOperation(FourOperationsType fourOperationsType, SignOfOperation sign, QuestionType questionType, int maximumLimit, int numberOfQuestions)
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
		public ArithmeticOperation(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, QuestionType questionType, int maximumLimit, int numberOfQuestions)
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
		public override void MarkFormulaList()
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
					_formulas.Add(strategy.CreateFormula(_maximumLimit, _questionType));
				}
			}
			else
			{
				for (var i = 0; i < _numberOfQuestions; i++)
				{
					RandomNumberComposition random = new RandomNumberComposition(0, _signs.Count - 1);
					SignOfOperation sign = _signs[random.GetRandomNumber()];

					strategy = GetPatternInstance(sign);
					_formulas.Add(strategy.CreateFormula(_maximumLimit, _questionType));
				}
			}
		}
	}
}
