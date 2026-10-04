using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uzenetkuldo.Interfaces;
using Uzenetkuldo.Models;

namespace Uzenetkuldo.Services
{
    public class UzenetService : ICRUD
    {
        public string Create(Tablazat rekord)
        {
            try
            {
                string kapcsolatiAdat = "SERVER = localhost;" +
                              "DATABASE= uzenetkuldo;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = kapcsolatiAdat;
                conn.Open();
                string sql = "INSERT INTO uzenet(Szoveg, KüldesiIdo, UzenetTipus, Telefon, Email) VALUES (@p_szoveg,@p_kuldesiido,@p_uzenettipus,@p_telefon,@p_email)";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@p_szoveg", (rekord as Uzenet).Szoveg);
                cmd.Parameters.AddWithValue("@p_kuldesiido", (rekord as Uzenet).KuldesiIdo);
                cmd.Parameters.AddWithValue("@p_uzenettipus", (rekord as Uzenet).UzenetTipus);
                if ((rekord as Uzenet).UzenetTipus == "SMS")
                {
                    cmd.Parameters.AddWithValue("@p_email", null);
                    cmd.Parameters.AddWithValue("@p_telefon", (rekord as Uzenet).Telefon);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@p_telefon", null);
                    cmd.Parameters.AddWithValue("@p_email", (rekord as Uzenet).Email);
                }
                int erintettSorok = cmd.ExecuteNonQuery();
                conn.Close();

                return erintettSorok > 0 ? "Az üzenet mentése rendben megtörtént" : "Az üzenetet nem sikerült elmenteni";
            }
            catch (Exception ex)
            {
                return Convert.ToString(ex);
            }
        }

        public string Delete(int id)
        {
            string kapcsolatiAdat = "SERVER = localhost;" +
                        "DATABASE= uzenetkuldo;" +
                        "UID = root;" +
                        "PASSWORD =;";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = kapcsolatiAdat;
            conn.Open();
            string sql = "DELETE FROM uzenet WHERE Id=@id";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@id", id);
            int erintettSorok = cmd.ExecuteNonQuery();
            conn.Close();

            return erintettSorok > 0 ? "Az üzenet eltávolítása megtörtént" : "Az üzenetet nem sikerült eltávolítani";
        }

        public List<Tablazat> Read()
        {
           
                List<Tablazat> rekordok = new List<Tablazat>();

                string kapcsolatiAdat = "SERVER = localhost;" +
                              "DATABASE= uzenetkuldo;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = kapcsolatiAdat;
                conn.Open();
                string sql = "SELECT * FROM uzenet";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Az aktuális sor átalakítása üzenet objektummá
                    Uzenet beolvasott = new Uzenet();
                    beolvasott.Id = reader.GetInt32("Id");
                    beolvasott.Szoveg = reader.GetString("Szoveg");
                    beolvasott.KuldesiIdo = reader.GetDateTime("KüldesiIdo");
                    beolvasott.UzenetTipus = reader.GetString("UzenetTipus");
                if (beolvasott.UzenetTipus == "SMS")
                {
                    beolvasott.Telefon = reader.GetString("Telefon");
                }
                else
                {
                    beolvasott.Email = reader.GetString("Email");
                }
                    rekordok.Add(beolvasott);
                }
                conn.Close();

                return rekordok;
            
            

        }

        public bool LetezikId(int id)
        {
            string kapcsolatiAdat = "SERVER = localhost;" +
                        "DATABASE= uzenetkuldo;" +
                        "UID = root;" +
                        "PASSWORD =;";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = kapcsolatiAdat;
            conn.Open();
            string sql = "SELECT Id FROM uzenet WHERE Id=@id";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            bool vanTalalat = reader.HasRows;
            conn.Close();

            return vanTalalat;
        }

        public string Update(Tablazat rekord)
        {
            string kapcsolatiAdat = "SERVER = localhost;" +
                        "DATABASE= uzenetkuldo;" +
                        "UID = root;" +
                        "PASSWORD =;";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = kapcsolatiAdat;
            conn.Open();
            string sql = "UPDATE uzenet SET Szoveg=@p_szoveg, KüldesiIdo=@p_kuldesiido, UzenetTipus=@p_uzenettipus, Telefon=@p_telefon, Email=@p_email WHERE Id=@id";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@p_szoveg", (rekord as Uzenet).Szoveg);
            cmd.Parameters.AddWithValue("@p_kuldesiido", (rekord as Uzenet).KuldesiIdo);
            cmd.Parameters.AddWithValue("@p_uzenettipus", (rekord as Uzenet).UzenetTipus);
            if ((rekord as Uzenet).UzenetTipus == "SMS")
            {
                cmd.Parameters.AddWithValue("@p_email", null);
                cmd.Parameters.AddWithValue("@p_telefon", (rekord as Uzenet).Telefon);
            }
            else
            {
                cmd.Parameters.AddWithValue("@p_telefon", null);
                cmd.Parameters.AddWithValue("@p_email", (rekord as Uzenet).Email);
            }
            cmd.Parameters.AddWithValue("@id", (rekord as Uzenet).Id);
            int erintettSorok = cmd.ExecuteNonQuery();
            conn.Close();

            return erintettSorok > 0 ? "Az üzenet módosítása megtörtént" : "Az üzenetet nem sikerült módosítani";
        }
    }
}