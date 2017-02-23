using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssignmentComplete
{
  public class GameState : IComponent
  {
    List<ITruck> trucks;
    IFactory factory1;
    IFactory factory2;
    List<IStateMachine> processes;
    Texture2D background;

    
    public GameState(Texture2D background, Texture2D mine_cart, Texture2D product_box, Texture2D volvo, Texture2D mine, Texture2D ikea, Texture2D ore_container, Texture2D product_container)
    {
      this.background = background;
      factory1 = new Mine(new Vector2(100, 70), volvo, mine, mine_cart, ore_container);
      factory2 = null;//new Ikea(new Vector2(600, 340), volvo, ikea, product_box, product_container);
      processes = new List<IStateMachine>();
      trucks = new List<ITruck>();

      //this.processes = new List<IStateMachine>();
      //this.processes.Add(new Repeat(new Call(new AddTruckFromFactory(factory1, trucks))));
      //this.processes.Add(new Repeat(new Call(new AddTruckFromFactory(factory2, trucks))));


    }
    public void Update(float dt)
    {
      trucks.RemoveAll(truck => truck.Position.X < -50 || truck.Position.X > 1000);
      foreach (var process in processes)
      {
        process.Update(dt);
      }
      foreach (var truck in trucks)
      {
        truck.Update(dt);
      }
      if(factory1 != null)
        factory1.Update(dt);
      if(factory2 != null)
        factory2.Update(dt);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(background, Vector2.Zero, Color.White);

      foreach (var truck in trucks)
      {
        truck.Draw(spriteBatch);
      }
      if(factory1 != null)
        factory1.Draw(spriteBatch);
      if(factory2 != null)
        factory2.Draw(spriteBatch);
    }
  }

  class AddTruckFromFactory : IAction
  {
    IFactory factory;
    List<ITruck> trucks;

    public AddTruckFromFactory(IFactory factory, List<ITruck> trucks)
    {
      this.factory = factory;
      this.trucks = trucks;

    }
    public void Run()
    {
      var maybe_truck = factory.GetReadyTruck();
      if (maybe_truck != null)
      {
        maybe_truck.StartEngine();
        trucks.Add(maybe_truck);
      }
    }
  }
}
