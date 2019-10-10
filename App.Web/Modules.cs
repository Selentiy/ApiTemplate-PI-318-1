using App.Configuration;

namespace App.Web
{
    /// <summary>
    /// IMPORTANT ! In order to use classes and endpoints, defined in your own module, it should be referenced here as it shown
    /// </summary>
    [ModuleUsing(typeof(Example.ExampleModule))]
    [ModuleUsing(typeof(Loans.LoansModule))]// < ---- Example of module registration

    public class Modules
    {
    }
}
