using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HardWare
{
    
    class main
    {
        public static volatile  int a = 10000;

        public static void threadi_funkcia()
        {
            while (true)
            {
                a--;
                Console.WriteLine(a);
            }
        }

       public  static void Main(string[] args)
        {
            ThreadStart del = new ThreadStart(threadi_funkcia);

            Thread ob = new Thread(del);

            ob.Start();

            while (true)
            {
                if (a == 0)
                {
                    ob.Abort();
                    break;
                }
            }
            ob.Join();//esi petqa grvi main i verchum,vor main @ blok @ngni minchev thread@ prcni
           













            Console.ReadKey();

        }
    }
}
