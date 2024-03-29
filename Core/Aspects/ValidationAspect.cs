﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validations;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects
{
    public class ValidationAspect : MethodInterception // is a Attribute
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) // ör. ProductValidator
        {       // Defensive Coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu Bir Doğrulama Sınıfı Değil");
            }

            _validatorType = validatorType;
        }
        // - Neden sadece OnBefore override edildi?
        // - Validation(Doğrulama) metodun başında yapılır :)
        protected override void OnBefore(IInvocation invocation) 
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
