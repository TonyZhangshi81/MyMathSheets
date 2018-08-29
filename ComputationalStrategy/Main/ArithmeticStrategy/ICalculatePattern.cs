using ComputationalStrategy.Item;

namespace ComputationalStrategy.Main.ArithmeticStrategy
{
	/// <summary>
	/// 
	/// </summary>
	public interface ICalculatePattern
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		Formula CreateFormula(int maximumLimit, QuestionType type = QuestionType.Standard);	
	}
}
