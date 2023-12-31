﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source;
using Honeymoon.Source.SavedData;
using Honeymoon.Source.World.Creatures.Player;
using Honeymoon.Source.World.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YamlDotNet.Serialization;

namespace Honeymoon.Source.Menus
{
	public class InGameMenu
	{
		private protected MenuButton ingMenuUi, ingMenuTitle, resume, resolution;
		private protected Texture2D background;
		private protected List<MenuButton> ingMenuIcons = new List<MenuButton>();
		private protected int ingMenuMode = 0;
		public OptionBoolSelector confirmExit;
		private protected string[] resolutions = { "Fullscreen", "Windowed", "Windowed Fullscreen" };

		public InGameMenu()
		{
			background = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			Color[] data = new Color[1 * 1];
			for (int pixel = 0; pixel < data.Count(); pixel++)
			{
				data[pixel] = new Color(255, 255, 255, 120);
			}
			background.SetData(data);
			ingMenuUi = new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Vector2.Zero, new Rectangle(96, 0, 96, 64), 6, Color.White);
			ingMenuTitle = new MenuButton(FontManager.hm_f_menu, "General", Vector2.Zero, 2f, Color.White);
			resume = new MenuButton(FontManager.hm_f_menu, "Resume", Vector2.Zero, 3f, Color.White);
			resolution = new MenuButton(FontManager.hm_f_default, "Resolution: ", Vector2.Zero, 1.3f, Color.Black);
			confirmExit = new OptionBoolSelector();
			ingMenuIcons.Add(new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), "General", Vector2.Zero, new Rectangle(68, 64, 9, 9), 5, Color.White)); //general
			ingMenuIcons.Add(new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), "Volume", Vector2.Zero, new Rectangle(77, 64, 9, 9), 5, Color.White)); //volume
			ingMenuIcons.Add(new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), "Video", Vector2.Zero, new Rectangle(86, 64, 9, 9), 5, Color.White)); //video
			ingMenuIcons.Add(new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), "Save & Exit", Vector2.Zero, new Rectangle(95, 64, 9, 9), 5, Color.White)); //exit
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			ingMenuUi.position = new Vector2(-(int)Camera.cameraOffsetX + ingMenuUi.PerfectMidPositionTexture().X, -(int)Camera.cameraOffsetY + ingMenuUi.PerfectMidPositionTexture().Y);
			for (int i = 0; i < ingMenuIcons.Count; i++)
			{
				ingMenuIcons[i].position = new Vector2(ingMenuUi.position.X + 54, ingMenuUi.position.Y + 54 + (ingMenuIcons[i].inGameData.Width + 30) * i);
			}
			ingMenuTitle.position = new Vector2(-(int)Camera.cameraOffsetX + ingMenuTitle.PerfectMidPositionText().X, ingMenuUi.position.Y + 36);
			resume.position = new Vector2(-(int)Camera.cameraOffsetX + resume.PerfectMidPositionText().X, -(int)Camera.cameraOffsetY + resume.PerfectMidPositionText().Y + 240);
			resolution.position = new Vector2(-(int)Camera.cameraOffsetX + resolution.PerfectMidPositionText().X - 30, -(int)Camera.cameraOffsetY + resolution.PerfectMidPositionText().Y - 80);
			resolution.hitbox = new Rectangle((int)resolution.position.X + (int)Camera.cameraOffsetX, (int)resolution.position.Y + (int)Camera.cameraOffsetY, (int)resolution.GetTextBtnSize().X, (int)resolution.GetTextBtnSize().Y);
		}
		public void SavePlayerProfile(PlayerSave playersave)
		{
			string profilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles\\" + playersave.Name + ".yaml");
			var stringBuilder = new StringBuilder();
			var serializer = new SerializerBuilder().Build();
			stringBuilder.AppendLine(serializer.Serialize(playersave));
			System.GC.Collect();
			System.GC.WaitForPendingFinalizers();
			using (StreamWriter writer = new StreamWriter(profilePath))
			{
				serializer.Serialize(writer, playersave);
			}
		}
		private void SaveWorld(WorldSave worldsave)
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerWorlds\\" + worldsave.name + ".dat");
			var serializer = new DataContractSerializer(typeof(WorldSave));
			var fs = new FileStream(path, FileMode.Create);
			System.GC.Collect();
			System.GC.WaitForPendingFinalizers();
			using (var writer = XmlDictionaryWriter.CreateBinaryWriter(fs))
			{
				serializer.WriteObject(writer, worldsave);
			}
			System.Diagnostics.Debug.WriteLine("-----------------------SUCCESFULLY SAVED WORLD WITH NAME: " + worldsave.name + " -----------------------------------------------");
		}
		public virtual void Update()
		{
			if (!confirmExit.draw)
			{
				foreach (MenuButton icon in ingMenuIcons)
				{
					icon.UpdateTextureButton();
					icon.hitbox = new Rectangle((int)icon.position.X + (int)Camera.cameraOffsetX, (int)icon.position.Y + (int)Camera.cameraOffsetY, icon.inGameData.Width, icon.inGameData.Height);
					if (icon.IsButtonHovered())
					{
						icon.color = new Color(Color.White, 70);
					}
					else
					{
						icon.color = Color.White;
					}
				}
				resume.Update();
				if (resume.IsHoveredAndClicked())
				{
					Globals.openIngMenu = false;
				}

				if (ingMenuIcons[0].IsHoveredAndClicked()) //general
				{
					ingMenuMode = 0;
					ingMenuTitle.text = "General";
				}
				else if (ingMenuIcons[1].IsHoveredAndClicked()) //volume
				{
					ingMenuMode = 1;
					ingMenuTitle.text = "Volume";
				}
				else if (ingMenuIcons[2].IsHoveredAndClicked()) //video
				{
					ingMenuMode = 2;
					ingMenuTitle.text = "Video";
				}
				else if (ingMenuIcons[3].IsHoveredAndClicked()) //exit
				{
					confirmExit.draw = true;
				}

				if (ingMenuMode == 2)
				{
					resolution.Update();
					resolution.text = "Resolution: " + resolutions[Globals.selectedResolution - 1];
					if (resolution.IsHoveredAndClicked())
					{
						Globals.selectedResolution++;
						if (Globals.selectedResolution == 1)
						{
							resolution.text = "Resolution: " + resolutions[0];
							Globals.ChangeGameResolution(1);
						}
						if (Globals.selectedResolution == 2)
						{
							resolution.text = "Resolution: " + resolutions[1];
							Globals.ChangeGameResolution(2);
						}
						if (Globals.selectedResolution == 3)
						{
							resolution.text = "Resolution: " + resolutions[2];
							Globals.ChangeGameResolution(3);
						}
						if (Globals.selectedResolution > 3)
						{
							Globals.ChangeGameResolution(1);
							Globals.selectedResolution = 1;
						}
						Globals.gameManager.ResolutionReload();
						Globals.menuManager.ResolutionReload();
					}
				}
			}
			else
			{
				confirmExit.Update();
				if (confirmExit.yesButton.IsHoveredAndClicked())
				{
					PlayerSave playerSave = new PlayerSave
					{
						Name = Globals.player.nickname,
						mapId = Map.GetCurrentMapId(),
						Position = Globals.player.position,
						shirtColor = Globals.player.shirtColor,
						pantsColor = Globals.player.pantsColor,
						bootsColor = Globals.player.bootsColor,
						skinColor = Globals.player.skinColor,
						eyesColor = Globals.player.eyesColor,
						walkSpeed = Globals.player.speed
					};
					WorldSave worldSave = new WorldSave
					{
						name = Globals.objectLayer.linkedSave.name,
						mapObjectsData = Globals.objectLayer.linkedSave.mapObjectsData
					};
					SavePlayerProfile(playerSave);
					SaveWorld(worldSave);
					Globals.gameState = 0;
					Globals.openIngMenu = false;
					Globals.persistentSettings = new SavedSettings();
					SavedSettings.SaveSettings(Globals.persistentSettings);
					confirmExit.draw = false;
				}
				else if (confirmExit.noButton.IsHoveredAndClicked())
				{
					confirmExit.draw = false;
				}
			}
			ResolutionReload();
		}

		public virtual void Draw()
		{
			Vector2 fixedMousePos = new Vector2(-Camera.cameraOffsetX + Globals.mousePosition.X, -Camera.cameraOffsetY + Globals.mousePosition.Y);
			Globals.spriteBatch.Draw(background, new Rectangle(-(int)Camera.cameraOffsetX, -(int)Camera.cameraOffsetY, (int)Globals.windowSize.X, (int)Globals.windowSize.Y), Color.Black);
			ingMenuUi.DrawTexture();
			ingMenuTitle.DrawString();
			resume.DrawString();
			foreach (MenuButton icon in ingMenuIcons)
			{
				icon.DrawTexture();
				if (icon.IsButtonHovered())
				{
					Globals.spriteBatch.DrawString(FontManager.hm_f_menu, icon.text, new Vector2(fixedMousePos.X - icon.hitbox.Width / 2, fixedMousePos.Y - icon.hitbox.Height / 2), Color.White);
				}
			}
			if (ingMenuMode == 0)
			{
			}
			else if (ingMenuMode == 1)
			{
			}
			else if (ingMenuMode == 2)
			{
				resolution.DrawString();
			}

			if (confirmExit.draw)
			{
				confirmExit.DrawBoolSelector();
			}
		}
	}
}
