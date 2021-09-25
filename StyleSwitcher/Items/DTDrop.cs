using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StyleSwitcher.Items
{
	public class DTDrop : ModItem //took me a full 10 minutes to realize I put "Eclipsuim" instead of "Eclipsium"
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DT Drop");
			Tooltip.SetDefault("");
			ItemID.Sets.SortingPriorityMaterials[item.type] = 59; // influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
		}

		public override void SetDefaults()
		{
			item.maxStack = 999999;
			item.value = 0;
			item.rare = 11;
		}
		public override bool ItemSpace(Player player)
		{
			return true;
		}

		public override bool OnPickup(Player player)
		{
			StylePlayer p = player.GetModPlayer<StylePlayer>();
			if (Main.bloodMoon && !p.DTon)
			{
				if (player.HasBuff(ModContent.BuffType<Buffs.DevilOverFlow>()))
				{
					p.DT += 10;
				}
				else
				{
					p.DT += 20;
				}
			}
			else if (!p.DTon)
			{
				if (player.HasBuff(ModContent.BuffType<Buffs.DevilOverFlow>()))
				{
					p.DT += 5;
				}
				else
				{
					p.DT += 10;
				}
			}
			return false;
		}

		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange = 200;
		}
	}
}
