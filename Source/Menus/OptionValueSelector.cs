using Honeymoon.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Honeymoon.Managers;

namespace Honeymoon.Source.Menus
{
	public class OptionValueSelector
	{
		public Texture2D valueIncrease, valueDecrease;
		public Rectangle sourceData, inGameData;
		public int value, maxValue;
		public bool isHovered;
		public bool lastHover;
		public Color color1, color2;

		public OptionValueSelector(int value, int maxValue)
		{
			valueIncrease = Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements");
			valueDecrease = Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements");
			sourceData = new Rectangle(54, 64, 5, 7);
			inGameData.Width = sourceData.Width * 4;
			inGameData.Height = sourceData.Height * 4;
			this.value = value;
			this.maxValue = maxValue;
		}

		public virtual void UpdateSelector()
		{
			Rectangle decreaseHitBox = new Rectangle((int)inGameData.X, (int)inGameData.Y, sourceData.Width * 4, sourceData.Height * 4);
			Rectangle increaseHitBox = new Rectangle((int)inGameData.X + 30, (int)inGameData.Y, sourceData.Width * 4, sourceData.Height * 4);

			lastHover = isHovered;
			if (decreaseHitBox.Contains(Globals.mousePosition))
			{
				isHovered = true;
				color1 = Color.Orange;
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					if (value > 0)
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
					if (value < maxValue - 1)
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
			Globals.spriteBatch.Draw(valueDecrease, inGameData, sourceData, color1, 0f, Vector2.Zero, SpriteEffects.None, 0);
			Globals.spriteBatch.Draw(valueIncrease, new Rectangle(inGameData.X + 30, inGameData.Y, inGameData.Width, inGameData.Height), sourceData, color2, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
		}
	}
}
