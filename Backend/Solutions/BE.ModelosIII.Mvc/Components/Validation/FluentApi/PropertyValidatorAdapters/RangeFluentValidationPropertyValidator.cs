﻿using System.Collections.Generic;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Web.Http.Validation;

namespace BE.ModelosIII.Mvc.Components.Validation.FluentApi.PropertyValidatorAdapters {
    internal class RangeFluentValidationPropertyValidator : FluentValidationPropertyValidator {
		public RangeFluentValidationPropertyValidator(IEnumerable<ModelValidatorProvider> providers, PropertyRule rule, IPropertyValidator validator)
            : base(providers, rule, validator)
        {
		}
	}
}