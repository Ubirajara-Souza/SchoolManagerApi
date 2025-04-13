using Bira.App.SchoolManager.Domain.Package;

namespace Bira.App.SchoolManager.Domain.Entities
{
    public class School : EntityBase
    {
        public string Description { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}