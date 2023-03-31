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
	public class VideoSettingsMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton videoSettingsLogo;
		private string[] resolutions = { "Fullscreen", "Windowed", "Windowed Fullscreen" };

		public VideoSettingsMenu()
		{
			//add loading saved resolution from file
			videoSettingsLogo = new MenuButton(FontManager.hm_f_menu, "Video", Vector2.Zero, 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Resolution: ", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting2", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting3", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
		}

		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i));
			}
			videoSettingsLogo.size = videoSettingsLogo.GetButtonSize();
			videoSettingsLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(videoSettingsLogo.size.X), GlobalFunctions.PerfectMidPosY(videoSettingsLogo.size.Y) - 250);
			if (Globals.selectedResolution == 1)
			{
				menuButtons[0].text = "Resolution: " + resolutions[0];
			}
			else if (Globals.selectedResolution == 2)
			{
				menuButtons[0].text = "Resolution: " + resolutions[1];
			}
			else if (Globals.selectedResolution == 3)
			{
				menuButtons[0].text = "Resolution: " + resolutions[2];
			}

			if (menuButtons[0].IsHoveredAndClicked())
			{
				Globals.selectedResolution++;
				if (Globals.selectedResolution == 1)
				{
					menuButtons[0].text = "Resolution: " + resolutions[0];
					Globals.ChangeGameResolution(1);
				}
				if (Globals.selectedResolution == 2)
				{
					menuButtons[0].text = "Resolution: " + resolutions[1];
					Globals.ChangeGameResolution(2);
				}
				if (Globals.selectedResolution == 3)
				{
					menuButtons[0].text = "Resolution: " + resolutions[2];
					Globals.ChangeGameResolution(3);
				}
				if (Globals.selectedResolution > 3)
				{
					Globals.ChangeGameResolution(1);
					Globals.selectedResolution = 1;
				}
			}
			else if (menuButtons[3].IsHoveredAndClicked())
			{
				Globals.gameState = 2;
			}
		}

		public virtual void Draw()
		{
			videoSettingsLogo.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
		}
	}
}
