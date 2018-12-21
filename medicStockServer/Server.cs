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
        List<List<List<string>>> medicList = new List<List<List<string>>>();
        List<List<List<string>>> userList = new List<List<List<string>>>();
        Int32 port = 0;
        TcpListener ecoute = null;
        int i = 0;

        public Server(int p_port)
        {
            DAO dao = new DAO();
            medicList = dao.getMedicament();
            userList = dao.getUser();

            port = p_port;
            try
            {
                // on crée le service - il écoutera sur toutes les interfaces réseau de la machine
                ecoute = new TcpListener(IPAddress.Any, port);
                // on le lance
                ecoute.Start();
                // boucle de service
                TcpClient tcpClient = null;
                // boucle infinie - sera arrêtée par Ctrl-C
                while (true)
                {
                    // Attente d'un client
                    Console.WriteLine("En attente d'un client");
                    tcpClient = ecoute.AcceptTcpClient();
                    // le service est assuré par une autre tâche
                    ThreadPool.QueueUserWorkItem(Service, tcpClient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ecoute.Stop();
            }
        }

        public void Service(Object infos)
        {
            String demande = null;
            String reponse = null;
            TcpClient client = infos as TcpClient;


            try
            {
                Console.WriteLine("Client connected");
                using (NetworkStream networkStream = client.GetStream())
                {
                    using (StreamReader reader = new StreamReader(networkStream))
                    {
                        using (StreamWriter writer = new StreamWriter(networkStream))
                        {
                            writer.AutoFlush = true;
                            
                            while (demande != "exit")
                            {
                                demande = reader.ReadLine();
                                writer.WriteLine(reponse);
                                if (demande.ToLower().Equals("medic"))
                                {
                                    writer.WriteLine(reponse);

                                    foreach (List<List<string>> subList in medicList)
                                    {
                                        foreach (List<string> subSubList in subList)
                                        {
                                            foreach (string data in subSubList)
                                            {
                                                writer.WriteLine(data);
                                                i++;
                                                if (i == 3)
                                                {
                                                    writer.WriteLine("\n");
                                                    i = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (demande.ToLower().Equals("user"))
                                {
                                    foreach (List<List<string>> subList in userList)
                                    {
                                        foreach (List<string> subSubList in subList)
                                        {
                                            foreach (string data in subSubList)
                                            {
                                                writer.WriteLine(data);
                                                i++;
                                                if (i == 3)
                                                {
                                                    writer.WriteLine("\n");
                                                    i = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {}
        }
    }
}
  
