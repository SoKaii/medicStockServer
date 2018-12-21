using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicStockServer
{
    class Server
    {
        public Server()
        {
            DAO dao = new DAO();
            Console.WriteLine("--------------------------------------------------MEDICAMENTS--------------------------------------------------");
            print_Data(dao.getMedicament());
            Console.WriteLine("--------------------------------------------------USERS--------------------------------------------------");
            print_Data(dao.getUser());
        }

        public void print_Data(List<List<List<string>>> dataList)
        {
            int i = 0;

            foreach (List<List<string>> subList in dataList)
            {
                foreach (List<string> subSubList in subList)
                {
                    foreach (string data in subSubList)
                    {
                        Console.WriteLine(data);
                        i++;
                        if (i == 3)
                        {
                            Console.WriteLine("\n");
                            i = 0;
                        }
                    }
                }
            }
        }
    }
}
