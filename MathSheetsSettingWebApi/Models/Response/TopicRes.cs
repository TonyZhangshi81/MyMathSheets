using System.Runtime.Serialization;

namespace MyMathSheets.WebApi.Models.Response
{
	/// <summary>
	///
	/// </summary>
	[DataContract]
	public class TopicRes
	{
		/// <summary>
		///
		/// </summary>
		[DataMember]
		public string Result { get; set; }

		/// <summary>
		///
		/// </summary>
		[DataMember]
		public int StatusCode { get; set; }
	}
}