using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Honeymoon.Managers;
using Honeymoon.Menus;

namespace Honeymoon
{
	public class Main : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		public static Vector2 windowSize, mousePosition;
		public static Main self;

		public Main()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			self = this;
		}

		protected override void Initialize()
		{
			base.Initialize();
			//_graphics.IsFullScreen = true;
			_graphics.PreferredBackBufferWidth = 1280;
			_graphics.PreferredBackBufferHeight = 756;
			_graphics.HardwareModeSwitch = false;
			windowSize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
			_graphics.ApplyChanges();
		}

		public static bool IsOutOfFocus()
		{
			return !self.IsActive;
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			//windowSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
			//windowSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
			FontManager.LoadContent(Content);
			MainMenu.LoadContent(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			windowSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
			if (!IsOutOfFocus())
			{
				MouseState mouseState = Mouse.GetState();
				mousePosition = new Vector2(mouseState.X, mouseState.Y);
				InputManager.SetCurrentStates(new ButtonState[] { mouseState.LeftButton, mouseState.RightButton });
				InputManager.SetCurrentStates(Keyboard.GetState());
			}
			MainMenu.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, null);
			_graphics.GraphicsDevice.Clear(new Color(255, 235, 177));
			MainMenu.Draw(_spriteBatch);
			_spriteBatch.End();
		}
	}
}