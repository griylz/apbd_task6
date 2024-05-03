using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetAnimals()
    {
    //    SqlConnection connection2 = new SqlConnection();
    //    try
    //    {
    //        connection2.Open();
    //    }
    //    finally
    //    {
    //        connection2.Close();
    //        connection2.Dispose();
    //    }
        
        
        
        
        
        
        
        
        //Open connection
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        //Create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM Animal;";

        var reader = command.ExecuteReader();
        var animals = new List<Animal>();
        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");
        while (reader.Read())
        {
            animals.Add((new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameOrdinal),
            }));
        }
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        //Create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES (@animalName,'','','')";
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.ExecuteNonQuery();
        //TODO: ADD TO REPOSITORY METHODS
        return Created("",null);
    }
}