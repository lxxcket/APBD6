
using APBD6.Models;
using APBD6.Repositories;

namespace APBD6.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        return _animalRepository.GetAnimals(FiltrateOrderByString(orderBy));
    }

    public Animal? GetAnimal(int animalId)
    {
        return _animalRepository.GetAnimal(animalId);
    }

    public int CreateAnimal(Animal animal)
    {
        return _animalRepository.CreateAnimal(animal);
    }

    public int UpdateAnimal(Animal animal)
    {
        return _animalRepository.UpdateAnimal(animal);
    }

    public int DeleteAnimal(int animalId)
    {

        return _animalRepository.DeleteAnimal(animalId);
    }

    private string FiltrateOrderByString(string orderBy)
    {
        if (string.IsNullOrEmpty(orderBy) || 
            (orderBy != "name" && orderBy != "description" && orderBy != "category" && orderBy != "area"
                && orderBy != "NAME" && orderBy != "DESCRIPTION" && orderBy != "CATEGORY" && orderBy != "AREA"))
        {
            return "name";
        }

        return orderBy;
    }
    
}