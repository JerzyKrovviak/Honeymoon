using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.SavedData;
using Honeymoon.Source.World.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Honeymoon.Source.Menus
{
	public class WorldSelectionMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton logo;
		private static List<WorldSelectionCell> worldSelectionCells;
		private protected static string worldsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds");

		public WorldSelectionMenu()
		{
			logo = new MenuButton(FontManager.hm_f_menu, "World Selection", Vector2.Zero, 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create new ", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
			}
			logo.position = new Vector2(logo.PerfectMidPositionText().X, 20);
		}
		public static void LoadWorldSaves()
		{
			worldSelectionCells = new List<WorldSelectionCell>();
			var serializer = new DataContractSerializer(typeof(WorldSave));
			string[] fileEntries = Directory.GetFiles(worldsPath, "*.dat*");
			foreach (string fileName in fileEntries)
			{
				System.GC.Collect();
				System.GC.WaitForPendingFinalizers();
				FileStream fs = new FileStream(fileName, FileMode.Open);
				var reader = XmlDictionaryReader.CreateBinaryReader(fs, XmlDictionaryReaderQuotas.Max);
				WorldSave worldSave = (WorldSave)serializer.ReadObject(reader);
				if (!worldSelectionCells.Any(p => p.worldName == worldSave.name))
				{
					worldSelectionCells.Add(new WorldSelectionCell(worldSave));
				}
			}
		}
		public virtual void Update()
		{
			if (worldSelectionCells.Count > 0)
			{
				for (int i = 0; i < worldSelectionCells.Count; i++)
				{
					worldSelectionCells[i].Update();
					worldSelectionCells[i].position = new Vector2(Globals.windowSize.X / 2 - worldSelectionCells[i].inGameData.Width / 2, GlobalFunctions.PerfectMidPosY(worldSelectionCells[i].inGameData.Width) + 100);
				}
			}
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
			}

			if (menuButtons[0].IsHoveredAndClicked())
			{
				Globals.gameState = 9;
			}
			else if (menuButtons[1].IsHoveredAndClicked())
			{
				Globals.gameState = 6;
			}
		}
		public virtual void Draw()
		{
			logo.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
			if (worldSelectionCells.Count > 0)
			{
				for (int i = 0; i < worldSelectionCells.Count; i++)
				{
					worldSelectionCells[i].Draw();
				}
			}
		}
	}
}
