using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using People.Models;

namespace People;

public class PersonRepository
{
    private string _dbPath;
    private SQLiteAsyncConnection conn;

    public string StatusMessage { get; set; }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
        Init().Wait();
    }

    private async Task Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<Person>();  
    }

    public async Task AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            await Init();

            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            result = await conn.InsertAsync(new Person { Name = name });

            StatusMessage = $"{result} record(s) added [Name: {name}]";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
        }
    }

    public async Task<List<Person>> GetAllPeople()
    {
        try
        {
            await Init();

            return await conn.Table<Person>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to retrieve data. {ex.Message}";
        }

        return new List<Person>();
    }
}


