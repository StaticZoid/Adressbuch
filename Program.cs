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
            bool bconn = true;
            while (bconn == true)
            {
                try
                {
                    OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\Adressbuch2.mdb");
                    conn.Open();
                    Console.WriteLine("Verbindung wird versucht hergestellt...");
                    bconn = false;
                }
                catch
                {
                    Console.WriteLine("Verbindung konnte NICHT hergestellt...");
                }
            }
            Console.WriteLine("...");
            Console.WriteLine("Die Verbindung zur Datenbank wurde erfolgreich hergestellt.");
            Console.WriteLine("");
            Console.WriteLine("Was möchtest du tun?");
            Console.WriteLine("");
            Console.WriteLine("(1) Datensätze anzeigen");
            Console.WriteLine("(2) Datensatz hinzufügen");
            Console.WriteLine("(3) Datensatz löschen");

            Console.ReadLine();
        }
    }
}

