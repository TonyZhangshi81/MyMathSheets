using System.Runtime.Serialization;

namespace MyMathSheets.WebApi.Models.Response
{
	/// <summary>
	/// 應答對象
	/// </summary>
	[DataContract]
	public class TopicRes
	{
		/// <summary>
		/// 頁面訪問地址
		/// </summary>
		[DataMember]
		public string Url { get; set; }
	}
}