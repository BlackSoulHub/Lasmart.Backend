using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt.Common;
using Lasmart.DAL.Repositories.Interfaces;
using Lasmart.Services.Interfaces;
using Lastmart.Domain.DBModels;
using Lastmart.Domain.Exceptions;
using Lastmart.Domain.Models.Dto;
using Lastmart.Domain.Models.Errors;
using Lastmart.Domain.Requests.Dot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lasmart.Services.Implementation
{
    public class DotService : IDotService
    {
        private readonly IBaseRepository<DotModel> _dotRepository;

        public DotService(IBaseRepository<DotModel> dotRepository)
        {
            _dotRepository = dotRepository;
        }

        public async Task<Result<DotModel>> Create(CreateDotRequest data)
        {
            var validationErrors = new List<ValidationError>();

            if (data.X > 10000 || data.X < 0)
            {
                validationErrors.Add(new ValidationError
                {
                    Property = "X",
                    ErrorMessage = "X не может быть больше 10000 или меньше 0"
                });
            }
            
            if (data.Y > 10000 || data.Y < 0)
            {
                validationErrors.Add(new ValidationError
                {
                    Property = "Y",
                    ErrorMessage = "Y не может быть больше 10000 или меньше 0"
                });
            }

            if (data.Radius > 10 | data.Radius < 1)
            {
                validationErrors.Add(new ValidationError
                {
                    Property = "Radius",
                    ErrorMessage = "Радиус не может быть больше 10 или меньше 1"
                });
            }
            
            if (validationErrors.Any())
            {
                var validationException = new ValidationException(validationErrors);
                return new Result<DotModel>(validationException);
            }

            var newDotModel = new DotModel
            {
                X = data.X,
                Y = data.Y,
                Color = data.Color,
                Radius = data.Radius
            };

            var createResult = await _dotRepository.CreateAsync(newDotModel);

            return createResult.Match(createdDot => createdDot, exception =>
            {
                //_logger.LogError("Ошибка сохранения данных: {1}", exception.Message);
                return new Result<DotModel>(exception);
            });
        }

        public async Task<Result<DotModel>> Update(UpdateDotRequest data)
        {
            var foundedDot = await _dotRepository.GetByIdAsync(data.Id);
            if (foundedDot is null)
            {
                var notFoundException = new EntityNotFoundException();
                return new Result<DotModel>(notFoundException);
            }

            foundedDot.X = data.X;
            foundedDot.Y = data.Y;
            foundedDot.Radius = data.Radius;
            foundedDot.Color = data.Color;

            var updateResult = await _dotRepository.UpdateAsync(foundedDot);
            return updateResult.Match(updatedDot => updatedDot, exception => new Result<DotModel>(exception));
        }

        public async Task<Result<bool>> DeleteByIdAsync(int id)
        {
            var foundedDot = await _dotRepository.GetByIdAsync(id);
            if (foundedDot is null)
            {
                var notFoundException = new EntityNotFoundException();
                return new Result<bool>(notFoundException);
            }

            var deleteResult = await _dotRepository.Delete(foundedDot);
            return deleteResult.Match(ok => ok, exception => new Result<bool>(exception));
        }

        public async Task<IEnumerable<DotDto>> GetAllAsync()
        {
            return await _dotRepository.GetAll().Select(d => new DotDto
            {
                Id = d.Id,
                X = d.X,
                Y = d.Y,
                Radius = d.Radius,
                Color = d.Color,
                Comments = d.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Text = c.Text,
                    BackgroundColor = c.BackgroundColor
                })
            }).ToListAsync();
        }
    }
}