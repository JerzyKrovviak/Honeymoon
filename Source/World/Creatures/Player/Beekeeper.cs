using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Honeymoon.Source.World.Creatures.Player
{
	public class Beekeeper : PhysicalComponent
	{
		public Beekeeper()
		{
			texture = Globals.content.Load<Texture2D>("Creatures/Beekeeper/beekeeper");
			position = new Vector2(800, 800);
			sourceData = new Rectangle(0, 0, 16, 32);
			color = Color.White;
			rotation = 0;
			origin = Vector2.Zero;
			velocity = new Vector2(130,130);
			animation.Add(new Animation2d("walkHorizontal", 3, 0, 90f));
			animation.Add(new Animation2d("walkVerticalDown", 4, 32, 70f));
			animation.Add(new Animation2d("walkVerticalUp", 4, 64, 70f));
		}

		public virtual void Update()
		{
			Globals.input.Update();
			Camera.CalculateTranslation();
		}
	}
}
