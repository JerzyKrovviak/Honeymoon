using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Honeymoon.Source.World.Map;

namespace Honeymoon.Source.World.Creatures.Player
{
	public class Camera
	{
		public static Matrix _translation;

		public static void CalculateTranslation()
		{
			var dx = (Globals.windowSize.X / 2) - Globals.player.position.X;
			if (Map.Map.GetCurrentMapScaledSizePixels().X >= Globals.windowSize.X)
			{
				dx = MathHelper.Clamp(dx, -Map.Map.GetCurrentMapScaledSizePixels().X + Globals.windowSize.X + (Map.Map.scaledTileWidth / 2), 0) + (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
			}
			var dy = (Globals.windowSize.Y / 2) - Globals.player.position.Y - 28;
			if (Map.Map.GetCurrentMapScaledSizePixels().Y >= Globals.windowSize.Y)
			{
				dy = MathHelper.Clamp(dy, -Map.Map.GetCurrentMapScaledSizePixels().Y + Globals.windowSize.Y + (Map.Map.scaledTileHeight / 2), 0) + (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
			}
			_translation = Matrix.CreateTranslation(dx, dy, 0f);
		}
	}
}
