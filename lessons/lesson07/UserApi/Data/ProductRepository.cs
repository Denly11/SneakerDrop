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

    internal List<Product> GetUserProductList(int userId, decimal? min_price, decimal? max_price, string? sort)
    {
        IEnumerable<Product> Query = _products;

        Query = Query.Where(n => n.UserId == userId);

        if ( min_price != null)
        { 
            Query = Query.Where(n => n.Price >= min_price);
        }
        
        if(max_price != null)
        {
            Query = Query.Where(n => n.Price <= max_price);
        }

        if(sort == "asc")
        {
            Query = Query.OrderBy(n => n.Price);//nativní fungování OrderBy
        }

        if (sort == "desc")
        {
            Query = Query.OrderByDescending(n => n.Price);
        }

        return Query.ToList();
    }

    public Product? Edit(int id, CreateProductRequest request)
    {
        var FoundProduct = GetById(id);

        if(FoundProduct == null)
        {
            return null;
        }

        FoundProduct.Name = request.Name;
        FoundProduct.Price = request.Price;
        FoundProduct.Category = request.Category;

        return FoundProduct;
    }

    public Product? GetById(int id)
    {
        var FoundProduct = _products.FirstOrDefault(n => n.Id == id);

        if(FoundProduct == null)
        { 
            return null;
        }
        else
        {
            return FoundProduct;
        }
    }
}
