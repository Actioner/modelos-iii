using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using BE.ModelosIII.Mvc.Components.DependencyInyector;
using Castle.Windsor;

namespace BE.ModelosIII.Mvc.Components.Windsor
{
    internal class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        public object GetService(Type t)
        {
            return this.container.Kernel.HasComponent(t) ? this.container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return this.container.ResolveAll(t).Cast<object>().ToArray();
        }

        public IDependencyScope BeginScope()
        {
            return new ReleasingDependencyScope(this as IDependencyScope, this.container.Release);
        }

        public void Dispose()
        {
        }
    }
}