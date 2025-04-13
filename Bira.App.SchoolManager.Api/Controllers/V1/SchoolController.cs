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
    public class SchoolController : BaseController
    {
        private readonly ISchoolService _schoolService;
        public SchoolController(ISchoolService schoolService, INotifier notifier, IUser user) : base(notifier, user)
        {
            _schoolService = schoolService;
        }

        [ClaimsAuthorize("School", "Add")]
        [HttpPost]
        public async Task<ActionResult<SchoolDto>> AddRequest(SchoolDto requestDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _schoolService.Add(requestDto);

            return CustomResponse(requestDto);
        }

        [ClaimsAuthorize("School", "GetById")]
        [HttpGet("{code}")]
        public async Task<ActionResult<SchoolResponse>> GetById(int code)
        {
            var schoolResponse = await _schoolService.GetById(code);

            if (schoolResponse is null) return NotFound();
            return schoolResponse;
        }

        [ClaimsAuthorize("School", "GetAll")]
        [HttpGet]
        public async Task<ActionResult<SchoolResponse>> GetAll([FromQuery] string description = null)
            => CustomResponse(await _schoolService.GetAll(description));

        [ClaimsAuthorize("School", "Update")]
        [HttpPut]
        public async Task<ActionResult<SchoolDto>> Update([FromQuery] int code, SchoolDto requestDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _schoolService.Update(code, requestDto);

            return CustomResponse(requestDto);
        }

        [ClaimsAuthorize("School", "Delete")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int code)
        {
            await _schoolService.Delete(code);

            return CustomResponse();
        }
    }
}