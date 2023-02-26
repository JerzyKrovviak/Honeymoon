using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.Menus;
using Honeymoon.Source;
using Honeymoon.Source.World.Map;
using Honeymoon.Source.World.Creatures.Player;

namespace Honeymoon
{
	public class Main : Game
	{
		public static Main self;
		MainMenu mainMenu;

		public Main()
		{
			Globals._graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			self = this;
		}

		protected override void Initialize()
		{
			base.Initialize();
			Globals.windowSize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
			if (SavedSettings.CheckIfFileExists())
			{
				Globals.persistentSettings = SavedSettings.LoadSettings();
				Globals.ChangeGameResolution(Globals.persistentSettings.resolution);
			}
			else
			{
				Globals.ChangeGameResolution(2);
			}
			Globals._graphics.ApplyChanges();
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
			Globals.generalSettings = new GeneralSettings();
			Globals.volumeSettings = new VolumeSettings();
			Globals.videoSettings = new VideoSettings();
			Globals.map = new Map();
			Globals.tilesetManager = new TilesetManager();
			Globals.player = new Beekeeper();
		}

		protected override void Update(GameTime gameTime)
		{
			Globals.windowSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
			if (!IsOutOfFocus())
			{
				Globals.gameTime = gameTime;
				MouseState mouseState = Mouse.GetState();
				Globals.mousePosition = new Vector2(mouseState.X, mouseState.Y);
				InputManager.SetCurrentStates(new ButtonState[] { mouseState.LeftButton, mouseState.RightButton });
				InputManager.SetCurrentStates(Keyboard.GetState());
				if (Globals.gameState == 0)
				{
					mainMenu.Update();
				}
				else if (Globals.gameState == 1)
				{
					Globals.map.Update();
					Globals.player.Update();
				}
				else if (Globals.gameState == 2)
				{
					Globals.settingsMenu.Update();
				}
				else if (Globals.gameState == 3)
				{
					Globals.generalSettings.Update();
				}
				else if (Globals.gameState == 4)
				{
					Globals.videoSettings.Update();
				}
				else if (Globals.gameState == 5)
				{
					Globals.volumeSettings.Update();
				}
			}
			AudioManager.audioEngine.Update();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, null);
			Globals._graphics.GraphicsDevice.Clear(new Color(255, 235, 177));
			if (Globals.gameState == 0)
			{
				mainMenu.Draw();
				Globals.player.Draw();
			}
			else if (Globals.gameState == 1)
			{
				Globals.map.Draw(0);
				Globals.player.Draw();
			}
			else if (Globals.gameState == 2)
			{
				Globals.settingsMenu.Draw();
			}
			else if (Globals.gameState == 3)
			{
				Globals.generalSettings.Draw();
			}
			else if (Globals.gameState == 4)
			{
				Globals.videoSettings.Draw();
			}
			else if (Globals.gameState == 5)
			{
				Globals.volumeSettings.Draw();
			}
			Globals.spriteBatch.End();
		}
	}
}