using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt.Common;
using Lastmart.Domain.DBModels;
using Lastmart.Domain.Models.Dto;
using Lastmart.Domain.Requests.Dot;

namespace Lasmart.Services.Interfaces
{
    public interface IDotService
    {
        Task<Result<DotModel>> Create(CreateDotRequest data);
        Task<Result<DotModel>> Update(UpdateDotRequest data);
        Task<Result<bool>> DeleteByIdAsync(int id);
        Task<IEnumerable<DotDto>> GetAllAsync();
    }
}