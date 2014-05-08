using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncResultConsole
{
    public delegate int TestDelegate(string str);
    class Program
    {
        static void Main(string[] args)
        {
            TestDelegate td1 = TestDelegateClass.CalculateString;
            TestDelegate td2 = TestDelegateClass.ParseString;

            var ar1 = td1.BeginInvoke("this is a very long string", null, null);
            var ar2 = td2.BeginInvoke("this is a very long string", null, null);

            //td2.BeginInvoke("100", null, null);

            Console.WriteLine(td1.EndInvoke(ar1));
            Console.WriteLine(td2.EndInvoke(ar2));

            Console.WriteLine("end a program");
            Console.ReadLine();
        }
    }

    public class TestDelegateClass
    {
        public static int CalculateString(string str)
        {
            Thread.Sleep(2000);
            var lngth = str.Length;
            Console.WriteLine("Length of string {0} is {1}", str, lngth);
            return lngth;
        }

        public static int ParseString(string str)
        {
            int val;
            Thread.Sleep(1000);
            int.TryParse(str, out val);
            return val;
        }
    }
}
