using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.World.Creatures.Player;
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
		private static List<MenuButton> beekeperDoll = new List<MenuButton>();
		private static MenuButton playerCreationLogo, uiBox, pantsColorText, shirtColorText;
		//private static Color[] shirtColors = new Color[5] { Color.LightGreen, Color.LightPink, Color.LightSteelBlue, Color.Gold, Color.LightBlue };
		//private static Color[] pantsColors = new Color[3] { Color.Black, Color.Brown, Color.Gray };
		private static Color[] clothesColors = new Color[5] { Color.LightGreen, Color.LightPink, Color.LightSteelBlue, Color.Gold, Color.LightBlue };
		private OptionValueSelector shirtsSelector, pantsSelector;

		public PlayerCreationMenu()
		{
			playerCreationLogo = new MenuButton(FontManager.hm_f_menu, "Create beekeeper", Vector2.Zero, 5, Color.White);
			pantsColorText = new MenuButton(FontManager.hm_f_default, "Pants Color: ", Vector2.Zero, 1.3f, Color.Brown);
			shirtColorText = new MenuButton(FontManager.hm_f_default, "Shirt Color: ", Vector2.Zero, 1.3f, Color.Brown);
			uiBox = new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Rectangle.Empty, new Rectangle(0, 0, 96, 64));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));

			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Rectangle.Empty, new Rectangle(0, 0, 16, 32))); //torso
			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Rectangle.Empty, new Rectangle(96, 0, 16, 32))); //pants
			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_shirts"), Rectangle.Empty, new Rectangle(0, 0, 16, 32))); //shirt
			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Rectangle.Empty, new Rectangle(48, 0, 16, 32))); //hands
			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Rectangle.Empty, new Rectangle(144, 0, 16, 32))); //shoulders

			shirtsSelector = new OptionValueSelector(1, clothesColors.Length);
			pantsSelector = new OptionValueSelector(1, clothesColors.Length);
		}

		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i) + 250);
			}
			playerCreationLogo.size = playerCreationLogo.GetButtonSize();
			playerCreationLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(playerCreationLogo.size.X), GlobalFunctions.PerfectMidPosY(playerCreationLogo.size.Y) - 270);
			shirtColorText.position = new Vector2(uiBox.inGameData.X + 50, uiBox.inGameData.Y + 110);

			beekeperDoll[2].color = clothesColors[shirtsSelector.value];
			beekeperDoll[4].color = clothesColors[shirtsSelector.value];
			beekeperDoll[1].color = clothesColors[pantsSelector.value];

			pantsColorText.position = new Vector2(uiBox.inGameData.X + 50, uiBox.inGameData.Y + 150);
			uiBox.inGameData = new Rectangle((int)GlobalFunctions.PerfectMidPosX(576), (int)GlobalFunctions.PerfectMidPosY(384), 576, 384);

			shirtsSelector.UpdateSelector();
			shirtsSelector.inGameData.X = (int)shirtColorText.position.X + 160;
			shirtsSelector.inGameData.Y = (int)shirtColorText.position.Y;
			pantsSelector.UpdateSelector();
			pantsSelector.inGameData.X = (int)pantsColorText.position.X + 160;
			pantsSelector.inGameData.Y = (int)pantsColorText.position.Y;

			foreach (MenuButton bodypiece in beekeperDoll)
			{
				bodypiece.inGameData = new Rectangle((int)uiBox.inGameData.X + 380, (int)uiBox.inGameData.Y + 170, bodypiece.sourceData.Width * 5, bodypiece.sourceData.Height * 5);
			}

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
			shirtColorText.DrawString();
			pantsColorText.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
			foreach (MenuButton bodypiece in beekeperDoll)
			{
				bodypiece.DrawTexture();
			}
			shirtsSelector.DrawSelector();
			pantsSelector.DrawSelector();
		}
	}
}
