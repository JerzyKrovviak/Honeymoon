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
			position = new Vector2(333, 111);
			sourceData = new Rectangle(0, 0, 16, 32);
			color = Color.White;
			rotation = 0;
			origin = Vector2.Zero;
			framesCount = 3;
			currentFrame = 0;
			frameSpeed = 60f;
		}

		public virtual void Update()
		{
			PlayAnimation();
			position.X += 0.5f;
			System.Diagnostics.Debug.WriteLine("updating skurwysyna");
		}
	}
}
