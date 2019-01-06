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

namespace medicStockServer
{
   
    class DAO
    {
        private string connexion_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Desktop\Git\medicStockServer\medicStockServer\bin\Debug\DatabaseHIA.mdf;Integrated Security=True"; // Création d'un string permettant d'ouvrir la dB avec des parametres prédéfinis 
        
        List<List<string>> attributsMedic = new List<List<string>>(); // Création de la liste récupérant les attributs de chaque médicament
        List<List<string>> attributsUser = new List<List<string>>(); // Création de la liste récupérant les attributs de chaque utilisateur

        List<List<List<string>>> medicaments = new List<List<List<string>>>(); // Création de la liste récupérant les médicaments
        List<List<List<string>>> users = new List<List<List<string>>>(); // Création de la liste récupérant les utilisateurs
        public DAO()
        {
            int indexY = 0; // Création d'un index permettant de naviguer dans les dans le DataReader afin de récupérer chaque attribut

            try
            {
                SqlConnection MyConnection = new SqlConnection(connexion_string); // Ouverture d'une connexion à la dB avec la connexion_string en parametres
                MyConnection.Open(); // Activation de la connexion

                SqlCommand cmdMedic = new SqlCommand("Select * from Medicament",MyConnection); // Création de la requête SQL de récupération des médicaments et liaison à la dB
                SqlDataReader readerM = cmdMedic.ExecuteReader(); // Création du DataReader que l'on configure avec la requête precedement déclarée

                while (readerM.Read()) // Tant que le DataReader voit quelque chose ( Navigation sur chaque médicament de la dB)
                {
                    for (indexY = 0; indexY < 12; indexY++) // Pour chaque attribut du médicament ciblé
                    {
                        attributsMedic.Add(new List<string> { readerM[indexY].GetType().ToString(), readerM.GetName(indexY), readerM[indexY].ToString() }); // Stockage dans la liste "attributs" du Type, du nom de champ et de la valeur du champ ciblé
                    }
                    medicaments.Add(attributsMedic); // Ajout des attributs d'un médicament dans la liste de médicaments
                    indexY = 0; // Remise à zéro de l'index 
                }
                readerM.Close(); // Fermeture du DataReader
                MyConnection.Close(); // Fermeture de la dB

                

            MyConnection.Close();
            }
            catch (Exception e) // Si l'éxécution du code rencontre un problème bloquant
            {
                Console.WriteLine(e.Message); // Affichage du message d'erreur renvoyé 
            }

            try
            {
                SqlConnection MyConnection = new SqlConnection(connexion_string); // Ouverture d'une connexion à la dB avec la connexion_string en parametres
                MyConnection.Open(); // Activation de la connexion

                SqlCommand cmdUser = new SqlCommand("Select * from Utilisateur", MyConnection); // Création de la requête SQL de récupération des utilisateurs et liaison à la dB
                SqlDataReader readerU = cmdUser.ExecuteReader(); // Création du DataReader que l'on configure avec la requête precedement déclarée

                while (readerU.Read()) // Tant que le DataReader voit quelque chose ( Navigation sur chaque médicament de la dB)
                {
                    for (indexY = 0; indexY < 5; indexY++) // Pour chaque attribut de l'utilisateur ciblé
                    {
                        attributsUser.Add(new List<string> { readerU[indexY].GetType().ToString(), readerU.GetName(indexY), readerU[indexY].ToString() }); // Stockage dans la liste "attributs" du Type, du nom de champ et de la valeur du champ ciblé
                    }
                    users.Add(attributsUser); // Ajout des attributs d'un utilisateur dans la liste d'utilisateurs
                    indexY = 0; // Remise à zéro de l'index
                }
                readerU.Close(); // Fermeture du DataReader
                MyConnection.Close(); // Fermeture de la dB
            }
            catch (Exception e) // Si l'éxécution du code rencontre un problème bloquant
            {
                Console.WriteLine(e.Message); // Affichage du message d'erreur renvoyé 
            }

        }

        public List<List<List<string>>> getMedicament() // Méthode de récupération de la liste globale médicaments
        {
            return medicaments;
        }

        public List<List<List<string>>> getUser() // Méthode de récupération de la liste globale utilisateurs
        {
            return users;
        }

        
    }
}
