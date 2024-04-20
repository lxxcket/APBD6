using APBD6.Models;

namespace APBD6.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    Animal GetAnimal(int animalId);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int animalId);
} 