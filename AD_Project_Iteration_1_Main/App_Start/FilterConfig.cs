﻿using System.Web;
using System.Web.Mvc;

namespace AD_Project_Iteration_1_Main
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
