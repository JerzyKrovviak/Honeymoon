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

namespace Honeymoon.Source
{
	public class Globals
	{
		public static GraphicsDeviceManager _graphics;

		public static SpriteBatch spriteBatch;

		public static ContentManager content;

		public static GameTime gameTime;

		public static Random random = new Random();

		public static MainMenu mainMenu;

		public static SettingsMenu settingsMenu;

		public static Vector2 windowSize, mousePosition;
	}
}
