using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Honeymoon.Managers;
using Honeymoon.Menus;

namespace Honeymoon.Source.Menus
{
	public class GeneralSettingsMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton generalSettingsLogo;
		public GeneralSettingsMenu()
		{
			generalSettingsLogo = new MenuButton(FontManager.hm_f_menu, "General", new Vector2(500,500), 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting1", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting2", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting3", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			generalSettingsLogo.position = new Vector2(generalSettingsLogo.PerfectMidPositionText().X, generalSettingsLogo.PerfectMidPositionText().Y - 250);
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i);
			}
		}
		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i);
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
