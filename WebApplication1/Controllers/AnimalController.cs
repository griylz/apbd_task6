using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalRepository _repository;
    public AnimalController(IAnimalRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "Name")
    {
        try
        {
            var animals = _repository.GetAllAnimals(orderBy);
            return Ok(animals);
        }
        catch (Exception ex)
        {
            // Log the exception details
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        //Create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.CommandText = "INSERT INTO Animal VALUES (@animalName,'','','')";
        command.ExecuteNonQuery();
        //TODO: ADD TO REPOSITORY METHODS
        return Created("",null);
    }
}