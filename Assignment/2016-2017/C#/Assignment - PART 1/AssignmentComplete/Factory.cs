using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{
  class Mine : IFactory
  {

    class AddOreBoxToMine : IAction
    {
      Mine mine;
      public AddOreBoxToMine(Mine mine)
      {
        this.mine = mine;
      }
      public void Run()
      {
        mine.ProductsToShip.Add(CreateOreBox(mine.Position + new Vector2(-80, 40 + -30 * mine.ProductsToShip.Count)));
      }
      Ore CreateOreBox(Vector2 position)
      {
        var box = new Ore(100, mine.oreBox);
        box.Position = position;
        return box;
      }
    }

    Texture2D mine, oreContainer, oreBox, truckTexture;
    List<IStateMachine> processes;
    ITruck waitingTruck;
    bool isTruckReady = false;
    Vector2 position;
    List<IContainer> productsToShip;

    public Mine(Vector2 position, Texture2D truck_texture, Texture2D mine, Texture2D ore_box, Texture2D ore_container)
    {
      processes = new List<IStateMachine>();
      ProductsToShip = new List<IContainer>();
      this.mine = mine;
      this.truckTexture = truck_texture;
      this.oreContainer = ore_container;
      this.oreBox = ore_box;
      this.position = position;


      processes.Add(
        new Repeat(new Seq(new Timer(1.0f),
                           new Call(new AddOreBoxToMine(this)))));
    }


    public ITruck GetReadyTruck()
    {
      //not implemented yet
      return null;
    }

    public Vector2 Position
    {
      get
      {
        return position;
      }
    }
    public List<IContainer> ProductsToShip
    {
      get
      {
        return productsToShip;
      }
      set
      {
        productsToShip = value;
      }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
      foreach (var cart in ProductsToShip)
      {
        cart.Draw(spriteBatch);
      }
      spriteBatch.Draw(mine, Position, Color.White);
    }
    public void Update(float dt)
    {
      foreach (var process in processes)
      {
        process.Update(dt);
      }
    }
    
  }

}
