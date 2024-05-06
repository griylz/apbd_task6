using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IAnimalRepository
{
    List<Animal> GetAllAnimals(string orderBy);
    bool AddAnimal(Animal animal);
    bool UpdateAnimal(Animal animal);
    bool DeleteAnimal(int id);
}