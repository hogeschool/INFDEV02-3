using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentComplete
{
    public interface IStateMachine : IUpdateable
  {
      bool Busy { get; }
      void Reset();
    }

    public interface IAction {
      void Run();
  }
}
