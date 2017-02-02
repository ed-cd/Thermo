using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermo
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine(Test());

            (int A, int B) Test() => (1, 2);
        }
    }
}
