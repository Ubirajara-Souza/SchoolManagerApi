namespace Bira.App.SchoolManager.Domain.DTOs.Response
{
    public class StudentResponse : BaseResponse
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CPF { get; set; }
        public string CellPhone { get; set; }
        public AddressResponse Address { get; set; }
        public SchoolResponse School{ get; set; }
    }
}