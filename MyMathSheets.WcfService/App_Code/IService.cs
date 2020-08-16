using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MyMathSheets.WcfService
{
	/// <summary>
	/// </summary>
	[ServiceContract]
	public interface IService
	{
		/// <summary>
		/// </summary>
		/// <param name="parameters"> </param>
		/// <returns> </returns>
		[OperationContract]
		[WebInvoke(UriTemplate = "/MarkFormula", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]
		string MarkFormula(TopicParamater parameters);
	}

	/// <summary>
	/// </summary>
	[DataContract]
	public class TopicParamater
	{
		/// <summary>
		/// </summary>
		[DataMember]
		public List<TopicManagement> TopicManagement;
	}

	/// <summary>
	/// </summary>
	[DataContract]
	public class TopicManagement
	{
		private string _id;
		private string _number;

		/// <summary>
		/// </summary>
		[DataMember(Order = 0)]
		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// </summary>
		[DataMember(Order = 1)]
		public string Number
		{
			get { return _number; }
			set { _number = value; }
		}
	}
}