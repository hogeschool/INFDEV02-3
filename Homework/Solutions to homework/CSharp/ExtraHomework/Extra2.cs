using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.ExtraHomework.Extra2
{
  class Program
  {
    public static void SumInterval()
    {
      int l = int.Parse(Console.ReadLine());
      int u = int.Parse(Console.ReadLine());
      int sum = 0;
      for (int i = l; i <= u; i++)
      {
        sum = sum + i;
      }
      Console.WriteLine(sum);
    }
  }
}
