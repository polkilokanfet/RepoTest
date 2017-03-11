using HVTApp.DataAccess;
using HVTApp.Model;

namespace ToTest
{
    class Program
    {
        static void Main(string[] args)
        {
            HVTAppContext context = new HVTAppContext();

            context.Countries.Add(new Country());
        }
    }
}
