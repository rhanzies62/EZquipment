using Autofac;
using Autofac.Integration.Mvc;
using Ems.Data;
using Ems.Domain.Services;
using Ems.ExternalServices;
using Ems.ExternalServices.Interface;
using Ems.Services;
using Ems.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Web.App_Start
{
    public class DependencyResolutionConfig
    {
        public static void Init()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            DependencyResolutionConfig.RegisterServices(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerRequest();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerRequest();
        }
    }
}