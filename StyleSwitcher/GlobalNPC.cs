using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace StyleSwitcher.NPCs
{
	public class StyleNPC : GlobalNPC
	{
		public override void SetDefaults(NPC npc)
		{
			npc.buffImmune[ModContent.BuffType<Buffs.TrickYield>()] = false;
		}
		public virtual void UpdateLifeRegen(NPC npc, ref int damage)
		{
			
		}

		public virtual void HitEffect(NPC npc, int hitDirection, double damage) 
		{
			
		}

		public override void NPCLoot(NPC npc)
		{
			if(Main.maxTilesY - 125 < Main.LocalPlayer.position.Y && Main.hardMode)
			{
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.RedOrb>(), Main.rand.Next((int)(Math.Sqrt(npc.lifeMax) * 0.45), (int)(Math.Sqrt(npc.lifeMax) * 0.55f)));
			}
		}

		public override void SetupShop(int type, Chest shop, ref int nextSlot) 
		{
			
		}

		public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit) 
		{
			if(Main.hardMode && Main.rand.NextBool(50))
			{
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.DTDrop>(), 1);
			}
			return true;
		}

		public override void GetChat(NPC npc, ref string chat) 
		{
			if (npc.type == NPCID.Guide && Main.rand.NextBool(4))
			{
				WeightedRandom<string> Talk = new WeightedRandom<string>();
				Talk.Add("Royalguard boasts strong defensive abilities for those who can get good at it, and massive damage for those who can master it.");
				Talk.Add("Trickster is an easy to grasp style, and is excellent for focussing on dodging.");
				Talk.Add("Swordmaster allows you to stay in the air longer with normal melee attacks.");
				Talk.Add("Stinger can be either used as a movement tool for crossing gaps, or a means to close distance.");
				if (Main.hardMode)
				{
					Talk.Add("You want advice on that demonic power you have? Sorry, but I've never seen anything like that before, so I don't know anything about it.");
				}
				chat = Talk;
			}
		}
	}
}
