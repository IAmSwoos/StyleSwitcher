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
	public class RoyalBlockShield : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			Main.projFrames[projectile.type] = 5;
		}

		public override void SetDefaults() 
		{
			projectile.width = 0;
			projectile.height = 0;
			projectile.timeLeft = 30;
			projectile.penetrate = 9999;
			projectile.aiStyle = 1;
			projectile.rotation = 45f;
			projectile.friendly = true;
			aiType = ProjectileID.Bullet;
			projectile.tileCollide = false;
			projectile.light = 0.2f;
		}

		//private void CastLights()
		//{
			// Cast a light along the line of the laser
			//DelegateMethods.v3_1 = new Vector3(0.5f, 0.2f, 0.2f);
			//Utils.PlotTileLine(projectile.Center, projectile.Center, 26, DelegateMethods.CastLight);
		//}

		public override void AI() 
		{
			if (++projectile.frameCounter >= 6) {
				projectile.frameCounter = 0;
				if (++projectile.frame >= 5) {
					projectile.frame = 0;
				}
			}

			if (projectile.owner == Main.myPlayer)
			{
				StylePlayer p = Main.LocalPlayer.GetModPlayer<StylePlayer>();
				if(p.GuardTimer <= 0)
				{
					projectile.Kill();
				}
			}

			Player player = Main.player[projectile.owner];
			if (player.direction == 1)
			{
				projectile.position.X = player.position.X + 10;
			}
			else
			{
				projectile.position.X = player.position.X - 10;
			}
			projectile.position.Y = player.position.Y + 24;
		}

		private void UpdatePlayer(Player player)
		{
			player.heldProj = projectile.whoAmI;
		}
		private void ChargeLaser(Player player)
		{
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if(p.GuardTimer <= 0)
			{
				projectile.Kill();
			}
		}
	}
}