namespace Lastmart.Domain.Models.Errors
{
    public class ValidationError
    {
        public string Property { get; set; }
        public string ErrorMessage { get; set; }
    }
}