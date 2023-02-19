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

namespace Honeymoon.Menus
{
	public class MainMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton logo;
		private static bool lastHover = false;

		public MainMenu()
		{
			logo = new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/logo"), Rectangle.Empty, new Rectangle(0, 0, 152, 68));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Singleplayer", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Multiplayer", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Settings", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Exit", Vector2.Zero, 3));
		}

		public virtual void Update()
		{
			logo.inGameData = new Rectangle((int)GlobalFunctions.PerfectMidPosX(608), 30, 608, 272);
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i));
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

			if (menuButtons[2].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Main.gameState = 2;
				}
			}
			else if (menuButtons[3].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Main.self.Exit();
				}
			}
		}

		public virtual void Draw()
		{
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
			logo.DrawTexture();
		}
	}
}
