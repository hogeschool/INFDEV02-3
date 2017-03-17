using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
  public interface IDrawable
  {
    void Draw(SpriteBatch spriteBatch);
  }
  public interface IUpdateable
  {
    void Update(float dt);
  }
  public interface IComponent : IDrawable, IUpdateable { }

  public interface IStateMachine : IUpdateable
  {
      bool Busy { get; }
      void Reset();
    }

    public interface IAction {
      void Run();
  }
}
