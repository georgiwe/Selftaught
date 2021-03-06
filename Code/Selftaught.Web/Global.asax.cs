﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Selftaught.Web.Infrastructure.ModelMapping;

namespace Selftaught.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new AutoMapperConfiguration(Assembly.GetExecutingAssembly()).Execute();
        }

        protected void Session_Start()
        {
            HttpContext.Current.Session.Add("language", "German");
        }
    }
}
