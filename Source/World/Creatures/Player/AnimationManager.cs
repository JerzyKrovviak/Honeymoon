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
		private FrameSet framesData;
		private Texture2D texture;
		private Color pantsColor, shirtColor, skinColor, bootsColor, eyesColor;
		public List<AnimatedComponent> walkDown, walkUp, walkRight, walkDownRight, walkUpRight;
		public AnimationManager()
		{
			walkDown = new List<AnimatedComponent>();
			walkDownRight = new List<AnimatedComponent>();
			walkRight = new List<AnimatedComponent>();
			walkUpRight = new List<AnimatedComponent>();
			walkUp = new List<AnimatedComponent>();
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
			pantsColor = Globals.player.pantsColor;
			shirtColor = Globals.player.shirtColor;
			skinColor = Globals.player.skinColor;
			bootsColor = Globals.player.bootsColor;
			eyesColor = Globals.player.eyesColor;
			float speed = 0.1f;

			walkDown.Add(new AnimatedComponent(framesData.texture, "walkDownPants", 16, 32, new Vector2[] { framesData.frameData[0], framesData.frameData[1], framesData.frameData[2], framesData.frameData[0], framesData.frameData[3], framesData.frameData[4] }, speed, pantsColor));
			walkDown.Add(new AnimatedComponent(framesData.texture, "walkDownShirt", 16, 32, new Vector2[] { framesData.frameData[5], framesData.frameData[6], framesData.frameData[7], framesData.frameData[5], framesData.frameData[8], framesData.frameData[9] }, speed, shirtColor));
			walkDown.Add(new AnimatedComponent(framesData.texture, "walkDownSkin", 16, 32, new Vector2[] { framesData.frameData[10], framesData.frameData[11], framesData.frameData[12], framesData.frameData[10], framesData.frameData[13], framesData.frameData[14] }, speed, skinColor));
			walkDown.Add(new AnimatedComponent(framesData.texture, "walkDownEyes", 16, 32, new Vector2[] { framesData.frameData[15], framesData.frameData[16], framesData.frameData[17], framesData.frameData[15], framesData.frameData[18], framesData.frameData[19] }, speed, eyesColor));
			walkDown.Add(new AnimatedComponent(framesData.texture, "walkDownBoots", 16, 32, new Vector2[] { framesData.frameData[20], framesData.frameData[21], framesData.frameData[22], framesData.frameData[20], framesData.frameData[23], framesData.frameData[24] }, speed, bootsColor));

			walkDownRight.Add(new AnimatedComponent(framesData.texture, "walkDownRightPants", 16, 32, new Vector2[] { framesData.frameData[25], framesData.frameData[26], framesData.frameData[27], framesData.frameData[25], framesData.frameData[28], framesData.frameData[29] }, speed, pantsColor));
			walkDownRight.Add(new AnimatedComponent(framesData.texture, "walkDownRightShirt", 16, 32, new Vector2[] { framesData.frameData[30], framesData.frameData[31], framesData.frameData[32], framesData.frameData[30], framesData.frameData[33], framesData.frameData[34] }, speed, shirtColor));
			walkDownRight.Add(new AnimatedComponent(framesData.texture, "walkDownRightSkin", 16, 32, new Vector2[] { framesData.frameData[35], framesData.frameData[36], framesData.frameData[37], framesData.frameData[35], framesData.frameData[38], framesData.frameData[39] }, speed, skinColor));
			walkDownRight.Add(new AnimatedComponent(framesData.texture, "walkDownRightEyes", 16, 32, new Vector2[] { framesData.frameData[40], framesData.frameData[41], framesData.frameData[42], framesData.frameData[40], framesData.frameData[43], framesData.frameData[44] }, speed, eyesColor));
			walkDownRight.Add(new AnimatedComponent(framesData.texture, "walkDownRightBoots", 16, 32, new Vector2[] { framesData.frameData[45], framesData.frameData[46], framesData.frameData[47], framesData.frameData[45], framesData.frameData[48], framesData.frameData[49] }, speed, bootsColor));

			walkRight.Add(new AnimatedComponent(framesData.texture, "walkRightPants", 16, 32, new Vector2[] { framesData.frameData[50], framesData.frameData[51], framesData.frameData[52], framesData.frameData[50], framesData.frameData[53], framesData.frameData[54] }, speed, pantsColor));
			walkRight.Add(new AnimatedComponent(framesData.texture, "walkRightShirt", 16, 32, new Vector2[] { framesData.frameData[55], framesData.frameData[56], framesData.frameData[57], framesData.frameData[55], framesData.frameData[58], framesData.frameData[59] }, speed, shirtColor));
			walkRight.Add(new AnimatedComponent(framesData.texture, "walkRightSkin", 16, 32, new Vector2[] { framesData.frameData[60], framesData.frameData[61], framesData.frameData[62], framesData.frameData[60], framesData.frameData[63], framesData.frameData[64] }, speed, skinColor));
			walkRight.Add(new AnimatedComponent(framesData.texture, "walkRightEyes", 16, 32, new Vector2[] { framesData.frameData[65], framesData.frameData[66], framesData.frameData[67], framesData.frameData[65], framesData.frameData[68], framesData.frameData[69] }, speed, eyesColor));
			walkRight.Add(new AnimatedComponent(framesData.texture, "walkRightBoots", 16, 32, new Vector2[] { framesData.frameData[70], framesData.frameData[71], framesData.frameData[72], framesData.frameData[70], framesData.frameData[73], framesData.frameData[74] }, speed, bootsColor));

			walkUpRight.Add(new AnimatedComponent(framesData.texture, "walkUpRightPants", 16, 32, new Vector2[] { framesData.frameData[75], framesData.frameData[76], framesData.frameData[77], framesData.frameData[75], framesData.frameData[78], framesData.frameData[79] }, speed, pantsColor));
			walkUpRight.Add(new AnimatedComponent(framesData.texture, "walkUpRightShirt", 16, 32, new Vector2[] { framesData.frameData[80], framesData.frameData[81], framesData.frameData[82], framesData.frameData[80], framesData.frameData[83], framesData.frameData[84] }, speed, shirtColor));
			walkUpRight.Add(new AnimatedComponent(framesData.texture, "walkUpRightSkin", 16, 32, new Vector2[] { framesData.frameData[85], framesData.frameData[86], framesData.frameData[87], framesData.frameData[85], framesData.frameData[88], framesData.frameData[89] }, speed, skinColor));
			walkUpRight.Add(new AnimatedComponent(framesData.texture, "walkUpRightBoots", 16, 32, new Vector2[] { framesData.frameData[90], framesData.frameData[91], framesData.frameData[92], framesData.frameData[90], framesData.frameData[93], framesData.frameData[94] }, speed, bootsColor));

			walkUp.Add(new AnimatedComponent(framesData.texture, "walkUpPants", 16, 32, new Vector2[] { framesData.frameData[100], framesData.frameData[101], framesData.frameData[102], framesData.frameData[100], framesData.frameData[103], framesData.frameData[104] }, speed, pantsColor));
			walkUp.Add(new AnimatedComponent(framesData.texture, "walkUpShirt", 16, 32, new Vector2[] { framesData.frameData[105], framesData.frameData[106], framesData.frameData[107], framesData.frameData[105], framesData.frameData[108], framesData.frameData[109] }, speed, shirtColor));
			walkUp.Add(new AnimatedComponent(framesData.texture, "walkUpSkin", 16, 32, new Vector2[] { framesData.frameData[110], framesData.frameData[111], framesData.frameData[112], framesData.frameData[110], framesData.frameData[113], framesData.frameData[114] }, speed, skinColor));
			walkUp.Add(new AnimatedComponent(framesData.texture, "walkUpBoots", 16, 32, new Vector2[] { framesData.frameData[115], framesData.frameData[116], framesData.frameData[117], framesData.frameData[115], framesData.frameData[118], framesData.frameData[119] }, speed, bootsColor));
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
