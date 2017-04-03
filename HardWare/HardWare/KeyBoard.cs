using System;
using System.Text;
using System.Threading.Tasks;

namespace HardWare
{
    class KeyBoard
    {
        public string Buffer { set; get; }

        public string Input()
        {
            Buffer=Console.ReadLine();
            return Buffer;
        }

    }
}
