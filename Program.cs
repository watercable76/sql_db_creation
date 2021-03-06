﻿using System;
using System.Data.SQLite;
using System.Collections.Generic;


public class DbInteraction
{
    static void Main()
    {
        Coolio stuff = new Coolio();
        string cs = stuff.cs;

        // testing to get data from external file
        // Console.WriteLine($"The connection string is {cs}.");
        // System.Environment.Exit(0);

        using var con = new SQLiteConnection(cs);
        con.Open();

        using var cmd = new SQLiteCommand(con);

        // create user interaction here
        string x = "\x0A";
        var option = 10;

        // create sample db
        SampleDb(cs);

        while (option != 0)
        {
            Print_New_Lines(3);
            Console.WriteLine($@"Welcome to the Database tutorial! By running this program,
    you are starting your first database. A database is a way to store data, into column
    and row structures. This data can be accessed, changed, deleted, or displayed.{x}");
            Console.WriteLine("For this tutorial, you will select an option listed below.\n");
            Console.WriteLine($@"You will be able to: 
        1 - Look up data in a sample database
        2 - Insert data into the existing database
        3 - Create your own table and insert information (this will display all of your data afterwards{x}
        0 - Will end the program{x}");
            Console.Write("Which option will you chooose: ");
            try
            {
                // did not know where to add first open paren, so did it here
                option = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {
                // need to add single quotes for all string values
                Console.WriteLine("That is an invalid entry. Please try again.\n\n");
                option = 10;
            }

            if (option == 1)
            {
                // read data from db
                ReadSample(cs);
            }
            else if (option == 2)
            {
                // insert data call
                InsertData(cs);
            }
            else if (option == 3)
            {
                // create table call
                CreateTable(cs);
            }
        }

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
        Print_New_Lines(2);
        Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-12} {rdr.GetName(2),8}");

        while (rdr.Read())
        {
            Console.WriteLine($@"{rdr.GetInt32(0),-3} {rdr.GetString(1),-12} {rdr.GetInt32(2),8}");
        }
    }

    /// <summary>Read the db and display contents</summary>
    public static void ReadTable(string cs)
    {
        using var con = new SQLiteConnection(cs);
        con.Open();

        Console.WriteLine("Which table would you like to read from?");
        string table = Console.ReadLine();

        string stm = "SELECT * FROM " + table;

        using var cmd = new SQLiteCommand(stm, con);
        using SQLiteDataReader rdr = cmd.ExecuteReader();
        // outputs 
        Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-12} {rdr.GetName(2),8}");

        while (rdr.Read())
        {
            Console.WriteLine($@"{rdr.GetInt32(0),-3} {rdr.GetString(1),-12} {rdr.GetInt32(2),8}");
        }
        rdr.Close();
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
        Print_New_Lines(2);
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
        cmd.CommandText = "DROP TABLE IF EXISTS " + table;
        cmd.ExecuteNonQuery();

        cmd.CommandText = insert;
        cmd.ExecuteNonQuery();
        Console.WriteLine("Table was created!");

        // sample db will be
        // contact (table)
        // include ID for all tables
        // first_name - string
        // city - string


        // determine that data was input and is in table
        Print_New_Lines(3);
        string insertStatement = InsertUserTable(table, col);
        Console.WriteLine(insertStatement);
        // System.Environment.Exit(0);
        cmd.CommandText = insertStatement;
        cmd.ExecuteNonQuery();
        // Console.WriteLine("Insert was successful!!!");
        Print_New_Lines(2);

        string stm = "SELECT * FROM " + table;
        using var cmnd = new SQLiteCommand(stm, con);
        using SQLiteDataReader rdr = cmnd.ExecuteReader();
        // outputs 
        // + num is right align, - is left align
        for (int i = 0; i <= col.Length; i++)
        {
            Console.Write($"{rdr.GetName(i),-12}");
        }
        Print_New_Lines(1);
        // Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-12} {rdr.GetName(2),8} {rdr.GetName(3),12}");

        while (rdr.Read())
        {
            Console.Write($"{rdr.GetInt16(0),-12}");
            for (int i = 1; i <= col.Length; i++)
            {
                Console.Write($"{rdr.GetString(i),-12}");
            }
            Print_New_Lines(1);
        }
        // System.Environment.Exit(0);

        rdr.Close();

    }

    ///<summary>
    ///Get column names for inserting into user's new table. Will format the input and make it into
    /// correct sql format
    ///</summary>
    public static string InsertUserTable(string table, string[] array)
    {
        // everything up to this point works. Need to parse object into arrays
        // jacob salt lake city => 'jacob', 'salt lake city'
        // string insert = @"INSERT INTO " + table + " VALUES";
        string insert = @"INSERT INTO " + table + '(';

        Console.Write("How many rows of data are you going to insert now? ");
        int rows = Int16.Parse(Console.ReadLine());

        // debugging purposes
        // foreach (var item in array)
        // {
        //     Console.WriteLine($"The row is {item}.");
        // }

        // create var to hold col names
        string[] columns = new string[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            // have to declare a array to hold array value,
            // and populate that with value from original array
            string value = array[i];
            string[] holder = value.Split(' ');
            columns[i] = holder[0];
            if (i == array.Length - 1) { insert += holder[0] + ") VALUES"; }
            else { insert += holder[0] + ','; }
            // Console.Write($"{columns[i]} ");
        }

        Console.WriteLine($"Here is the insert statement: {insert}\n");

        // how do I dynamically create this to allocate arrays to hold all of these values???
        // for (int i = 0 ...) {}
        // need to create new array, assign them name, and assign size

        // create a dictionary and store values there???
        Dictionary<int, string> dict = new Dictionary<int, string>();

        string[] names = new string[rows];
        string[] address = new string[rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns.Length; j++)
            {
                Console.Write($"What value will go into {columns[j]}: ");
                string value = Console.ReadLine();
                // would not let me check if key does not exist, so I had to do a try
                // and if it fails, then I add values later
                try
                {
                    // did not know where to add first open paren, so did it here
                    dict.Add(i, "('" + value);
                }
                catch (System.Exception)
                {
                    // need to add single quotes for all string values
                    dict[i] += "','" + value;
                }
            }
            // close the () for the insert and add comma to it
            if (i == rows - 1) { dict[i] += "')"; }
            else { dict[i] += "'), "; }
        }

        foreach (KeyValuePair<int, string> ele1 in dict)
        {
            // Console.WriteLine("{0} and {1}", ele1.Key, ele1.Value);
            insert += ele1.Value;
        }
        // Console.WriteLine(insert);

        return insert;
    }

    ///<summary>Print multiple new lines. Laziness so I don't have to write console... multiples times :)</summary>
    static void Print_New_Lines(int lines)
    {
        for (int i = 0; i < lines; i++)
        {
            Console.Write("\n");
        }
    }
}