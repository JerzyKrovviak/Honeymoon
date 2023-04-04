using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			settingsMenuLogo.position = new Vector2(settingsMenuLogo.PerfectMidPositionText().X, settingsMenuLogo.PerfectMidPositionText().Y - 250);
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
