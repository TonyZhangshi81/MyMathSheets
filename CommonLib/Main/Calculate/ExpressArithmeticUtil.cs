using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static MyMathSheets.CommonLib.Main.Calculate.Priority;

namespace MyMathSheets.CommonLib.Main.Calculate
{
	/// <summary>
	///
	/// </summary>
	public class ExpressArithmeticUtil
	{
		/// <summary>
		/// 所有操作符
		/// </summary>
		private readonly string[] Operateors = new string[] { "+", "-", "*", "/", "(", ")", "[", "]", "{", "}", "#" };

		/// <summary>
		/// 四則運算處理
		/// </summary>
		/// <param name="x">參數1</param>
		/// <param name="y">餐數2</param>
		/// <param name="operators">運算符</param>
		/// <returns>計算結果</returns>
		public static string Arithmetic(string x, string y, string operators)
		{
			var a = decimal.Parse(x, CultureInfo.CurrentCulture);
			var b = decimal.Parse(y, CultureInfo.CurrentCulture);

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
			return result.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="express">表達式</param>
		/// <param name="result">計算式結果</param>
		/// <returns>推算成功:TRUE返回</returns>
		public bool IsResult(string express, out decimal result)
		{
			var expressQueue = SplitExpress(express);
			var list = InorderToPostorder(expressQueue);

			return IsResult(list, out result);
		}

		/// <summary>
		/// 根據計算式後序表達式列表推算出計算式結果
		/// </summary>
		/// <param name="postorderExpress">計算式後序表達式列表</param>
		/// <param name="result">計算式結果</param>
		/// <returns>推算成功:TRUE返回</returns>
		public bool IsResult(List<string> postorderExpress, out decimal result)
		{
			if (postorderExpress != null)
			{
				try
				{
					postorderExpress.Add("#");

					var aryExpress = postorderExpress.ToArray();
					var length = aryExpress.Length;
					var i = 0;
					while (aryExpress[i] != "#")
					{
						if (IsOperateors(aryExpress[i]))
						{
							aryExpress[i - 2] = Arithmetic(aryExpress[i - 2], aryExpress[i - 1], aryExpress[i]);
							for (int j = i; j < length; j++)
							{
								if (j + 1 < length)
								{
									aryExpress[j - 1] = aryExpress[j + 1];
								}
							}
							length -= 2;
							i -= 2;
						}
						i++;
					}
					result = decimal.Parse(aryExpress[0], CultureInfo.CurrentCulture);
					return true;
				}
				catch
				{
					throw new ArithmeticExpressException(MessageUtil.GetMessage(() => MsgResources.E0039L));
				}
			}
			result = 0;
			return false;
		}

		/// <summary>
		/// 中序表達式轉後序表達式列表
		/// </summary>
		/// <param name="expressQueue">中序表達式隊列</param>
		/// <returns>後序表達式列表</returns>
		/// <exception cref="ArgumentNullException"><paramref name="expressQueue"/>為NULL的情況</exception>
		public List<string> InorderToPostorder(Queue<string> expressQueue)
		{
			Guard.ArgumentNotNull(expressQueue, "expressQueue");

			// 操作符棧（後進先出）
			var inOrder = new Stack<string>();
			// 初期棧頂元素設置為#
			inOrder.Push("#");

			// 中序表達式列表
			var posterOrder = new List<string>();

			// 隊列長度
			var count = expressQueue.Count;
			var i = 0;
			// 順序讀取中序表達式隊列
			while (i < count)
			{
				// 隊列中讀取並刪除一個元素
				var item = expressQueue.Dequeue();
				// 該元素是不是操作符
				if (IsOperateors(item))
				{
					var priority = Priority.GetPriority(inOrder.First(), item);
					// 操作符優先級為大於的情況
					while (priority == PriorityType.Larger)
					{
						var top = inOrder.Pop();
						if (top != "(" && top != ")" && top != "[" && top != "]" && top != "{" && top != "}")
						{
							posterOrder.Add(top);
						}
						priority = Priority.GetPriority(inOrder.First(), item);
					}

					// 操作符優先級為等於號的情況
					if (priority == PriorityType.Equal)
					{
						inOrder.Pop();
					}
					// 操作符優先級為小於號的情況
					else if (priority != PriorityType.Unmatched)
					{
						inOrder.Push(item);
					}
					// 操作符優先級無效的情況（不匹配）
					else
					{
						return null;
					}
				}
				else
				{
					// 將數值加入中序表達式列表
					posterOrder.Add(item);
				}

				i++;
			}
			return inOrder.Count == 0 ? posterOrder : null;
		}

		/// <summary>
		/// 分割表達式並入隊列
		/// </summary>
		/// <param name="express">表達式</param>
		/// <returns>中序表達式隊列</returns>
		public Queue<string> SplitExpress(string express)
		{
			express += "#";

			// 隊列定義（先進先出）
			var expressQueue = new Queue<string>();
			// 表達式字符分解
			var arryExpress = express.ToArray();

			var i = 0;
			var j = 0;
			while (j < express.Length)
			{
				// 判斷當前字符是不是操作符
				if (IsOperateors(arryExpress[j].ToString(CultureInfo.CurrentCulture)))
				{
					if (i != j)
					{
						// 數值入棧
						string tempNum = express.Substring(i, j - i);
						expressQueue.Enqueue(tempNum);
						expressQueue.Enqueue(arryExpress[j].ToString(CultureInfo.CurrentCulture));
						i = j + 1;
					}
					else
					{
						// 操作符入棧
						expressQueue.Enqueue(arryExpress[j].ToString(CultureInfo.CurrentCulture));
						i++;
					}
				}
				j++;
			}
			// 中序表達式隊列
			return expressQueue;
		}

		/// <summary>
		/// 判斷是否為操作符
		/// </summary>
		/// <param name="input">輸入參數</param>
		/// <returns>操作:TRUE返回</returns>
		public bool IsOperateors(string input)
		{
			return Operateors.Any(d => d.Equals(input, StringComparison.CurrentCultureIgnoreCase));
		}
	}

	/// <summary>
	/// 優先級定義
	/// </summary>
	/// <remarks>
	/// 行為入棧運算符；列為棧頂運算符；
	/// 2表示等於號；1表示大於號；0表示小於號；-1表示不匹配；
	///       '+'  '-'  '*'  '/'  '('  ')'  '['  ']'  '{'  '}'  '#'
	///      -------------------------------------------------------
	/// '+'  | 1    1    0    0    0    1    0    1    0    1    1 |
	/// '-'  | 1    1    0    0    0    1    0    1    0    1    1 |
	/// '*'  | 1    1    1    1    0    1    0    1    0    1    1 |
	/// '/'  | 1    1    1    1    0    1    0    1    0    1    1 |
	/// '('  | 0    0    0    0    0    2    0    1    0    1   -1 |
	/// ')'  | 0    0    0    0   -1    1    0    1    0    1    1 |
	/// '['  | 0    0    0    0    0    0    0    2    0    1   -1 |
	/// ']'  | 0    0    0    0    0    0   -1    1    0    1    1 |
	/// '{'  | 0    0    0    0    0    0    0    0    0    2   -1 |
	/// '}'  | 0    0    0    0    0    0    0    0   -1    1    1 |
	/// '#'  | 0    0    0    0    0   -1    0   -1    0   -1    2 |
	///      -------------------------------------------------------
	/// </remarks>
	internal static class Priority
	{
		/// <summary>
		/// 優先級字典表定義
		/// </summary>
		private static readonly Dictionary<string, Dictionary<string, PriorityType>> DicOperators = new Dictionary<string, Dictionary<string, PriorityType>>();

		/// <summary>
		/// 優先級
		/// </summary>
		public enum PriorityType : int
		{
			/// <summary>
			/// 不匹配
			/// </summary>
			Unmatched = -1,

			/// <summary>
			/// 小於
			/// </summary>
			Less = 0,

			/// <summary>
			/// 大於
			/// </summary>
			Larger = 1,

			/// <summary>
			/// 等於
			/// </summary>
			Equal = 2
		}

		/// <summary>
		/// 初期化處理 - 構築操作符優先級關係列表
		/// </summary>
		static Priority()
		{
			DicOperators.Add("+", new Dictionary<string, PriorityType>());
			DicOperators.Add("-", new Dictionary<string, PriorityType>());
			DicOperators.Add("*", new Dictionary<string, PriorityType>());
			DicOperators.Add("/", new Dictionary<string, PriorityType>());
			DicOperators.Add("(", new Dictionary<string, PriorityType>());
			DicOperators.Add(")", new Dictionary<string, PriorityType>());
			DicOperators.Add("[", new Dictionary<string, PriorityType>());
			DicOperators.Add("]", new Dictionary<string, PriorityType>());
			DicOperators.Add("{", new Dictionary<string, PriorityType>());
			DicOperators.Add("}", new Dictionary<string, PriorityType>());
			DicOperators.Add("#", new Dictionary<string, PriorityType>());

			// 加法優先級列表
			DicOperators["+"].Add("+", PriorityType.Larger);
			DicOperators["+"].Add("-", PriorityType.Larger);
			DicOperators["+"].Add("*", PriorityType.Less);
			DicOperators["+"].Add("/", PriorityType.Less);
			DicOperators["+"].Add("(", PriorityType.Less);
			DicOperators["+"].Add(")", PriorityType.Larger);
			DicOperators["+"].Add("[", PriorityType.Less);
			DicOperators["+"].Add("]", PriorityType.Larger);
			DicOperators["+"].Add("{", PriorityType.Less);
			DicOperators["+"].Add("}", PriorityType.Larger);
			DicOperators["+"].Add("#", PriorityType.Larger);

			// 減法優先級列表
			DicOperators["-"].Add("+", PriorityType.Larger);
			DicOperators["-"].Add("-", PriorityType.Larger);
			DicOperators["-"].Add("*", PriorityType.Less);
			DicOperators["-"].Add("/", PriorityType.Less);
			DicOperators["-"].Add("(", PriorityType.Less);
			DicOperators["-"].Add(")", PriorityType.Larger);
			DicOperators["-"].Add("[", PriorityType.Less);
			DicOperators["-"].Add("]", PriorityType.Larger);
			DicOperators["-"].Add("{", PriorityType.Less);
			DicOperators["-"].Add("}", PriorityType.Larger);
			DicOperators["-"].Add("#", PriorityType.Larger);

			// 乘法優先級列表
			DicOperators["*"].Add("+", PriorityType.Larger);
			DicOperators["*"].Add("-", PriorityType.Larger);
			DicOperators["*"].Add("*", PriorityType.Larger);
			DicOperators["*"].Add("/", PriorityType.Larger);
			DicOperators["*"].Add("(", PriorityType.Less);
			DicOperators["*"].Add(")", PriorityType.Larger);
			DicOperators["*"].Add("[", PriorityType.Less);
			DicOperators["*"].Add("]", PriorityType.Larger);
			DicOperators["*"].Add("{", PriorityType.Less);
			DicOperators["*"].Add("}", PriorityType.Larger);
			DicOperators["*"].Add("#", PriorityType.Larger);

			// 除法優先級列表
			DicOperators["/"].Add("+", PriorityType.Larger);
			DicOperators["/"].Add("-", PriorityType.Larger);
			DicOperators["/"].Add("*", PriorityType.Larger);
			DicOperators["/"].Add("/", PriorityType.Larger);
			DicOperators["/"].Add("(", PriorityType.Less);
			DicOperators["/"].Add(")", PriorityType.Larger);
			DicOperators["/"].Add("[", PriorityType.Less);
			DicOperators["/"].Add("]", PriorityType.Larger);
			DicOperators["/"].Add("{", PriorityType.Less);
			DicOperators["/"].Add("}", PriorityType.Larger);
			DicOperators["/"].Add("#", PriorityType.Larger);

			// 左(小)括號優先級列表
			DicOperators["("].Add("+", PriorityType.Less);
			DicOperators["("].Add("-", PriorityType.Less);
			DicOperators["("].Add("*", PriorityType.Less);
			DicOperators["("].Add("/", PriorityType.Less);
			DicOperators["("].Add("(", PriorityType.Less);
			DicOperators["("].Add(")", PriorityType.Equal);
			DicOperators["("].Add("[", PriorityType.Less);
			DicOperators["("].Add("]", PriorityType.Larger);
			DicOperators["("].Add("{", PriorityType.Less);
			DicOperators["("].Add("}", PriorityType.Larger);
			DicOperators["("].Add("#", PriorityType.Unmatched);

			// 右(小)括號優先級列表
			DicOperators[")"].Add("+", PriorityType.Less);
			DicOperators[")"].Add("-", PriorityType.Less);
			DicOperators[")"].Add("*", PriorityType.Less);
			DicOperators[")"].Add("/", PriorityType.Less);
			DicOperators[")"].Add("(", PriorityType.Unmatched);
			DicOperators[")"].Add(")", PriorityType.Larger);
			DicOperators[")"].Add("[", PriorityType.Less);
			DicOperators[")"].Add("]", PriorityType.Larger);
			DicOperators[")"].Add("{", PriorityType.Less);
			DicOperators[")"].Add("}", PriorityType.Larger);
			DicOperators[")"].Add("#", PriorityType.Larger);

			// 左(中)括號優先級列表
			DicOperators["["].Add("+", PriorityType.Less);
			DicOperators["["].Add("-", PriorityType.Less);
			DicOperators["["].Add("*", PriorityType.Less);
			DicOperators["["].Add("/", PriorityType.Less);
			DicOperators["["].Add("(", PriorityType.Less);
			DicOperators["["].Add(")", PriorityType.Less);
			DicOperators["["].Add("[", PriorityType.Less);
			DicOperators["["].Add("]", PriorityType.Equal);
			DicOperators["["].Add("{", PriorityType.Less);
			DicOperators["["].Add("}", PriorityType.Larger);
			DicOperators["["].Add("#", PriorityType.Unmatched);

			// 右(中)括號優先級列表
			DicOperators["]"].Add("+", PriorityType.Less);
			DicOperators["]"].Add("-", PriorityType.Less);
			DicOperators["]"].Add("*", PriorityType.Less);
			DicOperators["]"].Add("/", PriorityType.Less);
			DicOperators["]"].Add("(", PriorityType.Less);
			DicOperators["]"].Add(")", PriorityType.Less);
			DicOperators["]"].Add("[", PriorityType.Unmatched);
			DicOperators["]"].Add("]", PriorityType.Larger);
			DicOperators["]"].Add("{", PriorityType.Less);
			DicOperators["]"].Add("}", PriorityType.Larger);
			DicOperators["]"].Add("#", PriorityType.Larger);

			// 左(大)括號優先級列表
			DicOperators["{"].Add("+", PriorityType.Less);
			DicOperators["{"].Add("-", PriorityType.Less);
			DicOperators["{"].Add("*", PriorityType.Less);
			DicOperators["{"].Add("/", PriorityType.Less);
			DicOperators["{"].Add("(", PriorityType.Less);
			DicOperators["{"].Add(")", PriorityType.Less);
			DicOperators["{"].Add("[", PriorityType.Less);
			DicOperators["{"].Add("]", PriorityType.Less);
			DicOperators["{"].Add("{", PriorityType.Less);
			DicOperators["{"].Add("}", PriorityType.Equal);
			DicOperators["{"].Add("#", PriorityType.Unmatched);

			// 右(大)括號優先級列表
			DicOperators["}"].Add("+", PriorityType.Less);
			DicOperators["}"].Add("-", PriorityType.Less);
			DicOperators["}"].Add("*", PriorityType.Less);
			DicOperators["}"].Add("/", PriorityType.Less);
			DicOperators["}"].Add("(", PriorityType.Less);
			DicOperators["}"].Add(")", PriorityType.Less);
			DicOperators["}"].Add("[", PriorityType.Less);
			DicOperators["}"].Add("]", PriorityType.Less);
			DicOperators["}"].Add("{", PriorityType.Unmatched);
			DicOperators["}"].Add("}", PriorityType.Larger);
			DicOperators["}"].Add("#", PriorityType.Larger);

			// 默認優先級列表
			DicOperators["#"].Add("+", PriorityType.Less);
			DicOperators["#"].Add("-", PriorityType.Less);
			DicOperators["#"].Add("*", PriorityType.Less);
			DicOperators["#"].Add("/", PriorityType.Less);
			DicOperators["#"].Add("(", PriorityType.Less);
			DicOperators["#"].Add(")", PriorityType.Unmatched);
			DicOperators["#"].Add("[", PriorityType.Less);
			DicOperators["#"].Add("]", PriorityType.Unmatched);
			DicOperators["#"].Add("{", PriorityType.Less);
			DicOperators["#"].Add("}", PriorityType.Unmatched);
			DicOperators["#"].Add("#", PriorityType.Equal);
		}

		/// <summary>
		/// 取得優先級
		/// </summary>
		/// <param name="inputOpt">入棧運算符</param>
		/// <param name="topOpt">棧頂運算符</param>
		/// <returns>優先級</returns>
		public static PriorityType GetPriority(string inputOpt, string topOpt)
		{
			return DicOperators[inputOpt][topOpt];
		}
	}
}