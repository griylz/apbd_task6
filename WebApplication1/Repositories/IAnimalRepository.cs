using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Repositories;

public interface IAnimalRepository
{
    List<Animal> GetAllAnimals(string orderBy);
    bool AddAnimal(Animal animal);
    bool UpdateAnimal(int id,AddAnimal animal);
    bool DeleteAnimal(int id);
}