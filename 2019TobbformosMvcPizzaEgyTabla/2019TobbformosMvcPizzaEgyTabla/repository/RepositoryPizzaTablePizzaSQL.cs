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
        public List<Pizza> getPizzasFromDatabaseTable()
        {
            List<Pizza> pizzas = new List<Pizza>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = Pizza.getSQLCommandGetAllRecord();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["pnev"].ToString();
                    bool goodResult = false;
                    int id = -1;
                    goodResult = int.TryParse(dr["pazon"].ToString(), out id);
                    if (goodResult)
                    {
                        int price = -1;
                        goodResult = int.TryParse(dr["par"].ToString(), out price);
                        if (goodResult)
                        {
                            Pizza p = new Pizza(id, name, price);
                            pizzas.Add(p);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Pizzaadatok beolvasása az adatbázisból nem sikerült!");
            }
            return pizzas;
        }

        public List<Futar> getFutarokFromDatabaseTable()
        {
            List<Futar> futarok = new List<Futar>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = Futar.getSQLCommandGetAllRecord();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["nev"].ToString();
                    string address = dr["lakcim"].ToString();
                    string phonenumber = dr["telefonszam"].ToString();
                    string email = dr["email"].ToString();
                    bool goodResult = false;
                    int id = -1;
                    goodResult = int.TryParse(dr["id"].ToString(), out id);
                    if (goodResult)
                    {
                            Futar f = new Futar(id, name, address, phonenumber, email);
                            futarok.Add(f);
                    }
                }
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Futár adatok beolvasása az adatbázisból nem sikerült!");
            }
            return futarok;
        }

        public void deletePizzaFromDatabase(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "DELETE FROM ppizza WHERE pazon=" + id;
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(id + " idéjű pizza törlése nem sikerült.");
                throw new RepositoryException("Sikertelen törlés az adatbázisból.");
            }
        }

        public void deleteFutarFromDatabase(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "DELETE FROM futarok WHERE id=" + id;
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(id + " idéjű futár törlése nem sikerült.");
                throw new RepositoryException("Sikertelen törlés az adatbázisból.");
            }
        }

        public void updatePizzaInDatabase(int id, Pizza modified)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = modified.getUpdate(id);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(id + " idéjű pizza módosítása nem sikerült.");
                throw new RepositoryException("Sikertelen módosítás az adatbázisból.");
            }
        }

        public void updateFutarInDatabase(int id, Futar modified)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = modified.getUpdateFutar(id);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(id + " idéjű futár módosítása nem sikerült.");
                throw new RepositoryException("Sikertelen módosítás az adatbázisból.");
            }
        }

        public void insertPizzaToDatabase(Pizza ujPizza)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = ujPizza.getInsert();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(ujPizza + " pizza beszúrása adatbázisba nem sikerült.");
                throw new RepositoryException("Sikertelen beszúrás az adatbázisból.");
            }
        }

        public void insertFutarToDatabase(Futar ujFutar)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = ujFutar.getInsertFutar();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(ujFutar + " futár beszúrása adatbázisba nem sikerült.");
                throw new RepositoryException("Sikertelen beszúrás az adatbázisból.");
            }
        }
    }
}
