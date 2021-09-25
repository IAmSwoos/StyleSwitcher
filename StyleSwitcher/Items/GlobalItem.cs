using System;
using System.Diagnostics.Tracing;
using Terraria;
using Terraria.ID;
using System.Runtime.CompilerServices;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace StyleSwitcher.Items
{
	public class globalItem : GlobalItem
	{
		public override void SetDefaults(Item item)
		{
			if(item.type == ModContent.ItemType<Items.RedOrb>())
			{
				item.maxStack = 999999;
			}
		}
		public override void ModifyHitNPC(Item item, Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if(player.HasBuff(ModContent.BuffType<Buffs.RoyalInfusion>()))
			{
				damage += p.GuardStorage;
				p.GuardStorage = 0;
				p.GuardMeter = 0;
			}
			else if (p.Stinger > 1)
			{
				if (p.DTon)
				{
					damage *= 4;
				}
				else
				{
					damage += (int)(damage * (3f - (3f * p.Stinger / (float)item.useTime)));
				}
			}
			else if (p.DTon)
			{
				damage += damage/5;
			}
		}

		public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockback, bool crit) 
		{
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if(target.type == NPCID.EyeofCthulhu && target.life <= 0 && p.DSChallengeFail == 0)
			{
				p.DSMastery = 1;
			}
			if(target.type == NPCID.EyeofCthulhu)
			{
				p.DSChallengeStart = 1;
			}
			if (Main.rand.Next(0, 60) <= item.useTime && !p.DTon)
			{
				if (Main.bloodMoon)
				{
					p.DT += 2;
				}
				else
				{
					p.DT += 1;
				}
			}
		}

		public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult)
		{
			//StylePlayer p = player.GetModPlayer<StylePlayer>();
			//if (p.Stinger > 1)
			//{
			//	if (p.DTon)
			//	{
			//		mult *= 4;
			//	}
			//	else
			//	{
			//		mult *= 1f + (3f - (3f * p.Stinger / (float)item.useTime));
			//	}
			//}
			//else if (p.DTon)
			//{
			//	mult *= 1.2f;
			//}
		}

		public override void GetWeaponKnockback(Item item, Player player, ref float knockback)
		{
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if (p.Stinger > 1)
			{
				if (p.DTon)
				{
					knockback *= 10f;
				}
				else
				{
					knockback *= 1f + (9f - (9f * p.Stinger / (float)item.useTime));
				}
			}
		}

		public override bool UseItem(Item item, Player player)
		{
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if (item.melee == true && item.useStyle == 3 && p.Style == 1)
			{
				if (StyleSwitcher.Style1.Current && p.StyleActCancel < 0 )
				{
					p.Stinger += item.useTime;
					p.StyleActCancel += (int)(item.useTime * 1.1f);
				}
			}
			else
			{
				
			}
			return true;
		}

		public override void UpdateInventory(Item item, Player player)
		{
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if (Main.LocalPlayer.inventory[player.selectedItem] == item)
			{
				if(item.melee == true)
				{
					p.meleetype = item.useStyle;
				}
				p.StingerTime = item.useTime;
			}
		}

		public override bool CanUseItem(Item item, Player player) 
		{
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if(item.melee == true && (p.Style == 1 || player.name == "Vergil") && p.suspendTime < 120 && player.velocity.Y != 0 && item.pick == 0)
			{
				p.suspension = item.useTime;
			}
			if (item.melee == true && item.useStyle == 3 && p.Style == 1)
			{
				if (StyleSwitcher.Style1.Current && p.StyleActCancel == 0 )
				{
					p.StyleActCancel += (int)(item.useTime * 1.1f);
				}
			}
			p.StyleActCancel = item.useTime;
			return true;
		}
	}
}