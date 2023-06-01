using System.Threading.Tasks;
using LanguageExt.Common;
using Lastmart.Domain.DBModels;
using Lastmart.Domain.Requests.Comment;

namespace Lasmart.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Result<CommentModel>> Create(CreateCommentRequest data);
    }
}