using Honeymoon.Menus;
using Honeymoon.Source.SavedData;
using Honeymoon.Source.World.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.World.HudElements
{
	public class InventoryItemSlot
	{
		private Texture2D background, invSlotTexture;
		public Vector2 position;
		private Item containingItem;
		public const int size = 64;
		private Color slotColor = Color.White;
		public InventoryItemSlot()
		{
			containingItem = null;
			background = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			Color[] data = new Color[1 * 1];
			for (int pixel = 0; pixel < data.Count(); pixel++)
			{
				data[pixel] = slotColor;
			}
			invSlotTexture = Globals.content.Load<Texture2D>("HudElements/hudElements");
			background.SetData(data);
		}

		public bool IsHovered()
		{
			Rectangle hitbox = new Rectangle((int)position.X + (int)Camera.cameraOffsetX, (int)position.Y + (int)Camera.cameraOffsetY, size, size);
			if (hitbox.Contains(Globals.mousePosition))
			{
				return true;
			}
			return false;
		}

		public void Update()
		{
			Rectangle hitbox = new Rectangle((int)position.X + (int)Camera.cameraOffsetX, (int)position.Y + (int)Camera.cameraOffsetY, size, size);
			if (hitbox.Contains(Globals.mousePosition))
			{
				slotColor = new Color(Color.White, 120);
			}
			else
			{
				slotColor = new Color(Color.White, 0);
			}
		}

		public virtual void Draw()
		{
			Globals.spriteBatch.Draw(invSlotTexture, new Rectangle((int)position.X, (int)position.Y, 18 * 4, 18 * 4), new Rectangle(85,39,18,18), Color.White);
			Globals.spriteBatch.Draw(background, new Rectangle((int)position.X + 4, (int)position.Y + 4, size, size), slotColor);
		}
	}

	public class MainInventory
	{
		//private Item[] items;
		private MenuButton invTexture;
		private protected Texture2D background;
		private const int columns = 4, rows = 4, totalAmount = columns * rows;
		private List<InventoryItemSlot> inventoryItemSlots = new List<InventoryItemSlot>();
		public MainInventory()
		{
			invTexture = new MenuButton(Globals.content.Load<Texture2D>("HudElements/hudElements"), Vector2.Zero, new Rectangle(0, 0, 85, 85), 4, Color.White);
			background = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			Color[] data = new Color[1 * 1];
			for (int pixel = 0; pixel < data.Count(); pixel++)
			{
				data[pixel] = new Color(255, 255, 255, 120);
			}
			background.SetData(data);
			for (int i = 0; i < totalAmount; i++)
			{
				inventoryItemSlots.Add(new InventoryItemSlot());
			}
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			invTexture.position = new Vector2(-(int)Camera.cameraOffsetX + invTexture.PerfectMidPositionTexture().X, -(int)Camera.cameraOffsetY + invTexture.PerfectMidPositionTexture().Y);
			foreach (var invSlot in inventoryItemSlots) invSlot.Update();
			for (int y = 0; y < columns; y++)
			{
				for (int x = 0; x < rows; x++)
				{
					int index = y * columns + x;
					var position = new Vector2(x * 68, y * 68);
					inventoryItemSlots[index].position = new Vector2(invTexture.position.X + position.X + 32, invTexture.position.Y + position.Y + 32);
				}
			}
		}
		public virtual void Update()
		{
			ResolutionReload();
		}
		public virtual void Draw()
		{
			Globals.spriteBatch.Draw(background, new Rectangle(-(int)Camera.cameraOffsetX, -(int)Camera.cameraOffsetY, (int)Globals.windowSize.X, (int)Globals.windowSize.Y), Color.Black);
			invTexture.DrawTexture();
			foreach (var invSlot in inventoryItemSlots) invSlot.Draw();
		}
	}
}
