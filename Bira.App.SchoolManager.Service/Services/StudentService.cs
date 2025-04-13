using AutoMapper;
using Bira.App.SchoolManager.Application.Validators;
using Bira.App.SchoolManager.Domain.DTOs.Request;
using Bira.App.SchoolManager.Domain.DTOs.Response;
using Bira.App.SchoolManager.Domain.Entities;
using Bira.App.SchoolManager.Domain.Interfaces.Repositories;
using Bira.App.SchoolManager.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq.Expressions;


namespace Bira.App.SchoolManager.Service.Services
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger _logger;

        public StudentService
        (
            IStudentRepository studentRepository,
            IAddressRepository addressRepository,
            INotifier notifier,
            IMapper mapper,
            ILogger<StudentService> logger
        ) : base(notifier)
        {
            _studentRepository = studentRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Add(StudentDto studentDto)
        {
            var cepConsultado = await ConsultarCepAsync(studentDto.Address);
            if (!cepConsultado)
            {
                Notify("CEP inválido ou não encontrado");
                return;
            }

            var request = _mapper.Map<Student>(studentDto);

            if (!RunValidation(new AddressValidators(), request.Address))
                return;

            await _addressRepository.Add(request.Address);

            request.CodeAddress = request.Address.Code;

            if (!RunValidation(new StudentValidators(), request)) return;

            await _studentRepository.Add(request);
        }
        public async Task<StudentResponse> GetById(int code)
        {
            var student = await _studentRepository.GetIncludeById(
                x => x.Code == code,
                x => x.Address,
                x => x.School
            );

            if (student is null)
            {
                Notify("Aluno não encontrado.");
                return null;
            }

            return _mapper.Map<StudentResponse>(student);
        }
        public async Task<IEnumerable<StudentResponse>> GetAll(string name = null, string cpf = null)
        {
            Expression<Func<Student, bool>> predicate = null;

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(cpf))
            {
                var nameLower = name.ToLower();
                Expression<Func<Student, bool>> filter = x => x.Name.ToLower().Contains(nameLower) && x.CPF.Contains(cpf);
                predicate = filter;
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var nameLower = name.ToLower();
                    Expression<Func<Student, bool>> nameFilter = x => x.Name.ToLower().Contains(nameLower);
                    predicate = nameFilter;
                }

                if (!string.IsNullOrEmpty(cpf))
                {
                    Expression<Func<Student, bool>> cpfFilter = x => x.CPF.Contains(cpf);
                    predicate = cpfFilter;
                }
            }

            var students = await _studentRepository.GetIncludeAll(
                predicate,
                x => x.Address,
                x => x.School
            );

            return _mapper.Map<IEnumerable<StudentResponse>>(students);
        }
        public async Task Update(int code, StudentDto studentDto)
        {
            var student = await _studentRepository.GetIncludeById(
                x => x.Code == code,
                x => x.Address,
                x => x.School
            );

            if (student is null)
            {
                Notify("Aluno não encontrado.");
                return;
            }

            _mapper.Map(studentDto, student);
            student.DataUpdate = DateTime.Now;
            student.Address.DataUpdate = DateTime.Now;

            if (!RunValidation(new StudentValidators(), student)
               || !RunValidation(new AddressValidators(), student.Address)) return;

            await _studentRepository.Update(student);
        }
        public async Task Delete(int code)
        {     
            var student = await _studentRepository.GetIncludeById(
                x => x.Code == code,
                x => x.Address,
                x => x.School
            );

            if (student is null)
            {
                Notify("Aluno não encontrado.");
                return;
            }

            student.DateDeactivation = DateTime.Now;
            await _studentRepository.Update(student);

            student.Address.DateDeactivation = DateTime.Now;
            await _addressRepository.Update(student.Address);
        }
        public void Dispose()
        {
            _studentRepository.Dispose();
        }
        private async Task<bool> ConsultarCepAsync(AddressDto addressDto)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"https://viacep.com.br/ws/{addressDto.ZipCode}/json/";

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var address = JsonConvert.DeserializeObject<ViaCepResponse>(content);

                    if (address is not null)
                    {
                        addressDto.Street = address.Logradouro;
                        addressDto.Neighborhood = address.Bairro;
                        addressDto.City = address.Localidade;
                        addressDto.State = address.Uf;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao consultar CEP: {ex.Message}");
            }

            return false;
        }
    }
}