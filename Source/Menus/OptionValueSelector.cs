using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Honeymoon.Managers;

namespace Honeymoon.Source.Menus
{
	public class OptionValueSelector
	{
		public Texture2D valueIncrease;
		public Vector2 position;
		public Rectangle sourceData, inGameData;
		public int value, maxValue, distance, scale;
		public bool isHovered;
		public bool lastHover;
		public Color color1, color2;

		public OptionValueSelector(Vector2 position, int value, int maxValue, int scale, int distance)
		{
			valueIncrease = Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements");
			sourceData = new Rectangle(54, 64, 5, 7);
			inGameData = new Rectangle((int)position.X, (int)position.Y, sourceData.Width * scale, sourceData.Height * scale);
			this.value = value;
			this.maxValue = maxValue;
			this.scale = scale;
			this.distance = distance;
		}

		public virtual void UpdateSelector()
		{
			inGameData.X = (int)position.X;
			inGameData.Y = (int)position.Y;
			Rectangle decreaseHitBox = new Rectangle((int)inGameData.X, (int)inGameData.Y, sourceData.Width * scale, sourceData.Height * scale);
			Rectangle increaseHitBox = new Rectangle((int)inGameData.X + distance, (int)inGameData.Y, sourceData.Width * scale, sourceData.Height * scale);

			lastHover = isHovered;
			if (decreaseHitBox.Contains(Globals.mousePosition))
			{
				isHovered = true;
				color1 = Color.Orange;
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					if (value - 1 < 0)
					{
						value = maxValue;
					}
					else
					{
						value--;
					}
					AudioManager.soundBank.PlayCue("selectorAdd");
				}
			}
			else
			{
				isHovered = false;
				color1 = Color.White;
			}
			if (increaseHitBox.Contains(Globals.mousePosition))
			{
				isHovered = true;
				color2 = Color.Orange;
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					if (value + 1 > maxValue)
					{
						value = 0;
					}
					else
					{
						value++;
					}
					AudioManager.soundBank.PlayCue("selectorAdd");
				}
			}
			else
			{
				isHovered = false;
				color2 = Color.White;
			}

			if (!lastHover && isHovered)
			{
				AudioManager.soundBank.PlayCue("optionHover");
			}
		}

		public virtual void DrawSelector()
		{
			Globals.spriteBatch.Draw(valueIncrease, inGameData, sourceData, color1, 0f, Vector2.Zero, SpriteEffects.None, 0);
			Globals.spriteBatch.Draw(valueIncrease, new Rectangle(inGameData.X + distance, inGameData.Y, inGameData.Width, inGameData.Height), sourceData, color2, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
		}
	}
}
