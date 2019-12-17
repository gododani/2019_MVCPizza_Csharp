using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TobbbformosPizzaAlkalmazasEgyTabla.Model;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TobbbformosPizzaAlkalmazasEgyTabla.Repository
{
    partial class RepositoryDatabaseTablePizza
    {
        public void fillPizzasWithTestDataFromSQLCommand()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                string query =
                    "INSERT INTO `ppizza` (`pazon`, `pnev`, `par`) VALUES " +
                            " (1, 'Capricciosa', 900), " +
                            " (2, 'Frutti di Mare', 1100), " +
                            " (3, 'Hawaii', 780), " +
                            " (4, 'Vesuvio', 890), " +
                            " (5, 'Sorrento', 990); ";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Tesztadatok feltöltése sikertelen.");
            }
        }
        public void fillFutarokWithTestDataFromSQLCommand()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                string query =
                    "INSERT INTO `futarok` (`id`, `nev`, `lakcim`, `telefonszam`, `email`) VALUES " +
                            " (1, 'Dániel', 'Szeged, Valami utca 5.', '06305776901', 'valami@gmail.com'), " +
                            " (2, 'Norbert', 'Szeged, Valami utca 6.', '06206554123', 'valami2@gmail.com'), " +
                            " (3, 'Péter', 'Szeged, Valami utca 7.', '06301447896', 'valami3@gmail.com'), " +
                            " (4, 'Ferenc', 'Szeged, Valami utca 8.', '06203974165', 'valami4@gmail.com'), " +
                            " (5, 'Béla', 'Szeged, Valami utca 9.', '06304771424', 'valami5@gmail.com')";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Tesztadatok feltöltése sikertelen!");
            }
        }
    }
}
