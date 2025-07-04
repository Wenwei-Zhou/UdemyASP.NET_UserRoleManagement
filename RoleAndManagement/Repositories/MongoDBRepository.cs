using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using RoleAndManagement.Models;
using RoleAndManagement.Repositories;
using RoleAndManagement.Data;

namespace RoleAndManagement.Repositories
{
    public class MongoDBRepository : IRepository<JobPosting>
    {
        private readonly IMongoCollection<JobPosting> _jobs; // IMongoCollection<T> 是 MongoDB 集合（collection）的接口。

        public MongoDBRepository(IOptions<MongoDBContext> context) // IOptions<T> 是 ASP.NET Core 中用来读取配置文件appsettings.json
        {
            var client = new MongoClient(context.Value.ConnectionString);
            var database = client.GetDatabase(context.Value.DatabaseName);
            _jobs = database.GetCollection<JobPosting>(context.Value.JobCollection);
        }
        public async Task AddAsync(JobPosting entity)
        {
            await _jobs.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var jobPosting = await _jobs.Find(j => j.Id == id).FirstOrDefaultAsync();
            if(jobPosting == null)
            {
                throw new KeyNotFoundException();
            }

            await _jobs.DeleteOneAsync(j => j.Id == id);
        }

        public async Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            var filter = Builders<JobPosting>.Filter.Empty;
            return await _jobs.Find(filter).ToListAsync();
        }

        public async Task<JobPosting> GetByTypeAsync(string JobType)
        {
            var jobPosting = await _jobs.Find(j => j.Classification == JobType).FirstOrDefaultAsync();

            if(jobPosting == null)
            {
                throw new KeyNotFoundException();
            }

            return jobPosting;
        }

        public async Task<JobPosting> GetByIdAsync(int id)
        {
            var jobPosting = await _jobs.Find(j => j.Id == id).FirstOrDefaultAsync();

            if(jobPosting == null)
            {
                throw new KeyNotFoundException();
            }

            return jobPosting;
        }

        public async Task UpdateAsync(int id, JobPosting entity)
        {
            await _jobs.ReplaceOneAsync(j => j.Id == id, entity);
        }
    }
}