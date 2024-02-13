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
            var entity = await _jobRepository.GetByIdAsync(jobDelete.Id);

            if (entity == null)
                throw new JobNotFoundException(jobDelete.Id);

            Expression<Func<Employee, bool>> employeesWithThisJob = employee => employee.Job.Id == jobDelete.Id;
            List<Employee> dependentEmployees = await _employeeRepository.GetFilteredAsync([employeesWithThisJob], null, null);
            if (dependentEmployees.Count > 0)
                throw new DependentEmployeesExistException(dependentEmployees);

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
            var entity = await _jobRepository.GetByIdAsync(id);
            
            if (entity == null)
                throw new JobNotFoundException(id);

            return _mapper.Map<JobGet>(entity);
        }

        public async Task UpdateJobAsync(JobUpdate jobUpdate)
        {
            await _updateValidator.ValidateAndThrowAsync(jobUpdate);

            var existingEntity = await _jobRepository.GetByIdAsync(jobUpdate.Id);
            if (existingEntity == null)
                throw new JobNotFoundException(jobUpdate.Id);

            var entity = _mapper.Map<Job>(jobUpdate);

            _jobRepository.Update(entity);
            await _jobRepository.SaveChangesAsync();
        }
    }
}
