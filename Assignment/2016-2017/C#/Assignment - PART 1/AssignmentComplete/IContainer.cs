using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AssignmentComplete
{
  public interface IContainer : IDrawable
  {
    int CurrentAmount { get; }
    int MaxCapacity { get; }
    bool AddContent(int amount);
    Vector2 Position { get; set; }
  }
}
