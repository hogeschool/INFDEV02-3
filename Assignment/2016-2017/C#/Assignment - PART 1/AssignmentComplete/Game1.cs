using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AssignmentComplete
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    IStateMachine sm;
    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
      base.Initialize();
    }

    
    GameState gameState;
    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
      Texture2D volvo = Content.Load<Texture2D>("volvo.png");
      Texture2D mine = Content.Load<Texture2D>("mine.png");
      Texture2D ore_container = Content.Load<Texture2D>("ore_container.png");
      Texture2D product_container = Content.Load<Texture2D>("product_container.png");
      Texture2D ikea = Content.Load<Texture2D>("ikea.png");
      Texture2D background = Content.Load<Texture2D>("background.png");
      Texture2D mine_cart = Content.Load<Texture2D>("mine_cart.png");
      Texture2D product_box = Content.Load<Texture2D>("product_box.png");
      gameState = new GameState(background, mine_cart, product_box, volvo, mine, ikea, ore_container, product_container);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      gameState.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.LightGreen);
      spriteBatch.Begin();
      gameState.Draw(spriteBatch);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
