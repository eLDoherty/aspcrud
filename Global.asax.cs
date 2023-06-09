﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace learnnet
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Backup_Application_BeginRequest(Object sender, EventArgs e)
        {
            // Allow https pages in debugging
            if (Request.IsLocal)
            {
                if (Request.Url.Scheme == "http")
                {
                    int localSslPort = 3000; // Your local IIS port for HTTPS

                    var path = "https://" + Request.Url.Host + ":" + localSslPort + Request.Url.PathAndQuery;

                    Response.Status = "301 Moved Permanently";
                    Response.AddHeader("Location", path);
                }
            }
            else
            {
                switch (Request.Url.Scheme)
                {
                    case "https":
                        Response.AddHeader("Strict-Transport-Security", "max-age=31536000");
                        break;
                    case "http":
                        var path = "https://" + Request.Url.Host + Request.Url.PathAndQuery;
                        Response.Status = "301 Moved Permanently";
                        Response.AddHeader("Location", path);
                        break;
                }
            }
        }
    }
}


