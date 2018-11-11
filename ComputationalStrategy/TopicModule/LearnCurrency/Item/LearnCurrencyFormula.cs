namespace MyMathSheets.ComputationalStrategy.LearnCurrency.Item
{
	/// <summary>
	/// 題型計算式對象構成
	/// </summary>
	public class LearnCurrencyFormula
	{
		/// <summary>
		/// 將數字轉換成人民幣
		/// </summary>
		/// <param name="Value"></param>
		public static string DoubleToMoney(string Value)
		{
			string j = "", f = "";
			if (double.TryParse(Value, out double valu))
			{
				int valua = (int)valu;
				int i = Value.IndexOf('.');
				if (i >= 0)
				{
					if (Value.IndexOf('.') + 2 <= Value.Length)
					{
						j = Value.Substring(Value.IndexOf('.') + 1, 1);
					}
					if (Value.IndexOf('.') + 3 <= Value.Length)
					{
						f = Value.Substring(Value.IndexOf('.') + 2, 1);
					}
				}
				if ((j == "0" || j == "") && (f == "0" || f == ""))
				{
					return $"{valua.ToString()}元";
				}
				if (f == "0" || f == "")
				{
					return $"{valua.ToString()}元{j}角";
				}
				return $"{valua.ToString()}元{j}角{f}分";
			}
			else
			{
				return "";
			}
		}
	}
}
