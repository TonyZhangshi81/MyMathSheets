using System;

namespace MyMathSheets.WebApi.Filters.Swagger
{
    /// <summary>
    /// 指定待調試API的Head中使用token時使用此標籤
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ApiAuthorizeAttribute : Attribute
    {
    }
}