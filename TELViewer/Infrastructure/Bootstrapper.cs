using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NHibernate;
using TELViewer.Core.Repositories;
using TELViewer.Core.Repositories.Impl;

namespace TELViewer.Infrastructure
{
    public class Bootstrapper
    {
        public static IWindsorContainer CreateContainer()
        {
            IWindsorContainer container = new WindsorContainer();

            container
                .Register(Component.For<ISessionFactory>()
                              .LifestyleSingleton()
                              .UsingFactoryMethod(new NHibernateConfigurator().CreateSessionFactory))
                 .Register(Component.For<ISession>()
                              .LifestylePerWebRequest()
                              .UsingFactoryMethod(kernel => kernel.Resolve<ISessionFactory>().OpenSession()));

            container.Install(FromAssembly.This());
            container.Register(Component.For<ILogRepository>().ImplementedBy<LogRepository>().LifestyleTransient());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));

            return container;
        }
    }
}