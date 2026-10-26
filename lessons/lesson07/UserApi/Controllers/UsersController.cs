using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
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
    private readonly UserRepository _repository;
    private readonly ProductRepository _productRepository;

    public UsersController(UserRepository repository, ProductRepository productRepository)
    {
        _repository = repository;
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

        var user = _repository.Add(request);
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

        var FoundUser = _repository.GetById(id);
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

        SearchedUsers = _repository.Search(name, min_age, max_age, limit);

        return Ok(SearchedUsers);
    }

    [HttpPut("{id:int}")]//s metodou mut to příjde na odkaz s id uživatele

    public ActionResult<User> Edit([FromHeader(Name = "ApiKey")] string? ApiKey, int id, CreateUserRequest request)
    {

        if (ValidApiKey != ApiKey)
        {
            return Unauthorized(new { error = "Missing or invalid Api-Key header." });
        }
        var FoundUser = _repository.GetById(id);
        if (FoundUser == null)
        {
            return NotFound(new { error = "User not found." });
        }

        if (_repository.EmailIsUsed(request.Email, id) == true)
        {
            return Conflict(new { error = "Email is already used by another user." });
        }

        FoundUser = _repository.Edit(id,request);
        

        return Ok(FoundUser);
    }

    [HttpPost("{userId:int}/products")]
    public ActionResult CreateUserProduct([FromHeader(Name = "ApiKey")] string? apiKey, int userId, CreateProductRequest request )
    {
        if (ValidApiKey != apiKey)
        {
            return Unauthorized(new { error = "Missing or invalid Api-Key header." });//401
        }

        var user = _repository.GetById(userId);
        if(user == null)
        {
            return NotFound(new { error = "User not found." });//404
        }

        var product = _productRepository.CreateProduct(userId, request);
        return Created($"/api/users/{userId}/products/{product.Id}", product);
    }

    [HttpGet("{userId:int}/products")]

    public ActionResult ShowUserProductList(int userId, decimal? min_price, decimal? max_price, string? sort)
    {
        var user = _repository.GetById(userId);
        if (user == null)
        {
            return NotFound(new { error = "User not found." });
        
        }
        var products = _productRepository.GetProductsByUserId(userId, min_price, max_price, sort);
        return Ok(products);
    }

}
