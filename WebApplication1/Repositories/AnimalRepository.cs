using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Animal> GetAllAnimals(string orderBy)
    {
        var animals = new List<Animal>();
        string sqlCommand = "SELECT * FROM Animal";
        switch (orderBy.ToLower())
        {
            case "name":
                sqlCommand += " ORDER BY Name;";
                break;
            case "description":
                sqlCommand += " ORDER BY Description;";
                break;
            case "category":
                sqlCommand += " ORDER BY Category;";
                break;
            case "area":
                sqlCommand += " ORDER BY Area;";
                break;
            default:
                sqlCommand += " ORDER BY Name;";
                break;
        }

        using (var connection = new SqlConnection(_configuration.GetConnectionString("2019SBD")))
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = sqlCommand;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    animals.Add((new Animal()
                    {
                        IdAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                        Category = reader.GetString(reader.GetOrdinal("Category")),
                        Area = reader.GetString(reader.GetOrdinal("Area"))
                        
                    }));
                }
            }
        }

        return animals;
    }

    public bool AddAnimal(Animal animal)
    {
        string sqlCommand = @"
            INSERT INTO Animal (Name, Description, Category, Area) 
            VALUES (@Name, @Description, @Category, @Area);
            SELECT CAST(SCOPE_IDENTITY() as int);";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("2019SBD")))
        {
            connection.Open();
            using (var command = new SqlCommand(sqlCommand, connection))
            {
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", animal.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);

                
                int id = (int)command.ExecuteScalar();
                
                animal.IdAnimal = id;
            }
        }

        return true;
    }

    public bool UpdateAnimal(Animal animal)
    {
        throw new NotImplementedException();
    }

    public bool DeleteAnimal(int id)
    {
        throw new NotImplementedException();
    }
}