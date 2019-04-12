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
using MySql.Data.MySqlClient;

namespace medicStockServer
{
    class DAO
    {
        private List<String> dataList = new List<String>(); // Initialisation d'une liste de String pour recevoir toutes les données
        public DAO()
        {
            try
            {
                int indexY = 0;
                List<String> cmdList = "Select * from user;Select* from interaction;Select* from lotmedic;Select* from medicament;".Split(';').ToList(); // Configuration de la liste de requete en fonction des requetes situées dans le App.Config

                for (int i =0; i<=cmdList.Count() -1; i++) // Pour chaque table de la dB
                {
                    MySqlConnection MyConnection = new MySqlConnection("server = localhost; database = hia; uid = medicstock; password = azerty"); // Configuration de la connexion à la base de données
                    MySqlCommand request = new MySqlCommand(cmdList[i],MyConnection); // Configuration de la requete SQL en fonction des requetes situées dans la liste de requetes
                    MyConnection.Open(); // Ouverture de la connexion
                    MySqlDataReader reader = request.ExecuteReader(); // Création du DataReader que l'on configure avec la requête precedement déclarée
                    while (reader.Read()) // Pour chaque enregistrement de l'objet ciblé 
                    {
                        for (indexY = 0; indexY < reader.FieldCount; indexY++) // Pour chaque attribut de l'enregistrement ciblé
                        {
                            dataList.Add(reader[indexY].ToString() + ","); // Stockage dans la liste "dataList" du tout le contenu de la base de données en String
                        }
                        dataList.Add(";"); // Ajout des séparations entre chaque enregistrement
                        indexY = 0; // Remise à zéro de l'index 
                    }
                    reader.Close(); // Fermeture du DataReader
                    MyConnection.Close(); // Fermeture de la connexion
                    dataList.Add("%"); // Ajout des séparations entre chaque objet
                }
            }
            catch (Exception e) // Si l'éxécution du code rencontre un problème bloquant
            {
                Console.WriteLine(e.Message); // Affichage du message d'erreur renvoyé 
            }
        }

        public List<String> getData() // Methode de récuperation de la liste contenant toute la dB
        {
            return dataList;
        }

        public void update(List<String> commandsList)
        {

        }
    }
}
