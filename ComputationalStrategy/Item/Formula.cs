using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Item
{
	/// <summary>
	/// 计算式对象构成
	/// </summary>
	public class Formula
	{
		/// <summary>
		/// 运算符左边参数
		/// </summary>
		public int LeftParameter { get; set; }
		/// <summary>
		/// 运算符右边参数
		/// </summary>
		public int RightParameter { get; set; }
		/// <summary>
		/// 运算符
		/// </summary>
		/// <see cref="Item.SignOfOperation"/>
		public SignOfOperation Sign { get; set; }
		/// <summary>
		/// 等式结果
		/// </summary>
		public int Answer { get; set; }
		/// <summary>
		/// 填空随机项目（运算符左边参数、运算符右边参数、等式结果）
		/// </summary>
		public GapFilling Gap { get; set; }
	}

	/// <summary>
	/// 运算符
	/// </summary>
	public enum SignOfOperation : int
	{
		/// <summary>
		/// 加号
		/// </summary>
		Plus = 0,
		/// <summary>
		/// 减号
		/// </summary>
		Subtraction,
		/// <summary>
		/// 乘号
		/// </summary>
		Multiple,
		/// <summary>
		/// 除号
		/// </summary>
		Division
	}

	/// <summary>
	/// 填空随机项目
	/// </summary>
	public enum GapFilling : int
	{
		/// <summary>
		/// 运算符左边参数
		/// </summary>
		Left = 0,
		/// <summary>
		/// 运算符右边参数
		/// </summary>
		Right,
		/// <summary>
		/// 等式结果(符号个数 = 3)
		/// </summary>
		Answer
	}

	/// <summary>
	/// 题型（标准、随机填空）
	/// </summary>
	public enum QuestionType : int
	{
		/// <summary>
		/// 标准
		/// </summary>
		Standard = 1,
		/// <summary>
		/// 随机填空
		/// </summary>
		GapFilling
	}

	/// <summary>
	/// 四则运算类型（标准、随机出题）
	/// </summary>
	public enum FourOperationsType : int
	{
		/// <summary>
		/// 未指定
		/// </summary>
		Default = 0,
		/// <summary>
		/// 标准
		/// </summary>
		Standard,
		/// <summary>
		/// 随机出题（加减乘除）
		/// </summary>
		Random
	}
}
