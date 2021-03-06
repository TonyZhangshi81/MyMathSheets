﻿using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.CommonLib.Main.VirtualHelper
{
	/// <summary>
	/// 虛擬會話作成抽象類
	/// </summary>
	public abstract class VirtualHelperBase<T> : IVirtualHelper<T>
	{
		/// <summary>
		/// 智能提示最大條數
		/// </summary>
		private int _dialogueMaxCount;

		/// <summary>
		/// 答題集合
		/// </summary>
		protected IList<T> Formulas { get; private set; }

		/// <summary>
		/// 對應指定題型數據類型作成幫助提示
		/// </summary>
		/// <param name="formulas">答題集合</param>
		/// <returns>智能提示對象</returns>
		/// <exception cref="ArgumentNullException"><paramref name="formulas"/>為NULL的情況</exception>
		public HelperDialogue CreateHelperDialogue(IList<T> formulas)
		{
			Guard.ArgumentNotNull(formulas, "formulas");

			if (formulas.Count == 0)
			{
				return new HelperDialogue() { Dialogues = new List<string>(), FormulaIndex = new List<int>() };
			}

			// 答題集合
			Formulas = formulas;

			// 根據答題集合數量配置智能提示所需參數
			_dialogueMaxCount = SettingParameterInit();

			// 智能提示會話內容作成
			List<int> formulaIndex = CreateFormulaIndex();
			List<string> dialogues = CreateDialogue(formulaIndex);
			return new HelperDialogue() { Dialogues = dialogues, FormulaIndex = formulaIndex };
		}

		/// <summary>
		/// 根據答題集合數量配置智能提示所需參數
		/// </summary>
		/// <remarks>
		/// 智能提示最大條數: 每5題有一個提示
		/// </remarks>
		protected virtual int SettingParameterInit()
		{
			// 智能提示最大條數
			return Convert.ToInt32(Math.Ceiling(Formulas.Count / 5.0m));
		}

		/// <summary>
		/// 會話內容所對應題目的序號（即位置）作成
		/// </summary>
		/// <returns>序號集合</returns>
		protected virtual List<int> CreateFormulaIndex()
		{
			List<int> formulaIndex = new List<int>();
			for (int index = 0; index < _dialogueMaxCount; index++)
			{
				var number = CommonUtil.GetRandomNumber(0, Formulas.Count - 1);
				if (formulaIndex.Any(d => d == number))
				{
					index--;
					continue;
				}
				formulaIndex.Add(number);
			}

			formulaIndex.Sort();
			return formulaIndex;
		}

		/// <summary>
		/// 智能提示會話內容作成
		/// </summary>
		/// <param name="formulaIndex">會話內容所對應題目的序號</param>
		/// <returns>會話內容列表</returns>
		protected abstract List<string> CreateDialogue(List<int> formulaIndex);
	}
}