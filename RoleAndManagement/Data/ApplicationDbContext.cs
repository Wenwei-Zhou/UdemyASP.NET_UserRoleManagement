using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoleAndManagement.Models;

namespace RoleAndManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<JobPosting> JobPostings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}

// 识别 DbSet<JobPosting>：告诉 EF Core 你想在数据库中创建一个与 JobPosting 类对应的表。
// 在运行 dotnet ef migrations add 时：EF 会把 JobPosting 类的属性转换成数据库表中的字段。
// 运行 dotnet ef database update 时：EF 会在数据库中创建或更新这张 JobPostings 表。