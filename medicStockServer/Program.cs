﻿using System;
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
            Server server = new Server(22);
            System.Threading.Thread.Sleep(10000);
         }
    }
}
