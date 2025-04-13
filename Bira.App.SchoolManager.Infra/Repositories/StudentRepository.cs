using Bira.App.SchoolManager.Domain.Entities;
using Bira.App.SchoolManager.Domain.Interfaces.Repositories;
using Bira.App.SchoolManager.Infra.Repositories.BaseContext;

namespace Bira.App.SchoolManager.Infra.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApiDbContext _context) : base(_context) { }

    }
}