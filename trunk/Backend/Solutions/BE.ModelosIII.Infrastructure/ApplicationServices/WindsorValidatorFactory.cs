using System;
using Castle.MicroKernel;
using FluentValidation;

namespace BE.ModelosIII.Infrastructure.ApplicationServices
{
    public class WindsorValidatorFactory : ValidatorFactoryBase
    {
        private readonly IKernel _container;

        public WindsorValidatorFactory(IKernel container)
        {
            this._container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (_container.HasComponent(validatorType))
                return _container.Resolve(validatorType) as IValidator;
            return null;
        }
    }
}
