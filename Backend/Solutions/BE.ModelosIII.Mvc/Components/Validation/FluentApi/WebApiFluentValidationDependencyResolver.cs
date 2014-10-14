using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using System.Web.Http.Validation;

namespace BE.ModelosIII.Mvc.Components.Validation.FluentApi
{
    public class WebApiFluentValidationDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyResolver defaultResolver;

        public WebApiFluentValidationDependencyResolver(IDependencyResolver defaultResolver)
        {
            this.defaultResolver = defaultResolver;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(ModelValidatorProvider))
            {
                return new WebApiFluentValidationModelValidatorProvider();
            }

            return defaultResolver == null ? null : defaultResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == typeof(ModelValidatorProvider))
            {
                return new List<object>() { new WebApiFluentValidationModelValidatorProvider() };
            }

            return defaultResolver == null ? new List<object>() : defaultResolver.GetServices(serviceType);
        }

        public void Dispose()
        {
        }
    }
}
