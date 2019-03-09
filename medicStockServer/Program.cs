using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicStockServer
{
    class Program
    { 
         static void Main()
         {
            //Server server = new Server(22);
            DAO dao = new DAO();
            dao.setMedicStock("Doliprane", 63);
           Console.ReadLine();
         }
    }
}
