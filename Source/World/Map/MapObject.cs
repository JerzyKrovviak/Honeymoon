﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Managers;
using Honeymoon.Source.World.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.World.Map
{
	[DataContract(Namespace = "")]
	public class MapObject : PhysicalComponent
	{
		[DataMember(Name = "mapid", IsRequired = false, Order = 3, EmitDefaultValue = false)]
		public int mapid { get; set; }
		[DataMember(Name = "objname", IsRequired = false, Order = 4, EmitDefaultValue = false)]
		public string name { get; set; }
		[DataMember(Name = "objpos", IsRequired = false, Order = 5, EmitDefaultValue = false)]
		public Vector2 position;
		public int[] spawnableTiles;
		public bool drawAboveEntity, isAnimated, isPassable;
		public int framesCount;
		private int currentFrame;
		public float frameSpeed;
		private double timeSinceLastFrame;
		private readonly float spawnChance;

		public MapObject(int mapid, string name, Vector2 position)
		{
			this.mapid = mapid;
			this.position = position;
			this.name = name;
		}

		public bool IsObjectHovered()
		{
			Vector2 fixedMousePos = new Vector2(-Camera.cameraOffsetX + Globals.mousePosition.X, -Camera.cameraOffsetY + Globals.mousePosition.Y);
			if (hitBox.Contains(fixedMousePos))
			{
				return true;
			}
			return false;
		}

		public bool IsObjectClicked()
		{
			Vector2 fixedMousePos = new Vector2(-Camera.cameraOffsetX + Globals.mousePosition.X, -Camera.cameraOffsetY + Globals.mousePosition.Y);
			if (hitBox.Contains(fixedMousePos))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					return true;
				}
			}
			return false;
		}

		public void PlayAnimation()
		{
			timeSinceLastFrame += Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
			if (timeSinceLastFrame > frameSpeed)
			{
				if (currentFrame < framesCount - 1)
				{
					currentFrame += 1;
				}
				else
					currentFrame = 0;
				timeSinceLastFrame = 0;
			}
			sourceData.X = sourceData.Width * currentFrame;
		}
	}
}
