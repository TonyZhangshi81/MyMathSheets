using ComputationalStrategy.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Main.Util
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
	}
}
