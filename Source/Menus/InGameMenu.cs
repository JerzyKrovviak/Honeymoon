using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Honeymoon.Source.Menus
{
	public class InGameMenu
	{
		private protected Texture2D ingMenuUi;
		private protected List<MenuButton> ingMenuIcons = new List<MenuButton>();
		private protected int ingMenuMode;

		public InGameMenu()
		{
			ingMenuUi = Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements");
			ingMenuIcons.Add(new MenuButton(ingMenuUi, Vector2.Zero, new Rectangle(68, 64, 9, 9), 4, Color.White)); //general
			ingMenuIcons.Add(new MenuButton(ingMenuUi, Vector2.Zero, new Rectangle(77, 64, 9, 9), 4, Color.White)); //volume
			ingMenuIcons.Add(new MenuButton(ingMenuUi, Vector2.Zero, new Rectangle(86, 64, 9, 9), 4, Color.White)); //video
			ingMenuIcons.Add(new MenuButton(ingMenuUi, Vector2.Zero, new Rectangle(95, 64, 9, 9), 4, Color.White)); //exit
		}

		public virtual void Draw()
		{
			foreach (MenuButton icon in ingMenuIcons)
			{
				if (icon.isHovered)
				{
					icon.color = Color.Red;
				}
			}
		}

		public virtual void Update()
		{
		}
	}
}
