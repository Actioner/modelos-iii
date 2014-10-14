#region License
// Copyright (c) Jeremy Skinner (http://www.jeremyskinner.co.uk)
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://www.codeplex.com/FluentValidation
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Metadata;
using BE.ModelosIII.Mvc.Components.Validation.FluentApi.PropertyValidatorAdapters;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System.Web.Http.Validation;

namespace BE.ModelosIII.Mvc.Components.Validation.FluentApi {
    public delegate ModelValidator WebApiFluentValidationModelValidationFactory(IEnumerable<ModelValidatorProvider> providers, PropertyRule rule, IPropertyValidator validator);

	/// <summary>
	/// Implementation of ModelValidatorProvider that uses FluentValidation.
	/// </summary>
	public class WebApiFluentValidationModelValidatorProvider : ModelValidatorProvider {
		public bool AddImplicitRequiredValidator { get; set; }
		public IValidatorFactory ValidatorFactory { get; set; }

        private Dictionary<Type, WebApiFluentValidationModelValidationFactory> validatorFactories = new Dictionary<Type, WebApiFluentValidationModelValidationFactory>() {
			{ typeof(INotNullValidator), (providers, rule, validator) => new RequiredFluentValidationPropertyValidator(providers, rule, validator) },
			{ typeof(INotEmptyValidator), (providers, rule, validator) => new RequiredFluentValidationPropertyValidator(providers, rule, validator) },
			// email must come before regex.
			{ typeof(IEmailValidator), (providers, rule, validator) => new EmailFluentValidationPropertyValidator(providers, rule, validator) },			
			{ typeof(IRegularExpressionValidator), (providers, rule, validator) => new RegularExpressionFluentValidationPropertyValidator(providers, rule, validator) },
			{ typeof(ILengthValidator), (providers, rule, validator) => new StringLengthFluentValidationPropertyValidator(providers, rule, validator)},
			{ typeof(InclusiveBetweenValidator), (providers, rule, validator) => new RangeFluentValidationPropertyValidator(providers, rule, validator) },
			{ typeof(CreditCardValidator), (providers, description, validator) => new CreditCardFluentValidationPropertyValidator(providers, description, validator) }
		};

		public WebApiFluentValidationModelValidatorProvider(IValidatorFactory validatorFactory = null) {
			AddImplicitRequiredValidator = true;
			ValidatorFactory = validatorFactory ?? new AttributedValidatorFactory();
		}

		public void Add(Type validatorType, WebApiFluentValidationModelValidationFactory factory) {
			if(validatorType == null) throw new ArgumentNullException("validatorType");
			if(factory == null) throw new ArgumentNullException("factory");

			validatorFactories[validatorType] = factory;
		}

		public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> providers) {
			if (IsValidatingProperty(metadata)) {
				return GetValidatorsForProperty(metadata, providers, ValidatorFactory.GetValidator(metadata.ContainerType));
			}

            return GetValidatorsForModel(metadata, providers, ValidatorFactory.GetValidator(metadata.ModelType));
		}

        IEnumerable<ModelValidator> GetValidatorsForProperty(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> providers, IValidator validator)
        {
			var modelValidators = new List<ModelValidator>();

			if (validator != null) {
				var descriptor = validator.CreateDescriptor();

				var validatorsWithRules = from rule in descriptor.GetRulesForMember(metadata.PropertyName)
										  let propertyRule = (PropertyRule)rule
										  let validators = rule.Validators
										  where validators.Any()
										  from propertyValidator in validators
										  let modelValidatorForProperty = GetModelValidator(metadata, providers, propertyRule, propertyValidator)
										  where modelValidatorForProperty != null
										  select modelValidatorForProperty;
					
				modelValidators.AddRange(validatorsWithRules);
			}

			if(validator != null && AddImplicitRequiredValidator) {
				bool hasRequiredValidators = modelValidators.Any(x => x.IsRequired);

				//If the model is 'Required' then we assume it must have a NotNullValidator. 
				//This is consistent with the behaviour of the DataAnnotationsModelValidatorProvider
				//which silently adds a RequiredAttribute

				if(! hasRequiredValidators) {
					modelValidators.Add(CreateNotNullValidatorForProperty(providers));
				}
			}

			return modelValidators;
		}

        private ModelValidator GetModelValidator(ModelMetadata meta, IEnumerable<ModelValidatorProvider> providers, PropertyRule rule, IPropertyValidator propertyValidator)
        {
			var type = propertyValidator.GetType();
			
			var factory = validatorFactories
				.Where(x => x.Key.IsAssignableFrom(type))
				.Select(x => x.Value)
                .FirstOrDefault() ?? ((provs, description, validator) => new FluentValidationPropertyValidator(provs, description, validator));

			return factory(providers, rule, propertyValidator);
		}

        ModelValidator CreateNotNullValidatorForProperty(IEnumerable<ModelValidatorProvider> providers)
        {
            return new RequiredFluentValidationPropertyValidator(providers, null, new NotNullValidator());
		}



        IEnumerable<ModelValidator> GetValidatorsForModel(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> providers, IValidator validator)
        {
			if (validator != null) {
				yield return new WebApiFluentValidationModelValidator(providers, validator);
			}
		}

		bool IsValidatingProperty(ModelMetadata metadata) {
			return metadata.ContainerType != null && !string.IsNullOrEmpty(metadata.PropertyName);
		}
	}
}