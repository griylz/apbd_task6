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
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] AddAnimal animalDTO)
    {
        var animal = new Animal()
        {
            Name = animalDTO.Name,
            Description = animalDTO.Description,
            Area = animalDTO.Area,
            Category = animalDTO.Category
        };

        if (_repository.AddAnimal(animal))
        {
            return Created("api/animals", animal);
        } 
        else
        {
            return BadRequest("Failed to add animal");
        }
    }

    [HttpPut("{idAnimal}")]
    public IActionResult UpdateAnimal(int idAnimal, [FromBody] AddAnimal animalDTO)
    {
        if (_repository.UpdateAnimal(idAnimal,animalDTO))
        {
            return Ok();
        }
        else
        {
            return BadRequest("Failed to update");
        }
    }
}