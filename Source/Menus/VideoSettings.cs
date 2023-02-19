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
	public class VideoSettings
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton videoSettingsLogo;
		private static bool lastHover = false;
		private string[] resolutions = { "Fullscreen", "Windowed", "Windowed Fullscreen" };

		public VideoSettings()
		{
			//add loading saved resolution from file
			videoSettingsLogo = new MenuButton(FontManager.hm_f_menu, "Video", Vector2.Zero, 5);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Resolution: ", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting2", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "setting3", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3));
		}

		public virtual void Update()
		{
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

			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[0].size.Y - 170 * i));
				lastHover = menuButtons[i].isHovered;
				menuButtons[i].size = menuButtons[i].GetButtonSize();
				if (menuButtons[i].scale == 3)
				{
					menuButtons[i].hitbox = new Rectangle((int)menuButtons[i].position.X, (int)menuButtons[i].position.Y, (int)menuButtons[i].size.X, (int)menuButtons[i].size.Y);
				}
				if (menuButtons[i].hitbox.Contains(Globals.mousePosition))
				{
					menuButtons[i].isHovered = true;
					menuButtons[i].color = Color.Orange;
					if (menuButtons[i].scale < 4.0f)
					{
						menuButtons[i].scale += 0.1f;
					}
					if (InputManager.IsLeftButtonNewlyPressed())
					{
						AudioManager.soundBank.PlayCue("optionSelect");
					}
				}
				else
				{
					if (menuButtons[i].scale > 3.0f)
					{
						menuButtons[i].scale -= 0.1f;
					}
					menuButtons[i].isHovered = false;
					menuButtons[i].color = Color.White;
				}
				if (!lastHover && menuButtons[i].isHovered)
				{
					AudioManager.soundBank.PlayCue("optionHover");
				}
			}
			if (menuButtons[0].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
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
			}
			else if (menuButtons[3].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 2;
				}
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
