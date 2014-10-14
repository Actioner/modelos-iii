using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BE.ModelosIII.Infrastructure.NHibernateMaps;
using BE.ModelosIII.Mvc.CastleWindsor;
using BE.ModelosIII.Mvc.Components.Validation;
using BE.ModelosIII.Mvc.Components.Validation.FluentApi;
using BE.ModelosIII.Mvc.Components.ViewEngine;
using BE.ModelosIII.Mvc.Controllers;
using BE.ModelosIII.Resources;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using FluentValidation;
using FluentValidation.Mvc;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Domain.Events;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.Castle;
using SharpArch.Web.Mvc.ModelBinder;
using log4net.Config;
using WindsorControllerFactory = BE.ModelosIII.Mvc.Components.Windsor.WindsorControllerFactory;
using WindsorDependencyResolver = BE.ModelosIII.Mvc.Components.Windsor.WindsorDependencyResolver;

namespace BE.ModelosIII.Mvc
{
    // BE.ModelosIII.Web.Mvc.CastleWindsor


    /// <summary>
    /// Represents the MVC Application
    /// </summary>
    /// <remarks>
    /// For instructions on enabling IIS6 or IIS7 classic mode, 
    /// visit http://go.microsoft.com/?LinkId=9394801
    /// </remarks>
    public class MvcApplication : System.Web.HttpApplication
    {
        private WebSessionStorage _webSessionStorage;
        private static IWindsorContainer _container;

        /// <summary>
        /// Due to issues on IIS7, the NHibernate initialization must occur in Init().
        /// But Init() may be invoked more than once; accordingly, we introduce a thread-safe
        /// mechanism to ensure it's only initialized once.
        /// See http://msdn.microsoft.com/en-us/magazine/cc188793.aspx for explanation details.
        /// </summary>
        public override void Init()
        {
            base.Init();
            this._webSessionStorage = new WebSessionStorage(this);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(this.InitialiseNHibernateSessions);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Useful for debugging
            var ex = this.Server.GetLastError();
            var serverEx = ex as ReflectionTypeLoadException;
            if (serverEx != null && serverEx.InnerException != null)
            {
                ex = serverEx.InnerException;
            }
            else
            {
                ex = ex.GetBaseException();
            }
            if (!ExceptionPolicy.HandleException(ex, "Exception Policy"))
            {
                // Redirecciono manualmente cuando HandleException indica que no hay que hacer un Rethrow para no perder la sesión
                Response.Redirect(
                    ((CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors")).DefaultRedirect, false);
                Server.ClearError();
            }
        }

        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new BackEndViewEngine());

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();
            DefaultModelBinder.ResourceClassKey = "DefaultModelBinderMessages";
            ValidatorOptions.ResourceProviderType = typeof(DefaultValidationMessages);

            var fluentValidationModelValidatorProvider = new FluentValidationModelValidatorProvider
                                                             {
                                                                 AddImplicitRequiredValidator = false,
                                                             };
            ModelValidatorProviders.Providers.Add(fluentValidationModelValidatorProvider);


            DataAnnotationsModelValidatorProvider.RegisterAdapter(
             typeof(RequiredAttribute),
             typeof(CustomRequiredAttributeAdapter));

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            _container = InitializeServiceLocator();
            var webApiFluentValidationModelValidatorProvider = new WebApiFluentValidationModelValidatorProvider(_container.Resolve<IValidatorFactory>())
                                                                   {
                                                                       AddImplicitRequiredValidator = false
                                                                   };
            GlobalConfiguration.Configuration.Services.Add(typeof(System.Web.Http.Validation.ModelValidatorProvider), webApiFluentValidationModelValidatorProvider);
            GlobalConfiguration.Configuration.DependencyResolver = new WebApiFluentValidationDependencyResolver(new WindsorDependencyResolver(_container));
            FluentValidationModelValidatorProvider.Configure(provider => provider.ValidatorFactory = _container.Resolve<IValidatorFactory>());

            this.InitializeAutoMapping();
        }

        private void InitializeAutoMapping()
        {
            Tasks.AutoMappingsCreator.CreateCommandMappings();
            AutoMappingsCreator.CreateViewModelMappings();
        }


        /// <summary>
        /// Instantiate the container and add all Controllers that derive from
        /// WindsorController to the container.  Also associate the Controller
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual IWindsorContainer InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));


            var windsorServiceLocator = new WindsorServiceLocator(container);
            DomainEvents.ServiceLocator = windsorServiceLocator;
            ServiceLocator.SetLocatorProvider(() => windsorServiceLocator);

            container.RegisterControllers(typeof(SecurityController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);
            container.Register(Classes.FromAssemblyContaining<SecurityController>().BasedOn<IHttpController>().LifestylePerWebRequest());
            return container;
        }

        private void InitialiseNHibernateSessions()
        {
            NHibernateSession.ConfigurationCache = new NHibernateConfigurationFileCache();

            NHibernateSession.Init(
                this._webSessionStorage,
                new[] { Server.MapPath("~/bin/BE.ModelosIII.Infrastructure.dll") },
                new AutoPersistenceModelGenerator().Generate(),
                Server.MapPath("~/NHibernate.config"));
        }
    }
}