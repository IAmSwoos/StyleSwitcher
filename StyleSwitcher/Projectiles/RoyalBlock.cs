using System;
using System.Diagnostics.Tracing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace StyleSwitcher.Projectiles
{
	public class RoyalBlock : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			
		}

		public override void SetDefaults() 
		{
			projectile.width = 75;
			projectile.height = 75;
			projectile.timeLeft = 10;
			projectile.penetrate = 9999;
			projectile.aiStyle = 1;
			projectile.rotation = 45f;
			projectile.friendly = true;
			aiType = ProjectileID.Bullet;
			projectile.tileCollide = false;
			projectile.light = 0.5f;
		}

		//private void CastLights()
		//{
			// Cast a light along the line of the laser
			//DelegateMethods.v3_1 = new Vector3(0.5f, 0.2f, 0.2f);
			//Utils.PlotTileLine(projectile.Center, projectile.Center, 26, DelegateMethods.CastLight);
		//}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			knockback = 5f;
			damage = 1;
		}
	}
}