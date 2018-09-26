using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib
{
	/// <summary>
	/// 自定義屬性(系統模塊識別號)
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class MathSheetMarkerAttribute : Attribute
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="id">模塊識別號</param>
        public MathSheetMarkerAttribute(SystemModel id)
        {
            this.SystemId = id;
        }

        /// <summary>
        /// 識別號
        /// </summary>
        public SystemModel SystemId
        {
            get;
            set;
        }
    }
}
