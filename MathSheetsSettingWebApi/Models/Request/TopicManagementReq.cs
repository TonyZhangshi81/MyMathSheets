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
		[DataMember]
		public string Id
		{
			get;
			set;
		}

		/// <summary>
		/// </summary>
		[DataMember]
		public string Number
		{
			get;
			set;
		}
	}
}