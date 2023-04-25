using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.World.Creatures.Player
{
	public class AnimationManager
	{
		private FrameSet shirtsData;
		private FrameSet framesData;
		private Texture2D texture;
		private Color pantsColor, shirtColor;
		public List<AnimatedComponent> walkDown, walkUp, walkRight;
		public AnimationManager()
		{
			walkDown = new List<AnimatedComponent>();
			walkUp = new List<AnimatedComponent>();
			walkRight = new List<AnimatedComponent>();
			texture = Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_base");
			framesData = new FrameSet(texture);
			framesData.frameWidth = 16;
			framesData.frameHeight = 32;
			framesData.columns = framesData.texture.Width / 16;
			framesData.rows = framesData.texture.Height / 32;
			framesData.totalFrames = framesData.columns * framesData.rows;
			framesData.frameData = new Vector2[framesData.totalFrames];

			for (int y = 0; y < framesData.rows; y++)
			{
				for (int x = 0; x < framesData.columns; x++)
				{
					int index = y * framesData.columns + x;
					var position = new Vector2(x * framesData.frameWidth, y * framesData.frameHeight);
					framesData.frameData[index] = new Vector2(position.X, position.Y);
				}
			}

			shirtsData = new FrameSet(Globals.content.Load<Texture2D>("Creatures/Beekeeper/hm_beekeeper_shirts"));
			shirtsData.frameWidth = 16;
			shirtsData.frameHeight = 32;
			shirtsData.columns = shirtsData.texture.Width / shirtsData.frameWidth;
			shirtsData.rows = shirtsData.texture.Height / shirtsData.frameHeight;
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
			pantsColor = Globals.player.pantsColor;
			shirtColor = Globals.player.shirtColor;
			float speed = 0.1f;

			walkDown.Add(new AnimatedComponent(framesData.texture, "walkDownTorso", 16, 32, new Vector2[] { framesData.frameData[0], framesData.frameData[1], framesData.frameData[0], framesData.frameData[2] }, speed, Color.White));
			walkDown.Add(new AnimatedComponent(framesData.texture, "walkDownPants", 16, 32, new Vector2[] { framesData.frameData[6], framesData.frameData[7], framesData.frameData[6], framesData.frameData[8] }, speed, pantsColor));
			walkDown.Add(new AnimatedComponent(shirtsData.texture, "walkDownShirt", 16, 32, new Vector2[] { shirtsData.frameData[0], shirtsData.frameData[1], shirtsData.frameData[0], shirtsData.frameData[2] }, speed, shirtColor));
			walkDown.Add(new AnimatedComponent(framesData.texture, "walkDownHands", 16, 32, new Vector2[] { framesData.frameData[3], framesData.frameData[4], framesData.frameData[3], framesData.frameData[5] }, speed, Color.White));
			walkDown.Add(new AnimatedComponent(framesData.texture, "walkDownShoulders", 16, 32, new Vector2[] { framesData.frameData[9], framesData.frameData[10], framesData.frameData[9], framesData.frameData[11] }, speed, shirtColor));

			walkRight.Add(new AnimatedComponent(framesData.texture, "walkRightTorso", 16, 32, new Vector2[] { framesData.frameData[20], framesData.frameData[21], framesData.frameData[22], framesData.frameData[20], framesData.frameData[23], framesData.frameData[24] }, speed, Color.White));
			walkRight.Add(new AnimatedComponent(framesData.texture, "walkRightPants", 16, 32, new Vector2[] { framesData.frameData[30], framesData.frameData[31], framesData.frameData[32], framesData.frameData[30], framesData.frameData[33], framesData.frameData[34] }, speed, pantsColor));
			walkRight.Add(new AnimatedComponent(shirtsData.texture, "walkRightShirt", 16, 32, new Vector2[] { shirtsData.frameData[5], shirtsData.frameData[6], shirtsData.frameData[7], shirtsData.frameData[5], shirtsData.frameData[8], shirtsData.frameData[9] }, speed, shirtColor));
			walkRight.Add(new AnimatedComponent(framesData.texture, "walkRightHands", 16, 32, new Vector2[] { framesData.frameData[25], framesData.frameData[26], framesData.frameData[27], framesData.frameData[25], framesData.frameData[28], framesData.frameData[29] }, speed, Color.White));
			walkRight.Add(new AnimatedComponent(framesData.texture, "walkRightShoulders", 16, 32, new Vector2[] { framesData.frameData[35], framesData.frameData[36], framesData.frameData[37], framesData.frameData[35], framesData.frameData[38], framesData.frameData[39] }, speed, shirtColor));

			walkUp.Add(new AnimatedComponent(framesData.texture, "walkUpTorso", 16, 32, new Vector2[] { framesData.frameData[40], framesData.frameData[41], framesData.frameData[40], framesData.frameData[42] }, speed, Color.White));
			walkUp.Add(new AnimatedComponent(framesData.texture, "walkUpPants", 16, 32, new Vector2[] { framesData.frameData[46], framesData.frameData[47], framesData.frameData[46], framesData.frameData[48] }, speed, pantsColor));
			walkUp.Add(new AnimatedComponent(shirtsData.texture, "walkUpShirt", 16, 32, new Vector2[] { shirtsData.frameData[10], shirtsData.frameData[11], shirtsData.frameData[10], shirtsData.frameData[12] }, speed, shirtColor));
			walkUp.Add(new AnimatedComponent(framesData.texture, "walkUpHands", 16, 32, new Vector2[] { framesData.frameData[43], framesData.frameData[44], framesData.frameData[43], framesData.frameData[45] }, speed, Color.White));
			walkUp.Add(new AnimatedComponent(framesData.texture, "walkUpShoulders", 16, 32, new Vector2[] { framesData.frameData[49], framesData.frameData[50], framesData.frameData[49], framesData.frameData[51] }, speed, shirtColor));
		}
		
		public void PlayAnimationSet(List<AnimatedComponent> animList)
		{
			for (int i = 0; i < animList.Count; i++)
			{
				animList[i].PlayAnimation();
			}
		}

		public void StopAnimationSet(List<AnimatedComponent> animList)
		{
			for (int i = 0; i < animList.Count; i++)
			{
				animList[i].StopAnimation();
			}
		}

		public void DrawAnimationSet(List<AnimatedComponent> animList, bool flipHorizontal)
		{
			for (int i = 0; i < animList.Count; i++)
			{
				animList[i].Draw(Globals.player.position);
				animList[i].flipHorizontal = flipHorizontal;
			}
		}

		public void DrawFreezeAnimationSet(List<AnimatedComponent> animList, bool flipHorizontal)
		{
			for (int i = 0; i < animList.Count; i++)
			{
				animList[i].StopAnimation();
				animList[i].Draw(Globals.player.position);
				animList[i].flipHorizontal = flipHorizontal;
			}
		}
	}
}
