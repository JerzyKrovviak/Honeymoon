using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Honeymoon.Managers
{
	public class FontManager
	{
		public static SpriteFont hm_f_menu, hm_f_outline, hm_f_default, hm_f_textInput;

		public static void LoadContent(ContentManager Content)
		{
			hm_f_menu = Content.Load<SpriteFont>("Fonts/hm_f_menu"); //bitmap fontTexture
			hm_f_outline = Content.Load<SpriteFont>("Fonts/hm_f_outline");
			hm_f_default = Content.Load<SpriteFont>("Fonts/hm_f_default");
		}
	}
}
