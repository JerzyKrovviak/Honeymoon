﻿using System;
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
		private static bool lastHover = false;

		public SettingsMenu()
		{
			settingsMenuLogo = new MenuButton(FontManager.hm_f_menu, "Settings", Vector2.Zero, 5);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "General", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Video", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Volume", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3));
		}

		public virtual void Update()
		{
			settingsMenuLogo.size = settingsMenuLogo.GetButtonSize();
			settingsMenuLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(settingsMenuLogo.size.X), GlobalFunctions.PerfectMidPosY(settingsMenuLogo.size.Y) - 250);
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
					Globals.gameState = 3;
				}
			}
			else if (menuButtons[1].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 4;
				}
			}
			else if (menuButtons[2].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 5;
				}
			}
			else if (menuButtons[3].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 0;
				}
			}
		}

		public virtual void Draw()
		{
			settingsMenuLogo.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
		}
	}
}
