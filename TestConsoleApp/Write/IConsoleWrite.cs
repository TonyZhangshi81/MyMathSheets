using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp.Write
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IConsoleWrite<T>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formulas"></param>
		void ConsoleFormulas(T formulas);
	}
}
