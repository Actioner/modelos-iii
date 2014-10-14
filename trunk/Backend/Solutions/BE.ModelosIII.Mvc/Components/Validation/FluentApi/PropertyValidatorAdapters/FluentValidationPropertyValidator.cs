using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Web.Http.Validation;
using System.Web.Http.Metadata;

namespace BE.ModelosIII.Mvc.Components.Validation.FluentApi.PropertyValidatorAdapters {
    public class FluentValidationPropertyValidator : ModelValidator {
		public IPropertyValidator Validator { get; private set; }
		public PropertyRule Rule { get; private set; }

		public FluentValidationPropertyValidator(IEnumerable<ModelValidatorProvider> providers, PropertyRule rule, IPropertyValidator validator)
            : base(providers)
        {
			this.Validator = validator;

            Rule = rule;
		}

        public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
        {
            var fakeRule = new PropertyRule(Rule == null ? null : Rule.Member, x => metadata.Model, null, null, metadata.ModelType, null)
            {
                PropertyName = metadata.PropertyName,
                DisplayName = Rule == null ? null : Rule.DisplayName,
                RuleSet = Rule == null ? null : Rule.RuleSet,
            };

            var fakeParentContext = new ValidationContext(container);
            var context = new PropertyValidatorContext(fakeParentContext, fakeRule, metadata.PropertyName);
            var result = Validator.Validate(context);

            foreach (var failure in result)
            {
                yield return new ModelValidationResult { Message = failure.ErrorMessage };
            }
        }
	}
}