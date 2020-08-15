using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMathSheets.CommonLib.Main.Calculate;

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
			var calc = new ExpressArithmeticUtil();

			Assert.AreEqual("7", calc.Arithmetic("3", "4", "+"));
			Assert.AreEqual("1", calc.Arithmetic("4", "3", "-"));
			Assert.AreEqual("12", calc.Arithmetic("3", "4", "*"));
			Assert.AreEqual("7", calc.Arithmetic("28", "4", "/"));

			Assert.AreEqual(3, calc.Formulas[0].LeftParameter);
			Assert.AreEqual(4, calc.Formulas[0].RightParameter);
			Assert.AreEqual(SignOfOperation.Plus, calc.Formulas[0].Sign);
			Assert.AreEqual(7, calc.Formulas[0].Answer);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest01()
		{
			Express = "(3+1)*5-6/2";

			var calc = new ExpressArithmeticUtil();
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
			Assert.AreEqual("-", resultExpress[7]);
			Assert.AreEqual("6", resultExpress[8]);
			Assert.AreEqual("/", resultExpress[9]);
			Assert.AreEqual("2", resultExpress[10]);
			Assert.AreEqual("#", resultExpress[11]);

			var list = calc.InorderToPostorder(express);
			Assert.AreEqual(9, list.Count);

			Assert.IsTrue(calc.IsResult(list, out int result));
			Assert.AreEqual(17, result);

			Assert.AreEqual(4, calc.Formulas.Count);

			var f1 = calc.Formulas[0];
			Assert.AreEqual(3, f1.LeftParameter);
			Assert.AreEqual(SignOfOperation.Plus, f1.Sign);
			Assert.AreEqual(1, f1.RightParameter);
			Assert.AreEqual(4, f1.Answer);

			var f2 = calc.Formulas[1];
			Assert.AreEqual(4, f2.LeftParameter);
			Assert.AreEqual(SignOfOperation.Multiple, f2.Sign);
			Assert.AreEqual(5, f2.RightParameter);
			Assert.AreEqual(20, f2.Answer);

			var f3 = calc.Formulas[2];
			Assert.AreEqual(6, f3.LeftParameter);
			Assert.AreEqual(SignOfOperation.Division, f3.Sign);
			Assert.AreEqual(2, f3.RightParameter);
			Assert.AreEqual(3, f3.Answer);

			var f4 = calc.Formulas[3];
			Assert.AreEqual(20, f4.LeftParameter);
			Assert.AreEqual(SignOfOperation.Subtraction, f4.Sign);
			Assert.AreEqual(3, f4.RightParameter);
			Assert.AreEqual(17, f4.Answer);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest02()
		{
			Express = "(30+10)*50+60/20";

			var calc = new ExpressArithmeticUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result));
			Assert.AreEqual(2003, result);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest03()
		{
			Express = "40+(60-1)*5";

			var calc = new ExpressArithmeticUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result));
			Assert.AreEqual(335, result);
		}

		/// <summary>
		///
		/// </summary>
		[TestMethod()]
		public void SplitExpressTest04()
		{
			var calc = new ExpressArithmeticUtil();
			var express = calc.SplitExpress("40+5");
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result01));
			Assert.AreEqual(45, result01);

			Assert.IsTrue(calc.IsResult("40+5", out result01));
			Assert.AreEqual(45, result01);

			express = calc.SplitExpress("40/5");
			list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result02));
			Assert.AreEqual(8, result02);

			Assert.IsTrue(calc.IsResult("40/5", out result02));
			Assert.AreEqual(8, result02);

			express = calc.SplitExpress("40*5");
			list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result03));
			Assert.AreEqual(200, result03);

			Assert.IsTrue(calc.IsResult("40*5", out result03));
			Assert.AreEqual(200, result03);

			express = calc.SplitExpress("40-5");
			list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result04));
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

			var calc = new ExpressArithmeticUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result));
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

			var calc = new ExpressArithmeticUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result));
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

			var calc = new ExpressArithmeticUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result));
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

			var calc = new ExpressArithmeticUtil();
			var express = calc.SplitExpress(Express);
			var list = calc.InorderToPostorder(express);
			Assert.IsTrue(calc.IsResult(list, out int result));
			Assert.AreEqual(625, result);

			Assert.IsTrue(calc.IsResult(Express, out result));
			Assert.AreEqual(625, result);
		}
	}
}