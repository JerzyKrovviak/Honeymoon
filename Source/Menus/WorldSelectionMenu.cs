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
		private static MenuButton logo;

		public WorldSelectionMenu()
		{
			logo = new MenuButton(FontManager.hm_f_menu, "World Selection", Vector2.Zero, 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create new ", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
			}
			logo.position = new Vector2(logo.PerfectMidPositionText().X, 20);
		}
		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
			}

			if (menuButtons[0].IsHoveredAndClicked())
			{
				Globals.gameState = 9;
			}
			else if (menuButtons[1].IsHoveredAndClicked())
			{
				Globals.gameState = 6;
			}
		}

		public virtual void Draw()
		{
			logo.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
		}
	}
}
