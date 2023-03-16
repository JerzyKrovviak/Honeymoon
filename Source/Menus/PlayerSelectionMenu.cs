using Honeymoon.Managers;
using Honeymoon.Menus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.Menus
{
	public class PlayerSelectionMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton playerSelectionMenuLogo;

		public PlayerSelectionMenu()
		{
			playerSelectionMenuLogo = new MenuButton(FontManager.hm_f_menu, "Beekeeper Selection", Vector2.Zero, 5);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create new ", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3));
		}

		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i) + 180);
			}
			playerSelectionMenuLogo.size = playerSelectionMenuLogo.GetButtonSize();
			playerSelectionMenuLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(playerSelectionMenuLogo.size.X), GlobalFunctions.PerfectMidPosY(playerSelectionMenuLogo.size.Y) - 250);
			if (menuButtons[0].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 7;
				}
			}
			else if (menuButtons[1].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 0;
				}
			}
		}

		public virtual void Draw()
		{
			playerSelectionMenuLogo.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
		}
	}
}
