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
		public FrameSet shirtsData;

		public Beekeeper()
		{
			texture = Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base");
			position = Map.Map.TileIdPosToXY(new Vector2(25,25));
			//sourceData = new Rectangle(0, 0, 16, 32);
			color = Color.White;
			rotation = 0;
			origin = Vector2.Zero;
			velocity = new Vector2(130,130);
			hitBox = new Rectangle((int)position.X, (int)position.Y + sourceData.Height / 2, 64,64);

			framesData = new FrameSet(texture);
			framesData.frameWidth = 16;
			framesData.frameHeight = 32;
			framesData.columns = texture.Width / 16;
			framesData.rows = texture.Height / 32;
			framesData.totalFrames = framesData.columns * framesData.rows;
			framesData.frameData = new Vector2[framesData.totalFrames];

			for (int y = 0; y < framesData.rows; y++)
			{
				for (int x = 0; x < framesData.columns; x++)
				{
					int index = y * framesData.columns + x;
					var position = new Vector2(x * framesData.frameWidth, y * framesData.frameHeight);
					framesData.frameData[index] = new Vector2(position.X, position.Y);
					Debug.WriteLine(index + " poson texture: " + position);
				}
			}

			shirtsData = new FrameSet(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_shirts"));
			shirtsData.frameWidth = 16;
			shirtsData.frameHeight = 32;
			shirtsData.columns = texture.Width / 16;
			shirtsData.rows = texture.Height / 32;
			shirtsData.totalFrames = shirtsData.columns * shirtsData.rows;
			shirtsData.frameData = new Vector2[shirtsData.totalFrames];

			for (int y = 0; y < shirtsData.rows; y++)
			{
				for (int x = 0; x < shirtsData.columns; x++)
				{
					int index = y * shirtsData.columns + x;
					var position = new Vector2(x * shirtsData.frameWidth, y * shirtsData.frameHeight);
					shirtsData.frameData[index] = new Vector2(position.X, position.Y);
				}
			}

			animation.Add(new AnimatedComponent(framesData.texture, "walkDownTorso", 16, 32, new Vector2[] { framesData.frameData[0], framesData.frameData[1], framesData.frameData[0], framesData.frameData[2] }, 350, Color.White));
			animation.Add(new AnimatedComponent(framesData.texture, "walkDownPants", 16, 32, new Vector2[] { framesData.frameData[6], framesData.frameData[7], framesData.frameData[6], framesData.frameData[8] }, 350, Color.LightBlue));
			animation.Add(new AnimatedComponent(shirtsData.texture, "walkDownShirt", 16, 32, new Vector2[] { shirtsData.frameData[0], shirtsData.frameData[1], shirtsData.frameData[0], shirtsData.frameData[2] }, 350, Color.LightPink));
			animation.Add(new AnimatedComponent(framesData.texture, "walkDownHands", 16, 32, new Vector2[] { framesData.frameData[3], framesData.frameData[4], framesData.frameData[3], framesData.frameData[5] }, 350, Color.White));
			animation.Add(new AnimatedComponent(framesData.texture, "walkDownShoulders", 16, 32, new Vector2[] { framesData.frameData[9], framesData.frameData[10], framesData.frameData[9], framesData.frameData[11] }, 350, Color.LightPink));

			animation.Add(new AnimatedComponent(framesData.texture, "walkRightTorso", 16, 32, new Vector2[] { framesData.frameData[20], framesData.frameData[21], framesData.frameData[22], framesData.frameData[20], framesData.frameData[23], framesData.frameData[24] }, 350, Color.White));
			animation.Add(new AnimatedComponent(framesData.texture, "walkRightPants", 16, 32, new Vector2[] { framesData.frameData[30], framesData.frameData[31], framesData.frameData[32], framesData.frameData[30], framesData.frameData[33], framesData.frameData[34] }, 350, Color.LightBlue));
			animation.Add(new AnimatedComponent(shirtsData.texture, "walkRightShirt", 16, 32, new Vector2[] { shirtsData.frameData[5], shirtsData.frameData[6], shirtsData.frameData[7], shirtsData.frameData[5], shirtsData.frameData[8], shirtsData.frameData[9] }, 350, Color.LightPink));
			animation.Add(new AnimatedComponent(framesData.texture, "walkRightHands", 16, 32, new Vector2[] { framesData.frameData[25], framesData.frameData[26], framesData.frameData[27], framesData.frameData[25], framesData.frameData[28], framesData.frameData[29] }, 350, Color.White));
			animation.Add(new AnimatedComponent(framesData.texture, "walkRightShoulders", 16, 32, new Vector2[] { framesData.frameData[35], framesData.frameData[36], framesData.frameData[37], framesData.frameData[35], framesData.frameData[38], framesData.frameData[39] }, 350, Color.LightPink));
		}

		public static Vector2 GetPlayerTile()
		{
			return new Vector2((Globals.player.hitBox.X + Globals.player.hitBox.Width / 2) / Map.Map.scaledTileWidth, (Globals.player.hitBox.Y + Globals.player.hitBox.Height / 2) / Map.Map.scaledTileHeight);
		}

		public virtual void Update()
		{
			hitBox = new Rectangle((int)position.X, (int)position.Y + 64, 64, 64);
			Globals.input.Update();
			Camera.CalculateTranslation();
			//System.Diagnostics.Debug.WriteLine(animation[3].name + " " + animation[3].sourceData.X);
		}
	}
}
