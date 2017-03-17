using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
  public interface Ball : IComponent
  {
    Vector2 Position { get; }
    Vector2 Velocity { get; }
  }

  public class AgentBall : Ball
  {
    private Vector2 position;
    public Vector2 Position
    {
      get
      {
        return position;
      }
    }
    private Vector2 velocity;
    public Vector2 Velocity
    {
      get
      {
        return velocity;
      }
    }

    Texture2D texture;
    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(texture, position, Color.Red);
    }

    public AgentBall(Random seed, Texture2D texture, Ball players_ball)
    {
      
      this.texture = texture;
      position = new Vector2(seed.Next(400) + 100, seed.Next(400) + 100);
      velocity = new Vector2(0, 0);
      process =
          new Repeat(new Seq(new Timer(0.5f),
                             new Call(() => { if(seed.Next(10) < 5)
                                                velocity = players_ball.Position - position;
                                              else
                                                 velocity = position - players_ball.Position;
                                              velocity.Normalize(); })));
    }
    IStateMachine process;
    public void Update(float dt)
    {
      position = position + velocity * dt * max_velocity;
      process.Update(dt);
    }
    float max_velocity = 100;

  }


  public class ControllableBall : Ball
  {
    private Vector2 position;
    public Vector2 Position
    {
      get
      {
        return position;
      }
    }
    private Vector2 velocity;
    public Vector2 Velocity
    {
      get
      {
        return velocity;
      }
    }

    private Texture2D texture;
    public ControllableBall(Texture2D texture)
    {
      this.texture = texture;
      position = new Vector2(0, 0);
      velocity = new Vector2(0, 0);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(texture, position, Color.Blue);
    }
    float max_velocity = 100;
    public void Update(float dt)
    {

      position = position + velocity * dt * max_velocity;

      KeyboardState keyboard_state = Keyboard.GetState();
      if (keyboard_state.IsKeyDown(Keys.W))
        velocity = new Vector2(0, -1);
      else if (keyboard_state.IsKeyDown(Keys.S))
        velocity = new Vector2(0, 1);
      else if (keyboard_state.IsKeyDown(Keys.A))
        velocity = new Vector2(-1, 0);
      else if (keyboard_state.IsKeyDown(Keys.D))
        velocity = new Vector2(1, 0);
      else
        velocity = Vector2.Zero;


    }
  }

}
