using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SteamKiller.DPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Infrastructure.Filters
{
    public class AccountIdValidatorFilter : Attribute, IActionFilter
    {
        bool isDevelopment = false;

        public AccountIdValidatorFilter(IHostingEnvironment env)
        {
            isDevelopment = env.IsDevelopment();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (isDevelopment)
                return;
            
            string requestId = GetIdFromRequest(context, new string[] { "accId", "id" });
            string jwtId = GetIdFromClaims(context.HttpContext.User, new string[] { "Id" });

            if(requestId != jwtId)
                context.Result = new JsonResult(new FailedStatus("Can't get this resource, because it's not your id!"));
        }

        private string GetIdFromRequest(ActionExecutingContext context, string[] queryParamVariants)
        {
            string result;

            foreach (var c in queryParamVariants)
            {
                result = context.RouteData.Values[c].ToString();

                if (result != null)
                    return result;
            }

            foreach (var c in queryParamVariants)
            {
                result = context.HttpContext.Request.Query[c].FirstOrDefault();

                if (result != null)
                    return result;
            }

            return null;
        }

        private string GetIdFromClaims(ClaimsPrincipal claims, string[] queryParamVariants)
        {
            string result;

            foreach (var c in queryParamVariants)
            {
                result = claims.FindFirstValue(c);

                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
