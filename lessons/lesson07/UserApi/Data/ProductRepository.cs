using UserApi.Dtos;
using UserApi.Entities;

namespace UserApi.Data;

public class ProductRepository
{
    private readonly List<Product> _products = [];
    private int _lastId;

    public Product? CreateProduct(int userId, CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Category = request.Category,
            UserId = userId,
            Id = ++_lastId,
        };
        _products.Add(product);
        return product;
    }
}
