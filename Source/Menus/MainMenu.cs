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
using Honeymoon.Source;

namespace Honeymoon.Menus
{
	public class MainMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton logo;

		public MainMenu()
		{
			logo = new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/logo"), Rectangle.Empty, new Rectangle(0, 0, 152, 68));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Singleplayer", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Multiplayer", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Settings", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Exit", Vector2.Zero, 3));
		}

		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i));
			}

			logo.inGameData = new Rectangle((int)GlobalFunctions.PerfectMidPosX(608), 20, 608, 272);
			if (menuButtons[0].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 6;
				}
			}
			else if (menuButtons[2].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 2;
				}
			}
			else if (menuButtons[3].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.persistentSettings = new SavedSettings();
					SavedSettings.SaveSettings(Globals.persistentSettings);
					Main.self.Exit();
				}
			}
		}

		public virtual void Draw()
		{
			foreach (var menuButton in menuButtons)
			{
				menuButton.DrawString();
			}
			logo.DrawTexture();
		}
	}
}
