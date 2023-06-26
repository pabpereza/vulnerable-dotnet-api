namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

[ApiController]
[Route("iusers")]

public class DesSQL : ControllerBase
{

    // SQL INJECTION
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {

        var connection = new SqliteConnection("Data Source=LocalDatabase.db");

        connection.Open();

        string query = "SELECT firstname, lastname FROM Users WHERE id='" + id + "'";
        Console.WriteLine(query);

        var command = connection.CreateCommand();
        command.CommandText = query;

        var reader = command.ExecuteReader(); 

        string output = "";
        while (reader.Read())
         {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                output += reader.GetName(i) + ": "  + reader.GetValue(i) + "\n";
            }
  
         }

        connection.Close();

        return Ok(output);

    }

    // SQL INJECTION
    [HttpGet("parametrized/{id}")]
     public IActionResult GetByIdParametrized(string id)
    {

        var connection = new SqliteConnection("Data Source=LocalDatabase.db");

        connection.Open();

        string query = "SELECT firstname, lastname FROM Users WHERE id=@id";
        Console.WriteLine(query);

        var command = connection.CreateCommand();
        command.CommandText = query;

        command.Parameters.AddWithValue("@id", id);

        var reader = command.ExecuteReader();
        
        
        string output = "";
        while (reader.Read())
         {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                output += reader.GetName(i) + ": "  + reader.GetValue(i) + "\n";
            }
  
         }

        connection.Close();

        return Ok(output);

    }
}



