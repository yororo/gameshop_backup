using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            app.GetUserToken();

            System.Console.ReadKey();
        }
    }
}
