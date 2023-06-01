using System.Threading.Tasks;
using Lasmart.Services.Interfaces;
using Lastmart.Domain.Exceptions;
using Lastmart.Domain.Requests.Comment;
using Lastmart.Domain.Responses.Error;
using Microsoft.AspNetCore.Mvc;

namespace Lastmart.WebApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController
    {
        private readonly ICommentService _commentService;
        
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody] CreateCommentRequest input)
        {
            var createCommentResult = await _commentService.Create(input);
            return createCommentResult.Match<ActionResult>(createdComment => new JsonResult(createdComment), exception =>
            {
                if (exception is ValidationException validationException)
                {
                    return new JsonResult(validationException.ToResponse())
                    {
                        StatusCode = 400
                    };
                }

                if (exception is EntityNotFoundException)
                {
                    return new JsonResult(new BaseErrorResponse("Точка с заданным Id не была найдена"))
                    {
                        StatusCode = 404
                    };
                }

                return new JsonResult(BaseErrorResponse.ServerErrorResponse());
            });
        }
    }
}