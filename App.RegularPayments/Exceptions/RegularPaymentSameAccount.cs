namespace App.RegularPayments.Exceptions
{
    public class RegularPaymentSameAccount : ValidationRegularPaymentException
    {
        public RegularPaymentSameAccount(string paramName) : base(paramName, $"Invoices of Payer and Recipient {paramName} are same") { }
    }
}
