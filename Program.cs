using System;
using System.Data.SQLite;

public class CreateTable
{
    static void Main()
    {
        string cs = @"URI=file:C:\Users\water\OneDrive\Documents\7TH SEMESTER\CSE 310\sql_db_creation\test.db";

        using var con = new SQLiteConnection(cs);
        con.Open();

        using var cmd = new SQLiteCommand(con);

        cmd.CommandText = "DROP TABLE IF EXISTS cars";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE cars(id INTEGER PRIMARY KEY,
                    name TEXT, price INT)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo',29000)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley',350000)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen',21000)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer',41400)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
        cmd.ExecuteNonQuery();

        Console.WriteLine("Table cars created");

        // Since the classes are all in the same namespace, can be called directly between files
        // ReadTable.Read();

        // can write all functions inside this same file
        // Read(cs);

        // call interaction function
        UserInteraction();
    }

    /// <summary>Read the db and display contents</summary>
    public static void Read(string cs)
    {
        using var con = new SQLiteConnection(cs);
        con.Open();

        string stm = "SELECT * FROM cars LIMIT 5";

        using var cmd = new SQLiteCommand(stm, con);
        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetInt32(2)}");
        }
    }

    // how to create summary for c# functions
    /// <summary>
    /// Void function that will input data into the tables
    /// </summary>
    // static void InsertData(string cs)
    // {
    //     using var con = new SQLiteConnection(cs);
    //     con.OpenAndReturn();

    //     string stm = "INSERT INTO ";
    // }

    static void UserInteraction()
    {

        // use just write to have text on line, but without \n char
        Console.WriteLine("Welcome to the DB tutorial!");
        Console.Write("Please enter your name here: ");
        string name = Console.ReadLine();
        Console.WriteLine("Your name is " + name);

        Console.Write("Please enter your age: ");
        var age = (Console.ReadLine());
        // formatting strings just like old way for python
        Console.WriteLine("You are {0} years old", age);


    }
}