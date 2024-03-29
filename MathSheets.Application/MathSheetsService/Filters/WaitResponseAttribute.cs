﻿using MyMathSheets.CommonLib.Configurations;
using System;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyMathSheets.WebApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class WaitResponseAttribute : ActionFilterAttribute
    {
        private readonly int _waitTime;

        /// <summary>
        ///
        /// </summary>
        /// <param name="waitTime"></param>
        public WaitResponseAttribute(int waitTime)
        {
            _waitTime = waitTime;
        }

        /// <summary>
        ///
        /// </summary>
        public WaitResponseAttribute()
        {
            _waitTime = Convert.ToInt32(ConfigurationUtil.GetKeyValue("responsewaittime"));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (_waitTime != 0)
            {
                Thread.Sleep(_waitTime);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}