using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace BE.ModelosIII.Mvc.Components.DependencyInyector
{
    internal class ReleasingDependencyScope : IDependencyScope
    {
        private readonly List<object> _instances;
        private readonly Action<object> _release;
        private readonly IDependencyScope _scope;

        public ReleasingDependencyScope(IDependencyScope scope, Action<object> release)
        {
            if (scope == null)
            {
                throw new ArgumentNullException("scope");
            }

            if (release == null)
            {
                throw new ArgumentNullException("release");
            }

            this._scope = scope;
            this._release = release;
            this._instances = new List<object>();
        }

        public object GetService(Type t)
        {
            object service = this._scope.GetService(t);
            this.AddToScope(service);

            return service;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            var services = this._scope.GetServices(t);
            this.AddToScope(services);

            return services;
        }

        public void Dispose()
        {
            foreach (object instance in this._instances)
            {
                this._release(instance);
            }
        }

        private void AddToScope(params object[] services)
        {
            if (services.Any())
            {
                this._instances.AddRange(services);
            }
        }
    }
}