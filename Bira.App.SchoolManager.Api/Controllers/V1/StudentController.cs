using Bira.App.SchoolManager.Domain.DTOs.Request;
using Bira.App.SchoolManager.Domain.DTOs.Response;
using Bira.App.SchoolManager.Domain.Extensions;
using Bira.App.SchoolManager.Domain.Interfaces;
using Bira.App.SchoolManager.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bira.App.SchoolManager.Api.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService, INotifier notifier, IUser user) : base(notifier, user)
        {
            _studentService = studentService;
        }
        [ClaimsAuthorize("Student", "Add")]
        [HttpPost]
        public async Task<ActionResult<StudentDto>> AddRequest(StudentDto requestDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _studentService.Add(requestDto);

            return CustomResponse(requestDto);
        }

        [ClaimsAuthorize("Student", "GetById")]
        [HttpGet("{code}")]
        public async Task<ActionResult<StudentResponse>> GetById(int code)
        {
            var studentResponse = await _studentService.GetById(code);

            if (studentResponse is null) return NotFound();
            return studentResponse;
        }

        [ClaimsAuthorize("Student", "GetAll")]
        [HttpGet]
        public async Task<ActionResult<StudentResponse>> GetAll([FromQuery] string name = null, [FromQuery] string cpf = null)
            => CustomResponse(await _studentService.GetAll(name, cpf));

        [ClaimsAuthorize("Student", "Update")]
        [HttpPut]
        public async Task<ActionResult<StudentDto>> Update([FromQuery] int code, StudentDto requestDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _studentService.Update(code, requestDto);

            return CustomResponse(requestDto);
        }

        [ClaimsAuthorize("Student", "Delete")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int code)
        {
            await _studentService.Delete(code);

            return CustomResponse();
        }
    }
}