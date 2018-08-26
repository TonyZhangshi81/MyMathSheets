using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Main
{
	/// <summary>
	/// 
	/// </summary>
	public class RandomNumberComposition
	{
		/// <summary>
		/// 声明一个静态整数变量 通过他的改变 测试后感觉可以是随机数 不再紧靠（就是随机数虽然不同但是 接近）
		/// </summary>
		private static int randomCount = 0;

		/// <summary>
		/// 
		/// </summary>
		private readonly int _maxValue;
		/// <summary>
		/// 
		/// </summary>
		private readonly int _minValue;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		public RandomNumberComposition(int minValue, int maxValue)
		{
			_maxValue = maxValue;
			_minValue = minValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int GetRandomNumber()
		{
			Random ran = new Random(CreateRandomSeed());
			int randKey = ran.Next(_minValue, _maxValue);

			return randKey;
		}

		/// <summary>
		/// 利用guid哈希值、当前时间ticks和计数器相乘来计算种子，生成rand变量。
		/// </summary>
		private static int CreateRandomSeed()
		{
			randomCount++;
			//s实例化一个Guid类
			Guid guid = Guid.NewGuid();

			int key1 = guid.GetHashCode();
			// 摘要:获取表示此实例的日期和时间的计时周期数。
			// 返回结果: 表示此实例的日期和时间的计时周期数。该值介于 DateTime.MinValue.Ticks 和 DateTime.MaxValue.Ticks之间。
			int key2 = unchecked((int)DateTime.Now.Ticks);
			int seed = unchecked(key1 * key2 * randomCount);

			return seed;
		}
	}
}
