using App.Configuration;

namespace App.Loans
{
    public interface ISomeService
    {
        void DoSmth();
    }

    /// <summary>
    /// This service is a demonstration of registering dependencies, using utility <see cref="ITransientDependency"/> interface
    /// </summary>
    public class SomeService : ISomeService, ITransientDependency
    {
        public void DoSmth()
        {
            // do nothing
        }
    }
}
