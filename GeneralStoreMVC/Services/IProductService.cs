using GeneralStoreMVC.Models.Product;

namespace GeneralStoreMVC.Services
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductCreate model);
        Task<IEnumerable<ProductListItem>> GetAllProduct();
        Task<ProductDetail> GetProductById(int productId);
        Task<bool> UpdateProduct(ProductEdit model);

        Task<bool> DeleteProduct(int productId);
    }
}
