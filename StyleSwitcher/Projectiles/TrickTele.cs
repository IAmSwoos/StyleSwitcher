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
	public class TrickTele : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			
		}

		public override void SetDefaults() 
		{
			projectile.width = 2;
			projectile.height = 2;
			projectile.timeLeft = 1;
			projectile.penetrate = 1;
			projectile.aiStyle = 1;
			projectile.rotation = 45f;
			projectile.friendly = true;
			aiType = ProjectileID.Bullet;
			projectile.tileCollide = false;
			projectile.light = 0.0f;
		}

		//private void CastLights()
		//{
			// Cast a light along the line of the laser
			//DelegateMethods.v3_1 = new Vector3(0.5f, 0.2f, 0.2f);
			//Utils.PlotTileLine(projectile.Center, projectile.Center, 26, DelegateMethods.CastLight);
		//}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			knockback = 0f;
			damage = 1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) 
		{
			Player player = Main.player[projectile.owner];
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if (player.position.X < Main.MouseWorld.X)
			{
				player.position.X = target.position.X - 8;
			}
			else
			{
				player.position.X = target.position.X + target.width + 8;
			}
            player.position.Y = target.position.Y;
			player.immune = true;
			player.immuneTime = 30;
			if (player.name != "Vergil")
			{
				if(player.HasBuff(BuffID.Featherfall))
                {
                    player.velocity.Y = -0.5f;
                }
			    else
                {
                    player.velocity.Y = -5;
                }
			}
			target.AddBuff(ModContent.BuffType<Buffs.TrickYield>(), 126);
			if (player.name == "Vergil")
			{
				p.suspendTime = 0;
                p.suspendReset = 0;
				player.wingTime = player.wingTimeMax;
			}
		}
	}
}