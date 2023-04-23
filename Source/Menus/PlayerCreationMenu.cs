using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.SavedData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace Honeymoon.Source.Menus
{
	public class PlayerCreationMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static List<MenuButton> beekeperDoll = new List<MenuButton>();
		private static MenuButton playerCreationLogo, uiBox, pantsColorText, shirtColorText;
		private static Color[] clothesColors = new Color[5] { Color.Green, Color.Pink, Color.BlueViolet, Color.Gold, Color.OrangeRed };
		private OptionValueSelector shirtsSelector, pantsSelector;
		private TextInput nameInput;

		public PlayerCreationMenu()
		{
			playerCreationLogo = new MenuButton(FontManager.hm_f_menu, "Create beekeeper", Vector2.Zero, 5, Color.White);
			pantsColorText = new MenuButton(FontManager.hm_f_default, "Pants Color: ", Vector2.Zero, 1.3f, Color.Brown);
			shirtColorText = new MenuButton(FontManager.hm_f_default, "Shirt Color: ", Vector2.Zero, 1.3f, Color.Brown);
			uiBox = new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Vector2.Zero, new Rectangle(0, 0, 96, 64), 6, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Vector2.Zero, new Rectangle(0, 0, 16, 32), 5, Color.White)); //torso
			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Vector2.Zero, new Rectangle(96, 0, 16, 32), 5, Color.White)); //pants
			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_shirts"), Vector2.Zero, new Rectangle(0, 0, 16, 32), 5, Color.White)); //shirt
			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Vector2.Zero, new Rectangle(48, 0, 16, 32), 5, Color.White)); //hands
			beekeperDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Vector2.Zero, new Rectangle(144, 0, 16, 32), 5, Color.White)); //shoulders
			shirtsSelector = new OptionValueSelector(Vector2.Zero, 1, clothesColors.Length - 1, 4, 30);
			pantsSelector = new OptionValueSelector(Vector2.Zero, 1, clothesColors.Length - 1, 4, 30);
			nameInput = new TextInput("ziemblox", Vector2.Zero);
			ResolutionReload();
		}

		public virtual void ResolutionReload()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
			}
			playerCreationLogo.position = new Vector2(playerCreationLogo.PerfectMidPositionText().X, 20);
			uiBox.position = new Vector2(uiBox.PerfectMidPositionTexture().X, uiBox.PerfectMidPositionTexture().Y);
			shirtColorText.position = new Vector2(uiBox.position.X + 50, uiBox.position.Y + 110);
			pantsColorText.position = new Vector2(uiBox.position.X + 50, uiBox.position.Y + 150);
			foreach (MenuButton bodypiece in beekeperDoll)
			{
				bodypiece.position = new Vector2((int)uiBox.position.X + 380, (int)uiBox.position.Y + 170);
			}
			shirtsSelector.position = new Vector2((int)shirtColorText.position.X + 160, (int)shirtColorText.position.Y);
			pantsSelector.position = new Vector2((int)pantsColorText.position.X + 160, (int)pantsColorText.position.Y);
			nameInput.position = new Vector2(uiBox.position.X + 307, uiBox.position.Y + 50);
		}
		public virtual void Update()
		{
			beekeperDoll[2].color = clothesColors[shirtsSelector.value];
			beekeperDoll[4].color = clothesColors[shirtsSelector.value];
			beekeperDoll[1].color = clothesColors[pantsSelector.value];
			shirtsSelector.UpdateSelector();
			pantsSelector.UpdateSelector();
			nameInput.UpdateTextInput();
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
				menuButtons[i].Update();
			}
			if (menuButtons[0].IsHoveredAndClicked() && !string.IsNullOrEmpty(nameInput.text) && !CheckIfProfileExists(nameInput.text))
			{
				PlayerSave playerSave = new PlayerSave
				{
					Name = nameInput.text,
					shirtColor = clothesColors[shirtsSelector.value],
					pantsColor = clothesColors[pantsSelector.value],
					mapId = 0,
					Position = new Vector2(1637, 1487),
					walkSpeed = 0.18f
				};
				CreatePlayerProfile(playerSave);
				PlayerSelectionMenu.LoadPlayerProfiles();
				Globals.gameState = 6;
			}
			else if (menuButtons[1].IsHoveredAndClicked())
			{
				Globals.gameState = 6;
			}

			if (CheckIfProfileExists(nameInput.text))
			{
				nameInput.color = Color.Red;
			}
			else
			{
				nameInput.color = Color.Black;
			}
		}
		public void CreatePlayerProfile(PlayerSave playersave)
		{
			if (!CheckIfProfileExists(playersave.Name))
			{
				string profilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles\\" + playersave.Name + ".yaml");
				var stringBuilder = new StringBuilder();
				var serializer = new SerializerBuilder().Build();
				stringBuilder.AppendLine(serializer.Serialize(playersave));
				using (StreamWriter writer = new StreamWriter(profilePath))
				{
					serializer.Serialize(writer, playersave);
				}
			}
		}
		public bool CheckIfProfileExists(string name)
		{
			if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles\\" + name + ".yaml")))
			{
				return true;
			}
			return false;
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
			nameInput.DrawTextInput();

			if (CheckIfProfileExists(nameInput.text))
			{
				Globals.spriteBatch.DrawString(FontManager.hm_f_outline, "profile with name '" + nameInput.text + "' already exists!", new Vector2(uiBox.inGameData.X - 120, uiBox.inGameData.Y - 55), Color.Red, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
			}
			else if (string.IsNullOrEmpty(nameInput.text))
			{
				Globals.spriteBatch.DrawString(FontManager.hm_f_outline, "name cannot be empty!", new Vector2(uiBox.inGameData.X + 40, uiBox.inGameData.Y - 55), Color.Red, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
			}
		}
	}
}
