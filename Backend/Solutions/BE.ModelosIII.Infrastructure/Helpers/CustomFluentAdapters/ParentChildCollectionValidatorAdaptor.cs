using System;
using System.Collections;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace BE.ModelosIII.Infrastructure.Helpers.CustomFluentAdapters
{
    public class ParentChildCollectionValidatorAdaptor<T, TCollectionElement> : NoopPropertyValidator
    {
        private readonly Func<T, IValidator<TCollectionElement>> _validatorFactory;
        public Func<object, bool> Predicate { get; set; }

        public ParentChildCollectionValidatorAdaptor(Func<T, IValidator<TCollectionElement>> validatorFactory)
        {
            this._validatorFactory = validatorFactory;
        }

        public override IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            if (context.Rule.Member == null)
            {
                throw new InvalidOperationException(string.Format("Nested validators can only be used with Member Expressions."));
            }

            var parent = (T)context.ParentContext.InstanceToValidate;
            var validator = _validatorFactory.Invoke(parent);

            var collection = context.PropertyValue as IEnumerable;

            if (collection == null)
            {
                yield break;
            }

            int count = 0;
            var predicate = Predicate ?? (x => true);
            foreach (var element in collection)
            {
                if (element == null || !(predicate(element)))
                {
                    count++;
                    continue;
                }

                var newContext = new ValidationContext(element, context.ParentContext.PropertyChain, context.ParentContext.Selector);
                newContext.PropertyChain.Add(context.Rule.Member);
                newContext.PropertyChain.AddIndexer(count++);

                var results = validator.Validate(newContext).Errors;

                foreach (var result in results)
                {
                    yield return result;
                }
            }
        }
    }
}