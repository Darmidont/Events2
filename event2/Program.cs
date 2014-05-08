using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace event2
{
    class Program
    {
        static void Main(string[] args)
        {
            var tc = new TestClass();
            var h1 = new Handler1();
            tc.MyEvent += h1.Test;
            tc.Test();

            Console.WriteLine("end a program");
            Console.ReadLine();
        }
    }

    public class TestClass
    {
        private EventHandler _myHandler;

        public event EventHandler MyEvent
        {
            add
            {
                lock (this)
                {
                    _myHandler += value;
                }
            }
            remove
            {
                lock (this)
                {
                    if (_myHandler != null && value != null)
                        _myHandler -= value;
                }
            }
        }

        public void Test()
        {
            EventHandler handler = _myHandler;
            if (null != handler)
            {
                handler(this, new EventArgs());
            }
        }
    }

    public class Handler1
    {
        public void Test(object sender, EventArgs e)
        {
            Console.WriteLine("Handler1 test");
        }
    }
}
