using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathSheets.CommonLib.Main.Item
{
	/// <summary>
	/// 貨幣單位
	/// </summary>
	public class Currency
	{
		/// <summary>
		/// 元单位
		/// </summary>
		public int? Yuan { get; set; }
		/// <summary>
		/// 角单位
		/// </summary>
		public int? Jiao { get; set; }
		/// <summary>
		/// 分单位
		/// </summary>
		public int? Fen { get; set; }
	}
}
