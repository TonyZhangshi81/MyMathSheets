using System;

namespace MyMathSheets.ComputationalStrategy.Main.Util
{
	/// <summary>
	/// 随机数取得机能对象
	/// </summary>
	public class RandomNumberComposition
	{
		/// <summary>
		/// 声明一个静态整数变量 通过他的改变 为使随机数不再紧靠（即：随机数虽然不同但是接近）
		/// </summary>
		private static int randomCount = 0;
		/// <summary>
		/// 随机上限值
		/// </summary>
		private readonly int _maxValue;
		/// <summary>
		/// 随机下限值
		/// </summary>
		private readonly int _minValue;

		/// <summary>
		/// 随机数取得机能对象构造函数
		/// </summary>
		/// <param name="minValue">随机下限值</param>
		/// <param name="maxValue">随机上限值</param>
		public RandomNumberComposition(int minValue, int maxValue)
		{
			_maxValue = maxValue;
			_minValue = minValue;
		}

		/// <summary>
		/// 取得随机数
		/// </summary>
		/// <returns>随机数</returns>
		public int GetRandomNumber()
		{
			Random ran = new Random(CreateRandomSeed());
			int randKey = ran.Next(_minValue, _maxValue + 1);

			return randKey;
		}

		/// <summary>
		/// 利用guid哈希值、当前时间ticks和计数器相乘来计算种子，生成rand变量。
		/// </summary>
		/// <remarks>获取表示此实例的日期和时间的计时周期数。</remarks>
		/// <returns>表示此实例的日期和时间的计时周期数（避免取得相同的随机数）</returns>
		private static int CreateRandomSeed()
		{
			randomCount++;
			// 实例化一个Guid类
			Guid guid = Guid.NewGuid();

			int key1 = guid.GetHashCode();
			// 返回结果介于 DateTime.MinValue.Ticks 和 DateTime.MaxValue.Ticks之间。
			int key2 = unchecked((int)DateTime.Now.Ticks);
			int seed = unchecked(key1 * key2 * randomCount);

			return seed;
		}
	}
}
