using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;

namespace Michalski
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {   
            base.OnStartup(e);
            // main
            string dburi = @"URI=file:C:\Users\buyuk\source\repos\ProgramowanieWizualne\data.db";
            var connection = new SQLiteConnection(dburi);
            connection.Open();

            var cmd = new SQLiteCommand("select * from violins", connection);
            var reader = cmd.ExecuteReader();
            Console.WriteLine("<Violins>");
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var maker = reader.GetString(1);
                var year = reader.GetInt32(2);
                var price = reader.GetInt32(3);
                var state = reader.GetString(4);
                var v = new Violin(maker, name, (uint)year, (uint)price, state);
                Console.WriteLine(v.toString());
            }
            Console.WriteLine("</Violins>");
            cmd.Dispose();
            reader.Dispose();

            cmd = new SQLiteCommand("select * from makers", connection);
            reader = cmd.ExecuteReader();
            Console.WriteLine("<Makers>");
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var number = reader.GetString(1);
                var address = reader.GetString(2);
                var m = new Maker(name, number, address);
                Console.WriteLine(m.toString());
            }
            Console.WriteLine("</Makers>");
            cmd.Dispose();
            reader.Dispose();

            connection.Dispose();
        }
    }
}
