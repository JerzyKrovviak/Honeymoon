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
using Honeymoon.Menus;

namespace Honeymoon.Source.Menus
{
	public class GeneralSettingsMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton generalSettingsLogo;
		private static bool lastHover = false;

		public GeneralSettingsMenu()
		{
			generalSettingsLogo = new MenuButton(FontManager.hm_f_menu, "General", Vector2.Zero, 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting1", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting2", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting3", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
		}

		public virtual void Update()
		{
			generalSettingsLogo.size = generalSettingsLogo.GetButtonSize();
			generalSettingsLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(generalSettingsLogo.size.X), GlobalFunctions.PerfectMidPosY(generalSettingsLogo.size.Y) - 250);
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i));
			}

			if (menuButtons[3].IsHoveredAndClicked())
			{
				Globals.gameState = 2;
			}
		}

		public virtual void Draw()
		{
			generalSettingsLogo.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
		}
	}
}
