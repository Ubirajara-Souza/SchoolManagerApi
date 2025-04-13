using Bira.App.SchoolManager.Domain.Package;

namespace Bira.App.SchoolManager.Domain.Entities
{
    public class Address : EntityBase
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}