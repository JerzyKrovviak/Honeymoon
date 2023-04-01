using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.SavedData;
using Honeymoon.Source.World.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Honeymoon.Source.Menus
{
	public class PlayerProfileCell
	{
		public Texture2D texture;
		public PlayerSave linkedSave;
		public string name;
		public Vector2 position;
		public Rectangle sourceData, inGameData;
		private List<MenuButton> playerDoll;
		public MenuButton deleteProfile;
		public int width, height;
		private Color color;

		public PlayerProfileCell(PlayerSave linkedsave)
		{
			this.texture = Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements");
			this.linkedSave = linkedsave;
			this.sourceData = new Rectangle(96, 0, 96, 37);
			width = sourceData.Width * 5;
			height = sourceData.Height * 5;
			name = linkedSave.Name;
			deleteProfile = new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements"), Rectangle.Empty, new Rectangle(59,64,9,9), Color.White);
			playerDoll = new List<MenuButton>();
			playerDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Rectangle.Empty, new Rectangle(0, 0, 16, 32))); //torso
			playerDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Rectangle.Empty, new Rectangle(96, 0, 16, 32), linkedSave.pantsColor)); //pants
			playerDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_shirts"), Rectangle.Empty, new Rectangle(0, 0, 16, 32), linkedSave.shirtColor)); //shirt
			playerDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Rectangle.Empty, new Rectangle(48, 0, 16, 32))); //hands
			playerDoll.Add(new MenuButton(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base"), Rectangle.Empty, new Rectangle(144, 0, 16, 32), linkedSave.shirtColor)); //shoulders
		}

		public virtual void Update()
		{
			foreach (MenuButton bodypart in playerDoll)
			{
				bodypart.inGameData = new Rectangle((int)position.X + 45, (int)position.Y + 15, 16 * 4, 32 * 4);
			}
			inGameData = new Rectangle((int)position.X, (int)position.Y, width, height);
			if (inGameData.Contains(Globals.mousePosition))
			{
				color = Color.Orange;
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					Globals.player = new Beekeeper(linkedSave);
					Globals.gameState = 1;
				}
			}
			else
			{
				color = Color.White;
			}
			deleteProfile.inGameData = new Rectangle((int)position.X + 485, (int)position.Y + 140, deleteProfile.sourceData.Width * 4, deleteProfile.sourceData.Height * 4);
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
			Globals.spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), sourceData, color);
			foreach (MenuButton bodypart in playerDoll)
			{
				bodypart.DrawTexture();
			}
			Globals.spriteBatch.DrawString(FontManager.hm_f_default, name, new Vector2(position.X + 135, position.Y + 25), Color.Brown, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
			deleteProfile.DrawTexture();
		}
	}
}
