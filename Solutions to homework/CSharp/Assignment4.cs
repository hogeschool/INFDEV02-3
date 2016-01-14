using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
  interface Entity
  {
    float positionX { get; }
    void Tick(float deltaTime);
  }

  interface Component
  {
  }

  interface Engine : Component
  {
    int Cylinders { get; }
    int HorsePower { get; }
    float turn(float fuel);
  }

  interface Gearbox : Component
  {
    float turn(float engineRPM);
  }

  interface Wheels : Component
  {
    float Radius { get; }
    float turn(float gearBoxRPM);
  }

  interface FuelTank : Component
  {
    float Capacity { get; }
    float FuelAmount { get; }
    float pumpFuel(float deltaTime);
  }

  class Auto : Entity
  {
    public float positionX { get; private set; }

    FuelTank fuelTank;
    Engine engine;
    Gearbox gearbox;
    Wheels wheels;

    public Auto(FuelTank fuelTank, Engine engine, Gearbox gearbox, Wheels wheels)
    {
      this.fuelTank = fuelTank;
      this.engine = engine;
      this.gearbox = gearbox;
      this.wheels = wheels;
    }

    void Drive(float deltaTime)
    {
      if (fuelTank.FuelAmount > 0.0f)
      {
        var fuel = fuelTank.pumpFuel(deltaTime);
        var rpm_e = engine.turn(fuel);
        var rpm_g = gearbox.turn(rpm_e);
        var distance = wheels.turn(rpm_g);
        positionX += distance;
      }
    }

    public override string ToString()
    {
      return "An auto at position " + positionX;
    }

    public void Tick(float deltaTime)
    {
      Drive(deltaTime);
    }
  }

  class Mercedes500 : Engine
  {
    public int Cylinders
    {
      get
      {
        return 12;
      }
    }

    public int HorsePower
    {
      get
      {
        return 517;
      }
    }

    public float turn(float fuel)
    {
      return 3500 * fuel;
    }
  }

  class Mercedes722Comma6 : Gearbox
  {
    public float turn(float engineRPM)
    {
      return 7.5f * engineRPM / (3500.0f * 18 * 2.54f);
    }
  }

  class Wheels18Inch : Wheels
  {
    public float Radius
    {
      get
      {
        return 18 * 2.54f;
      }
    }

    public float turn(float gearBoxRPM)
    {
      return gearBoxRPM * Radius;
    }
  }

  class LargeTank : FuelTank
  {
    public float Capacity
    {
      get
      {
        return 125;
      }
    }

    public float FuelAmount { get; set; }

    public LargeTank()
    {
      FuelAmount = Capacity;
    }

    public float pumpFuel(float deltaTime)
    {
      var amount = deltaTime;
      if (FuelAmount > amount)
      {
        FuelAmount -= amount;
        return amount;
      }
      return 0.0f;
    }
  }
}
