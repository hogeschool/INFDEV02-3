using Assignment1;
using Assignment2;
using Assignment3;
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
      //TestAssignment2Advanced(new Node<Vehicle>(new Car(), new Node<Vehicle>(new Truck(), new Node<Vehicle>(new Enterprise(), new Empty<Vehicle>()))));
      //TestAssignment3();
      //TestAssignment3Advanced();
      //TestAssignment4();
      TestExtraAssignments();
    }

    private static void TestExtraAssignments()
    {
      //// extra assignment 2
      //ExtraHomework.Extra2.Program.SumInterval();

      //// extra assignment 3
      //ExtraHomework.Extra3.Interval i = new ExtraHomework.Extra3.Interval(1, 4);
      //Console.WriteLine(i.Sum);
      //Console.WriteLine(i.Product);
      
      ////Interfaces 
      // Exercise 0
      //TestInterfaceExercise0()
      
      ////Integer list 
      // Exercise 3
      //TestInterfaceExercise3()
      
        
      // extra assignments 6, 7, 8, 9
      //Console.WriteLine(line(new ExtraHomework.Extra6789.IntNumber(), 5, 2, -1));
      //Console.WriteLine(line(new ExtraHomework.Extra6789.FloatNumber(), 5.0f, 0.5f, -1.0f));

      // extra assignment 10
      ExtraHomework.Extra10.Interval<float> j = new ExtraHomework.Extra10.Interval<float>(new ExtraHomework.Extra6789.FloatNumber(), 0.5f, 3.5f);
      Console.WriteLine(j.Sum);
      Console.WriteLine(j.Product);
    }
    

    static N line<N>(ExtraHomework.Extra6789.Number<N> n, N x, N a, N b)
    {
      return n.Plus(n.Times(x, a), b);
    }

    private static void TestAssignment4()
    {
      var auto = new Auto(new LargeTank(), new Mercedes500(), 
        new Mercedes722Comma6(), new Wheels18Inch());
      while(true)
      {
        auto.Tick(1.0f);
        Console.WriteLine(auto);
        Console.ReadLine();
      }
    }

    private static void TestAssignment3Advanced()
    {
      var t = new Assignment3Advanced.TrafficLight();
      while (true)
      {
        t.Tick(1.0f);
        Console.WriteLine(t);
        Console.ReadLine();
      }
    }

    private static void TestAssignment3()
    {
      var t = new Assignment3.TrafficLight();
      while (true)
      {
        t.Tick(1.0f);
        Console.WriteLine(t);
        Console.ReadLine();
      }
    }
    
    static void TestInterfaceExercise0(){
            
            Animal animal1 = new Cat ();
			animal1.SaySomething ();

			Animal animal2 = new Dog ();
			animal2.SaySomething ();

			Animal animal3 = new Cow ();
			animal3.SaySomething ();
        
    }
    
    static void TestInterfaceExercise3(){
			ListInt ls = new NodeInt(10, new NodeInt(4, new NodeInt (5, new NodeInt (3, new EmptyInt ()))));
			ListInt lsFiltered = ls.Filter ((x) => x % 2 == 0);
			lsFiltered.Iter((x)=> Console.WriteLine(x));
//			Console.WriteLine (ls.Length);
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
