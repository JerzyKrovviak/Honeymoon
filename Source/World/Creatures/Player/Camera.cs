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
		public static float cameraOffsetX, cameraOffsetY;

		public static void CalculateTranslation()
		{
			cameraOffsetX = (Globals.windowSize.X / 2) - Globals.player.position.X;
			if (Map.Map.GetCurrentMapScaledSizePixels().X >= Globals.windowSize.X)
			{
				cameraOffsetX = MathHelper.Clamp(cameraOffsetX, -Map.Map.GetCurrentMapScaledSizePixels().X + Globals.windowSize.X + (Map.Map.scaledTileWidth / 2), 0) + (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
			}
			cameraOffsetY = (Globals.windowSize.Y / 2) - Globals.player.position.Y;
			if (Map.Map.GetCurrentMapScaledSizePixels().Y >= Globals.windowSize.Y)
			{
				cameraOffsetY = MathHelper.Clamp(cameraOffsetY, -Map.Map.GetCurrentMapScaledSizePixels().Y + Globals.windowSize.Y + (Map.Map.scaledTileHeight / 2), 0) + (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
			}
			_translation = Matrix.CreateTranslation(cameraOffsetX, cameraOffsetY, 0f);
		}
	}
}
