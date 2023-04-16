using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.SavedData;
using Honeymoon.Source.World.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.Menus
{
	public class WorldSelectionCell
	{
		public WorldSave linkedSave;
		private Texture2D texture;
		public string worldName;
		public Vector2 position;
		public Rectangle sourceData, inGameData;
		private Color color;
		public MenuButton deleteProfile;
		public WorldSelectionCell(WorldSave linkedsave)
		{
			this.linkedSave = linkedsave;
			this.texture = Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements");
			this.sourceData = new Rectangle(0, 76, 96, 37);
			this.worldName = linkedSave.name;
			deleteProfile = new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Vector2.Zero, new Rectangle(59, 64, 9, 9), 4, Color.White);
		}

		public virtual void Update()
		{
			inGameData = new Rectangle((int)position.X, (int)position.Y, sourceData.Width * 5, sourceData.Height * 5);
			if (inGameData.Contains(Globals.mousePosition))
			{
				color = Color.Orange;
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.objectLayer = new ObjectLayer(linkedSave);
					Globals.gameState = 1;
				}
			}
			else
			{
				color = Color.White;
			}

			deleteProfile.position = new Vector2((int)position.X + 485, (int)position.Y + 140);
			if (deleteProfile.inGameData.Contains(Globals.mousePosition))
			{
				deleteProfile.color = Color.Red;
			}
			else
			{
				deleteProfile.color = Color.White;
			}
		}

		public virtual void Draw()
		{
			Globals.spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, sourceData.Width * 5, sourceData.Height * 5), sourceData, color);
			Globals.spriteBatch.DrawString(FontManager.hm_f_default, worldName, new Vector2(position.X + 135, position.Y + 25), Color.Brown, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
			deleteProfile.DrawTexture();
		}
	}
}
