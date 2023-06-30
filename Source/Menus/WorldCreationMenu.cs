using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.SavedData;
using Honeymoon.Source.World.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Honeymoon.Source.Menus
{
	public class WorldCreationMenu
	{
		private MenuButton logo;
		private List<MenuButton> menuButtons = new List<MenuButton>();
		private TextInput worldName;
		public WorldCreationMenu()
		{
			logo = new MenuButton(FontManager.hm_f_menu, "Create World", Vector2.Zero, 5f, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Create", Vector2.Zero, 3f, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Back", Vector2.Zero, 3f, Color.White));
			worldName = new TextInput("My world", Vector2.Zero);
			CreateFolderIfNotExists();
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			logo.position = new Vector2(logo.PerfectMidPositionText().X, 20);
			worldName.position = worldName.PerfectMidPositionTexture();
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
			}
		}
		public static void CreateFolderIfNotExists()
		{
			if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds")))
			{
				Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds"));
			}
		}
		private List<MapObject> GenerateWorld()
		{
			//random world objects generation
			List<MapObject> mapObjects = new List<MapObject>();
			XmlDataCache.ObjectData objectData = Globals.content.Load<XmlDataCache.ObjectData>("Data/ObjectData");
			for (int i = 0; i < Map.maps.Count; i++)
			{
				List<Vector2> occupiedTiles = new List<Vector2>();
				foreach (var layer in Map.maps[i].layers)
				{
					for (int y = 0; y < Map.maps[i].tilesHeight; y++)
					{
						for (int x = 0; x < Map.maps[i].tilesWidth; x++)
						{
							int gid = layer.tileData[y * layer.tilesWidth + x];
							if (gid == 0) continue;
							for (int o = 0; o < objectData.objectData.Count; o++)
							{
								int chance = Globals.random.Next(0, 100);
								if (objectData.objectData[o].spawnableTiles.Contains(gid))
								{
									if (chance <= objectData.objectData[o].spawnChance)
									{
										Vector2 occupedTile = new Vector2(x, y);
										if (!occupiedTiles.Contains(occupedTile))
										{
											mapObjects.Add(new MapObject(i, objectData.objectData[o].Name, Map.TileIdPosToXY(new Vector2(x, y))));
											occupiedTiles.Add(occupedTile);
										}
									}
								}
							}
						}
					}
				}
			}
			return mapObjects;
		}
		private void GenerateSaveFile(string name)
		{
			var generatedWorld = GenerateWorld();
			WorldSave newWorld = new WorldSave
			{
				name = name,
				mapObjectsData = generatedWorld
			};
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds\\" + newWorld.name + ".dat");
			var serializer = new DataContractSerializer(typeof(WorldSave));
			var fs = new FileStream(path, FileMode.Create);
			using (XmlWriter writer = XmlDictionaryWriter.CreateBinaryWriter(fs))
			{
				serializer.WriteObject(writer, newWorld);
			}
			System.Diagnostics.Debug.WriteLine("-----------------------SUCCESFULLY GENERATED WORLD WITH NAME: " + newWorld.name + " -----------------------------------------------");
		}
		private static WorldSave LoadSave(string worldName)
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds\\" + worldName + ".dat");
			var serializer = new DataContractSerializer(typeof(WorldSave));
			FileStream fs = new FileStream(path, FileMode.Open);
			using (var reader = XmlDictionaryReader.CreateBinaryReader(fs, XmlDictionaryReaderQuotas.Max))
			{
				WorldSave worldSave = (WorldSave)serializer.ReadObject(reader);
				return worldSave;
			}
		}
		public bool CheckIfWorldExists(string name)
		{
			if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds\\" + name + ".dat")))
			{
				return true;
			}
			return false;
		}
		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
			}
			worldName.UpdateTextInput();
			if (menuButtons[0].IsHoveredAndClicked() && !string.IsNullOrEmpty(worldName.text) && !CheckIfWorldExists(worldName.text))
			{
				GenerateSaveFile(worldName.text);
				WorldSelectionMenu.LoadWorldSaves();
				Globals.gameState = 8;
			}
			if (menuButtons[1].IsHoveredAndClicked())
			{
				Globals.gameState = 8;
			}

			if (CheckIfWorldExists(worldName.text))
			{
				worldName.color = Color.Red;
			}
			else
			{
				worldName.color = Color.Black;
			}
		}
		public virtual void Draw()
		{
			logo.DrawString();
			foreach (var menuButton in menuButtons)
			{
				menuButton.DrawString();
			}
			worldName.DrawTextInput();

			if (CheckIfWorldExists(worldName.text))
			{
				Globals.spriteBatch.DrawString(FontManager.hm_f_outline, "World with name '" + worldName.text + "' already exists!", new Vector2(worldName.inGameData.X - 290, worldName.inGameData.Y - 70), Color.Red, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
			}
			else if (string.IsNullOrEmpty(worldName.text))
			{
				Globals.spriteBatch.DrawString(FontManager.hm_f_outline, "Name cannot be empty!", new Vector2(worldName.inGameData.X - 100, worldName.inGameData.Y - 70), Color.Red, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
			}
		}
	}
}
