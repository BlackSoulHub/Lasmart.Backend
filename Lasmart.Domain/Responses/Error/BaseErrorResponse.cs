namespace Lastmart.Domain.Responses.Error
{
    public class BaseErrorResponse
    {
        public string Message { get; set; }

        public BaseErrorResponse(string message)
        {
            Message = message;
        }

        public static BaseErrorResponse ServerErrorResponse()
        {
            return new BaseErrorResponse("Внутренняя ошибка сервера");
        }
    }
}