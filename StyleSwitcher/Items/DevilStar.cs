using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StyleSwitcher.Items
{
	public class DevilStar : ModItem //took me a full 10 minutes to realize I put "Eclipsuim" instead of "Eclipsium"
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fills your DT\nHas a 2 minute cool down");
			ItemID.Sets.SortingPriorityMaterials[item.type] = 59; // influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 18;
			item.maxStack = 10;
			item.value = 1000;
			item.rare = 5;
			item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
			item.consumable = true;
            item.UseSound = SoundID.Item3;
			item.useStyle = 2;
		}

		public override void AddRecipes()
		{
			
		}

		public override bool UseItem(Player player) 
		{
			player.GetModPlayer<StylePlayer>().DT = player.GetModPlayer<StylePlayer>().MaxDT;
			player.AddBuff(ModContent.BuffType<Buffs.DevilOverFlow>(), 7200);
			return true;
		}

		public override bool CanUseItem(Player player) 
		{
			return !player.GetModPlayer<StylePlayer>().DTon && !player.HasBuff(ModContent.BuffType<Buffs.DevilOverFlow>());
		}
	}
}
