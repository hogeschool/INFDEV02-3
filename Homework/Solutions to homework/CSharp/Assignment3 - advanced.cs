using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3Advanced
{
  class Timer
  {
    private Action onReached;
    private float targetTime;
    private float elapsedTime;

    public Timer(Action onReached, float targetTime)
    {
      this.elapsedTime = 0.0f;
      this.onReached = onReached;
      this.targetTime = targetTime;
    }

    public void Tick(float deltaTime)
    {
      elapsedTime += deltaTime;
      if (elapsedTime >= targetTime)
        onReached();
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
      toNextLight = new Timer(FromGreenToYellow, 6.0f);
    }

    void FromGreenToYellow()
    {
      color = Color.Yellow;
      toNextLight = new Timer(FromYellowToRed, 6.0f);
    }

    void FromYellowToRed()
    {
      color = Color.Red;
      toNextLight = new Timer(FromRedToGreen, 6.0f);
    }

    void FromRedToGreen()
    {
      color = Color.Green;
      toNextLight = new Timer(FromGreenToYellow, 6.0f);
    }
  }
}
