// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using RoleAndManagement.Repositories;
// using RoleAndManagement.Models;
// using RoleAndManagement.Data;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using RoleAndManagement.Contants;
// using RoleAndManagement.ViewModels;
// using Microsoft.AspNetCore.Authorization;


// namespace RoleAndManagement.Controllers
// {
//     // [Route("[controller]")]
//     [Authorize]
//     public class JobPostingsController : Controller
//     {
//         // private readonly ILogger<JobPostingsController> _logger;

//         // public JobPostingsController(ILogger<JobPostingsController> logger)
//         // {
//         //     _logger = logger;
//         // }

//         private readonly IRepository<JobPosting> _repository;
//         private readonly UserManager<IdentityUser> _userManager;

//         public JobPostingsController(IRepository<JobPosting> repository, UserManager<IdentityUser> userManager)
//         {
//             _repository = repository;
//             _userManager = userManager;
//         }

//         [AllowAnonymous] //允许匿名
//         public async Task<IActionResult> Index()
//         {
//             if(User.IsInRole(Roles.Employer))
//             {
//                 var allJobPostings = await _repository.GetAllAsync();
//                 var userId = _userManager.GetUserId(User);
//                 var filteredJobPostings = allJobPostings.Where(jp => jp.UserId == userId);
//                 return View(filteredJobPostings);
//             }
//             var JobPostings = await _repository.GetAllAsync();
//             return View(JobPostings);
//         }

//         [Authorize(Roles = "Admin,Employer")]
//         public IActionResult Create()
//         {
//             return View();
//         }

//         [HttpPost]
//         public async Task<IActionResult> Create(JobPostingViewModel jobPostingVm)
//         {
//             if(ModelState.IsValid)
//             {
//                 // jobPosting.UsedId = _userManager.GetUserId(user);
//                 // await _repository.AddAsync(jobPosting);
//                 var jobPosting = new JobPosting
//                 {
//                     Title = jobPostingVm.Title,
//                     Classification = jobPostingVm.Classification,
//                     Description = jobPostingVm.Description,
//                     Company = jobPostingVm.Company,
//                     Location = jobPostingVm.Location,
//                     UserId = _userManager.GetUserId(User)
//                 };

//                 await _repository.AddAsync(jobPosting);
//                 return RedirectToAction(nameof(Index));
//             }
            
//             return View(jobPostingVm);
//         }

//         [HttpDelete]
//         [Authorize(Roles = "Admin,Employer")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             var jobPosting = await _repository.GetByIdAsync(id);

//             if(jobPosting == null)
//             {
//                 return NotFound();
//             }

//             var userId = _userManager.GetUserId(User);

//             if(User.IsInRole(Roles.Admin) == false && jobPosting.UserId != userId)
//             {
//                 return Forbid();
//             }

//             await _repository.DeleteAsync(id);


//             return Ok();
//         }

//         // EASY WAY!!!!!!!!!!!!!!!!!
//         // [HttpDelete]
//         // [Authorize(Roles = "Admin,Employer")]
//         // public async Task<IActionResult> DeleteEasy(int id)
//         // {
//         //     var jobPosting = await _repository.GetByIdAsync(id);

//         //     if(jobPosting == null)
//         //     {
//         //         return NotFound();
//         //     }

//         //     var usedId = _userManager.GetUserId(User);

//         //     if(User.IsInRole(Roles.Admin) == false && jobPosting.UsedId != UsedId)
//         //     {
//         //         return Forbid();
//         //     }

//         //     await _repository.DeleteAsync();


//         //     return Ok();
//         // }

//         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//         public IActionResult Error()
//         {
//             return View("Error!");
//         }
//     }
// }