using Honeymoon.Managers;
using Honeymoon.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.Menus
{
	public class TextInput
	{
		public SpriteFont font;
		public string text;
		public Vector2 position, size;
		public Rectangle hitbox;
		public bool isFocused;
		public float scale;
		public Color color;
		public int maxChars;
		public Texture2D textUnderline;

		public TextInput(SpriteFont font, string text, Vector2 position, float scale, Color color, int maxChars)
		{
			this.font = font;
			this.text = text;
			this.position = position;
			this.scale = scale;
			this.color = color;
			this.maxChars = maxChars;
			textUnderline = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			textUnderline.SetData(new Color[] { Color.White });
		}

		public Vector2 GetButtonSize()
		{
			return new Vector2(font.MeasureString(text).X * scale, font.MeasureString(text).Y * scale);
		}

		public string GetLastKeysToString()
		{
			var keys = Keyboard.GetState().GetPressedKeys();
			string lastKey = "";
			if (keys.Count() > 0)
			{
				if (InputManager.IsKeyNewlyPressed(keys[keys.Length - 1]))
				{
					if (keys[keys.Length - 1] == Keys.Space)
					{
						lastKey += " ";
					}
					else if (keys.Intersect(InputManager.normalChars).Any())
					{
						lastKey += keys[keys.Length - 1].ToString().ToLower();
						AudioManager.soundBank.PlayCue("optionHover");
					}
				}
			}
			return lastKey;
		}

		public virtual void UpdateTextInput()
		{
			size = GetButtonSize();
			hitbox = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
			if (hitbox.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					AudioManager.soundBank.PlayCue("optionHover");
					isFocused = true;
					color = Color.Green;
				}
			}
			else
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					isFocused = false;
					color = Color.Brown;
				}
			}

			if (isFocused)
			{
				if (InputManager.IsKeyNewlyPressed(Keys.Back))
				{
					if (text.Length > 0)
					{
						text = text.Remove(text.Length - 1);
						AudioManager.soundBank.PlayCue("optionHover");
					}
				}
				else if (text.Length <= maxChars)
				{
					text += GetLastKeysToString();
				}
			}
		}

		public virtual void DrawTextInput()
		{
			Globals.spriteBatch.Draw(textUnderline, new Rectangle((int)position.X + 3, (int)position.Y + (int)size.Y, (int)size.X, 2), Color.Brown);
			Globals.spriteBatch.DrawString(font, text, position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
		}
	}
}
