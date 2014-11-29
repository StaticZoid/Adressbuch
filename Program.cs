using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Adressbuch
{
    class Program
    {
        static void Main()
        {
            string Name = "";
            string Vorname = "";
            string Straße = "";
            int Hausnummer;
            int zeile = 0;
            // The connection string assumes that the Access 
            // Northwind.mdb is located in the c:\Data folder.
            string connectionString =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + "..\\..\\Adressbuch.mdb;User Id=admin;Password=;";

            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT ID, Name, Vorname, Straße, Hausnummer from Adressbuch "
                /*+ "WHERE ID > ? "*/
                    + "ORDER BY ID DESC;";

            // Specify the parameter value.
            /*int paramValue = 5;*/

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (OleDbConnection connection =
                new OleDbConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                OleDbCommand command = new OleDbCommand(queryString, connection);
                /*command.Parameters.AddWithValue("@ausgabe", paramValue);*/

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                int auswahl = 0;
                while (true)
                {
                    Console.WriteLine("Bitte Wähle aus: ");
                    Console.WriteLine("(1) Datensätze anzeigen");
                    Console.WriteLine("(2) Datensätze anlegen");
                    Console.WriteLine("(3) Datensätze löschen");
                    Console.WriteLine("=========================");
                    auswahl = Convert.ToInt32(Console.ReadLine());

                    if (auswahl == 1)
                    {
                        try
                        {
                            connection.Open();
                            OleDbDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine("\t{0}\t\t{1}\t\t{2}\t{3}\t{4}",
                                    reader[0], reader[1], reader[2], reader[3], reader[4]);
                            }
                            reader.Close();
                        }
                        catch
                        {
                            Console.WriteLine("Datenbank verbindung konnte nicht hergestellt werden.");
                        }
                        
                    }


                    if (auswahl == 2)
                    {
                        try
                        {
                            connection.Open();
                            Console.Write("Bitte Gebe deinen Nachnamen an: ");

                            Name = Convert.ToString(Console.ReadLine());

                            Console.Write("Bitte gebe deinen Vornamen an: ");
                            Vorname = Convert.ToString(Console.ReadLine());
                            Console.Write("Bitte gebe deine Straße an: ");
                            Straße = Convert.ToString(Console.ReadLine());
                            Console.Write("Bitte gebe deine Hausnummer an: ");
                            Hausnummer = Convert.ToInt32(Console.ReadLine());

                            const string insertString = @"INSERT INTO Adressbuch(Name, Vorname, Straße, Hausnummer) VALUES(@Name,@Vorname,@Strasse,@Hausnummer)";
                            var insertcmd = new OleDbCommand(insertString, connection);
                            insertcmd.Parameters.AddWithValue("@Name", Name);
                            insertcmd.Parameters.AddWithValue("@Vorname", Vorname);
                            insertcmd.Parameters.AddWithValue("@Straße", Straße);
                            insertcmd.Parameters.AddWithValue("@Hausnummer", Hausnummer);
                            insertcmd.ExecuteNonQuery();

                            Console.WriteLine("Datenbank aktualisiert");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Probleme beim einfügen der Daten: ");
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    if (auswahl == 3)
                    {
                        try
                        {
                            connection.Open();
                            Console.Write("Gebe die ID des Datensatzes an den du löschen möchtest: ");
                            zeile = Convert.ToInt32(Console.ReadLine());
                            OleDbCommand deletecmd = connection.CreateCommand();

                            deletecmd.CommandText = "DELETE FROM Adressbuch WHERE ID = " + zeile;
                            deletecmd.ExecuteNonQuery();

                            {
                                Console.WriteLine("Löschen von Zeile " + zeile + " erfolgrech.");
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Da ist wohl etwas schiefgelaufen: ");
                            Console.WriteLine(e.Message);
                        }
                    }
                    connection.Close();
                }
            }

            Console.ReadLine();
        }
    }
}