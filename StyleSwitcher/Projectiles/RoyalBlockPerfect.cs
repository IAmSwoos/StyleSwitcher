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
	public class RoyalBlockPerfect : ModProjectile
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
			projectile.light = 1f;
		}

		//private void CastLights()
		//{
			// Cast a light along the line of the laser
			//DelegateMethods.v3_1 = new Vector3(0.4f, 0.4f, 1f);
			//Utils.PlotTileLine(projectile.Center, projectile.Center, 26, DelegateMethods.CastLight);
		//}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
			StylePlayer modPlayer = player.GetModPlayer<StylePlayer>();
			knockback = 10f;
			if(modPlayer.PerfectInfusion > 0)
			{
				damage = 100 * modPlayer.GuardStorage;
				modPlayer.GuardStorage = 0;
				modPlayer.GuardMeter = 0;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) 
		{
			Player player = Main.player[projectile.owner];
			if (player.direction == 1)
			{
				target.velocity.X = 25;
			}
			else
			{
				target.velocity.X = -25;
			}
			player.immune = true;
			player.immuneTime = 60;
		}
	}
}