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
		public OptionBoolSelector deleteConfirm;
		private OptionValueSelector worldCellsSelector;
		private protected int deleteIndex;
		private protected static string worldsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds");

		public WorldSelectionMenu()
		{
			logo = new MenuButton(FontManager.hm_f_menu, "World Selection", Vector2.Zero, 5, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create new ", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3, Color.White));
			deleteConfirm = new OptionBoolSelector();
			worldCellsSelector = new OptionValueSelector(Vector2.Zero, 0, 666, 6, 100);
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
		public virtual void DeleteWorldSave(WorldSave linkedsave)
		{
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds\\" + linkedsave.name + ".dat");
			System.GC.Collect();
			System.GC.WaitForPendingFinalizers();
			File.Delete(path);
			LoadWorldSaves();
			worldCellsSelector.value = 0;
		}
		public virtual void Update()
		{
			if (!deleteConfirm.draw)
			{
				if (worldSelectionCells.Count > 0)
				{
					if (worldSelectionCells.Count > 1)
					{
						worldCellsSelector.position = new Vector2((int)GlobalFunctions.PerfectMidPosX(worldCellsSelector.inGameData.Width) - worldCellsSelector.distance / 2, GlobalFunctions.PerfectMidPosY(worldCellsSelector.inGameData.Width) + 100);
						worldCellsSelector.UpdateSelector();
						worldCellsSelector.maxValue = worldSelectionCells.Count - 1;
					}
					for (int i = worldCellsSelector.value; i < worldCellsSelector.value + 1; i++)
					{
						worldSelectionCells[i].Update();
						worldSelectionCells[i].position = new Vector2(Globals.windowSize.X / 2 - worldSelectionCells[i].inGameData.Width / 2, GlobalFunctions.PerfectMidPosY(worldSelectionCells[i].inGameData.Width) + 100);
						if (worldSelectionCells[i].deleteProfile.inGameData.Contains(Globals.mousePosition))
						{
							if (InputManager.IsLeftButtonNewlyPressed())
							{
								deleteConfirm.draw = true;
								deleteIndex = i;
							}
						}
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
			else
			{
				deleteConfirm.Update();
				if (deleteConfirm.yesButton.IsHoveredAndClicked())
				{
					DeleteWorldSave(worldSelectionCells[deleteIndex].linkedSave);
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
			logo.DrawString();
			foreach (MenuButton button in menuButtons)
			{
				button.DrawString();
			}
			if (worldSelectionCells.Count > 0)
			{
				for (int i = worldCellsSelector.value; i < worldCellsSelector.value + 1; i++)
				{
					worldSelectionCells[i].Draw();
				}
			}
			if (worldSelectionCells.Count > 1)
			{
				worldCellsSelector.DrawSelector();
			}
			if (deleteConfirm.draw)
			{
				deleteConfirm.DrawBoolSelector();
			}
		}
	}
}
