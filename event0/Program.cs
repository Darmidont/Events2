using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace event0
{
    public delegate int TestDelegate(string str);
    class Program
    {
        static void Main(string[] args)
        {

            TestDelegate td1 = TestClass.CalculateString;
            TestDelegate td2 = TestClass.Parsing;
            AsyncCallback ac = TestClass.DisplayResult;

            td1.BeginInvoke("this is a very long string", ac, "Счётчик вернул '{0}' в потоке с ID = {1}");
            td2.BeginInvoke("100", ac, "Parser вернул '{0}' в потоке с ID = {1}");

            //Console.WriteLine("Основной поток с  ID = {0} продолжает выполняться",
            //    Thread.CurrentThread.ManagedThreadId);

            //Thread.Sleep(3000);

            Console.WriteLine("end a program");
            Console.ReadLine();
        }
    }

    public class TestClass
    {
        public static int CalculateString(string str)
        {
            Thread.Sleep(2000);
            var lngth = str.Length;
            Console.WriteLine("get string length '{0}' в потоке с ID = {1}",
                str, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("{0}  {1}", str, lngth);
            return lngth;
        }

        public static int Parsing(string str)
        {
            Thread.Sleep(1000);
            int val;
            Console.WriteLine("Парсинг строки '{0}' в потоке с ID = {1}",
                str, Thread.CurrentThread.ManagedThreadId);
            int.TryParse(str, out val);


            return val;
        }

       public static void DisplayResult(IAsyncResult result)
       {
           Console.WriteLine("display result");
           string format = (string)result.AsyncState;
           AsyncResult ar = (AsyncResult)result;
           TestDelegate delegateInstance =
                 (TestDelegate)ar.AsyncDelegate;
           Int32 methodResult = delegateInstance.EndInvoke(result);
           Console.WriteLine(format, methodResult, Thread.CurrentThread.ManagedThreadId);
       }
    }
}
