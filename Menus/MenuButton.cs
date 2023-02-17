using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Menus
{
	public class MenuButton
	{
		public SpriteFont font;
		public string text;
		public Vector2 position, size;
		public Rectangle hitbox;
		public Color color;
		public float rotation, scale;
		public Vector2 origin;
		public bool isHovered;

		public MenuButton(SpriteFont font)
		{
			this.font = font;
			text = string.Empty;
			position = Vector2.Zero;
			size = Vector2.Zero;
			hitbox = Rectangle.Empty;
			color = Color.White;
			rotation = 0f;
			origin = Vector2.Zero;
			scale = 1;
			isHovered = false;
		}

		public Vector2 GetButtonSize()
		{
			return font.MeasureString(text);
		}
	}
}
