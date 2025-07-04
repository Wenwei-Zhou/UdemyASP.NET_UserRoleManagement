using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoleAndManagement.Repositories;
using RoleAndManagement.Models;
using RoleAndManagement.Data;
using Microsoft.EntityFrameworkCore;
using RoleAndManagement.Contants;
using RoleAndManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RoleAndManagement.Controllers
{
    // [Route("[controller]")]
    [Authorize]
    public class MongoDBJobController : Controller
    {
        // private readonly ILogger<MongoDBJobController> _logger;

        // public MongoDBJobController(ILogger<MongoDBJobController> logger)
        // {
        //     _logger = logger;
        // }

        private readonly IRepository<JobPosting> _repository;

        private readonly User user = new User();

        

        public MongoDBJobController(IRepository<JobPosting> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous] //允许匿名
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if(currentUserRole == "Employer" || currentUserRole == "Admin")
            {
                var allJobPostings = await _repository.GetAllAsync();
                var userId = currentUserId;
                var filteredJoboPostings = allJobPostings.Where(jp => jp.UserId == userId);
                return View(filteredJoboPostings);
            }
            var JobPostings = await _repository.GetAllAsync();
            return View(JobPostings);
        }

        [Authorize(Roles = "Admin,Employer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobPostingViewModel jobPostingVm)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            
            if(currentUserName != null)
            {
                // jobPosting.UsedId = _userManager.GetUserId(user);
                // await _repository.AddAsync(jobPosting);
                var jobPosting = new JobPosting
                {
                    Title = jobPostingVm.Title,
                    Description = jobPostingVm.Description,
                    Company = jobPostingVm.Company,
                    Location = jobPostingVm.Location,
                    UserId = currentUserId
                };

                await _repository.AddAsync(jobPosting);
                return RedirectToAction(nameof(Index));
            }
            
            return View(jobPostingVm);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Employer")]
        [Route("MongoDB/DeleteEasy/{id}")]
        public async Task<IActionResult> DeleteEasy(int id)
        {
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var jobPosting = await _repository.GetByIdAsync(id);

            if(jobPosting == null)
            {
                return NotFound();
            }

            if(currentUserRole != "Admin" && jobPosting.UserId != currentUserId)
            {
                return Forbid();
            }

            await _repository.DeleteAsync(id);


            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}