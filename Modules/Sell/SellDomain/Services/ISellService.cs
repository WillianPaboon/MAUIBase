namespace SellDomain.Services
{
    /// <summary>
    /// Interface for selling services.
    /// </summary>
    public interface ISellService
    {
        /// <summary>
        /// Sells a product.
        /// </summary>
        void SellProduct();
    }

    /// <summary>
    /// Service for selling products.
    /// </summary>
    public class SellService : ISellService
    {
        /// <summary>
        /// Sells a product.
        /// </summary>
        public void SellProduct()
        {
            //Sell product
        }
    }
}
