using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StyleSwitcher.Items
{
	public class ConfidencePotion : ModItem //took me a full 10 minutes to realize I put "Eclipsuim" instead of "Eclipsium"
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Are you CURAZEE enough to use this potion effectively?\nDecreases defense to 0 but doubles Damage stored by perfect royal blocks.");
			ItemID.Sets.SortingPriorityMaterials[item.type] = 59; // influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 18;
			item.maxStack = 99;
			item.value = 1000;
			item.rare = 5;
			item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
			item.useStyle = 2;
			item.consumable = true;
			item.buffType = ModContent.BuffType<Buffs.Confidence>();
			item.buffTime = 72000;
		}

		public override void AddRecipes()
		{
			var CalamityMod = ModLoader.GetMod("CalamityMod");
			var calamityVanillaItemRecipeChanges = ModLoader.GetMod("calamityVanillaItemRecipeChanges");
			ModRecipe recipe1 = new ModRecipe(mod);
			recipe1.AddIngredient(126, 1);
			recipe1.AddIngredient(316, 1);
			recipe1.AddIngredient(313, 1);
			recipe1.AddTile(13);
			recipe1.SetResult(this);
			recipe1.AddRecipe();
			if (CalamityMod != null) 
			{
				ModRecipe recipe2 = new ModRecipe(mod);
				recipe2.AddIngredient(126, 1);
				recipe2.AddIngredient(CalamityMod.ItemType("BloodOrb"));
				recipe2.AddTile(355);
				recipe2.SetResult(this);
				recipe2.AddRecipe();
			}
			if (calamityVanillaItemRecipeChanges != null)
			{
				ModRecipe recipe3 = new ModRecipe(mod);
				recipe3.AddIngredient(126, 1);
				recipe3.AddIngredient(calamityVanillaItemRecipeChanges.ItemType("BloodOrb"));
				recipe3.AddTile(355);
				recipe3.SetResult(this);
				recipe3.AddRecipe();
			}
		}
	}
}
