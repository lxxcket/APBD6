using APBD6.Models;

namespace APBD6.Services;

public interface IAnimalService
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    Animal? GetAnimal(int animalId);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int animalId);
}