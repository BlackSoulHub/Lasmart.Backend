using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt.Common;
using Lasmart.DAL.Repositories.Interfaces;
using Lasmart.Services.Interfaces;
using Lastmart.Domain.DBModels;
using Lastmart.Domain.Exceptions;
using Lastmart.Domain.Models.Errors;
using Lastmart.Domain.Requests.Comment;

namespace Lasmart.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly IBaseRepository<DotModel> _dotRepository;
        private readonly IBaseRepository<CommentModel> _commentRepository;

        public CommentService(IBaseRepository<DotModel> dotRepository, IBaseRepository<CommentModel> commentRepository)
        {
            _dotRepository = dotRepository;
            _commentRepository = commentRepository;
        }

        public async Task<Result<CommentModel>> Create(CreateCommentRequest data)
        {
            var validationErrors = new List<ValidationError>();

            if (data.Text.Length > 20)
            {
                validationErrors.Add(new ValidationError
                {
                    Property = "Text",
                    ErrorMessage = "Длинна комментария не может быть больше 20 символов"
                });
            }

            if (validationErrors.Any())
            {
                var validationException = new ValidationException(validationErrors);
                return new Result<CommentModel>(validationException);
            }
            
            var foundedDot = await _dotRepository.GetByIdAsync(data.DotId);
            if (foundedDot is null)
            {
                var notFoundException = new EntityNotFoundException();
                return new Result<CommentModel>(notFoundException);
            }

            var newComment = new CommentModel
            {
                Text = data.Text,
                BackgroundColor = data.BackgroundColor,
                Dot = foundedDot
            };

            var createResult = await _commentRepository.CreateAsync(newComment);
            return createResult.Match(createdComment => createdComment, exception => new Result<CommentModel>(exception));
        }
    }
}