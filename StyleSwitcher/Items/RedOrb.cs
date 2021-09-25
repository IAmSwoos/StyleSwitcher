using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StyleSwitcher.Items
{
	public class RedOrb : ModItem //took me a full 10 minutes to realize I put "Eclipsuim" instead of "Eclipsium"
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Used to buy special items from Dante");
			ItemID.Sets.SortingPriorityMaterials[item.type] = 59; // influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
		}

		public override void SetDefaults()
		{
			item.maxStack = 999999;
			item.value = 0;
			item.rare = 11;
		}

		public override void AddRecipes()
		{
			
		}
	}
}
