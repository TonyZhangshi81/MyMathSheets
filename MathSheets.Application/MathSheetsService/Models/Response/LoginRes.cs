using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyMathSheets.WebApi.Models.Response
{
    /// <summary>
    /// 登錄處理應答對象
    /// </summary>
    [DataContract]
    public class LoginRes
    {
        /// <summary>
        /// Token
        /// </summary>
        [DataMember]
        public string Token { get; set; }
    }
}