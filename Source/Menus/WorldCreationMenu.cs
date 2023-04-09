using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.SavedData;
using Honeymoon.Source.World.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
		private List<MapObject[]> GenerateWorld()
		{
			List<MapObject[]> objectsList = new List<MapObject[]>();
			for (int i = 0; i < Map.maps.Count; i++)
			{
				int totalTiles = Map.maps[i].tilesHeight * Map.maps[i].tilesWidth;
				MapObject[] mapObjects = new MapObject[totalTiles / 2];
				for (int a = 0; a < totalTiles / 2; a++)
				{
					Vector2 randomPosition = new Vector2(Globals.random.Next(0, Map.maps[i].tilesWidth), Globals.random.Next(0, Map.maps[i].tilesHeight));
					mapObjects[a] = new MapObject(i, Globals.content.Load<Texture2D>("Objects/tree"), new Rectangle(0, 0, 48, 112), "tree", Map.TileIdPosToXY(randomPosition));
					System.Diagnostics.Debug.WriteLine("map: " + i + " object: " + a + " Creating object: " + mapObjects[a].name + ", on map: " + mapObjects[a].mapid + ", on position: " + mapObjects[a].position);
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
			//var stream = new MemoryStream();
			var fs = new FileStream(path, FileMode.Create);
			using (var writer = XmlDictionaryWriter.CreateBinaryWriter(fs))
			{
				serializer.WriteObject(writer, newWorld);
			}
			System.Diagnostics.Debug.WriteLine("-----------------------SUCCESFULLY GENERATED WORLD WITH NAME: " + newWorld.name + " -----------------------------------------------");

			WorldSave loadedWorld = LoadSave(newWorld.name);
			foreach (var data in loadedWorld.mapObjectsData)
			{
				foreach (var obj in data)
				{
					System.Diagnostics.Debug.WriteLine(obj.mapid + " " + obj.name + " " + obj.position);
				}
			}
			System.Diagnostics.Debug.WriteLine("-----------------------SUCCESFULLY LOADED WORLD WITH NAME: " + newWorld.name + " -----------------------------------------------");
		}
		private static WorldSave LoadSave(string worldName)
		{
			List<MapObject[]> objectsList = new List<MapObject[]>();
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds\\" + worldName + ".dat");
			var serializer = new DataContractSerializer(typeof(WorldSave));
			FileStream fs = new FileStream(path, FileMode.Open);
			using (var reader = XmlDictionaryReader.CreateBinaryReader(fs, XmlDictionaryReaderQuotas.Max))
			{
				WorldSave worldSave = (WorldSave)serializer.ReadObject(reader);
				return worldSave;
			}

			//WorldSave worldSave = (WorldSave)serializer.ReadObject(reader);
			//return worldSave;

			//var serializer = new DataContractSerializer(typeof(T));
			//using (var stream = new MemoryStream(data))
			//using (var reader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max))
			//{
			//	return (T)serializer.ReadObject(reader);
			//}
		}
		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i + 250);
			}
			worldName.UpdateTextInput();
			if (menuButtons[0].IsHoveredAndClicked())
			{
				GenerateSaveFile(worldName.text);
			}
			if (menuButtons[1].IsHoveredAndClicked())
			{
				Globals.gameState = 8;
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
		}
	}
}
