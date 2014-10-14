using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;

namespace BE.ModelosIII.Infrastructure.Helpers.CustomFluentAdapters
{
    public static class ParentChildCollectionValidatorAdaptorExtensions
    {
        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement> SetCollectionValidator<T, TCollectionElement>(this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder, Func<T, IValidator<TCollectionElement>> validatorFactory)
        {
            var adaptor = new ParentChildCollectionValidatorAdaptor<T, TCollectionElement>(validatorFactory);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        private class CollectionValidatorRuleBuilder<T, TCollectionElement> : CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
        {
            private readonly IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder;
            private readonly ParentChildCollectionValidatorAdaptor<T, TCollectionElement> adaptor;

            public CollectionValidatorRuleBuilder(IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder, ParentChildCollectionValidatorAdaptor<T, TCollectionElement> adaptor)
            {
                this.ruleBuilder = ruleBuilder;
                this.adaptor = adaptor;
            }

            public IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> SetValidator(IPropertyValidator validator)
            {
                return ruleBuilder.SetValidator(validator);
            }

            public IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> SetValidator(IValidator validator)
            {
                return ruleBuilder.SetValidator(validator);
            }

            public IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> SetValidator(IValidator<IEnumerable<TCollectionElement>> validator)
            {
                return ruleBuilder.SetValidator(validator);
            }

            public IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> Configure(Action<PropertyRule> configurator)
            {
                return ((IRuleBuilderOptions<T, IEnumerable<TCollectionElement>>)ruleBuilder).Configure(configurator);
            }

            public CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement> Where(Func<TCollectionElement, bool> predicate)
            {
                if (predicate == null) throw new ArgumentNullException("predicate", "Cannot pass null to Where");
                adaptor.Predicate = x => predicate((TCollectionElement)x);
                return this;
            }
        }
    }
}