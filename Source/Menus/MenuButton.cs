using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Managers;
using Honeymoon.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Menus
{
	public class MenuButton
	{
		public SpriteFont font;
		public Texture2D texture;
		public string text;
		public Vector2 position, size;
		public Rectangle inGameData, sourceData, hitbox;
		public Color color;
		public float rotation, scale;
		public Vector2 origin;
		public bool isHovered;
		public bool lastHover;

		public MenuButton(SpriteFont font, string text, Vector2 position, float scale)
		{
			this.font = font;
			this.text = text;
			this.position = position;
			this.scale = scale;
			size = Vector2.Zero;
			hitbox = Rectangle.Empty;
			color = Color.White;
			rotation = 0f;
			origin = Vector2.Zero;
			isHovered = false;
		}

		public MenuButton(Texture2D texture, Rectangle inGameData, Rectangle sourceData)
		{
			this.texture = texture;
			this.inGameData = inGameData;
			this.sourceData = sourceData;
			color = Color.White;
			rotation = 0f;
			origin = Vector2.Zero;
			isHovered = false;
		}

		public MenuButton(SpriteFont hm_f_menu, Texture2D texture, Vector2 position)
		{
			this.texture = texture;
			this.position = position;
			color = Color.White;
			rotation = 0f;
			origin = Vector2.Zero;
			isHovered = false;
		}

		public virtual void Update()
		{
			lastHover = isHovered;
			size = GetButtonSize();
			if (scale == 3)
			{
				hitbox = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
			}
			if (hitbox.Contains(Globals.mousePosition))
			{
				isHovered = true;
				color = Color.Orange;
				if (scale < 4.0f)
				{
					scale += 0.1f;
				}
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					AudioManager.soundBank.PlayCue("optionSelect");
				}
			}
			else
			{
				if (scale > 3.0f)
				{
					scale -= 0.1f;
				}
				isHovered = false;
				color = Color.White;
			}
			if (!lastHover && isHovered)
			{
				AudioManager.soundBank.PlayCue("optionHover");
			}
		}

		public virtual void DrawTexture()
		{
			Globals.spriteBatch.Draw(texture, inGameData, sourceData, color, rotation, origin, SpriteEffects.None, 0);
		}

		public virtual void DrawString()
		{
			Globals.spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0);
		}

		public Vector2 GetButtonSize()
		{
			return new Vector2(font.MeasureString(text).X * scale, font.MeasureString(text).Y * scale);
		}
	}
}
