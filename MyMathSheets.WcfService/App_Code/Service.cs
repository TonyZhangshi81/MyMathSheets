using System.Collections.Generic;
using System.ServiceModel.Activation;

namespace MyMathSheets.WcfService
{
	/// <summary>
	/// </summary>
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class Service : IService
	{
		/// <summary>
		/// </summary>
		/// <param name="parameter"> </param>
		/// <returns> </returns>
		public string MarkFormula(TopicParamater parameter)
		{
			return "http://localhost:8080/AppData/Work/Page/20200814205554998.html";
		}
	}
}