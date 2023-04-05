using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		private protected MenuButton ingMenuUi;
		private protected Texture2D background;
		private protected List<MenuButton> ingMenuIcons = new List<MenuButton>();
		private protected int ingMenuMode = 0;

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
			ingMenuIcons.Add(new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Vector2.Zero, new Rectangle(68, 64, 9, 9), 5, Color.White)); //general
			ingMenuIcons.Add(new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Vector2.Zero, new Rectangle(77, 64, 9, 9), 5, Color.White)); //volume
			ingMenuIcons.Add(new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Vector2.Zero, new Rectangle(86, 64, 9, 9), 5, Color.White)); //video
			ingMenuIcons.Add(new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Vector2.Zero, new Rectangle(95, 64, 9, 9), 5, Color.White)); //exit
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			ingMenuUi.position = new Vector2(-(int)Camera.cameraOffsetX + ingMenuUi.PerfectMidPositionTexture().X, -(int)Camera.cameraOffsetY + ingMenuUi.PerfectMidPositionTexture().Y);
			for (int i = 0; i < ingMenuIcons.Count; i++)
			{
				ingMenuIcons[i].position = new Vector2(ingMenuUi.position.X + 54, ingMenuUi.position.Y + 54 + (ingMenuIcons[i].inGameData.Width + 8) * i);
			}
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
		public virtual void Update()
		{
			ResolutionReload();
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

			if (ingMenuIcons[0].IsHoveredAndClicked())		
			{
				ingMenuMode = 0;
			}
			else if (ingMenuIcons[1].IsHoveredAndClicked())
			{
				ingMenuMode = 1;
			}
			else if (ingMenuIcons[2].IsHoveredAndClicked())
			{
				ingMenuMode = 2;
			}
			else if (ingMenuIcons[3].IsHoveredAndClicked())
			{
				PlayerSave playerSave = new PlayerSave
				{
					Name = Globals.player.nickname,
					mapId = Map.GetCurrentMapId(),
					Position = Globals.player.position,
					shirtColor = Globals.player.shirtColor,
					pantsColor = Globals.player.pantsColor,
					velocity = Globals.player.velocity
				};
				SavePlayerProfile(playerSave);
				Globals.gameState = 0;
				Globals.ingMenu = false;
			}
		}

		public virtual void Draw()
		{
			Globals.spriteBatch.Draw(background, new Rectangle(-(int)Camera.cameraOffsetX, -(int)Camera.cameraOffsetY, (int)Globals.windowSize.X, (int)Globals.windowSize.Y), Color.Black);
			ingMenuUi.DrawTexture();
			foreach (MenuButton icon in ingMenuIcons)
			{
				icon.DrawTexture();
			}
		}
	}
}
