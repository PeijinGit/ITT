using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace API.Filters
{
    /// <summary>
    /// Acquire token in request headers and check whether login exceed expire time 60mins
    /// </summary>
    public class ISActionFitler : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var tokenStr = context.HttpContext.Request.Headers["Authorization"];
                var token = TokenHelper.UnlockToken(tokenStr);
                if (token.isExp)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    var jsonResult = new JsonResult(new { isSuccess = "err", content = "timeExp" });
                    context.Result = jsonResult;
                    return;
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
                var jsonResult = new JsonResult(new { isSuccess = "err", content = "nonToken" });
                context.Result = jsonResult;
            }
        }
    }
}
