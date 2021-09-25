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
using StyleSwitcher.Buffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System;
using System.IO;
using System.Linq;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using Terraria.Utilities;
using StyleSwitcher.Projectiles;

namespace StyleSwitcher.Projectiles
{
	public class StyleProjectile : GlobalProjectile
	{
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (projectile.owner == Main.myPlayer)
			{
				if(Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.RoyalInfusion>()) && projectile.type != ModContent.ProjectileType<Projectiles.RoyalBlock>() && projectile.type != ModContent.ProjectileType<Projectiles.RoyalBlockPerfect>() && projectile.type != ModContent.ProjectileType<Projectiles.TrickTele>())
				{
					StylePlayer p = Main.LocalPlayer.GetModPlayer<StylePlayer>();
					damage += p.GuardStorage;
					p.GuardStorage = 0;
					p.GuardMeter = 0;
				}
			}
		}

		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit) 
		{
			
			Player player = Main.player[projectile.owner];
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if (projectile.owner == Main.myPlayer)
			{
				if(target.type == NPCID.EyeofCthulhu && target.life <= 0 && p.DSChallengeFail == 0)
				{
					p.DSMastery = 1;
				}
				if(target.type == NPCID.EyeofCthulhu)
				{
					p.DSChallengeStart = 1;
				}
			}
		}

		public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit)
		{
			StylePlayer p = target.GetModPlayer<StylePlayer>();
			if (p.GuardTimer > 24)
			{
				projectile.Kill();
			}
		}

		public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
		{
			StylePlayer p = target.GetModPlayer<StylePlayer>();
			if (p.GuardTimer > 24)
			{
				projectile.Kill();
			}
		}
	}
}