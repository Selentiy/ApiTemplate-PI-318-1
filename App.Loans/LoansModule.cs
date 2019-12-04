using App.Configuration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using App.Loans.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace App.Loans
{
    /// <summary>
    /// Endpoint class for registering the module in the system. This class should be referenced in the main module directly
    /// </summary>
    public class LoansModule : IModule
    {
        /// <summary>
        /// This method initialize additional module dependencies, if it is not possible to use utility interfaces
        /// </summary>
        /// <param name="container"></param>
        public void Initialize(IWindsorContainer container)
        {
            RegisterDbContext(container);
        }

        private Loan[] loans = new Loan[5];

        IEnumerable<Loan> InitLoans()
        {
            for (int i = 0; i < 5; i++)
            {
                loans[i] = new Loan(i+1, i * 1000 + 3000, i * 2 + 12, i + 0.5);
            }
            return loans;
        }
        private void RegisterDbContext(IWindsorContainer container)
        {
            container.Register(Component.For<DbContextOptions<LoansDBContext>>().UsingFactoryMethod(() =>
            {
                var builder = new DbContextOptionsBuilder<LoansDBContext>();
                builder.UseInMemoryDatabase("LoanDb");
                return builder.Options;
            }).LifestyleTransient());

            container.Register(Component.For<LoansDBContext>().LifestyleTransient());

            InitializeDbContext(container);
        }
        private void InitializeDbContext(IWindsorContainer container)
        {
            using (var context = container.Resolve<LoansDBContext>())
            {
                context.Loans.AddRange(InitLoans());

                context.SaveChanges();
            }
        }
    }
}

