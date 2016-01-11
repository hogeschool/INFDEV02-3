using Assignment1;
using Assignment2;
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
      //TestAssignment1();
      //TestAssignment2(new Car(), new Truck(), new Enterprise());
      TestAssignment2Advanced(new Node<Vehicle>(new Car(), new Node<Vehicle>(new Truck(), new Node<Vehicle>(new Enterprise(), new Empty<Vehicle>()))));
    }

    private static void TestAssignment2(Vehicle v1, Vehicle v2, Vehicle v3)
    {
      v1.LoadFuel(new Gasoline(5));
      v2.LoadFuel(new Dilithium(1000));
      v3.LoadFuel(new Dilithium(3));
      for (int i = 0; v1.Move(); i++)
        Console.WriteLine("Vehicle one is still going strong after " + i + " steps");
      for (int i = 0; v2.Move(); i++)
        Console.WriteLine("Vehicle two is still going strong after " + i + " steps");
      for (int i = 0; v3.Move(); i++)
        Console.WriteLine("Vehicle three is still going strong after " + i + " steps");
    }

    private static void TestAssignment2Advanced(Assignment2.List<Vehicle> vehicles)
    {
      vehicles.Iter(v => v.LoadFuel(new Gasoline(100)));
      vehicles.Iter(v => v.LoadFuel(new Diesel(100)));
      vehicles.Iter(v => v.LoadFuel(new Dilithium(100)));
      for (int i = 0; i < 99; i++)
        vehicles.Iter(v => v.Move());
      vehicles.Iter(v => Console.WriteLine(v));
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
