using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMathSheets.CommonLib.Main.Arithmetic;

namespace MyMathSheets.CommonLib.Util.Test
{
	/// <summary>
	///
	/// </summary>
	[TestClass()]
	public class ExpressCalculateUtilTest
	{
		/// <summary>
		/// 測試用計算表達式
		/// </summary>
		private string Express { get; set; }

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void ArithmeticTest01()
		{
			var calc = new ExpressCalculateUtil();
			Assert.AreEqual("7", calc.Arithmetic("3", "4", "+"));
			Assert.AreEqual("1", calc.Arithmetic("4", "3", "-"));
			Assert.AreEqual("12", calc.Arithmetic("3", "4", "*"));
			Assert.AreEqual("7", calc.Arithmetic("28", "4", "/"));
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest01()
		{
			Express = "(3+1)*5+6/2";

			var calc = new ExpressCalculateUtil();
			var express = calc.SplitExpress(Express);
			Assert.AreEqual(12, express.Count);

			// 中序表達式隊列
			var resultExpress = express.ToArray();
			Assert.AreEqual("(", resultExpress[0]);
			Assert.AreEqual("3", resultExpress[1]);
			Assert.AreEqual("+", resultExpress[2]);
			Assert.AreEqual("1", resultExpress[3]);
			Assert.AreEqual(")", resultExpress[4]);
			Assert.AreEqual("*", resultExpress[5]);
			Assert.AreEqual("5", resultExpress[6]);
			Assert.AreEqual("+", resultExpress[7]);
			Assert.AreEqual("6", resultExpress[8]);
			Assert.AreEqual("/", resultExpress[9]);
			Assert.AreEqual("2", resultExpress[10]);
			Assert.AreEqual("#", resultExpress[11]);

			var list = calc.InorderToPostorder(express);
			Assert.AreEqual(9, list.Count);

			Assert.IsTrue(calc.IsResult(list, out decimal result));
			Assert.AreEqual(23, result);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest02()
		{
			Express = "(30+10)*50+60/20";

			var calc = new ExpressCalculateUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result));
			Assert.AreEqual(2003, result);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest03()
		{
			Express = "40+(60-1)*5";

			var calc = new ExpressCalculateUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result));
			Assert.AreEqual(335, result);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest04()
		{
			var calc = new ExpressCalculateUtil();
			var express = calc.SplitExpress("40+5");
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result01));
			Assert.AreEqual(45, result01);

			Assert.IsTrue(calc.IsResult("40+5", out result01));
			Assert.AreEqual(45, result01);

			express = calc.SplitExpress("40/5");
			list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result02));
			Assert.AreEqual(8, result02);

			Assert.IsTrue(calc.IsResult("40/5", out result02));
			Assert.AreEqual(8, result02);

			express = calc.SplitExpress("40*5");
			list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result03));
			Assert.AreEqual(200, result03);

			Assert.IsTrue(calc.IsResult("40*5", out result03));
			Assert.AreEqual(200, result03);

			express = calc.SplitExpress("40-5");
			list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result04));
			Assert.AreEqual(35, result04);

			Assert.IsTrue(calc.IsResult("40-5", out result04));
			Assert.AreEqual(35, result04);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest05()
		{
			Express = "40+(60-10/2)*5";

			var calc = new ExpressCalculateUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result));
			Assert.AreEqual(315, result);

			Assert.IsTrue(calc.IsResult(Express, out result));
			Assert.AreEqual(315, result);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest06()
		{
			Express = "(40+(60-10/2))*5";

			var calc = new ExpressCalculateUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result));
			Assert.AreEqual(475, result);

			Assert.IsTrue(calc.IsResult(Express, out result));
			Assert.AreEqual(475, result);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest07()
		{
			Express = "[40+(60-10/2)]*5";

			var calc = new ExpressCalculateUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result));
			Assert.AreEqual(475, result);

			Assert.IsTrue(calc.IsResult(Express, out result));
			Assert.AreEqual(475, result);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest08()
		{
			Express = "100+{10+[40+(60-10/2)]}*5";

			var calc = new ExpressCalculateUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out decimal result));
			Assert.AreEqual(625, result);

			Assert.IsTrue(calc.IsResult(Express, out result));
			Assert.AreEqual(625, result);
		}
	}
}