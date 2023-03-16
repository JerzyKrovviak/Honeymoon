using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Honeymoon.Source.World.Creatures.Player
{
	public class Beekeeper : PhysicalComponent
	{
		public Beekeeper()
		{
			texture = Globals.content.Load<Texture2D>("Creatures/Beekeeper/beekeeper");
			position = Map.Map.TileIdPosToXY(new Vector2(25,25));
			sourceData = new Rectangle(0, 0, 16, 32);
			color = Color.White;
			rotation = 0;
			origin = Vector2.Zero;
			velocity = new Vector2(130,130);
			hitBox = new Rectangle((int)position.X, (int)position.Y + sourceData.Height / 2, 64,64);
			animation.Add(new AnimatedComponent("walkHorizontal", 3, 0, 90f));
			animation.Add(new AnimatedComponent("walkVerticalDown", 4, 32, 70f));
			animation.Add(new AnimatedComponent("walkVerticalUp", 4, 64, 70f));
		}

		public static Vector2 GetPlayerTile()
		{
			return new Vector2((Globals.player.hitBox.X + Globals.player.hitBox.Width / 2) / Map.Map.scaledTileWidth, (Globals.player.hitBox.Y + Globals.player.hitBox.Height / 2) / Map.Map.scaledTileHeight);
		}

		public virtual void Update()
		{
			hitBox = new Rectangle((int)position.X, (int)position.Y + 64, 64, 64);
			Globals.input.Update();
			Camera.CalculateTranslation();
		}
	}
}
