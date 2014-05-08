using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReposTfsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var counter = new Counter();
            Handler_I h11 = new Handler_I();
            Handler_II h2 = new Handler_II();
            counter.OnCount += h11.Message;
            counter.OnCount += h2.Message;

            counter.Count();
            Console.WriteLine("end a program");
            Console.ReadLine();
        }
    }

    public class Counter
    {
        public delegate void MethodCounter();

        public delegate string FirstDelegate(int x);

        public event MethodCounter OnCount;


        public void Count()
        {
            for (int i = 0; i < 100; i++)
            {
                if (i == 71)
                {
                    OnCount();
                    var dDelegate = new FirstDelegate(Test);
                    dDelegate.Invoke(4);
                }
            }
        }

        public string Test(int val)
        {

            Console.WriteLine("got {0}", val);
            return "777";
        }
    }

    internal class Handler_I //Это класс, реагирующий на событие (счет равен 71) записью строки в консоли.
    {
        public void Message()
        {
            //Не забудьте using System 
            //для вывода в консольном приложении
            Console.WriteLine("Пора действовать, ведь уже 71!");
        }
    }

    internal class Handler_II
    {
        public void Message()
        {
            Console.WriteLine("Точно, уже 71!");
        }
    }

}
