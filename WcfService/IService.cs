using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MyMathSheets.WcfService
{
	/// <summary>
	///
	/// </summary>
	[ServiceContract]
	public interface IService
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		[OperationContract]
		[WebInvoke(
			UriTemplate = "/MarkFormula",
			Method = "POST",
			BodyStyle = WebMessageBodyStyle.Wrapped,
			RequestFormat = WebMessageFormat.Json)]
		string MarkFormula(List<TopicManagement> parameter);
	}

	/// <summary>
	///
	/// </summary>
	[DataContract]
	public class TopicManagement
	{
		private string _id;
		private string _number;

		/// <summary>
		///
		/// </summary>
		[DataMember(Name = "id")]
		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		///
		/// </summary>
		[DataMember(Name = "number")]
		public string Number
		{
			get { return _number; }
			set { _number = value; }
		}
	}
}