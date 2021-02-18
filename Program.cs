using System;
using System.Data.SQLite;


public class DbInteraction
{
    static void Main()
    {
        // PrivateInfo connector = new PrivateInfo();
        // string cs = connector.cs();
        Coolio stuff = new Coolio();
        string cs = stuff.cs;

        // testing to get data from external file
        Console.WriteLine($"The connection string is {cs}.");
        // System.Environment.Exit(0);

        using var con = new SQLiteConnection(cs);
        con.Open();

        using var cmd = new SQLiteCommand(con);

        // create sample db
        SampleDb(cs);

        // read data from db
        ReadSample(cs);

        // Since the classes are all in the same namespace, can be called directly between files
        // ReadTable.Read();

        // can write all functions inside this same file
        // Read(cs);

        // call interaction function
        // UserInteraction();
    }

    /// <summary>Create sample db</summary>
    public static void SampleDb(string cs)
    {
        using var con = new SQLiteConnection(cs);
        con.Open();

        using var cmd = new SQLiteCommand(con);

        cmd.CommandText = "DROP TABLE IF EXISTS cars";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE cars(id INTEGER PRIMARY KEY,
                    name TEXT, price INT)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"INSERT INTO cars(name, price) 
                            VALUES
                              ('Audi',52642)
                            , ('Mercedes',57127)
                            , ('Skoda',9000)
                            , ('Volvo',29000)
                            , ('Bentley',350000)
                            , ('Citroen',21000)
                            , ('Hummer',41400)
                            , ('Volkswagen',21600)";
        cmd.ExecuteNonQuery();
        Console.WriteLine("Table cars created");

    }

    /// <summary>Read the db and display contents</summary>
    public static void ReadSample(string cs)
    {
        using var con = new SQLiteConnection(cs);
        con.Open();

        string stm = "SELECT * FROM cars LIMIT 5";

        using var cmd = new SQLiteCommand(stm, con);
        using SQLiteDataReader rdr = cmd.ExecuteReader();
        // outputs 
        Console.WriteLine($"{rdr.GetName(0), -3} {rdr.GetName(1), -8} {rdr.GetName(2), 8}");

        while (rdr.Read())
        {
            Console.WriteLine($@"{rdr.GetInt32(0),-3} {rdr.GetString(1), -8} {rdr.GetInt32(2), 8}");
        }
    }

    //how to create summary for c# functions
    /// <summary>Void function that will input data into the tables</summary>
    static void InsertData(string cs)
    {
        // insert into a table the user specifies
        using var con = new SQLiteConnection(cs);
        con.Open();

        using var cmd = new SQLiteCommand(con);

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
        cmd.ExecuteNonQuery();
    }

    /// <summary>Void function that will input data into the tables</summary>
    static void CreateTable(string cs)
    {
        // insert into a table the user specifies
        using var con = new SQLiteConnection(cs);
        con.Open();

        using var cmd = new SQLiteCommand(con);

        Console.Write("What would you like to name your table? (If it already exists, it will be removed) ");
        var table = Console.ReadLine();

        ReadSample(cs);
        Console.WriteLine("What data is going to be in the table? (Refer to example db above) ");

        cmd.CommandText = $"DROP IF EXISTS {table}";
        cmd.ExecuteNonQuery();

        cmd.CommandText = $@"CREATE TABLE {table}(id INTEGER PRIMARY KEY,
                    name TEXT, price INT)";
        cmd.ExecuteNonQuery();
    }

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
        Console.WriteLine($"You are {age} years old");


        // get table name
        Console.Write("What is the name of the table to be created? ");
        var table = Console.ReadLine();

        // get row names
        Console.Write(@"A DB contains column names that identify the data.
What will you name columns? Please enter the names separated by a space: ");
        var columns = Console.ReadLine();
        string[] col = columns.Split(' ');

        foreach (var item in col)
        {
            Console.WriteLine($"The row name is {item}.");
        }

        // get data


        // array of arrays to hold data for table info
        // 1-table name
        // 2-column names
        // 3-data to be inserted

        // will not let me declare and work with multi-array
        // string[][] multi_array = new string[3][];

        // multi_array[0][0] = table;

        // Console.WriteLine($"The name is {multi_array[0][0]}");

        // int count = 0;

        // foreach (var item in col)
        // {
        //     multi_array[1][count] = item;
        //     count++;
        // }

        // for (int i = 0; i < col.Length; i++)
        // {
        //     multi_array[1][i] = col[i];
        // }

        // foreach (var data in multi_array[1])
        // {
        //     Console.WriteLine($"The data is {data}.");
        // }


    }
}