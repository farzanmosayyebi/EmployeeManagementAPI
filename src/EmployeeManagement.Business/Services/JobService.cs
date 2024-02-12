using AutoMapper;
using EmployeeManagement.Business.Validation.Job;
using EmployeeManagement.Common.DTOs.Job;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Models;
using FluentValidation;

namespace EmployeeManagement.Business.Services
{
    public class JobService : IJobService
    {
        private readonly IGenericRepository<Job> _repository;
        private readonly IMapper _mapper;
        private readonly JobCreateValidator _createValidator;
        private readonly JobUpdateValidator _updateValidator;

        public JobService(IGenericRepository<Job> genericRepository, IMapper mapper, JobCreateValidator createValidator, JobUpdateValidator updateValidator)
        {
            _repository = genericRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<int> CreateJobAsync(JobCreate jobCreate)
        {
            await _createValidator.ValidateAndThrowAsync(jobCreate);

            var entity = _mapper.Map<Job>(jobCreate);

            await _repository.InsertAsync(entity);
            await _repository.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteJobAsync(JobDelete jobDelete)
        {
            var entity = await _repository.GetByIdAsync(jobDelete.Id);
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<JobGet>> GetAllJobsAsync()
        {
            var entityList = await _repository.GetAsync(null, null);

            return _mapper.Map<List<JobGet>>(entityList);
        }

        public async Task<JobGet> GetJobAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return _mapper.Map<JobGet>(entity);
        }

        public async Task UpdateJobAsync(JobUpdate jobUpdate)
        {
            await _updateValidator.ValidateAndThrowAsync(jobUpdate);

            var entity = _mapper.Map<Job>(jobUpdate);

            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }
    }
}
