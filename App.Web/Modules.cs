using App.Configuration;

namespace App.Web
{
    /// <summary>
    /// IMPORTANT ! In order to use classes and endpoints, defined in your own module, it should be referenced here as it shown
    /// </summary>
    [ModuleUsing(typeof(Example.ExampleModule))] // < ---- Example of module registration
    [ModuleUsing(typeof(RegularPayments.RegularPaymentModule))]
    [ModuleUsing(typeof(Cards.CardsModule))]
    [ModuleUsing(typeof(News.NewsModule))]
    [ModuleUsing(typeof(Currencies.CurrenciesModule))]
    [ModuleUsing(typeof(Accounts.AccountsModule))]
    public class Modules
    {
    }
}
