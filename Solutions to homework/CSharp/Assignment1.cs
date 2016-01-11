using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
  class Counter
  {
    int cnt, tgt;
    Action onTarget;

    public Counter()
    {
      cnt = 0;
      tgt = -1;
    }

    public Counter(int startFrom)
    {
      cnt = startFrom;
      tgt = -1;
    }

    public void Tick()
    {
      cnt = cnt + 1;
      if (cnt == tgt)
        onTarget();
    }

    public void Reset()
    {
      cnt = 0;
    }

    public static Counter operator +(Counter c1, Counter c2)
    {
      return new Counter(c1.cnt + c2.cnt);
    }

    public void OnTarget(int tgt, Action onTarget)
    {
      this.tgt = tgt;
      this.onTarget = onTarget;
    }

    public override string ToString()
    {
      return "Count is " + cnt;
    }
  }
}
