using System;
using System.Collections.Generic;
using Lastmart.Domain.Models.Errors;
using Lastmart.Domain.Responses.Error;

namespace Lastmart.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; protected set; }

        public ValidationException(IEnumerable<ValidationError> errors) : base("Ошибка валидации")
        {
            Errors = errors;
        }

        public ValidationErrorResponse ToResponse()
        {
            return new ValidationErrorResponse(Errors);
        }
    }
}