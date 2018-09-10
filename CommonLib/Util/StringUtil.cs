namespace CommonLib.Util
{
	/// <summary>
	/// 
	/// </summary>
	public static class StringUtil
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		public static string OperationToID(this SignOfOperation operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfOperation.Plus:
					flag = "P";
					break;
				case SignOfOperation.Subtraction:
					flag = "S";
					break;
				case SignOfOperation.Division:
					flag = "D";
					break;
				case SignOfOperation.Multiple:
					flag = "M";
					break;
				default:
					break;
			}
			return flag;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		public static string ToOperationString(this SignOfOperation operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfOperation.Plus:
					flag = "+";
					break;
				case SignOfOperation.Subtraction:
					flag = "-";
					break;
				case SignOfOperation.Division:
					flag = "÷";
					break;
				case SignOfOperation.Multiple:
					flag = "×";
					break;
				default:
					break;
			}
			return flag;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		public static string ToSignOfCompareString(this SignOfCompare operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfCompare.Equal:
					flag = "=";
					break;
				case SignOfCompare.Greater:
					flag = ">";
					break;
				case SignOfCompare.Less:
					flag = "<";
					break;
				default:
					break;
			}
			return flag;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		public static string ToSignOfCompareEnString(this SignOfCompare operation)
		{
			var flag = string.Empty;
			switch (operation)
			{
				case SignOfCompare.Equal:
					flag = "calculator";
					break;
				case SignOfCompare.Greater:
					flag = "char-more";
					break;
				case SignOfCompare.Less:
					flag = "char-less";
					break;
				default:
					break;
			}
			return flag;
		}
	}
}
