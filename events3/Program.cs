using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace events3
{

    internal delegate int SampleDelegate(string str);
    class Program
    {
        static void Main(string[] args)
        {
            SampleDelegate s1 = CountCharacters;
            SampleDelegate s2 = Parse;

            AsyncCallback callback = DisplayResult;

            s1.BeginInvoke("hello", callback, "Счётчик вернул '{0}' в потоке с ID = {1}");
            s2.BeginInvoke("10", callback, "Парсер вернул '{0}' в потоке с ID = {1}");

            Console.WriteLine("Основной поток с  ID = {0} продолжает выполняться",
                 Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(3000);
            //IAsyncResult ar1 = s1.BeginInvoke("ura fmkfms ", null, null);
            //IAsyncResult ar2 = s2.BeginInvoke("100", null, null);
            Console.WriteLine("here");

            //Console.WriteLine("Счётчик вернул '{0}'", s1.EndInvoke(ar1));
            //Console.WriteLine("Парсер вернул '{0}'", s2.EndInvoke(ar2));




            Console.WriteLine("end a program");
            Console.ReadLine();
        }

        static void DisplayResult(IAsyncResult result)
        {
            string format = (string)result.AsyncState;
            AsyncResult delegateResult = (AsyncResult)result;
            SampleDelegate delegateInstance =
                 (SampleDelegate)delegateResult.AsyncDelegate;
            Int32 methodResult = delegateInstance.EndInvoke(result);
            Console.WriteLine(format, methodResult, Thread.CurrentThread.ManagedThreadId);
        }

        static int CountCharacters(string str)
        {
            Thread.Sleep(2000);
            var lngth = str.Length;
            Console.WriteLine("{0}  {1}", str, lngth);
            return lngth;
        }

        static int Parse(string str)
        {
            Thread.Sleep(100);
            Console.WriteLine("Парсинг строки '{0}' в потоке с ID = {1}",
                 str, Thread.CurrentThread.ManagedThreadId);
            return int.Parse(str);
        }
    }
}
