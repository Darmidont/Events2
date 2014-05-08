using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events1
{
    class Program
    {        
        static void Main(string[] args)
        {
            var counter = new Counter();
            var h1 = new Handler1();
            counter.TestEvent += h1.Test;
            counter.Validate(4);
            Console.WriteLine("end a program");
            Console.ReadLine();
        }
    }

    public class Handler1
    {
        public void Test(object sender, EventArgs e)
        {
            Console.WriteLine("test");
        }
    }

    public class Counter
    {
        public event EventHandler TestEvent;

        public void Validate(int number)
        {
            for (int i = 0; i < 100; i++)
            {
                if (number == i)
                {
                    OnTestEvent();
                }
            }
        }

        protected virtual void OnTestEvent()
        {
            EventHandler handler = TestEvent;
            if (handler != null)
                handler(this, EventArgs.Empty);

        }

    }
}
