using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Honeymoon.Managers;
using Honeymoon.Menus;
using System.Linq;

namespace Honeymoon.Source.Menus
{
	public class OptionBoolSelector
	{
		public Texture2D background;
		public MenuButton yesButton, noButton;
		public bool draw;

		public OptionBoolSelector()
		{
			yesButton = new MenuButton(FontManager.hm_f_menu, "Yes", Vector2.Zero, 3, Color.Green);
			noButton = new MenuButton(FontManager.hm_f_menu, "No", Vector2.Zero, 3, Color.Red);
			background = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			Color[] data = new Color[1 * 1];
			for (int pixel = 0; pixel < data.Count(); pixel++)
			{
				data[pixel] = new Color(255,255,255,120);
			}
			background.SetData(data);
		}

		public virtual void Update()
		{
			yesButton.position = new Vector2(GlobalFunctions.PerfectMidPosX(yesButton.GetTextBtnSize().X + 200), 500);
			noButton.position = new Vector2(GlobalFunctions.PerfectMidPosX(noButton.GetTextBtnSize().X - 200), 500);
			yesButton.Update();
			noButton.Update();
			yesButton.color = Color.Green;
			noButton.color = Color.Red;
		}

		public virtual void DrawBoolSelector()
		{
			Globals.spriteBatch.Draw(background, new Rectangle(0,0, (int)Globals.windowSize.X, (int)Globals.windowSize.Y), Color.Black);
			var size = FontManager.hm_f_outline.MeasureString("Are you sure?");
			Globals.spriteBatch.DrawString(FontManager.hm_f_menu, "Are you sure?", new Vector2(GlobalFunctions.PerfectMidPosX(size.X * 3), 300), Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0);
			yesButton.DrawString();
			noButton.DrawString();
		}
	}
}
