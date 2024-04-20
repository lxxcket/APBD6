using APBD6.Exceptions;
using APBD6.Models;
using APBD6.Services;

namespace APBD6.Controllers;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private IAnimalService _animalService;

    public AnimalsController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpGet]
    public IActionResult GetAnimals([FromQuery] string orderBy = "name")
    {
        var animals = _animalService.GetAnimals(orderBy);
        return Ok(animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animalService.GetAnimal(id);
        if (animal == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }

        return Ok(animal);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
            _animalService.CreateAnimal(animal);
            return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        int status = _animalService.DeleteAnimal(id);
        if (status == 1)
        {
            return NoContent();
        }
        return StatusCode(StatusCodes.Status404NotFound);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(Animal animal, int id)
    {
        Animal animal1 = _animalService.GetAnimal(id);
        if (animal1 == null)
        {
            return BadRequest();
        }

        animal.IdAnimal = id;
        _animalService.UpdateAnimal(animal);
        return Ok();
    }
    
}