using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.World.HudElements
{
	internal class Item : PhysicalComponent
	{
		private const int maxStack = 999;
		private readonly string itemName;
		private int amount;
		private readonly string itemDesc;
		private readonly string itemType;
		private readonly bool canStack;
		private int invSlot;
	}
}
