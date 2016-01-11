using Assignment1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
  class Program
  {
    static void Main(string[] args)
    {
      TestAssignment1();
    }

    private static void TestAssignment1()
    {
      var c0 = new Counter();
      c0.Tick();
      var c = new Counter() + c0;
      c.OnTarget(50, () => Console.WriteLine("Reached 50!"));
      for (int i = 0; i < 100; i++)
      {
        c.Tick();
      }
      Console.WriteLine(c);
    }
  }
}
