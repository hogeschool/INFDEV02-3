using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.ExtraHomework.Extra6789
{
  abstract class Number<N>
  {
    public abstract N Zero { get; }
    public abstract N One { get; }
    public abstract N Negate(N n);

    public abstract N Plus(N x, N y);
    public abstract N Times(N x, N y);
    public N Minus(N x, N y) { return Plus(x, Negate(y)); }
    public abstract N DividedBy(N x, N y);

    public abstract bool SmallerThan(N x, N y);
    public abstract bool Equal(N x, N y);
    public bool NotEqual(N x, N y) { return !Equal(x, y); }
    public bool GreaterThan(N x, N y) { return SmallerThan(y, x); }
    public bool SmallerOrEqual(N x, N y) { return SmallerThan(x, y) || Equal(x, y); }
    public bool GreaterOrEqual(N x, N y) { return SmallerOrEqual(y, x); }
  }

  class IntNumber : Number<int>
  {
    public override int One
    {
      get
      {
        return 1;
      }
    }

    public override int Zero
    {
      get
      {
        return 0;
      }
    }

    public override int DividedBy(int x, int y)
    {
      return x / y;
    }

    public override bool Equal(int x, int y)
    {
      return x == y;
    }

    public override int Negate(int n)
    {
      return -n;
    }

    public override int Plus(int x, int y)
    {
      return x + y;
    }

    public override bool SmallerThan(int x, int y)
    {
      return x < y;
    }

    public override int Times(int x, int y)
    {
      return x * y;
    }
  }

  class FloatNumber : Number<float>
  {
    public override float One
    {
      get
      {
        return 1.0f;
      }
    }

    public override float Zero
    {
      get
      {
        return 0.0f;
      }
    }

    public override float DividedBy(float x, float y)
    {
      return x / y;
    }

    public override bool Equal(float x, float y)
    {
      return x == y;
    }

    public override float Negate(float n)
    {
      return -n;
    }

    public override float Plus(float x, float y)
    {
      return x + y;
    }

    public override bool SmallerThan(float x, float y)
    {
      return x < y;
    }

    public override float Times(float x, float y)
    {
      return x * y;
    }
  }

  //class StringNumber : Number<string>
  //{
  //  public override string Zero
  //  {
  //    get
  //    {
  //      return "";
  //    }
  //  }

  //  public override string DividedBy(string x, string y)
  //  {
  //    Cannot be implemented!
  //  }

  //  public override string Negate(string n)
  //  {
  //    Cannot be implemented!
  //  }

  //  public override string Plus(string x, string y)
  //  {
  //    return x + y;
  //  }

  //  public override string Times(string x, string y)
  //  {
  //    Cannot be implemented!
  //  }
  //
  //  ...
  //
  //}
}
