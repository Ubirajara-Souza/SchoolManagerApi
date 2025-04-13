
namespace Bira.App.SchoolManager.Domain.DTOs.Response
{
    public abstract class BaseResponse
    {
        public int Code { get; set; }
        public DateTime DateRegister { get; set; }
        public virtual DateTime? DateDeactivation { get; set; }
        public DateTime? DataUpdate { get; set; }
    }
}