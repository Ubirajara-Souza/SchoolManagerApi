using Bira.App.SchoolManager.Domain.DTOs.Request;
using Bira.App.SchoolManager.Domain.DTOs.Response;

namespace Bira.App.SchoolManager.Service.Interfaces
{
    public interface ISchoolService : IDisposable
    {
        Task Add(SchoolDto schoolDto);
        Task<SchoolResponse> GetById(int code);
        Task<IEnumerable<SchoolResponse>> GetAll(string description = null);
        Task Update(int code, SchoolDto schoolDto);
        Task Delete(int code);
    }
}