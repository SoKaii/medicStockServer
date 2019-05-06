using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace medicStockServer
{
    class Server
    {
        DAO dao;
        Int32 port = 0; // Création d'un entier qui servira comme port de connexion au Server
        TcpListener ecoute = null; // Création d'une service d'écoute
        Random rnd = new Random();
        List<String> dataList = new List<String>();
        String dataString;
        List<String> listUpdateCommands = new List<String>();
        public Server(int p_port)
        {
            try
            {
                dao = new DAO(); // Instanciation d'une dao 
                dataList = dao.getData();
                port = p_port; // Assignation du port de connexion
            
                ecoute = new TcpListener(IPAddress.Any, p_port); // Instanciation du service d'écoute 
             
                ecoute.Start(); // Lancement du service d'écoute
                TcpClient tcpClient = null; // Création d'un client virtuel afin de gérer le threading
                while (true) // Boucle d'écoute
                {
                     // Affichage d'un message d'attente
                    tcpClient = ecoute.AcceptTcpClient(); // Acceptation du client 
                    ThreadPool.QueueUserWorkItem(Service, tcpClient); // Création d'un thread pour le client précedemment accepté 
                }
            }
            catch (Exception ex) // Si l'éxécution rencontre un problème 
            {
                Console.WriteLine(ex.Message); // Affichage du message d'érreur renvoyé 
            }
            finally // Lorsque le boucle d'écoute est arrêtée
            {
                ecoute.Stop(); // Fermeture du service d'écoute
            }
        }

        public void Service(Object infos)
        {
            String demande = null ; // Création d'une string qui servira à stocker les demandes clients
            TcpClient client = infos as TcpClient; // Création du client qui communiquera avec le serveur
            dataString = null;

            try
            {
                int clientNumber = rnd.Next(1, 999);
                Console.WriteLine("Client n° " + clientNumber + " has connected"); // Message confirmant la connexion du client au service 
                Console.WriteLine("Waiting for an other client.....");
                using (NetworkStream networkStream = client.GetStream()) // Utilisant la laison entre le client et le serveur 
                {
                    using (StreamReader reader = new StreamReader(networkStream)) // Création d'un StreamReader servant à lire les demandes du client dans le Tcp
                    {
                        using (StreamWriter writer = new StreamWriter(networkStream)) // Création d'un StreamWriter servant à écrire les réponses du serveur dans le Tcp 
                        {
                            writer.AutoFlush = true; // Définit si le writer vide sa mémoire tampon entre chaque écriture 

                            foreach (String str in dataList) // Pour chaque List dans userList
                            {
                                dataString = dataString + str; // Le server envoi au client la string précedemment ciblée
                            }
                            writer.WriteLine(dataString);
                            
                            while(true)
                            {
                                demande = reader.ReadLine();
                                if (demande == "-1")
                                    break;
                                Console.WriteLine(demande);
                                dao.update(demande.Split(';').ToList());
                                updateServer();
                                break;
                            }
                            Console.WriteLine("Client n° " + clientNumber + " has disconnected\n"); // Message confirmant la deconnexion du client
                        }
                    }
                }
            }
            catch (Exception e) // Si une erreur est renvoyé dans l'éxécution du code
            {
                Console.WriteLine(e.Message); // Affichage du message d'erreur 
            }
            finally
            {}
        }

        public void updateServer()
        {
            try
            {
                dao = new DAO(); // Instanciation d'une dao 
                dataList = dao.getData();
            }
            catch (Exception ex) // Si l'éxécution rencontre un problème 
            {
                Console.WriteLine(ex.Message); // Affichage du message d'érreur renvoyé 
            }
        }
    }
}
  
