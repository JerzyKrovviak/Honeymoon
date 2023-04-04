using Honeymoon.Managers;
using Honeymoon.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Honeymoon.Menus
{
	public class MenuButton
	{
		private SpriteFont font;
		private Texture2D texture;
		public string text;
		public Vector2 position, size;
		public Rectangle inGameData, sourceData, hitbox;
		public Color color;
		public float rotation, scale;
		private Vector2 origin;
		public bool isHovered;
		public bool lastHover;

		public MenuButton(SpriteFont font, string text, Vector2 position, float scale, Color color)
		{
			this.font = font;
			this.text = text;
			this.position = position;
			this.scale = scale;
			size = Vector2.Zero;
			hitbox = Rectangle.Empty;
			this.color = color;
			rotation = 0f;
			origin = Vector2.Zero;
			isHovered = false;
		}
		public MenuButton(Texture2D texture, Vector2 position, Rectangle sourceData, int scale, Color color)
		{
			this.texture = texture;
			this.position = position;
			this.sourceData = sourceData;
			this.inGameData = new Rectangle((int)position.X, (int)position.Y, sourceData.Width * scale, sourceData.Height * scale);
			this.color = color;
			rotation = 0f;
			origin = Vector2.Zero;
			isHovered = false;
		}

		public Vector2 GetTextBtnSize()
		{
			return new Vector2(font.MeasureString(text).X * scale, font.MeasureString(text).Y * scale);
		}
		public Vector2 PerfectMidPositionText()
		{
			return new Vector2(Globals.windowSize.X / 2 - GetTextBtnSize().X / 2, Globals.windowSize.Y / 2 - GetTextBtnSize().Y / 2);
		}
		public Vector2 PerfectMidPositionTexture()
		{
			return new Vector2(Globals.windowSize.X / 2 - inGameData.Width / 2, Globals.windowSize.Y / 2 - inGameData.Height / 2);
		}
		public bool IsHoveredAndClicked()
		{
			if (hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					return true;
				}
			}
			return false;
		}
		public bool IsHovered()
		{
			if (hitbox.Contains(Globals.mousePosition))
			{
				return true;
			}
			return false;
		}
		public virtual void Update()
		{
			inGameData.X = (int)position.X;
			inGameData.Y = (int)position.Y;
			lastHover = isHovered;
			size = GetTextBtnSize();
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
			inGameData.X = (int)position.X;
			inGameData.Y = (int)position.Y;
			Globals.spriteBatch.Draw(texture, inGameData, sourceData, color, rotation, origin, SpriteEffects.None, 0);
		}
		public virtual void DrawString()
		{
			Globals.spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0);
		}
	}
}
