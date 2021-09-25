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
	public class JCE : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			
		}

		public override void SetDefaults() 
		{
			projectile.width = 500;
			projectile.height = 500;
			projectile.timeLeft = 10;
			projectile.penetrate = -1;
			projectile.aiStyle = 1;
			projectile.rotation = 45f;
			projectile.friendly = true;
			aiType = ProjectileID.Bullet;
			projectile.tileCollide = false;
			projectile.light = -1f;
		}

		//private void CastLights()
		//{
			// Cast a light along the line of the laser
			//DelegateMethods.v3_1 = new Vector3(0.5f, 0.2f, 0.2f);
			//Utils.PlotTileLine(projectile.Center, projectile.Center, 26, DelegateMethods.CastLight);
		//}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
			var CalamityMod = ModLoader.GetMod("CalamityMod");
            if (CalamityMod != null);
            {
				int WILF = NPC.FindFirstNPC(CalamityMod.NPCType("SupremeCalamitas"));
				if (target.type == CalamityMod.NPCType("SupremeCalamitas"))
				{
					damage = (int)(Math.Pow(target.lifeMax, 0.6f) * 8);
				}
			}
			if (target.boss == true)
			{
				damage = (int)(Math.Pow(target.lifeMax, 0.6f) * 2);
			}
			else
			{
				damage = (int)(Math.Pow(target.lifeMax, 0.6f) * 5);
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.immune[projectile.owner] = 0;
		}
	}
}