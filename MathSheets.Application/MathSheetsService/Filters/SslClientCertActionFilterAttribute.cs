﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyMathSheets.WebApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class SslClientCertActionFilterAttribute : ActionFilterAttribute
    {
        public List<string> AllowedThumbprints = new List<string>()
        {
            // Replace with the thumbprints the 3rd party
            // server will be presenting. You can make checks
            // more elaborate but always have thumbprint checking ...
            "0011223344556677889900112233445566778899",
            "1122334455667788990011223344556677889900"
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <exception cref="HttpResponseException"></exception>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (!AuthorizeRequest(request))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool AuthorizeRequest(HttpRequestMessage request)
        {
            var clientCertificate = request.GetClientCertificate();

            if (clientCertificate == null || AllowedThumbprints == null || AllowedThumbprints.Count < 1)
            {
                return false;
            }

            foreach (var thumbprint in AllowedThumbprints)
            {
                if (clientCertificate.Thumbprint != null && clientCertificate.Thumbprint.Equals(thumbprint, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}