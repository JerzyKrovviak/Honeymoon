using Honeymoon.Managers;
using Honeymoon.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.Menus
{
	public class PlayerCreationMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton playerCreationLogo, uiBox;
		private static TextInput playerNameInput;

		public PlayerCreationMenu()
		{
			playerCreationLogo = new MenuButton(FontManager.hm_f_menu, "Create beekeeper", Vector2.Zero, 5);
			uiBox = new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Rectangle.Empty, new Rectangle(0, 0, 96, 64));
			playerNameInput = new TextInput(FontManager.hm_f_default, "Ziemblox", Vector2.Zero, 1.5f, Color.Black, 11);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create", Vector2.Zero, 3));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3));
		}

		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i) + 250);
			}
			playerNameInput.UpdateTextInput();
			playerCreationLogo.size = playerCreationLogo.GetButtonSize();
			playerCreationLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(playerCreationLogo.size.X), GlobalFunctions.PerfectMidPosY(playerCreationLogo.size.Y) - 270);
			uiBox.inGameData = new Rectangle((int)GlobalFunctions.PerfectMidPosX(576), (int)GlobalFunctions.PerfectMidPosY(384), 576, 384);
			playerNameInput.position = new Vector2(uiBox.inGameData.X + 324, uiBox.inGameData.Y + 52);
			if (menuButtons[0].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 1;
				}
			}
			else if (menuButtons[1].hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.gameState = 6;
				}
			}
		}

		public virtual void Draw()
		{
			playerCreationLogo.DrawString();
			uiBox.DrawTexture();
			Globals.spriteBatch.Draw(Globals.player.texture, new Rectangle((int)playerNameInput.position.X + 50, (int)playerNameInput.position.Y + 80, 96, 192), new Rectangle(0, 0, 16, 32), Color.White); //torso
			Globals.spriteBatch.Draw(Globals.player.texture, new Rectangle((int)playerNameInput.position.X + 50, (int)playerNameInput.position.Y + 80, 96, 192), new Rectangle(96, 0, 16, 32), Color.White); //legs
			Globals.spriteBatch.Draw(Globals.player.texture, new Rectangle((int)playerNameInput.position.X + 50, (int)playerNameInput.position.Y + 80, 96, 192), new Rectangle(48, 0, 16, 32), Color.White); //hands
			Globals.spriteBatch.Draw(Globals.player.texture, new Rectangle((int)playerNameInput.position.X + 50, (int)playerNameInput.position.Y + 80, 96, 192), new Rectangle(144, 0, 16, 32), Color.White); //shoulders
			playerNameInput.DrawTextInput();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
		}
	}
}
