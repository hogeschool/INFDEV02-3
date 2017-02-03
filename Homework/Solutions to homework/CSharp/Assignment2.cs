using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
  interface Fuel { int Amount { get; } }

  class Gasoline : Fuel {
    public Gasoline(int amount) { Amount = amount; }
    public int Amount { get; set; }
  }
  class Diesel : Fuel {
    public Diesel(int amount) { Amount = amount; }
    public int Amount { get; set; }
  }
  class Dilithium : Fuel {
    public Dilithium(int amount) { Amount = amount; }
    public int Amount { get; set; }
  }

  interface Vehicle
  {
    bool Move();
    bool LoadFuel(Fuel fuel);
  }

  class Car : Vehicle
  {
    Gasoline tank = new Gasoline(0);

    public bool LoadFuel(Fuel fuel)
    {
      if (!(fuel is Gasoline))
        return false;
      tank = fuel as Gasoline;
      return true;
    }

    public bool Move()
    {
      if (tank.Amount > 0)
      {
        tank.Amount--;
        return true;
      }
      return false;
    }

    public override string ToString()
    {
      return "This is a Car. Amount of fuel: " + tank.Amount;
    }
  }

  class Truck : Vehicle
  {
    Diesel tank = new Diesel(0);

    public bool LoadFuel(Fuel fuel)
    {
      if (!(fuel is Diesel))
        return false;
      tank = fuel as Diesel;
      return true;
    }

    public bool Move()
    {
      if (tank.Amount > 0)
      {
        tank.Amount--;
        return true;
      }
      return false;
    }
    public override string ToString()
    {
      return "This is a Truck. Amount of fuel: " + tank.Amount;
    }
  }

  class Enterprise : Vehicle
  {
    Dilithium tank = new Dilithium(0);

    public bool LoadFuel(Fuel fuel)
    {
      if (!(fuel is Dilithium))
        return false;
      tank = fuel as Dilithium;
      return true;
    }

    public bool Move()
    {
      if (tank.Amount > 0)
      {
        tank.Amount--;
        return true;
      }
      return false;
    }

    public override string ToString()
    {
      return "This is an Enterprise. Amount of fuel: " + tank.Amount;
    }
  }
}
