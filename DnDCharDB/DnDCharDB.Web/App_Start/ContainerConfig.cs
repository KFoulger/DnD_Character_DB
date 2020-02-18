using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DnDCharDB.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DnDCharDB.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration http)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<InMemoryCharData>()
                   .As<ICharData>()
                   .SingleInstance();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            http.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}