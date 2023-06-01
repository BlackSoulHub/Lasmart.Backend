using System.Collections.Generic;
using Lastmart.Domain.Models.Errors;

namespace Lastmart.Domain.Responses.Error
{
    public class ValidationErrorResponse : BaseErrorResponse
    {
        public IEnumerable<ValidationError> Errors { get; set; }

        public ValidationErrorResponse(IEnumerable<ValidationError> errors) : base("Ошибка валидации данных")
        {
            Errors = errors;
        }
    }
}