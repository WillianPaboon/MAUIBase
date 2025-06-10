namespace BuyDomain.Services
{
    public interface IBuyService
    {
        string GetSampleData();
    }

    internal class BuyService : IBuyService
    {

        public BuyService()
        {
        }

        public string GetSampleData()
        {
            return "Sample data from BuyService";
        }

    }
}
