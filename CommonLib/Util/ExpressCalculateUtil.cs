using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	///
	/// </summary>
	public class ExpressCalculateUtil
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="operators"></param>
		/// <returns></returns>
		public string Arithmetic(string x, string y, string operators)
		{
			var a = decimal.Parse(x);
			var b = decimal.Parse(y);

			var result = 0m;

			switch (operators)
			{
				case "+":
					result = a + b;
					break;

				case "-":
					result = a - b;
					break;

				case "*":
					result = a * b;
					break;

				case "/":
					result = a / b;
					break;
			}
			return result.ToString();
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="PostorderExpress"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public bool IsResult(List<string> PostorderExpress, out decimal result)
		{
			if (PostorderExpress != null)
			{
				try
				{
					PostorderExpress.Add("#");
					string[] tempArry = PostorderExpress.ToArray();
					int length = tempArry.Length;
					int i = 0;
					while (tempArry[i] != "#")
					{
						if (IsOperateors(tempArry[i]))
						{
							tempArry[i - 2] = Arithmetic(tempArry[i - 2], tempArry[i - 1], tempArry[i]);
							for (int j = i; j < length; j++)
							{
								if (j + 1 < length)
									tempArry[j - 1] = tempArry[j + 1];
							}
							length -= 2;
							i -= 2;
						}
						i++;
					}
					result = decimal.Parse(tempArry[0]);
					return true;
				}
				catch
				{
					result = 0;
					return false;
				}
			}
			else
			{
				result = 0;
				return false;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="q"></param>
		/// <returns></returns>
		public List<string> InorderToPostorder(Queue<string> q)
		{
			List<string> posterOrder = new List<string>();
			Stack<string> inOrder = new Stack<string>();
			inOrder.Push("#");
			int count = q.Count;
			for (int i = 0; i < count; i++)
			{
				string item = q.Dequeue();
				if (IsOperateors(item))
				{
					string m = inOrder.First();
					int n = Priority.IsPriority(inOrder.First(), item);
					while (n == 1)
					{
						string temp = inOrder.Pop();
						if (temp != "(" && temp != ")")
						{
							posterOrder.Add(temp);
						}
						n = Priority.IsPriority(inOrder.First(), item);
					}
					if (n == 2)
					{
						inOrder.Pop();
					}
					else if (n != -1)
					{
						inOrder.Push(item);
					}
					else
					{
						return null;
					}
				}
				else
				{
					posterOrder.Add(item);
				}
			}
			return inOrder.Count == 0 ? posterOrder : null;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="express"></param>
		/// <returns></returns>
		public Queue<string> SplitExpress(string express)
		{
			express += "#";

			Queue<string> q = new Queue<string>();
			char[] arryExpress = express.ToArray();

			int i = 0;
			int j = 0;
			while (j < express.Length)
			{
				if (IsOperateors(arryExpress[j].ToString()))
				{
					if (i != j)
					{
						string tempNum = express.Substring(i, j - i);
						q.Enqueue(tempNum);
						q.Enqueue(arryExpress[j].ToString());
						i = j + 1;
					}
					else
					{
						q.Enqueue(arryExpress[j].ToString());
						i++;
					}
				}
				j++;
			}
			return q;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public bool IsOperateors(string input)
		{
			if (input == "+" || input == "-" || input == "*" || input == "/" || input == "(" || input == ")" || input == "#")
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>
	///
	/// </summary>
	internal static class Priority
	{
		private static readonly Dictionary<string, Dictionary<string, int>> DicOperators = new Dictionary<string, Dictionary<string, int>>();

		/// <summary>
		///
		/// </summary>
		static Priority()
		{
			DicOperators.Add("+", new Dictionary<string, int>());
			DicOperators.Add("-", new Dictionary<string, int>());
			DicOperators.Add("*", new Dictionary<string, int>());
			DicOperators.Add("/", new Dictionary<string, int>());
			DicOperators.Add("(", new Dictionary<string, int>());
			DicOperators.Add(")", new Dictionary<string, int>());
			DicOperators.Add("#", new Dictionary<string, int>());

			DicOperators["+"].Add("+", 1);
			DicOperators["+"].Add("-", 1);
			DicOperators["+"].Add("*", 0);
			DicOperators["+"].Add("/", 0);
			DicOperators["+"].Add("(", 0);
			DicOperators["+"].Add(")", 1);
			DicOperators["+"].Add("#", 1);

			DicOperators["-"].Add("+", 1);
			DicOperators["-"].Add("-", 1);
			DicOperators["-"].Add("*", 0);
			DicOperators["-"].Add("/", 0);
			DicOperators["-"].Add("(", 0);
			DicOperators["-"].Add(")", 1);
			DicOperators["-"].Add("#", 1);

			DicOperators["*"].Add("+", 1);
			DicOperators["*"].Add("-", 1);
			DicOperators["*"].Add("*", 1);
			DicOperators["*"].Add("/", 1);
			DicOperators["*"].Add("(", 0);
			DicOperators["*"].Add(")", 1);
			DicOperators["*"].Add("#", 1);

			DicOperators["/"].Add("+", 1);
			DicOperators["/"].Add("-", 1);
			DicOperators["/"].Add("*", 1);
			DicOperators["/"].Add("/", 1);
			DicOperators["/"].Add("(", 0);
			DicOperators["/"].Add(")", 1);
			DicOperators["/"].Add("#", 1);

			DicOperators["("].Add("+", 0);
			DicOperators["("].Add("-", 0);
			DicOperators["("].Add("*", 0);
			DicOperators["("].Add("/", 0);
			DicOperators["("].Add("(", 0);
			DicOperators["("].Add(")", 2);
			DicOperators["("].Add("#", -1);

			DicOperators[")"].Add("+", 0);
			DicOperators[")"].Add("-", 0);
			DicOperators[")"].Add("*", 0);
			DicOperators[")"].Add("/", 0);
			DicOperators[")"].Add("(", -1);
			DicOperators[")"].Add(")", 1);
			DicOperators[")"].Add("#", 1);

			DicOperators["#"].Add("+", 0);
			DicOperators["#"].Add("-", 0);
			DicOperators["#"].Add("*", 0);
			DicOperators["#"].Add("/", 0);
			DicOperators["#"].Add("(", 0);
			DicOperators["#"].Add(")", -1);
			DicOperators["#"].Add("#", 2);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="operator1"></param>
		/// <param name="operator2"></param>
		/// <returns></returns>
		public static int IsPriority(string operator1, string operator2)
		{
			return DicOperators[operator1][operator2];
		}
	}
}