using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Honeymoon.Managers;
using Honeymoon.Source.Menus;
using Honeymoon.Menus;
using Honeymoon.Source.World.Map;
using Honeymoon.Source.World.Creatures.Player;
using Honeymoon.Source.World.Creatures;
using Honeymoon.Source.Managers;

namespace Honeymoon.Source
{
	public class Globals
	{
		public static GraphicsDeviceManager _graphics;

		public static SpriteBatch spriteBatch;

		public static ContentManager content;

		public static GameTime gameTime;

		public static int gameState = 0;

		public static int selectedResolution;

		public static Vector2 windowSize, mousePosition;

		public static void ChangeGameResolution(int id)
		{
			if (id == 1)
			{
				_graphics.IsFullScreen = true;
				_graphics.HardwareModeSwitch = true;
				_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}
			else if (id == 2)
			{
				_graphics.IsFullScreen = false;
				_graphics.HardwareModeSwitch = false;
				_graphics.PreferredBackBufferWidth = 1280;
				_graphics.PreferredBackBufferHeight = 756;
			}
			else if (id == 3)
			{
				_graphics.IsFullScreen = false;
				_graphics.HardwareModeSwitch = false;
				_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}
			selectedResolution = id;
			_graphics.ApplyChanges();
		}

		public static Random random = new Random();

		public static MainMenu mainMenu;
		public static SettingsMenu settingsMenu;
		public static GeneralSettings generalSettings;
		public static VideoSettings videoSettings;
		public static VolumeSettings volumeSettings;

		public static SavedSettings persistentSettings;

		public static MenuManager menuManager;
		public static GameManager gameManager;
		public static Map map;
		public static TilesetManager tilesetManager;

		public static Beekeeper player;
		public static InputComponent input;
	}
}
