using System;
using System.Collections.Generic;

namespace StateMachine
{
	
	interface ListInt
	{
		bool IsEmplty {
			get;
		}

		int Length { get; }
		void Iter(Action<int> f);
		ListInt Map(Func<int, int> f);
		ListInt Filter(Func<int, bool> p);
	}

	class EmptyInt : ListInt
	{
		public bool IsEmplty {
			get{return true; }
		}

		public int Length
		{
			get
			{
				return 0;
			}
		}

		public ListInt Filter(Func<int, bool> p)
		{
			return new EmptyInt();
		}

		public void Iter(Action<int> f)
		{
		}

		public ListInt Map(Func<int, int> f)
		{
			return new EmptyInt();
		}
	}

	class NodeInt : ListInt
	{
		int head;
		ListInt tail;
		public NodeInt(int x, ListInt xs) { head = x; tail = xs; }

		public bool IsEmplty {
			get{return false; }
		}

		public int Length
		{
			get
			{
				return 1 + tail.Length;
			}
		}

		public ListInt Filter(Func<int, bool> p)
		{
			if (p(head))
				return new NodeInt(head, tail.Filter(p));
			else
				return tail.Filter(p);
		}

		public void Iter(Action<int> f)
		{
			f(head);
			tail.Iter(f);
		}

		public ListInt Map(Func<int, int> f)
		{
			return new NodeInt(f(head), tail.Map(f));
		}
	}

}
		

