using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MyMathSheets.WebApi.Models.Request
{
    /// <summary>
    /// 登錄處理請求對象
    /// </summary>
    [DataContract]
    public class LoginReq
    {
        /// <summary>
        /// 用戶ID
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "用戶ID必須輸入")]
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 登錄密碼
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "登錄密碼必須輸入")]
        public string Passord
        {
            get;
            set;
        }
    }
}