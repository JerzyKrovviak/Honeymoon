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

namespace Honeymoon.Menus
{
	public class MainMenu
	{
		private static List<MenuButton> menuButtons;

		public static void LoadContent(ContentManager Content)
		{
			menuButtons = new List<MenuButton>();
			for (int i = 0; i < 4; i++)
			{
				menuButtons.Add(new MenuButton(FontManager.hm_f_menu));
				menuButtons[i].scale = 3;			
			}
		}

		public static float PerfectMidPosX(float width)
		{
			return Main.windowSize.X / 2 - width / 2;
		}

		public static float PerfectMidPosY(float height)
		{
			return Main.windowSize.Y / 2 - height / 2;
		}

		public static void Update(GameTime gameTime)
		{
			#region menuButtons
			menuButtons[0].text = "Singleplayer";
			menuButtons[0].position = new Vector2(PerfectMidPosX(menuButtons[0].GetButtonSize().X * menuButtons[0].scale), PerfectMidPosY(menuButtons[0].GetButtonSize().Y * menuButtons[0].scale));
			menuButtons[1].text = "Multiplayer";
			menuButtons[1].position = new Vector2(PerfectMidPosX(menuButtons[1].GetButtonSize().X * menuButtons[1].scale), PerfectMidPosY(menuButtons[1].GetButtonSize().Y * menuButtons[1].scale - 180));
			menuButtons[2].text = "Settings";
			menuButtons[2].position = new Vector2(PerfectMidPosX(menuButtons[2].GetButtonSize().X * menuButtons[2].scale), PerfectMidPosY(menuButtons[2].GetButtonSize().Y * menuButtons[2].scale - 360));
			menuButtons[3].text = "Exit";
			menuButtons[3].position = new Vector2(PerfectMidPosX(menuButtons[3].GetButtonSize().X * menuButtons[3].scale), PerfectMidPosY(menuButtons[3].GetButtonSize().Y * menuButtons[3].scale - 540));

			foreach (MenuButton button in menuButtons)
			{
				//button.hitbox = new Rectangle((int)button.position.X, (int)button.position.Y, (int)button.GetButtonSize().X * 3, (int)button.GetButtonSize().Y * 3);
				button.hitbox = new Rectangle((int)button.position.X, (int)button.position.Y, (int)button.GetButtonSize().X * (int)button.scale, (int)button.GetButtonSize().Y * (int)button.scale);
				System.Diagnostics.Debug.WriteLine(button.scale);
				if (button.hitbox.Contains(Main.mousePosition))
				{
					button.isHovered = true;
					button.color = Color.Orange;
					if (button.scale < 4.0f)
					{
						button.scale += 0.1f;
					}
					if (InputManager.IsLeftButtonNewlyPressed())
					{
						System.Diagnostics.Debug.WriteLine("bombaclat");
					}
				}
				else
				{
					if (button.scale > 3.0f)
					{
						button.scale -= 0.1f;
					}
					button.isHovered = false;
					button.color = Color.White;
				}
			}
			#endregion
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (MenuButton button in menuButtons)
			{
				spriteBatch.DrawString(button.font, button.text, button.position, button.color, button.rotation, button.origin, button.scale, SpriteEffects.None, 0);
			}
		}
	}
}
