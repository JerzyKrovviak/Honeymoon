using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Managers;
using Honeymoon.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Honeymoon.Source.Menus
{
	public class WorldSelectionMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton worldSelectionMenu;

		public WorldSelectionMenu()
		{
			worldSelectionMenu = new MenuButton(FontManager.hm_f_menu, "World Selection", Vector2.Zero, 5);
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
			worldSelectionMenu.size = worldSelectionMenu.GetButtonSize();
			worldSelectionMenu.position = new Vector2(GlobalFunctions.PerfectMidPosX(worldSelectionMenu.size.X), GlobalFunctions.PerfectMidPosY(worldSelectionMenu.size.Y) - 250);
		}

		public virtual void Draw()
		{
			worldSelectionMenu.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
		}
	}
}
