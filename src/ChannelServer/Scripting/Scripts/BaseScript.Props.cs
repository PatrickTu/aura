﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using Aura.Channel.World;
using Aura.Channel.World.Entities;
using Aura.Data;
using Aura.Shared.Util;

namespace Aura.Channel.Scripting.Scripts
{
	public abstract partial class BaseScript
	{
		private const int PropDropRadius = 50;

		/// <summary>
		/// Creates prop and spawns it.
		/// </summary>
		protected Prop SpawnProp(int id, int regionId, int x, int y, float direction, PropFunc behavior = null)
		{
			var region = ChannelServer.Instance.World.GetRegion(regionId);
			if (region == null)
			{
				Log.Error("{1}.SpawnProp: Region '{0}' doesn't exist.", regionId, this.GetType().Name);
				return null;
			}

			var prop = new Prop(id, regionId, x, y, direction);
			prop.Behavior = behavior;

			region.AddProp(prop);

			return prop;
		}

		/// <summary>
		/// Spawns prop
		/// </summary>
		protected Prop SpawnProp(Prop prop, PropFunc behavior = null)
		{
			var region = ChannelServer.Instance.World.GetRegion(prop.RegionId);
			if (region == null)
			{
				Log.Error("{1}.SpawnProp: Region '{0}' doesn't exist.", prop.RegionId, this.GetType().Name);
				return null;
			}

			prop.Behavior = behavior;

			region.AddProp(prop);

			return prop;
		}

		/// <summary>
		/// Sets behavior for the prop with entityId.
		/// </summary>
		protected Prop SetPropBehavior(long entityId, PropFunc behavior)
		{
			var prop = ChannelServer.Instance.World.GetProp(entityId);
			if (prop == null)
			{
				Log.Error("{1}.SetPropBehavior: Prop '{0}' doesn't exist.", entityId.ToString("X16"), this.GetType().Name);
				return null;
			}

			prop.Behavior = behavior;

			return prop;
		}

		// Behaviors
		// ------------------------------------------------------------------

		protected PropFunc PropDrop(int dropType)
		{
			return Prop.GetDropBehavior(dropType);
		}

		protected PropFunc PropWarp(int region, int x, int y)
		{
			return Prop.GetWarpBehavior(region, x, y);
		}
	}
}