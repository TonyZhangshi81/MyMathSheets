using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class MathSheetMarkerAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public MathSheetMarkerAttribute(SystemModel id)
        {
            this.SystemId = id;
        }

        /// <summary>
        /// 
        /// </summary>
        public SystemModel SystemId
        {
            get;
            set;
        }
    }
}
