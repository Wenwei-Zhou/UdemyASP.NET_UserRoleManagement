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

        public MongoDBJobController(IRepository<JobPosting> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous] //允许匿名
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole(Roles.Employer))
            {
                var allJobPostings = await _repository.GetAllAsync();
                var userId = _userManager.GetUserId(User);
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
            if(ModelState.IsValid)
            {
                // jobPosting.UsedId = _userManager.GetUserId(user);
                // await _repository.AddAsync(jobPosting);
                var jobPosting = new JobPosting
                {
                    Title = jobPostingVm.Title,
                    Description = jobPostingVm.Description,
                    Company = jobPostingVm.Company,
                    Location = jobPostingVm.Location,
                    UserId = _userManager.GetUserId(User)
                };

                await _repository.AddAsync(jobPosting);
                return RedirectToAction(nameof(Index));
            }
            
            return View(jobPostingVm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}