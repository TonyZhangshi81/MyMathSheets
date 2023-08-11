using System.Runtime.Serialization;

namespace MyMathSheets.WebApi.Models.Response
{
    /// <summary>
    /// 應答對象
    /// </summary>
    [DataContract(Name = "應答電文")]
    public class TopicRes
    {
        /// <summary>
        /// 頁面訪問地址
        /// </summary>
        [DataMember(IsRequired = true, Name = "頁面訪問地址")]
        public string Url { get; set; }
    }
}