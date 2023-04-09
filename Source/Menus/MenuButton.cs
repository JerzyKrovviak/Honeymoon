using Honeymoon.Managers;
using Honeymoon.Source;
using Honeymoon.Source.World.Creatures.Player;
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
		public float rotation, scale, constScale;
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
			constScale = scale;
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

		public MenuButton(Texture2D texture, string nameId, Vector2 position, Rectangle sourceData, int scale, Color color)
		{
			this.texture = texture;
			this.text = nameId;
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
		public bool IsButtonHovered()
		{
			if (hitbox.Contains(Globals.mousePosition))
			{
				return true;
			}
			return false;
		}
		public virtual void UpdateTextureButton()
		{
			lastHover = isHovered;
			if (IsButtonHovered())
			{
				isHovered = true;
			}
			else
			{
				isHovered = false;
			}
			if (!lastHover && isHovered)
			{
				AudioManager.soundBank.PlayCue("optionHover");
			}

			if (IsHoveredAndClicked())
			{
				AudioManager.soundBank.PlayCue("optionSelect");
			}
		}
		public virtual void Update()
		{
			inGameData.X = (int)position.X;
			inGameData.Y = (int)position.Y;
			lastHover = isHovered;
			if (scale == constScale)
			{
				if (Globals.gameState == 1)
				{
					hitbox = new Rectangle((int)position.X + (int)Camera.cameraOffsetX, (int)position.Y + (int)Camera.cameraOffsetY, (int)GetTextBtnSize().X, (int)GetTextBtnSize().Y);
				}
				else
				{
					hitbox = new Rectangle((int)position.X, (int)position.Y, (int)GetTextBtnSize().X, (int)GetTextBtnSize().Y);
				}
			}
			if (hitbox.Contains(Globals.mousePosition))
			{
				isHovered = true;
				if (font == FontManager.hm_f_menu)
				{
					color = Color.Orange;
				}
				if (scale < constScale + 1)
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
				if (scale > constScale)
				{
					scale -= 0.1f;
				}
				isHovered = false;
				if (font == FontManager.hm_f_menu)
				{
					color = Color.White;
				}
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
