using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Test.Api.Tracing
{
    public class LogRequestTimeFilterAttribute : ActionFilterAttribute
    {
        readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly ILogger<LogRequestTimeFilterAttribute> _logger;

        public LogRequestTimeFilterAttribute(ILogger<LogRequestTimeFilterAttribute> logger) {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context) => _stopwatch.Start();

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            string msg = $"{DateTime.Now} - {context.HttpContext.Request.GetDisplayUrl()} - {_stopwatch.ElapsedMilliseconds}ms";
            _logger.LogInformation(msg);
            File.AppendAllLines($"logs.log", new string[]{ msg } );
        }
    }
}