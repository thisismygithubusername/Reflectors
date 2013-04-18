using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.MindBodyTestRunners.APITestRunner;
using ClassLibrary1.ReflectiveTestRunner;

namespace TestingWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            new ApiTestRunner().RunAllTests();
            Console.ReadLine();
        }
    }
}
