using System.Runtime.Serialization;

namespace MyMathSheets.CommonLib.Main.FromProcess.Support
{
	/// <summary>
	/// 題型參數類
	/// </summary>
	[DataContract]
	public class TopicManagement
	{
		/// <summary>
		/// 題型識別ID
		/// </summary>
		[DataMember(Name = "id")]
		public string TopicIdentifier { get; set; }

		/// <summary>
		/// 題型名稱
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// 題型編號
		/// </summary>
		[DataMember(Name = "number")]
		public string Number { get; set; }
	}
}