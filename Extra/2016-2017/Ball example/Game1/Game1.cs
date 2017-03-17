using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    GameState game;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      graphics.PreferredBackBufferHeight = 800;
      graphics.PreferredBackBufferHeight = 800;
      Content.RootDirectory = "Content";
    }
    protected override void Initialize()
    {
      base.Initialize();
    }
    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);
      Texture2D ball = Content.Load<Texture2D>("ball");
      game = new GameState(ball);
    }
    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      game.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();
      game.Draw(spriteBatch);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
