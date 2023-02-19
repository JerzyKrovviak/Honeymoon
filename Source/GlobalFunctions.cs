using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Honeymoon
{
	public class GlobalFunctions
	{
		public static float PerfectMidPosX(float width)
		{
			return Globals.windowSize.X / 2 - (width / 2);
		}

		public static float PerfectMidPosY(float height)
		{
			return Globals.windowSize.Y / 2 - (height / 2);
		}
	}
}
