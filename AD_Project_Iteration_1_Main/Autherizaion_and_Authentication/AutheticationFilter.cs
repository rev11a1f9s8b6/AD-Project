using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AD_Project_Iteration_1_Main.Autherizaion_and_Authentication
{
    public class AuthenticationFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string SessionId = HttpContext.Current.Request["sessionId"];
            if (SessionId ==null || LoginServices.GetUserBySessionId(SessionId)==null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                                {"controller","Login" },
                                {"action","Logout" }
                        }
                ); //function ends here
            }
        }
    }
}