using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoleAndManagement.Data;
using RoleAndManagement.Models;
using RoleAndManagement.Repositories;
using RoleAndManagement.Contants;

namespace MyWebApp.Tests
{
    public class JobPostingsRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingsRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("JobPostingDb")
            .Options;
        }

        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        [Fact]
        public void TestName()
        {
            // db context
        
            // job posting repository
        
            // job posting

            // execute

            // result

            // assert
        }

        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Location = "Test Location",
                UserId = "TestUserId"
            };

            await repository.AddAsync(jobPosting);

            // var result = db.JobPostings.SingleOrDefault(x => x.Title == "Test Title");
            var result = db.JobPostings.Find(jobPosting.Id);


            Assert.NotNull(result);
            Assert.Equal("Test Title", result.Title);
        }

        [Fact]
        public async Task GetByAsync_ShouldReturnJobPosting()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Location = "Test Location",
                UserId = "TestUserId"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            var result = await repository.GetByIdAsync(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal("Test Title", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => repository.GetByIdAsync(999)
            );
        }

        [Fact]
        public async Task AddAsync_ShouldReturnJobPosting()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting1 = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Location = "Test Location",
                UserId = "TestUserId"
            };

            var jobPosting2 = new JobPosting
            {
                Title = "Test Title2",
                Description = "Test Description 2",
                PostedDate = DateTime.Now,
                Location = "Test Location 2",
                UserId = "TestUserId 2"
            };

            await db.JobPostings.AddRangeAsync(jobPosting1, jobPosting2);
            await db.SaveChangesAsync();


            var result = await repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateJobPosting()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Location = "Test Location",
                UserId = "TestUserId"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            jobPosting.Description = "Updated Description";

            await repository.UpdateAsync(jobPosting);

            var result = db.JobPostings.Find(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated Description", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_ShouldUpdateJobPosting()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Location = "Test Location",
                UserId = "TestUserId"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            await repository.DeleteAsync(jobPosting.Id);

            var result = db.JobPostings.Find(jobPosting.Id);

            Assert.Null(result);
        }
    
    }
}