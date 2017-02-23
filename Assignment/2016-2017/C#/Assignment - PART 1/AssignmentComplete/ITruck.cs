using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{
  public interface ITruck : IComponent
  {
    IContainer Container { get; }
    Vector2 Position { get; }
    Vector2 Velocity { get; }
    void StartEngine();
    void AddContainer(IContainer container);
  }
}
