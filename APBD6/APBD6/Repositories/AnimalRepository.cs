using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using APBD6.Models;
namespace APBD6.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private IConfiguration _configuration;
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        List<Animal> list = new List<Animal>();
        string query = $"SELECT IdAnimal, Name, Category, Area, Description FROM ANIMAL ORDER BY {orderBy}";
        using (SqlConnection connection = new
                   SqlConnection(_configuration["ConnectionStrings:MyConnectionString"]))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query,
                       connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Animal animal = new Animal()
                        {
                            IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                            Name = reader["Name"].ToString(),
                            Category = reader["Category"].ToString(),
                            Area = reader["Area"].ToString(),
                            Description = reader["Description"].ToString()
                        };
                        list.Add(animal);
                    } }
            } }
        return list;
    }

    public Animal GetAnimal(int animalId)
    {
        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:MyConnectionString"]))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText =
                    "SELECT IdAnimal, Name, Category, Area, Description FROM ANIMAL WHERE IdAnimal = @IdAnimal";
                command.Parameters.AddWithValue("@IdAnimal", animalId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    Animal animal = new Animal()
                    {
                        IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Category = reader["Category"].ToString(),
                        Area = reader["Area"].ToString()
                    };
                    return animal;
                }
            }
        }
    }

    public int CreateAnimal(Animal animal)
    {
        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:MyConnectionString"]))
        {
            connection.Open();
            string query = "INSERT INTO ANIMAL VALUES(@Name, @Description, @Category, @Area)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Name", animal.Name);
            cmd.Parameters.AddWithValue("@Description", animal.Description);
            cmd.Parameters.AddWithValue("@Category", animal.Category);
            cmd.Parameters.AddWithValue("@Area", animal.Area);

            int affectedRows = cmd.ExecuteNonQuery();
            return affectedRows;
        }
    }

    public int UpdateAnimal(Animal animal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:MyConnectionString"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "UPDATE Animal SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    public int DeleteAnimal(int animalId)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:MyConnectionString"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", animalId);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
}