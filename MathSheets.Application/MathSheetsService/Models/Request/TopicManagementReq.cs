using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MyMathSheets.WebApi.Models.Request
{
    /// <summary>
    /// </summary>
    [DataContract]
    public class TopicManagementReq
    {
        /// <summary>
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "題型必須輸入")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "題型參數序列必須輸入")]
        public string Number
        {
            get;
            set;
        }
    }
}