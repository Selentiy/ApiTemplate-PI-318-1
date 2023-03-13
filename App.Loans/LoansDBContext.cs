using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using App.Configuration;
using App.Loans.Models;

namespace App.Loans
{
    public class LoansDBContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }

        public LoansDBContext(DbContextOptions<LoansDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder){}
    }
}
