using AutoMapper;
using Bira.App.SchoolManager.Application.Validators;
using Bira.App.SchoolManager.Domain.DTOs.Request;
using Bira.App.SchoolManager.Domain.DTOs.Response;
using Bira.App.SchoolManager.Domain.Entities;
using Bira.App.SchoolManager.Domain.Interfaces.Repositories;
using Bira.App.SchoolManager.Service.Interfaces;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Bira.App.SchoolManager.Service.Services
{
    public class SchoolService : BaseService, ISchoolService
    {
        private readonly IMapper _mapper;
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService
        (
            ISchoolRepository schoolRepository,
            INotifier notifier,
            IMapper mapper
        ) : base(notifier)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }
        public async Task Add(SchoolDto schoolDto)
        {
            var request = _mapper.Map<School>(schoolDto);

            if (!RunValidation(new SchoolValidators(), request))
                return;

            await _schoolRepository.Add(request);
        }
        public async Task<SchoolResponse> GetById(int code)
        {
            var school = await _schoolRepository.GetById(code);

            if (school is null)
            {
                Notify("Escola não encontrada.");
                return null;
            }

            return _mapper.Map<SchoolResponse>(school);
        }
        public async Task<IEnumerable<SchoolResponse>> GetAll(string description = null)
        {
            Expression<Func<School, bool>> predicate = null;

            if (!string.IsNullOrEmpty(description))
            {
                var descriptionLower = description.ToLower();
                Expression<Func<School, bool>> filter = x => x.Description.ToLower().Contains(descriptionLower);
                predicate = filter;
            }

            var schools = await _schoolRepository.GetAll(predicate);
            return _mapper.Map<IEnumerable<SchoolResponse>>(schools);
        }
        public async Task Update(int code, SchoolDto schoolDto)
        {
            var school = await _schoolRepository.GetById(code);

            if (school is null)
            {
                Notify("Escola não encontrada.");
                return;
            }

            _mapper.Map(schoolDto, school);

            school.DataUpdate = DateTime.Now;

            if (!RunValidation(new SchoolValidators(), school))
                return;

            await _schoolRepository.Update(school);
        }
        public async Task Delete(int code)
        {
            var school = await _schoolRepository.GetById(code);

            if (school is null)
            {
                Notify("Escola não encontrada.");
                return;
            }

            school.DateDeactivation = DateTime.Now;
            await _schoolRepository.Update(school);
        }
        public void Dispose()
        {
            _schoolRepository.Dispose();
        }
    }
}