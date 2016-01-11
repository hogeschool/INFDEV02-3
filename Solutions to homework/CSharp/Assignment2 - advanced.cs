using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
  interface List<T>
  {
    int Length { get; }
    void Iter(Action<T> f);
    List<U> Map<U>(Func<T, U> f);
    List<T> Filter(Func<T, bool> p);
  }

  class Empty<T> : List<T>
  {
    public int Length
    {
      get
      {
        return 0;
      }
    }

    public List<T> Filter(Func<T, bool> p)
    {
      return new Empty<T>();
    }

    public void Iter(Action<T> f)
    {
    }

    public List<U> Map<U>(Func<T, U> f)
    {
      return new Empty<U>();
    }
  }

  class Node<T> : List<T>
  {
    T head;
    List<T> tail;
    public Node(T x, List<T> xs) { head = x; tail = xs; }

    public int Length
    {
      get
      {
      return 1 + tail.Length;
      }
    }

    public List<T> Filter(Func<T, bool> p)
    {
      if (p(head))
        return new Node<T>(head, tail.Filter(p));
      else
        return tail.Filter(p);
    }

    public void Iter(Action<T> f)
    {
      f(head);
      tail.Iter(f);
    }

    public List<U> Map<U>(Func<T, U> f)
    {
      return new Node<U>(f(head), tail.Map(f));
    }
  }

}
