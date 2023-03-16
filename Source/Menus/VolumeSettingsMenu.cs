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
	public class VolumeSettingsMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton volumelSettingsLogo;

		public VolumeSettingsMenu()
		{
			volumelSettingsLogo = new MenuButton(FontManager.hm_f_menu, "Volume", Vector2.Zero, 5);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting1", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting2", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting3", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3));
		}

		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i));
			}
			volumelSettingsLogo.size = volumelSettingsLogo.GetButtonSize();
			volumelSettingsLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(volumelSettingsLogo.size.X), GlobalFunctions.PerfectMidPosY(volumelSettingsLogo.size.Y) - 250);
			if (menuButtons[3].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 2;
				}
			}
		}

		public virtual void Draw()
		{
			volumelSettingsLogo.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
		}
	}
}
