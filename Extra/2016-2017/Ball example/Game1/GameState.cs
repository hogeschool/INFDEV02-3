using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1
{
  public class GameState : IComponent
  {
    List<Ball> balls;

    public GameState(Texture2D ball_texture)
    {
      Random seed = new Random();
      Ball me = new ControllableBall(ball_texture);
      balls = new List<Ball>();
      balls.Add(me);
      for (int i = 0; i < 100; i++)
      {
        balls.Add(new AgentBall(seed, ball_texture, me));

      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      foreach (Ball ball in balls)
      {
        ball.Draw(spriteBatch);
      }
    }

    public void Update(float dt)
    {
      foreach (Ball ball in balls)
      {
        ball.Update(dt);
      }
    }
  }
}
