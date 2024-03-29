﻿using DogShow.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DogShow.Models;

namespace DogShow.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<DogShow.Models.DogCreateViewModel> DogCreateViewModel { get; set; }
        public DbSet<DogShow.Models.DogDetailsViewModel> DogDetailsViewModel { get; set; }
        public DbSet<DogShow.Models.DogAllViewModel> DogAllViewModel { get; set; }
    }
}
