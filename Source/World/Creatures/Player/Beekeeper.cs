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
using Honeymoon.Source.SavedData;

namespace Honeymoon.Source.World.Creatures.Player
{
	public class Beekeeper : PhysicalComponent
	{
		public FrameSet shirtsData;
		public FrameSet framesData;
		public Color shirtColor, pantsColor;
		public string nickname;
		public int mapId;
		public List<AnimatedComponent> animations = new List<AnimatedComponent>();

		public Beekeeper(PlayerSave playersave)
		{
			texture = Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base");
			position = playersave.Position;
			color = Color.White;
			rotation = 0;
			origin = Vector2.Zero;
			speed = playersave.walkSpeed;
			velocity = new Vector2(speed, speed);
			hitBox = new Rectangle((int)position.X, (int)position.Y + 64, 64,64);
			direction = "down";
			nickname = playersave.Name;
			shirtColor = playersave.shirtColor;
			pantsColor = playersave.pantsColor;
			mapId = playersave.mapId;
		}

		public virtual void Update()
		{
			hitBox = new Rectangle((int)position.X, (int)position.Y + 64, 64, 64);
			Globals.input.Update((float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds);
			Camera.CalculateTranslation();
			CheckForWindowBounds();
		}
	}
}
