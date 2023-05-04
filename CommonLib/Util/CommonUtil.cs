using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace MyMathSheets.CommonLib.Util
{
    /// <summary>
    /// 共通函數
    /// </summary>
    public static class CommonUtil
    {
        /// <summary>
        /// 反推判定次數
        /// </summary>
        private const int INVERSE_NUMBER = 10;

        /// <summary>
        /// 指定集合範圍內的隨機數取得
        /// </summary>
        /// <typeparam name="T">集合類型</typeparam>
        /// <param name="list">參數集合</param>
        /// <returns>隨機取得的對象</returns>
        /// <exception cref="ArgumentNullException"><paramref name="list"/>為NULL的情況</exception>
        public static T GetRandomNumber<T>(List<T> list)
        {
            Guard.ArgumentNotNull(list, "list");

            aaa;

            return list[GetRandomNumber(0, list.Count - 1)];
        }

        /// <summary>
        /// 指定範圍內的隨機數取得
        /// </summary>
        /// <param name="upper">上限值（包含）</param>
        /// <param name="lower">下限值（包含）</param>
        /// <returns>隨機數</returns>
        public static T GetRandomNumber<T>(T upper, T lower)
        {
            // 隨機數處理對象
            RandomNumberComposition random = new RandomNumberComposition(Convert.ToInt32(upper, CultureInfo.CurrentCulture), Convert.ToInt32(lower, CultureInfo.CurrentCulture));
            // 獲取隨機數并返回
            return (T)ConvertHelper.ChangeType(random.GetRandomNumber(), typeof(T), CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 指定範圍內的隨機數取得(帶條件)
        /// </summary>
        /// <param name="upper">上限值（包含）</param>
        /// <param name="lower">下限值（包含）</param>
        /// <param name="condition">條件表達式</param>
        /// <param name="getDefault">取得隨機數無法滿足條件的時候發生</param>
        /// <typeparam name="T">隨機數類型</typeparam>
        /// <returns>隨機數</returns>
        public static T GetRandomNumber<T>(T upper, T lower, Func<T, bool> condition, Func<T> getDefault)
        {
            // 當前反推判定次數（一次推算內次數累加）
            int defeated = 0;

            while (1 == 1)
            {
                // 如果大於10次則認為此題無法作成繼續下一題
                if (defeated == INVERSE_NUMBER)
                {
                    break;
                }

                var value = GetRandomNumber(upper, lower);
                if (!condition(value))
                {
                    defeated++;
                    continue;
                }
                return value;
            }

            return getDefault();
        }

        /// <summary>
        /// 時間轉換為秒數
        /// </summary>
        /// <param name="hours">小時數</param>
        /// <param name="minutes">分鐘數</param>
        /// <param name="seconds">秒數</param>
        /// <returns>轉換后的秒數</returns>
        public static int Time2Second(int hours, int minutes, int seconds)
        {
            DateTime startTime = new DateTime(1970, 1, 1, hours, minutes, seconds);
            DateTime endTime = new DateTime(1970, 1, 1);
            TimeSpan ts = new TimeSpan(startTime.Ticks - endTime.Ticks);
            return Convert.ToInt32(ts.TotalSeconds);
        }
    }
}