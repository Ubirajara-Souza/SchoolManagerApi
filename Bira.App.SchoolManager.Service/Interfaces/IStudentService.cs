using Bira.App.SchoolManager.Domain.DTOs.Request;
using Bira.App.SchoolManager.Domain.DTOs.Response;

namespace Bira.App.SchoolManager.Service.Interfaces
{
    public interface IStudentService : IDisposable
    {
        Task Add(StudentDto studentDto);
        Task<StudentResponse> GetById(int code);
        Task<IEnumerable<StudentResponse>> GetAll(string name = null, string cpf = null);
        Task Update(int code, StudentDto studentDto);
        Task Delete(int code);
    }
}