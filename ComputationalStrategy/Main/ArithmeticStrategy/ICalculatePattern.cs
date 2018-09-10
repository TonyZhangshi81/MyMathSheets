using CommonLib.Util;
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
		/// <param name="minimumLimit"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		Formula CreateFormula(int maximumLimit, QuestionType type = QuestionType.Standard, int minimumLimit = 0, GapFilling gap = GapFilling.Answer);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <param name="previousFormula"></param>
		/// <param name="type"></param>
		/// <param name="minimumLimit"></param>
		/// <param name="gap"></param>
		/// <returns></returns>
		Formula CreateFormula(int maximumLimit, Formula previousFormula, QuestionType type = QuestionType.GapFilling, int minimumLimit = 0, GapFilling gap = GapFilling.Right);

		/// <summary>
		/// 由計算結果推算出計算式(使用場景:水果連連看)
		/// </summary>
		/// <param name="maximumLimit">推算結果最大範圍(上限)</param>
		/// <param name="answer">計算結果</param>
		/// <param name="type">題型設定值</param>
		/// <param name="minimumLimit">推算結果最大範圍(下限)</param>
		/// <param name="gap">隨機項目設定值</param>
		/// <returns>計算式</returns>
		Formula CreateFormulaWithAnswer(int maximumLimit, int answer, QuestionType type = QuestionType.Standard, int minimumLimit = 0, GapFilling gap = GapFilling.Default);

		/// <summary>
		/// 指定範圍內隨機設定填空項目
		/// </summary>
		/// <param name="minValue">上限值</param>
		/// <param name="maxValue">下限值</param>>
		/// <returns></returns>
		void SetGapFillingItem(GapFilling minValue, GapFilling maxValue);
	}
}
