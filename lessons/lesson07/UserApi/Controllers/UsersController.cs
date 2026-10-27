using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data;
using UserApi.Dtos;
using UserApi.Entities;
using UserApi.Models;

namespace UserApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private const string ValidClientId = "Client123";
    private const string ValidApiKey = "Secret123";
    private const string ValidAdmin = "admin123";
    private readonly UserRepository _userRepository;
    private readonly ProductRepository _productRepository;

    public UsersController(UserRepository repository, ProductRepository productRepository)
    {
        _userRepository = repository;
        _productRepository = productRepository;
    }

    [HttpPost("create")]
    public ActionResult<User> Create(
        [FromHeader(Name = "Client-Id")] string? clientId,//sem se uložá ten klíč
        CreateUserRequest request)
    {
        if (clientId != ValidClientId)
        {
            return Unauthorized(new { error = "Missing or invalid Client-Id header." });
        }

        var user = _userRepository.Add(request);
        if (user is null)
        {
            return Conflict(new { error = "Email is already used by another user." });
        }

        return Created($"/api/users/{user.Id}", user);
    }


    [HttpGet("{id:int}")]//"adresa" konkrétního usera
    public ActionResult<User> GetById([FromHeader(Name = "ApiKey")] string? apiKey, int id)
    {
        if (ValidApiKey != apiKey)
        {
            return Unauthorized(new { error = "Missing or invalid Api-Key header." });
        }

        var FoundUser = _userRepository.GetById(id);
        if (FoundUser == null)
        { 
            return NotFound(new { error = "User not found." });
        }
        else
        {
            return Ok(FoundUser);
        }
    }

    [HttpGet("search")]//tady to není adresa konkrétního uživatele, ale filtr

    public ActionResult<List<User>> Search(string? name, [Range(1, int.MaxValue)] int? min_age, [Range(1, int.MaxValue)] int? max_age, [Range(1, int.MaxValue)] int? limit)
    {//[Range(1, int.MaxValue)] říká že hodnota musí být od 1 do max hodnoty intu
        //tohle je metadatový záznam pak to jde ještě reflexí a to je jinej zápis
        
        List<User> SearchedUsers = new List<User>();

        SearchedUsers = _userRepository.Search(name, min_age, max_age, limit);

        return Ok(SearchedUsers);
    }

    [HttpPut("{id:int}")]//s metodou mut to příjde na odkaz s id uživatele

    public ActionResult<User> Edit([FromHeader(Name = "ApiKey")] string? ApiKey, int id, CreateUserRequest request)
    {

        if (ValidApiKey != ApiKey)
        {
            return Unauthorized(new { error = "Missing or invalid Api-Key header." });
        }
        var FoundUser = _userRepository.GetById(id);
        if (FoundUser == null)
        {
            return NotFound(new { error = "User not found." });
        }

        if (_userRepository.EmailIsUsed(request.Email, id) == true)
        {
            return Conflict(new { error = "Email is already used by another user." });
        }

        FoundUser = _userRepository.Edit(id,request);
        

        return Ok(FoundUser);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete([FromHeader(Name = "Admin")] string? admin, int id)
    {
        if (admin == null)
        {
            return Unauthorized(new { error = "Missing Admin header." });
        }

        if (admin != ValidAdmin)
        {
            return StatusCode(403, new { error = "Invalid Admin header." });
        }

        var user = _userRepository.GetById(id);
        if (user == null)
        {
            return NotFound(new { error = "User not found." });
        }

        _userRepository.Delete(id);

        return NoContent();
    }

    [HttpPost("{userId:int}/products")]
    public ActionResult CreateUserProduct([FromHeader(Name = "ApiKey")] string? apiKey, int userId, CreateProductRequest request )
    {
        if (ValidApiKey != apiKey)
        {
            return Unauthorized(new { error = "Missing or invalid Api-Key header." });//401
        }

        var user = _userRepository.GetById(userId);
        if(user == null)
        {
            return NotFound(new { error = "User not found." });//404
        }

        var product = _productRepository.CreateProduct(userId, request);
        return Created($"/api/users/{userId}/products/{product.Id}", product);
    }

    [HttpGet("{userId:int}/products")]

    public ActionResult ShowUserProductList(int userId, decimal? min_price, decimal? max_price, [AllowedValues("asc", "desc")]string? sort)
    {
        if(userId == 999)
        {
            return StatusCode(500);
        }
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return NotFound(new { error = "User not found." });
        
        }
        var products = _productRepository.GetUserProductList(userId, min_price, max_price, sort);
        return Ok(products);
    }
    
    [HttpPut("{userId:int}/products/{productId:int}")]
    public ActionResult<Product> Edit([FromHeader(Name = "ApiKey")] string? ApiKey, int userId, int productId, CreateProductRequest request)
    {
        if (ValidApiKey != ApiKey)
        {
            return Unauthorized(new { error = "Missing or invalid Api-Key header." });
        }

        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return NotFound(new { error = "User does not exist." });
        }

        var product = _productRepository.GetById(productId);
        if(product == null)
        {
            return NotFound(new { error = "Product not found." });
        }

        if(product.UserId != userId)
        {
            return NotFound(new { error = "Invalid user id or product id. Ids do not match." });
        }

        product = _productRepository.Edit(productId, request);

        return Ok(product);
    }

    [HttpDelete("{userId:int}/products/{productId:int}")]
    
    public ActionResult Delete([FromHeader(Name = "Admin")] string? admin, int userId, int productId)
    {
        if (admin == null)
        {
            return Unauthorized(new { error = "Missing Admin header." });
        }

        if (admin != ValidAdmin)
        {
            return StatusCode(403, new { error = "Invalid Admin header." });
        }

        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return NotFound(new { error = "User does not exist." });
        }

        var product = _productRepository.GetById(productId);
        if (product == null)
        {
            return NotFound(new { error = "Product not found." });
        }

        if (product.UserId != userId)
        {
            return NotFound(new { error = "Invalid user id or product id. Ids do not match." });
        }

        _productRepository.Delete(productId);
        return NoContent();
       
    }

        
}
