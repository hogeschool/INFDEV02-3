using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
  interface Event
  {
    void Perform();
  }

  class Timer
  {
    private Event onReached;
    private float targetTime;
    private float elapsedTime;

    public Timer(Event onReached, float targetTime)
    {
      this.elapsedTime = 0.0f;
      this.onReached = onReached;
      this.targetTime = targetTime;
    }

    public void Tick(float deltaTime)
    {
      elapsedTime += deltaTime;
      if (elapsedTime >= targetTime)
        onReached.Perform();
    }

    public override string ToString()
    {
      return elapsedTime + "/" + targetTime;
    }
  }

  enum Color
  {
    Red, Green, Yellow
  }

  class TrafficLight
  {
    Color color;
    Timer toNextLight;

    public override string ToString()
    {
      return color + " for " + toNextLight;
    }

    public void Tick(float deltaTime)
    {
      toNextLight.Tick(deltaTime);
    }

    public TrafficLight()
    {
      color = Color.Green;
      toNextLight = new Timer(new FromGreenToYellow(this), 6.0f);
    }
    class FromGreenToYellow : Event
    {
      TrafficLight context;
      public FromGreenToYellow(TrafficLight ctxt)
      {
        context = ctxt;
      }

      public void Perform()
      {
        context.color = Color.Yellow;
        context.toNextLight = new Timer(new FromYellowToRed(context), 3.0f);
      }
    }
    class FromYellowToRed : Event
    {
      TrafficLight context;
      public FromYellowToRed(TrafficLight ctxt)
      {
        context = ctxt;
      }

      public void Perform()
      {
        context.color = Color.Red;
        context.toNextLight = new Timer(new FromRedToGreen(context), 10.0f);
      }
    }
    class FromRedToGreen : Event
    {
      TrafficLight context;
      public FromRedToGreen(TrafficLight ctxt)
      {
        context = ctxt;
      }

      public void Perform()
      {
        context.color = Color.Green;
        context.toNextLight = new Timer(new FromGreenToYellow(context), 6);
      }
    }
  }
}
