namespace App.Cards.Exceptions
{
    class BlockedCardException: InvalidBusinessOperationException
    {
        public BlockedCardException(long number, string message) : base(number, message) { }
    }
}
