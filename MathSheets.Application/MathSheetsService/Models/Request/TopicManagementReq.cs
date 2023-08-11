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
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Number
        {
            get;
            set;
        }
    }
}