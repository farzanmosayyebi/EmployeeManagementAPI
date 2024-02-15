using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation.Job;
using EmployeeManagement.Common.DTOs.Job;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Models;
using FluentValidation;
using System.Linq.Expressions;

namespace EmployeeManagement.Business.Services
{
    public class JobService : IJobService
    {
        private readonly IGenericRepository<Job> _jobRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;
        private readonly JobCreateValidator _createValidator;
        private readonly JobUpdateValidator _updateValidator;

        public JobService(IGenericRepository<Job> genericRepository, IGenericRepository<Employee> employeeRepository, IMapper mapper, JobCreateValidator createValidator, JobUpdateValidator updateValidator)
        {
            _jobRepository = genericRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<int> CreateJobAsync(JobCreate jobCreate)
        {
            await _createValidator.ValidateAndThrowAsync(jobCreate);

            var entity = _mapper.Map<Job>(jobCreate);

            await _jobRepository.InsertAsync(entity);
            await _jobRepository.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteJobAsync(JobDelete jobDelete)
        {
            Job entity = await _jobRepository.GetByIdAsync(jobDelete.Id, job => job.Employees)
                ?? throw new ItemNotFoundException(typeof(Job), jobDelete.Id);

            if (entity.Employees.Count > 0)
                throw new DependentEmployeesExistException(entity.Employees);

            _jobRepository.Delete(entity);
            await _jobRepository.SaveChangesAsync();
        }

        public async Task<List<JobGet>> GetAllJobsAsync()
        {
            var entityList = await _jobRepository.GetAsync(null, null);

            return _mapper.Map<List<JobGet>>(entityList);
        }

        public async Task<JobGet> GetJobAsync(int id)
        {
            var entity = await _jobRepository.GetByIdAsync(id)
                ?? throw new ItemNotFoundException(typeof(Job), id);

            return _mapper.Map<JobGet>(entity);
        }

        public async Task UpdateJobAsync(JobUpdate jobUpdate)
        {
            await _updateValidator.ValidateAndThrowAsync(jobUpdate);

            Job existingEntity = await _jobRepository.GetByIdAsync(jobUpdate.Id)
                ?? throw new ItemNotFoundException(typeof(Job), jobUpdate.Id);

            var entity = _mapper.Map<Job>(jobUpdate);

            _mapper.Map(entity, existingEntity);

            _jobRepository.Update(existingEntity);
            await _jobRepository.SaveChangesAsync();
        }
    }
}
