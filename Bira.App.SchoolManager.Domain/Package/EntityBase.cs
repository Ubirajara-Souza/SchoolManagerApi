using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bira.App.SchoolManager.Domain.Package
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            DateRegister = DateTime.Now;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public DateTime DateRegister { get; set; }
        public virtual DateTime? DateDeactivation { get; set; }
        public DateTime? DataUpdate { get; set; }
    }
}