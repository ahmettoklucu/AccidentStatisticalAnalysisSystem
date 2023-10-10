using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Utilities
{
    public static class ValidationTool
    {
        public static void Validate<T>(AbstractValidator<T> validator, T entity)
        {
            var result = validator.Validate(entity);
            if (result.Errors.Count > 0)
            {
               throw new ValidationException(result.Errors);
            }
        }
    }
}
