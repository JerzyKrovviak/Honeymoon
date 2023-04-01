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
			nameInput = new TextInput("ziemblox", Vector2.Zero);
		}

		public virtual void UpdateResolutionPositions()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i) + 250);
			}
			playerCreationLogo.size = playerCreationLogo.GetButtonSize();
			playerCreationLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(playerCreationLogo.size.X), GlobalFunctions.PerfectMidPosY(playerCreationLogo.size.Y) - 310);
			uiBox.inGameData = new Rectangle((int)GlobalFunctions.PerfectMidPosX(576), (int)GlobalFunctions.PerfectMidPosY(384), 576, 384);
			shirtColorText.position = new Vector2(uiBox.inGameData.X + 50, uiBox.inGameData.Y + 110);
			pantsColorText.position = new Vector2(uiBox.inGameData.X + 50, uiBox.inGameData.Y + 150);
			shirtsSelector.inGameData.X = (int)shirtColorText.position.X + 160;
			shirtsSelector.inGameData.Y = (int)shirtColorText.position.Y;
			pantsSelector.inGameData.X = (int)pantsColorText.position.X + 160;
			pantsSelector.inGameData.Y = (int)pantsColorText.position.Y;
			nameInput.position = new Vector2(uiBox.inGameData.X + 307, uiBox.inGameData.Y + 50);

			foreach (MenuButton bodypiece in beekeperDoll)
			{
				bodypiece.inGameData = new Rectangle((int)uiBox.inGameData.X + 380, (int)uiBox.inGameData.Y + 170, bodypiece.sourceData.Width * 5, bodypiece.sourceData.Height * 5);
			}
		}
		public virtual void Update()
		{
			UpdateResolutionPositions();
			beekeperDoll[2].color = clothesColors[shirtsSelector.value];
			beekeperDoll[4].color = clothesColors[shirtsSelector.value];
			beekeperDoll[1].color = clothesColors[pantsSelector.value];
			shirtsSelector.UpdateSelector();
			pantsSelector.UpdateSelector();
			nameInput.UpdateTextInput();

			if (menuButtons[0].IsHoveredAndClicked() && !string.IsNullOrEmpty(nameInput.text) && !CheckIfProfileExists(nameInput.text) && CheckProfilesAmount() != 4)
			{
				PlayerSave playerSave = new PlayerSave
				{
					Name = nameInput.text,
					shirtColor = clothesColors[shirtsSelector.value],
					pantsColor = clothesColors[pantsSelector.value],
					Position = new Vector2(20, 20)
				};
				CreatePlayerProfile(playerSave);
				PlayerSelectionMenu.LoadPlayerProfiles();
				if (CheckProfilesAmount() < 5)
				{
					Globals.gameState = 6;
				}
			}
			else if (menuButtons[1].IsHoveredAndClicked())
			{
				Globals.gameState = 6;
			}

			if (CheckIfProfileExists(nameInput.text) || CheckProfilesAmount() >= 4)
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
		public int CheckProfilesAmount()
		{
			string[] fileEntries = Directory.GetFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles"), "*.yaml*");
			return fileEntries.Length;
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
				Globals.spriteBatch.DrawString(FontManager.hm_f_outline, "profile with name " + nameInput.text + " already exists!", new Vector2(uiBox.inGameData.X - 120, uiBox.inGameData.Y - 55), Color.Red, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
			}
			else if (string.IsNullOrEmpty(nameInput.text))
			{
				Globals.spriteBatch.DrawString(FontManager.hm_f_outline, "name cannot be empty!", new Vector2(uiBox.inGameData.X + 40, uiBox.inGameData.Y - 55), Color.Red, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
			}
			else if (CheckProfilesAmount() >= 4)
			{
				Globals.spriteBatch.DrawString(FontManager.hm_f_outline, "maximum amount of 4 profiles reached!", new Vector2(uiBox.inGameData.X - 80, uiBox.inGameData.Y - 55), Color.Red, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
			}
		}
	}
}
