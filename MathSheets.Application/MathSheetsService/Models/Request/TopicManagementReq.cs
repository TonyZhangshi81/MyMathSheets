using System.Runtime.Serialization;

namespace MyMathSheets.WebApi.Models.Request
{
    /// <summary>
    /// </summary>
    [DataContract(Name = "請求電文")]
    public class TopicManagementReq
    {
        /// <summary>
        /// </summary>
        [DataMember(IsRequired = true, Name = "序列號")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        [DataMember(IsRequired = true, Name = "題型號")]
        public string Number
        {
            get;
            set;
        }
    }
}