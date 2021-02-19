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
        // Console.WriteLine($"The connection string is {cs}.");
        // System.Environment.Exit(0);

        using var con = new SQLiteConnection(cs);
        con.Open();

        using var cmd = new SQLiteCommand(con);

        // create sample db
        SampleDb(cs);

        // read data from db
        // ReadSample(cs);

        // Since the classes are all in the same namespace, can be called directly between files
        // ReadTable.Read();

        // call interaction function
        // UserInteraction();

        // insert data call
        InsertData(cs);

        ReadSample(cs);

        // create table call
        CreateTable(cs);

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
        // figuring out how to dynamically enter data
        string[] table_info = { "Audi", "52642" };
        cmd.CommandText = @"INSERT INTO cars(name, price) 
                            VALUES
                              (@audi,52642)
                            , ('Mercedes',57127)
                            , ('Skoda',9000)
                            , ('Volvo',29000)
                            , ('Bentley',350000)
                            , ('Citroen',21000)
                            , ('Hummer',41400)
                            , ('Volkswagen',21600)";
        cmd.Parameters.AddWithValue("@audi", table_info[0]);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Table cars created");

    }

    /// <summary>Read the db and display contents</summary>
    public static void ReadSample(string cs)
    {
        using var con = new SQLiteConnection(cs);
        con.Open();

        string stm = "SELECT * FROM cars";

        using var cmd = new SQLiteCommand(stm, con);
        using SQLiteDataReader rdr = cmd.ExecuteReader();
        // outputs 
        Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-12} {rdr.GetName(2),8}");

        while (rdr.Read())
        {
            Console.WriteLine($@"{rdr.GetInt32(0),-3} {rdr.GetString(1),-12} {rdr.GetInt32(2),8}");
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

        Console.Write("What car name and price would you like to include? (write both words separated by a space) ");
        string data = Console.ReadLine();
        string[] info = data.Split(' ');
        string insert = @"INSERT INTO cars(name, price) VALUES(@name, @price)";
        cmd.Parameters.AddWithValue("@name", info[0]);
        cmd.Parameters.AddWithValue("@price", info[1]);
        cmd.CommandText = insert;
        cmd.ExecuteNonQuery();
    }

    /// <summary>Void function that will create tables and their colummn names</summary>
    static void CreateTable(string cs)
    {
        // insert into a table the user specifies
        using var con = new SQLiteConnection(cs);
        con.Open();

        using var cmd = new SQLiteCommand(con);

        Console.Write("What would you like to name your table? (If it already exists, it will be removed) ");
        string table = Console.ReadLine();

        // display example db
        // ReadSample(cs);

        // get user input for column names, id's auto generated
        Console.WriteLine(@"Column names are how data is identified. They have data types, which specify
what data is going to be stored. Some data types include
        INTEGER - Whole numbers like 1, 2, 999, or even 234
        REAL    - A number that has a decimal point
        TEXT    - Any data that are words, phrases or characters
        NULL    - Basically, this stores a 'nothing' value, but something is stored");
        Console.Write(@"What data is going to be in the table? (Write the column names and data type separated by commas,
like this: name TEXT) ");
        string columns = Console.ReadLine();
        string[] col = columns.Split(',');

        string insert = @"CREATE TABLE " + table + "(ID INTEGER PRIMARY KEY, ";
        // to populate the data
        // string dynamics = "@";

        // how to dynamically add data to create table 
        // add to a string and set @data+toSTring(i)
        for (int i = 0; i < col.Length; i++)
        {
            if (i == col.Length - 1) { insert += col[i] + ")"; }
            else { insert += col[i] + ", "; }
        }

        Console.WriteLine(insert);

        // cmd.CommandText = @"DROP TABLE IF EXISTS @table";
        // cmd.Parameters.AddWithValue("@table", table);
        cmd.CommandText = "DROP TABLE IF EXISTS " + table;
        cmd.ExecuteNonQuery();

        cmd.CommandText = insert;
        cmd.ExecuteNonQuery();

        // sample db will be
        // contact (table)
        // include ID for all tables
        // first_name - string
        // city - string


        // determine that data was input and is in table
        cmd.CommandText = InsertUserTable(table);
        cmd.ExecuteNonQuery();

        string stm = "SELECT * FROM " + table;
        using var cmnd = new SQLiteCommand(stm, con);
        using SQLiteDataReader rdr = cmnd.ExecuteReader();
        // outputs 
        Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-12} {rdr.GetName(2),8}");

        while (rdr.Read())
        {
            Console.WriteLine($@"{rdr.GetInt32(0),-3} {rdr.GetString(1),-12} {rdr.GetInt32(2),8}");
        }

    }

    public static string InsertUserTable(string table)
    {
        // everything up to this point works. Need to parse object into arrays
        // jacob salt lake city => 'jacob', 'salt lake city'
        string insert = @"INSERT INTO " + table + " VALUES ";

        Console.Write("How many rows of data are you going to insert now? ");
        int rows = Int16.Parse(Console.ReadLine());

        // how do I dynamically create this to allocate arrays to hold all of these values???
        // for (int i = 0 ...) {}
        // need to create new array, assign them name, and assign size

        string[] names = new string [rows];
        string[] address = new string [rows];

        for (int i = 0; i < rows; i++)
        {   
            Console.Write("What is the name that will be input: ");
            names[i] = Console.ReadLine();
            Console.Write("What is the city name to be input: ");
            address[i] = Console.ReadLine();
        }

        Console.Write(@"What values are going into the table? (Write the values,
like this: jacob salt lake city, horace nile, etc.) ");
        string columns = Console.ReadLine();
        string[] col = columns.Split(',');

        for (int i = 0; i < col.Length; i++)
        {
            if (i == col.Length - 1) { insert += col[i] + ")"; }
            else { insert += col[i] + ", "; }
        }

        return insert;
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