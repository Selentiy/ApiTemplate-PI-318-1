using App.Configuration;
using App.Stocks.Interfaces;
using App.Stocks.Services;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Stocks
{
	public class StockModule : IModule
	{
		public void Initialize(IWindsorContainer container)
		{
			container.Register(Component.For<IValidateServices>().ImplementedBy<ValidateServices>().LifestyleTransient());
			container.Register(Component.For<ICompanyManager>().ImplementedBy<CompaniesManager>().LifestyleTransient());
			container.Register(Component.For<IStocksManager>().ImplementedBy<StockManager>().LifestyleTransient());
		}
	}
}
