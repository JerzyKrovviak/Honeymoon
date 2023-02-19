using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.Menus;
using Honeymoon.Source;

namespace Honeymoon
{
	public class Main : Game
	{
		private GraphicsDeviceManager _graphics;
		public static Main self;
		public static int gameState = 0;
		MainMenu mainMenu;

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
			_graphics.IsFullScreen = true;
			_graphics.PreferredBackBufferWidth = 1280;
			_graphics.PreferredBackBufferHeight = 756;
			_graphics.HardwareModeSwitch = false;
			Globals.windowSize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
			_graphics.ApplyChanges();
		}

		public static bool IsOutOfFocus()
		{
			return !self.IsActive;
		}

		protected override void LoadContent()
		{
			Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
			Globals.content = this.Content;
			AudioManager.LoadAudioContent();
			FontManager.LoadContent(Content);
			mainMenu = new MainMenu();
			Globals.settingsMenu = new SettingsMenu();
		}

		protected override void Update(GameTime gameTime)
		{
			Globals.windowSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
			if (!IsOutOfFocus())
			{
				MouseState mouseState = Mouse.GetState();
				Globals.mousePosition = new Vector2(mouseState.X, mouseState.Y);
				InputManager.SetCurrentStates(new ButtonState[] { mouseState.LeftButton, mouseState.RightButton });
				InputManager.SetCurrentStates(Keyboard.GetState());
				if (gameState == 0)
				{
					mainMenu.Update();
				}
				else if (gameState == 1)
				{

				}
				else if (gameState == 2)
				{
					Globals.settingsMenu.Update();
				}
			}
			AudioManager.audioEngine.Update();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, null);
			_graphics.GraphicsDevice.Clear(new Color(255, 235, 177));
			if (gameState == 0)
			{
				mainMenu.Draw();
			}
			else if (gameState == 1)
			{

			}
			else if (gameState == 2)
			{
				Globals.settingsMenu.Draw();
			}
			Globals.spriteBatch.End();
		}
	}
}