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
		private protected int deleteIndex;

		public PlayerSelectionMenu()
		{
			playerSelectionMenuLogo = new MenuButton(FontManager.hm_f_menu, "Beekeeper Selection", Vector2.Zero, 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create new ", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
			deleteConfirm = new OptionBoolSelector();
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
		}
		public virtual void Update()
		{
			if (!deleteConfirm.draw)
			{
				for (int i = 0; i < menuButtons.Count; i++)
				{
					menuButtons[i].Update();
					menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i) + 250);
				}

				playerSelectionMenuLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(playerSelectionMenuLogo.GetButtonSize().X), 50);

				for (int i = 0; i < playerSelectionCells.Count; i++)
				{
					playerSelectionCells[i].Update();
					playerSelectionCells[i].position = new Vector2(Globals.windowSize.X / 2 - playerSelectionCells[i].width / 2, 180 + (200 * i));
					if (playerSelectionCells[i].deleteProfile.inGameData.Contains(Globals.mousePosition))
					{
						if (InputManager.IsLeftButtonNewlyPressed())
						{
							deleteConfirm.draw = true;
							deleteIndex = i;
							//System.Diagnostics.Debug.WriteLine("deleting cell: " + playerSelectionCells[i].name);
							//DeleteProfile(playerSelectionCells[i].linkedSave);
							//AudioManager.soundBank.PlayCue("trashChar");
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
					System.Diagnostics.Debug.WriteLine("deleting cell: " + playerSelectionCells[deleteIndex].name);
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
			for (int i = 0; i < playerSelectionCells.Count; i++)
			{
				playerSelectionCells[i].Draw();
			}
			if (deleteConfirm.draw)
			{
				deleteConfirm.DrawBoolSelector();
			}
		}
	}
}
