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
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton playerSelectionMenuLogo;
		private static string profilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles");
		public static List<PlayerSave> playerProfiles;
		private static List <PlayerSelectionCell> playerSelectionCells;

		public PlayerSelectionMenu()
		{
			playerSelectionMenuLogo = new MenuButton(FontManager.hm_f_menu, "Beekeeper Selection", Vector2.Zero, 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create new ", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
			playerSelectionCells = new List<PlayerSelectionCell>();
			playerProfiles = LoadPlayerProfiles();
		}
		public static void CreateFolderIfNotExists()
		{
			if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles")))
			{
				Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles"));
			}
		}
		public static List<PlayerSave> LoadPlayerProfiles()
		{
			CreateFolderIfNotExists();
			List<PlayerSave> playerProfiles = new List<PlayerSave>();
			var deserializer = new DeserializerBuilder().Build();
			string[] fileEntries = Directory.GetFiles(profilesPath, "*.yaml*");
			foreach (string fileName in fileEntries)
			{
				var playerProfile = deserializer.Deserialize<PlayerSave>(File.OpenText(fileName));
				playerProfiles.Add(playerProfile);
				if (!playerSelectionCells.Any(p => p.name == playerProfile.Name))
				{
					playerSelectionCells.Add(new PlayerSelectionCell(playerProfile));
				}
			}
			return playerProfiles;
		}
		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(GlobalFunctions.PerfectMidPosX(menuButtons[i].size.X), GlobalFunctions.PerfectMidPosY(menuButtons[i].size.Y - 170 * i) + 250);
			}
			foreach (PlayerSelectionCell cell in playerSelectionCells)
			{
				cell.Update();
			}

			if (playerSelectionCells.ElementAtOrDefault(0) != null)
			{
				playerSelectionCells[0].position = new Vector2(Globals.windowSize.X / 2 - playerSelectionCells[0].width - 10, Globals.windowSize.Y / 2 - playerSelectionCells[0].height / 2 - 100);
			}
			if (playerSelectionCells.ElementAtOrDefault(1) != null)
			{
				playerSelectionCells[1].position = new Vector2(Globals.windowSize.X / 2 + 10, Globals.windowSize.Y / 2 - playerSelectionCells[1].height / 2 - 100);
			}
			if (playerSelectionCells.ElementAtOrDefault(2) != null)
			{
				playerSelectionCells[2].position = new Vector2(playerSelectionCells[0].position.X, playerSelectionCells[0].position.Y + playerSelectionCells[0].height + 20);
			}
			if (playerSelectionCells.ElementAtOrDefault(3) != null)
			{
				playerSelectionCells[3].position = new Vector2(playerSelectionCells[1].position.X, playerSelectionCells[1].position.Y + playerSelectionCells[1].height + 20);
			}

			playerSelectionMenuLogo.size = playerSelectionMenuLogo.GetButtonSize();
			playerSelectionMenuLogo.position = new Vector2(GlobalFunctions.PerfectMidPosX(playerSelectionMenuLogo.size.X), 50);

			if (menuButtons[0].IsHoveredAndClicked())
			{
				Globals.gameState = 7;
			}
			else if (menuButtons[1].IsHoveredAndClicked())
			{
				Globals.gameState = 0;
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
		}
	}
}
