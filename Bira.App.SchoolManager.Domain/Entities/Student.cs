using Bira.App.SchoolManager.Domain.Package;

namespace Bira.App.SchoolManager.Domain.Entities
{
    public class Student : EntityBase
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CPF { get; set; }
        public string CellPhone { get; set; }
        public int CodeSchool { get; set; }
        public int CodeAddress { get; set; }

        public virtual School School { get; set; }
        public virtual Address Address { get; set; }
    }
}