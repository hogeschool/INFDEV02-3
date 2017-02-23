using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{

  public class Ore : IContainer
  {
    Texture2D texture;
    Vector2 position = Vector2.One * -100;
    int currentAmount;

    public Ore(int amount, Texture2D texture)
    {
      this.texture = texture;
      AddContent(amount);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(texture, Position, Color.White);
    }

    public Vector2 Position
    {
      get
      {
        return position;
      }

      set
      {
        position = value;
      }
    }
    public int CurrentAmount
    {
      get
      {
        return currentAmount;
      }
    }

    public int MaxCapacity
    {
      get
      {
        return 1000;
      }
    }

    public bool AddContent(int amount)
    {
      if (CurrentAmount + amount > MaxCapacity)
      {
        Console.WriteLine("Too many...");
        return false;
      }
      currentAmount += amount;
      return true;
    }
  }
}
