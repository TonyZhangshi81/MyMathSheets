﻿using ComputationalStrategy.Item;
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
	public interface ICalculatePattern
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="maximumLimit"></param>
		/// <returns></returns>
		Formula Make(int maximumLimit);
	}
}
