using SQLite;
using People.Models;
using System;
using System.Collections.Generic;

namespace People;

public class PersonRepository
{
    private string _dbPath;
    private SQLiteConnection conn;

    public string StatusMessage { get; set; }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
        Init();
    }

    private void Init()
    {
        if (conn != null)
            return;     

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Person>();
    }

    public void AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            Init(); 

            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            result = conn.Insert(new Person { Name = name });

            StatusMessage = $"{result} record(s) added (Name: {name})";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
        }
    }

    public List<Person> GetAllPeople()
    {
        try
        {
            Init();
            return conn.Table<Person>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to retrieve data. {ex.Message}";
            return new List<Person>();
        }
    }
}
