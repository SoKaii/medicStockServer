using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicStockServer
{
    class Program
    { 
         /* public void print_dB(DAO dao)
        {
        Console.WriteLine("--------------------------------------------------MEDICAMENTS--------------------------------------------------");
        print_dBTable(dao.getMedicament());
        Console.WriteLine("--------------------------------------------------USERS--------------------------------------------------");
        print_dBTable(dao.getUser());
        } */



         static void Main()
         {
            //Server server = new Server(22);
            DAO dao = new DAO();
            dao.setMedicStock("Doliprane", 63);
           Console.ReadLine();
         }
    }
}
