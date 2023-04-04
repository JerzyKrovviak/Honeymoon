using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.SavedData;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Honeymoon.Source.Menus
{
	public class PlayerSelectionMenu
	{
		private protected static List<MenuButton> menuButtons = new List<MenuButton>();
		private protected static MenuButton playerSelectionMenuLogo;
		private protected static string profilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles");
		private protected static List<PlayerProfileCell> playerSelectionCells;
		public OptionBoolSelector deleteConfirm;
		private OptionValueSelector playerCellsSelector;
		private protected int deleteIndex;

		public PlayerSelectionMenu()
		{
			playerSelectionMenuLogo = new MenuButton(FontManager.hm_f_menu, "Beekeeper Selection", Vector2.Zero, 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create new ", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
			deleteConfirm = new OptionBoolSelector();
			playerCellsSelector = new OptionValueSelector(Vector2.Zero, 0, 666, 6, 100);
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			playerSelectionMenuLogo.position = new Vector2(playerSelectionMenuLogo.PerfectMidPositionText().X, 50);
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
			}
		}
		public static void CreateFolderIfNotExists()
		{
			if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles")))
			{
				Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles"));
			}
		}
		public static void LoadPlayerProfiles()
		{
			CreateFolderIfNotExists();
			playerSelectionCells = new List<PlayerProfileCell>();
			var deserializer = new DeserializerBuilder().Build();
			string[] fileEntries = Directory.GetFiles(profilesPath, "*.yaml*");
			foreach (string fileName in fileEntries)
			{
				var playerProfile = deserializer.Deserialize<PlayerSave>(File.OpenText(fileName));
				if (!playerSelectionCells.Any(p => p.name == playerProfile.Name))
				{
					playerSelectionCells.Add(new PlayerProfileCell(playerProfile));
				}
			}
		}
		public virtual void DeleteProfile(PlayerSave linkedsave)
		{
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles\\" + linkedsave.Name + ".yaml");
			System.GC.Collect();
			System.GC.WaitForPendingFinalizers();
			File.Delete(path);
			LoadPlayerProfiles();
			playerCellsSelector.value = 0;
		}
		public virtual void Update()
		{
			if (!deleteConfirm.draw)
			{
				//playerSelectionMenuLogo.position = new Vector2(playerSelectionMenuLogo.PerfectMidPositionText().X, 50);
				if (playerSelectionCells.Count > 1)
				{
					playerCellsSelector.position = new Vector2((int)GlobalFunctions.PerfectMidPosX(playerCellsSelector.inGameData.Width) - playerCellsSelector.distance / 2, GlobalFunctions.PerfectMidPosY(playerCellsSelector.inGameData.Width) + 100);
					playerCellsSelector.UpdateSelector();
					playerCellsSelector.maxValue = playerSelectionCells.Count - 1;
				}
				for (int i = 0; i < menuButtons.Count; i++)
				{
					menuButtons[i].Update();
					menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
				}
				if (playerSelectionCells.Count > 0)
				{
					for (int i = playerCellsSelector.value; i < playerCellsSelector.value + 1; i++)
					{
						playerSelectionCells[i].Update();
						playerSelectionCells[i].position = new Vector2(Globals.windowSize.X / 2 - playerSelectionCells[i].width / 2, GlobalFunctions.PerfectMidPosY(playerSelectionCells[i].inGameData.Width) + 100);
						if (playerSelectionCells[i].deleteProfile.inGameData.Contains(Globals.mousePosition))
						{
							if (InputManager.IsLeftButtonNewlyPressed())
							{
								deleteConfirm.draw = true;
								deleteIndex = i;
							}
						}
					}
				}
				if (menuButtons[0].IsHoveredAndClicked())
				{
					Globals.gameState = 7;
				}
				else if (menuButtons[1].IsHoveredAndClicked())
				{
					Globals.gameState = 0;
				}
			}
			else
			{
				deleteConfirm.Update();
				if (deleteConfirm.yesButton.IsHoveredAndClicked())
				{
					DeleteProfile(playerSelectionCells[deleteIndex].linkedSave);
					AudioManager.soundBank.PlayCue("trashChar");
					deleteConfirm.draw = false;
				}
				else if (deleteConfirm.noButton.IsHoveredAndClicked())
				{
					deleteConfirm.draw = false;
				}
			}
		}
		public virtual void Draw()
		{
			playerSelectionMenuLogo.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
			if (playerSelectionCells.Count > 0)
			{
				for (int i = playerCellsSelector.value; i < playerCellsSelector.value + 1; i++)
				{
					playerSelectionCells[i].Draw();
				}
			}
			if (playerSelectionCells.Count > 1)
			{
				playerCellsSelector.DrawSelector();
			}
			if (deleteConfirm.draw)
			{
				deleteConfirm.DrawBoolSelector();
			}
		}
	}
}
