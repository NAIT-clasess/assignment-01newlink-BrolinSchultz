using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleAnimationNamespace;


namespace Assignment_01;

/* Assignment 01 - Animation and Input in Monogame
Background rendering [2 pts] - Asset added + Completed
A background image is loaded through the MonoGame Content Pipeline and displayed clearly in the game window, and it is the same size as the window.
Static image rendering [2 pts] - Asset added + Completed
One additional non-animated image is loaded and displayed.
Animated sprite sequence 1 [4 pts] - asset added + COMPLETED
An animated sprite, different than the one that was used in class, is displayed using a sprite sheet and frame-based animation controlled by the provided SimpleAnimation class.
Animated sprite sequence 2 [4 pts] - asset added +completed
A second animated sprite sequence, also different than the one that was used in class, is displayed and is visually distinct from the first.
Text rendering [2 pts] - Asset added 
A string of text is rendered to the screen using a SpriteFont.
Automatic movement of on-screen content [2 pts] - asset added 
At least one displayed piece of content moves automatically without player input.
Keyboard-controlled movement [4 pts] - asset added + Completed
A displayed piece of content moves in response to keyboard input.
Submission requirements [0 pts] - 
The accomplishments file is complete, submitted, and matches the final GitHub version with all required progress markers completed
*/


public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundTexture;

    private Texture2D _staticBallTexture;

    private SpriteFont _monogramFont;

    private SimpleAnimation _animatedDogSpriteMovement;

    private SimpleAnimation _animatedCatSpriteMovement;

    private Vector2 _textPosition;

    private Vector2 _staticBallPosition;

    private Vector2 _animatedCatPosition;

    private Vector2 _animatedDogPosition;

    private Vector2 _dogVelocity = new Vector2(2f, 1f);

    private Vector2 _catVelocity = new Vector2(1f, 2f);

    




    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        // This is where you would typically set up game variables, load settings, etc.

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        // This is where you would typically load textures, sounds, etc.
        _backgroundTexture = Content.Load<Texture2D>("backgroundForest");
        _staticBallTexture = Content.Load<Texture2D>("Tennis Ball");
        _monogramFont = Content.Load<SpriteFont>("Monogram");
        Texture2D catTexture = Content.Load<Texture2D>("JumpCattt");
        Texture2D dogTexture = Content.Load<Texture2D>("GoldenBarking");
        _animatedDogSpriteMovement = new SimpleAnimation(dogTexture, 64, 64, 4, 4f);
        _animatedCatSpriteMovement = new SimpleAnimation(catTexture, 32, 32, 4, 4f);
        _textPosition = new Vector2(20, 20);
        _staticBallPosition = new Vector2(400, 300);
        _animatedCatPosition = new Vector2(100, 300);
        _animatedDogPosition = new Vector2(600, 300);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        // This is where you would typically update game objects, handle input, etc.
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Left))
        {
            _staticBallPosition.X -= 5f;
        }
        if (keyboardState.IsKeyDown(Keys.Right))
        {
            _staticBallPosition.X += 5f;
        }
        if (keyboardState.IsKeyDown(Keys.Up))
        {
            _staticBallPosition.Y -= 5f;
        }
        if (keyboardState.IsKeyDown(Keys.Down))
        {
            _staticBallPosition.Y += 5f;
        }

        // stay within window size for static ball
        _staticBallPosition.X = MathHelper.Clamp(_staticBallPosition.X, 0, _graphics.PreferredBackBufferWidth - _staticBallTexture.Width);
        _staticBallPosition.Y = MathHelper.Clamp(_staticBallPosition.Y, 0, _graphics.PreferredBackBufferHeight - _staticBallTexture.Height);

        // Update positions and handle bouncing off the window edges for
        // both x and y boundaries for both animated sprites
        _animatedDogPosition += _dogVelocity;
        if (_animatedDogPosition.X > _graphics.PreferredBackBufferWidth - 64 || _animatedDogPosition.X < 0)
        {
            _dogVelocity.X = -_dogVelocity.X;
        }
        if (_animatedDogPosition.Y > _graphics.PreferredBackBufferHeight - 64 || 
            _animatedDogPosition.Y < 0)
        {
            _dogVelocity.Y = -_dogVelocity.Y;
        }
        _animatedCatPosition += _catVelocity;
        if (_animatedCatPosition.Y > _graphics.PreferredBackBufferHeight - 32 || 
            _animatedCatPosition.Y < 0)
        {
            _catVelocity.Y = -_catVelocity.Y;
        }
        if (_animatedCatPosition.X > _graphics.PreferredBackBufferWidth - 32 || 
            _animatedCatPosition.X < 0)
        {
            _catVelocity.X = -_catVelocity.X;
        }
        _animatedDogSpriteMovement.Update(gameTime);
        _animatedCatSpriteMovement.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        // This is where you would typically draw your game objects
        _spriteBatch.Begin();
        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
        _spriteBatch.Draw(_staticBallTexture, _staticBallPosition, Color.White);
        _animatedCatSpriteMovement.Draw(_spriteBatch, _animatedCatPosition, SpriteEffects.None);
        _animatedDogSpriteMovement.Draw(_spriteBatch, _animatedDogPosition, SpriteEffects.None);
        _spriteBatch.DrawString(_monogramFont, "This is my Text Rendering!", _textPosition, Color.Black);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
