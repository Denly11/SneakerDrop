using UserApi.Dtos;
using UserApi.Entities;

namespace UserApi.Data;

public class ProductRepository
{
    public Product? CreateProduct(int userId, CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Category = request.Category,
        };
        return product;
    }
}
