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
using StyleSwitcher.Items;
using static Terraria.ModLoader.ModContent;

namespace StyleSwitcher
{
    // ModPlayer classes provide a way to attach data to Players and act on that data. ExamplePlayer has a lot of functionality related to 
    // several effects and items in ExampleMod. See SimpleModPlayer for a very simple example of how ModPlayer classes work.
   [AutoloadHead]
   public class DevilHunter : ModNPC
   {
		public override string Texture => "StyleSwitcher/DevilHunter";
		public override string[] AltTextures => new[] { "StyleSwitcher/DevilHunterAlt" };

		public override bool Autoload(ref string name) {
			name = "Devilhunter";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults() {
			// DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
			// DisplayName.SetDefault("Example Person");
			Main.npcFrameCount[npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults() {
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 600;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
			npc.lifeRegen = 1;
		}

		public virtual void UpdateLifeRegen(NPC npc, ref int damage) 
		{
			npc.lifeRegen = 60;
        }

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) 
		{
			for (int k = 0; k < 255; k++) 
			{
				Player player = Main.player[k];
				if (!player.active) 
				{
					continue;
				}
				if (Main.hardMode == true) 
				{
						return true;
				}
			}
			return false;
		}

		public override string TownNPCName() {
			switch (WorldGen.genRand.Next(1)) {
				default:
					return "Dante";
			}
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop) 
		{
			if (firstButton) 
			{
				shop = true;
			}
			else
			{
				switch (Main.rand.Next(2))
				{
					case 1: Main.npcChatText = "During bloodmoons, your \"Devil Trigger\" will fill twice as fast."; break;
					case 2: Main.npcChatText = "When the text below your Devil Trigger meter turns [c/e349ce:a dark purple] your Devil Trigger is able to be activated, what ever tham means."; break;
					default: Main.npcChatText = "The only way to fill that funny purple bar is to hurt enemies, unless you wanna give me red orbs for a devil star."; break;
				}
			}
		}
		public override string GetChat() 
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			var CalamityMod = ModLoader.GetMod("CalamityMod");
			if (CalamityMod != null) 
			{
				if (Main.LocalPlayer.HasBuff(CalamityMod.BuffType("HotE")))
				{
					chat.Add("Huh, looks like you got a collection goin'.");
				}
				chat.Add("Hey, if you see a white haired Demon in red, tell them that 'Dante did it first'... No I mean a different white haired demon in red.");
			}
			chat.Add("Are yah ready?");
			if (Main.LocalPlayer.name == "Vergil")
			{
				chat.Add("Good to see you again, 'big bro'");
			}
			if (Main.bloodMoon)
			{
				chat.Add("This party's getting crazy! Let's ROCK!");
				chat.Add("Are you kidding!? This is what I live for!");
				chat.Add("Curayzee!!");
			}
			chat.Add("Who is Nero? What the hell are you on about?");
			chat.Add("I wish I could just get out there and fight. but instead I gotta play vendor and mentor for some self proclaimed hero.");
			chat.Add("I'm still not sure what to call my shop... maybe... nah that wouldn't work.");
			return chat;
			
		}

		public override void SetChatButtons(ref string button, ref string button2) 
		{
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = "Advice";
		}

		public override void SetupShop(Chest shop, ref int nextSlot) 
		{
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.PurpleOrb>());
			shop.item[nextSlot].shopCustomPrice = 5000;
			shop.item[nextSlot].shopSpecialCurrency = StyleSwitcher.RedOrbCurrencyID;
			nextSlot++;
			if (!Main.LocalPlayer.HasItem(ModContent.ItemType<Items.DevilStar>()) || Main.LocalPlayer.GetModPlayer<StylePlayer>().PurpOrbs >= 7)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.DevilStar>());
				shop.item[nextSlot].shopCustomPrice = 4500;
				shop.item[nextSlot].shopSpecialCurrency = StyleSwitcher.RedOrbCurrencyID;
				nextSlot++;
			}
		}

		public virtual void DrawEffects(ref Color drawColor)
		{
			//
		}
    }
}
