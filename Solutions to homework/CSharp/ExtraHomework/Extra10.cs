using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.ExtraHomework.Extra6789;

namespace CSharp.ExtraHomework.Extra10
{
  class Interval<N>
  {
    N l;
    N u;
    Number<N> n;

    public Interval(Number<N> n, N l, N u)
    {
      this.n = n;
      this.l = l;
      this.u = u;
    }

    public N Sum
    {
      get
      {
        N sum = n.Zero;
        N i = l;
        while(n.SmallerOrEqual(i,u))
        {
          sum = n.Plus(sum, i);
          i = n.Plus(i, n.One);
        }
        return sum;
      }
    }

    public N Product
    {
      get
      {
        N prod = n.One;
        N i = l;
        while (n.SmallerOrEqual(i, u))
        {
          prod = n.Times(prod, i);
          i = n.Plus(i, n.One);
        }
        return prod;
      }
    }
  }
}
