using System.Linq;
using System.Threading.Tasks;
using Lasmart.Services.Interfaces;
using Lastmart.Domain.Exceptions;
using Lastmart.Domain.Requests.Dot;
using Lastmart.Domain.Responses.Error;
using Microsoft.AspNetCore.Mvc;

namespace Lastmart.WebApi.Controllers
{
    [ApiController]
    [Route("api/dot")]
    public class DotController
    {
        private readonly IDotService _dotService;

        public DotController(IDotService dotService)
        {
            _dotService = dotService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDot([FromBody] CreateDotRequest input)
        {
            var createResult = await _dotService.Create(input);

            return createResult.Match<ActionResult>(createdDot => new JsonResult(createdDot)
            {
                StatusCode = 200
            }, exception =>
            {
                if (exception is ValidationException validationException)
                {
                    return new JsonResult(validationException.ToResponse())
                    {
                        StatusCode = 400
                    };
                }

                return new JsonResult(BaseErrorResponse.ServerErrorResponse())
                {
                    StatusCode = 500
                };
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteDot([FromRoute] int id)
        {
            var deleteResult = await _dotService.DeleteByIdAsync(id);
            return deleteResult.Match<ActionResult>(ok =>
            {
                if (!ok)
                {
                    return new JsonResult(BaseErrorResponse.ServerErrorResponse())
                    {
                        StatusCode = 500
                    };
                }

                return new OkResult();
            }, _ =>
            {
                return new JsonResult(BaseErrorResponse.ServerErrorResponse())
                {
                    StatusCode = 500
                };
            });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDots()
        {
            var dots = await _dotService.GetAllAsync();
            return new JsonResult(dots);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDot([FromBody] UpdateDotRequest input)
        {
            var updateResult = await _dotService.Update(input);
            return updateResult.Match<ActionResult>(updatedDot => new JsonResult(updatedDot), exception =>
            {
                if (exception is EntityNotFoundException)
                {
                    return new JsonResult(new BaseErrorResponse("Сущность не найдена"))
                    {
                        StatusCode = 404
                    };
                }

                return new JsonResult(BaseErrorResponse.ServerErrorResponse())
                {
                    StatusCode = 500
                };
            });
        }
    }
}