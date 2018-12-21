using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;

namespace Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            string connexion_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\sokaii\source\repos\medicStockServer\medicStockServer\DatabaseHIA.mdf;Integrated Security=True"; // Création d'un string permettant d'ouvrir la dB avec des parametres prédéfinis 
            List<List<string>> attributsUser = new List<List<string>>();
            List<List<List<string>>> users = new List<List<List<string>>>();
            int indexY = 0;
            try
            {
                SqlConnection MyConnection = new SqlConnection(connexion_string); // Ouverture d'une connexion à la dB avec la connexion_string en parametres
                MyConnection.Open(); // Activation de la connexion

                SqlCommand cmdUser = new SqlCommand("Select * from User", MyConnection);
                SqlDataReader readerU = cmdUser.ExecuteReader();

                while (readerU.Read()) // Tant que le reader voit quelque chose 
                {
                    for (indexY = 0; indexY < 5; indexY++)
                    {
                        attributsUser.Add(new List<string> { readerU[indexY].GetType().ToString(), readerU.GetName(indexY), readerU[indexY].ToString() });
                    }
                    users.Add(attributsUser);
                    indexY = 0;
                }
                readerU.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
