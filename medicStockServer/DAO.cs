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
                List<String> cmdList = ConfigurationManager.AppSettings["allCommands"].Split(';').ToList(); // Configuration de la liste de requete en fonction des requetes situées dans le App.Config

                for (int i =0; i<=cmdList.Count() -1; i++) // Pour chaque table de la dB
                {
                    MySqlConnection MyConnection = new MySqlConnection("server = localhost; database = hia; uid = medicstock; password = azerty"); // Configuration de la connexion à la base de données
                    MySqlCommand request = new MySqlCommand(cmdList[i],MyConnection); // Configuration de la requete SQL en fonction des requetes situées dans la liste de requetes
                    MyConnection.Open(); // Ouverture de la connexion
                    MySqlDataReader reader = request.ExecuteReader(); // Création du DataReader que l'on configure avec la requête precedement déclarée
                    while (reader.Read()) // Pour chaque enregistrement de l'objet ciblé 
                    {
                        for (indexY = 0; indexY < reader.FieldCount -1; indexY++) // Pour chaque attribut de l'enregistrement ciblé
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

        public List<String> get_data() // Methode de récuperation de la liste contenant toute la dB
        {
            return dataList;
        }

        public void createMedic(String p_idMedic, String p_nom, String p_categorie, String p_substanceActive,
        String p_formeGalenique, int p_dosage, String p_localisation, int p_elevation) // Méthode de création d'un médicament dans la base de données
        {
     
            try
            {
                
                string connString = "server = localhost; database = hia; uid = medicstock; password = azerty"; // Création de la chaine de connexion 
                MySqlConnection MyConnection = new MySqlConnection(connString); // Création de la connexion avec la chaine 

                MySqlCommand MyCommand = MyConnection.CreateCommand(); // Initialisation de la commande SQL
                MyCommand.CommandText = "INSERT INTO hia.medicament(idMedicament,nom,categorie,substanceActive,formeGalenique,dosage,Localisation,Elevation) VALUES " +
                    "(@idMedicament, @nom, @categorie, @substanceActive, @formeGalenique, @dosage, @Localisation, @Elevation)";
                MyCommand.Parameters.AddWithValue("@idMedicament", p_idMedic);
                MyCommand.Parameters.AddWithValue("@nom", p_nom);
                MyCommand.Parameters.AddWithValue("@categorie", p_categorie);
                MyCommand.Parameters.AddWithValue("@substanceActive", p_substanceActive);
                MyCommand.Parameters.AddWithValue("@formeGalenique", p_formeGalenique);
                MyCommand.Parameters.AddWithValue("@dosage", p_dosage);
                MyCommand.Parameters.AddWithValue("@Localisation", p_localisation);
                MyCommand.Parameters.AddWithValue("@Elevation", p_elevation);

                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
                MyConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void setMedicStock(String p_medicName, int p_quantite)
        {
            string connString = "server = localhost; database = hia; uid = medicstock; password = azerty"; // Création de la chaine de connexion 
            try
            {
                MySqlConnection MyConnection = new MySqlConnection(connString); // Création de la connexion avec la chaine 

                MySqlCommand MyCommand = MyConnection.CreateCommand();
                MyCommand.CommandText = "UPDATE hia.boitemedic SET stock = " + p_quantite + " WHERE boitemedic.idMedicament = medicament.idMedicament AND medicament.nom = " + p_medicName;
                MyCommand.ExecuteNonQuery();

                MyConnection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
