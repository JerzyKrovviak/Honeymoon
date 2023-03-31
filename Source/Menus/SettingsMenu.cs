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
using Honeymoon.Menus;

namespace Honeymoon.Source.Menus
{
	public class SettingsMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton settingsMenuLogo;

		public SettingsMenu()
		{
			settingsMenuLogo = new MenuButton(FontManager.hm_f_menu, "Settings", Vector2.Zero, 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "General", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Video", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Volume", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Save & back", Vector2.Zero, 3, Color.White));
		}

		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i));
			}
			settingsMenuLogo.size = settingsMenuLogo.GetButtonSize();
			settingsMenuLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(settingsMenuLogo.size.X), GlobalFunctions.PerfectMidPosY(settingsMenuLogo.size.Y) - 250);

			if (menuButtons[0].IsHoveredAndClicked())
			{
				Globals.gameState = 3;
			}
			else if (menuButtons[1].IsHoveredAndClicked())
			{
				Globals.gameState = 4;
			}
			else if (menuButtons[2].IsHoveredAndClicked())
			{
				Globals.gameState = 5;
			}
			else if (menuButtons[3].IsHoveredAndClicked())
			{
				Globals.persistentSettings = new SavedSettings();
				SavedSettings.SaveSettings(Globals.persistentSettings);
				Globals.gameState = 0;
			}
		}

		public virtual void Draw()
		{
			foreach (var menuButton in menuButtons)
			{
				menuButton.DrawString();
			}
			settingsMenuLogo.DrawString();
		}
	}
}
