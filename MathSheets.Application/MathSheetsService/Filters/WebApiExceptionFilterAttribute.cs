﻿using MyMathSheets.CommonLib.Message;
using MyMathSheets.WebApi.Models.Response;
using MyMathSheets.WebApi.Properties;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http.Filters;

namespace MyMathSheets.WebApi.Filters
{
    /// <summary>
    /// WebApi異常篩選器
    /// </summary>
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly Common.Logging.ILog Log = Common.Logging.LogManager.GetLogger<WebApiExceptionFilterAttribute>();

        /// <summary>
        /// 異常處理
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Log.Error(MessageUtil.GetMessage(() => MsgResources.E0001A), actionExecutedContext.Exception);

            var messages = new List<string>
            {
                actionExecutedContext.Exception.Message
            };
            if (actionExecutedContext.Exception is NotImplementedException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotImplemented, new TopicRes
                {
                    Messages = messages
                });
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.RequestTimeout, new TopicRes
                {
                    Messages = messages
                });
            }
            else if (actionExecutedContext.Exception is AuthenticationException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new TopicRes
                {
                    Messages = messages
                });
            }
            else
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new TopicRes
                {
                    Messages = messages
                });
            }

            base.OnException(actionExecutedContext);
        }
    }
}