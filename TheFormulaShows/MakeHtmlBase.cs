using ComputationalStrategy.Item;
using ComputationalStrategy.Main.Operation;
using System.Collections.Generic;

namespace TheFormulaShows
{
	public abstract class MakeHtmlBase : IMakeHtml
	{
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
		public IList<Formula> Formulas { get => _formulas; private set => _formulas = value; }

		/// <summary>
		/// 
		/// </summary>
		public abstract string MakeHtml();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fourOperationsType"></param>
		/// <param name="sign"></param>
		/// <param name="questionType"></param>
		/// <param name="maximumLimit"></param>
		/// <param name="numberOfQuestions"></param>
		public MakeHtmlBase(FourOperationsType fourOperationsType, SignOfOperation sign, QuestionType questionType, int maximumLimit, int numberOfQuestions)
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
		public MakeHtmlBase(FourOperationsType fourOperationsType, IList<SignOfOperation> signs, QuestionType questionType, int maximumLimit, int numberOfQuestions)
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
		public void Structure()
		{
			SetThemeBase<List<Formula>> main = new ComputationalStrategy.Main.Operation.Arithmetic(_fourOperationsType, _signs, _questionType, _maximumLimit, _numberOfQuestions);

			main.MarkFormulaList();

			_formulas = main.Formulas;
		}
	}
}
