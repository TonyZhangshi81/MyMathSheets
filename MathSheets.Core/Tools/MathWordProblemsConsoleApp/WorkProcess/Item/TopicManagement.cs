using System.Runtime.Serialization;

namespace MyMathSheets.MathWordProblemsConsoleApp.WorkProcess.Item
{
	/// <summary>
	/// 題型參數類
	/// </summary>
	[DataContract]
	public class TopicManagement
	{
		/// <summary>
		/// 題型編號
		/// </summary>
		[DataMember(Name = "id")]
		public string ID { get; set; }

		/// <summary>
		/// 題型名稱
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// 提醒編號
		/// </summary>
		[DataMember(Name = "number")]
		public string Number { get; set; }
	}
}