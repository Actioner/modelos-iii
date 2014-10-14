using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Metadata;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using System.Web.Http.Validation;

namespace BE.ModelosIII.Mvc.Components.Validation.FluentApi {
    /// <summary>
	/// ModelValidator implementation that uses FluentValidation.
	/// </summary>
	internal class WebApiFluentValidationModelValidator : ModelValidator {
		private readonly IValidator validator;
        public WebApiFluentValidationModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders, IValidator validator)
            : base(validatorProviders)
        {
            this.validator = validator;
        }

		public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container) {
            if (metadata.Model != null)
            {
                var selector = new DefaultValidatorSelector();
				var context = new ValidationContext(metadata.Model, new PropertyChain(), selector);

				var result = validator.Validate(context);

				if (!result.IsValid) {
					return ConvertValidationResultToModelValidationResults(result);
				}
			}
			return Enumerable.Empty<ModelValidationResult>();
		}

		protected virtual IEnumerable<ModelValidationResult> ConvertValidationResultToModelValidationResults(ValidationResult result) {
			return result.Errors.Select(x => new ModelValidationResult {
				MemberName = x.PropertyName,
				Message = x.ErrorMessage
			});
		}
	}
}