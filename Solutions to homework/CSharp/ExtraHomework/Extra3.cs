using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.ExtraHomework.Extra3
{
  class Interval
  {
    private int l;
    private int u;

    public Interval(int l, int u)
    {
      this.l = l;
      this.u = u;
    }

    public int Sum {
      get
      {
        int sum = 0;
        for (int i = l; i <= u; i++)
        {
          sum = sum + i;
        }
        return sum;
      }
    }

    public int Product
    {
      get
      {
        int prod = 1;
        for (int i = l; i <= u; i++)
        {
          prod = prod * i;
        }
        return prod;
      }
    }
  }
}
