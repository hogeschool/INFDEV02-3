using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentComplete
{
  public class Done : IStateMachine
  {
    public Done(string message)
    {
      this.message = message;
    }
    string message;
    bool busy = true;
    public bool Busy
    {
      get
      {
        return busy;
      }

      set
      {
        busy = value;
      }
    }

    public void Update(float dt)
    {
      Busy = false;
    }

    public void Reset()
    {
      Busy = true;
    }
  }
  public class Print : IStateMachine
  {
    public Print(string message)
    {
      this.message = message;
    }
    string message;
    bool busy = true;
    public bool Busy
    {
      get
      {
        return busy;
      }

      set
      {
        busy = value;
      }
    }

    public void Update(float dt)
    {
      Console.WriteLine(message);
      Busy = false;
    }

    public void Reset()
    {
      Busy = true;
    }
  }
  public class Timer : IStateMachine
  {
    float initial_time;
    public Timer(float time)
    {
      this.time = time;
      initial_time = time;
    }
    float time;
    bool busy = true;
    public bool Busy
    {
      get
      {
        return busy;
      }

      set
      {
        busy = value;
      }
    }

    public void Update(float dt)
    {
      time -= dt;
      if (time <= 0)
        Busy = false;
    }

    public void Reset()
    {
      time = initial_time;
      Busy = true;
    }
  }

  public class Wait : IStateMachine
  {    
    public Wait(Func<bool> condition)
    {
      this.condition = condition;
    }
    Func<bool> condition;
    bool busy = true;
    public bool Busy
    {
      get
      {
        return busy;
      }

      set
      {
        busy = value;
      }
    }

    public void Update(float dt)
    {
      if (condition.Invoke())
        Busy = false;
    }

    public void Reset()
    {
      Busy = true;
    }
  }

  public class Seq : IStateMachine
  {
    IStateMachine current, next;
    IStateMachine s1, s2;

    public Seq(IStateMachine s1, IStateMachine s2)
    {
      current = s1;
      next = s2;
      this.s1 = s1;
      this.s2 = s2;
    }
    bool busy = true;
    public bool Busy
    {
      get
      {
        return busy;
      }

      set
      {
        busy = value;
      }
    }

    public void Update(float dt)
    {
      if (Busy == true)
      {
        current.Update(dt);
        if (current.Busy == false)
          current = next;
        if (next.Busy == false)
          Busy = false;
      }
    }

    public void Reset()
    {
      s1.Reset();
      s2.Reset();
      current = s1;
      Busy = true;
    }
  }
  public class Repeat : IStateMachine
  {
    IStateMachine action;
    public Repeat(IStateMachine action)
    {
      this.action = action;
    }
    public bool Busy
    {
      get
      {
        return true;
      }
      
    }

    public void Reset()
    {
      action.Reset();
    }

    public void Update(float dt)
    {
      if (action.Busy == false)
      {
        Reset();
      }
      action.Update(dt);
    }
  }

 

  public class Call : IStateMachine
  {
    public Call(IAction action)
    {
      this.action = action;
    }
    IAction action;
    bool busy = true;
    public bool Busy
    {
      get
      {
        return busy;
      }

      set
      {
        busy = value;
      }
    }

    public void Update(float dt)
    {
      action.Run();
      Busy = false;
    }

    public void Reset()
    {
      Busy = true;
    }
  }

}
