using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection cnn;
            SqlCommand command;
            SqlDataReader dataReader;
            int rowsAffected;
            String output;

            String connString = "Data Source=WINDOWS-4QT2NDK; Initial Catalog=Northwind;User ID=sa;Password=11Jan02*";
            //selecting name and age
            String query = "Select Name, Age from Student";

            //connecting to server
            cnn = new SqlConnection(connString);
            cnn.Open();
            Console.WriteLine("Connection is Open");
            command = new SqlCommand(query, cnn);

            //reading the name and age and outputting them
            dataReader = command.ExecuteReader();        
            while (dataReader.Read())
            {
                output = (String)dataReader.GetValue(0) + "\t" + dataReader.GetValue(1) + "\n";
                Console.WriteLine(output);
            }

            dataReader.Close();
            command.Dispose();


            //creating new data to input into the database
            Console.WriteLine("enter a new name: ");
            string name = Console.ReadLine();

            Console.WriteLine("enter their age : ");
            int age = Convert.ToInt32(Console.ReadLine());

            String createQuery = "INSERT INTO Student (Name, Age) VALUES (@name, @age)";
            command = new SqlCommand(createQuery, cnn);

            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Age", age);

            // Execute the query
            rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Inserted {rowsAffected} row(s) into the Student table.");

            command.Dispose();

            //updating data in the database
            string updateQuery = "UPDATE Student SET Name = 'frank' WHERE Name = 'Leo'";
            command = new SqlCommand(updateQuery, cnn);

            rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Updated {rowsAffected} row(s) in the Students table.");
            command.Dispose();

            //Deleting data in the database
            string deleteQuery = "DELETE FROM Student WHERE Name = 'frank'";
            command = new SqlCommand(deleteQuery, cnn);

            rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Deleted {rowsAffected} row(s) in the Students table.");

            command.Dispose();
            cnn.Close();



        }
    }
}

