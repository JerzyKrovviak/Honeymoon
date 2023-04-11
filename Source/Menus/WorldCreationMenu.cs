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
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace Honeymoon.Source.Menus
{
	public class WorldCreationMenu
	{
		private static MenuButton logo;
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static TextInput worldName;
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
		private List<List<MapObject>> GenerateWorld()
		{
			List<List<MapObject>> objectsList = new List<List<MapObject>>();
			XmlDataCache.ObjectData objectData = Globals.content.Load<XmlDataCache.ObjectData>("Data/ObjectData");
			for (int i = 0; i < Map.maps.Count; i++)
			{
				int totalTiles = Map.maps[i].tilesHeight * Map.maps[i].tilesWidth;
				int objectAmount = Globals.random.Next(0, totalTiles);
				int tileid = 0;
				Vector2 destination = Vector2.Zero;
				List<MapObject> mapObjects = new List<MapObject>();
				List<Vector2> objectpositions = new List<Vector2>();
				int[] topLayer = new int[totalTiles];
				foreach (var layer in Map.maps[i].layers)
				{
					if (layer.layerId == 1)
					{
						topLayer = layer.tileData;
					}
				}
				for (int y = 0; y < Map.maps[i].tilesHeight; y++)
				{
					for (int x = 0; x < Map.maps[i].tilesWidth; x++)
					{
						Vector2 objectposition = new Vector2(x, y);
						objectpositions.Add(objectposition);
					}
				}
				for (int a = 0; a < objectAmount; a++)
				{
					int dataindex = Globals.random.Next(0, objectData.objectData.Count);
					string randomObject = objectData.objectData[dataindex].Name;
					if (objectData.objectData[dataindex].spawnableTiles.Contains(topLayer[a]))
					{
						mapObjects.Add(new MapObject(i, randomObject, Map.TileIdPosToXY(objectpositions[a])));
						System.Diagnostics.Debug.WriteLine("objectid: " + a + " name: " + objectData.objectData[dataindex].Name + " can be spawned on tile: " + tileid + " position: " + objectpositions[a]);
					}
					else
					{
						System.Diagnostics.Debug.WriteLine("objectid: " + a + " name: " + objectData.objectData[dataindex].Name + " CANNOT be spawned on tile: " + tileid + " position: " + objectpositions[a]);
					}
				}
				objectsList.Add(mapObjects);
			}
			return objectsList;
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
			using (var writer = XmlDictionaryWriter.CreateBinaryWriter(fs))
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
