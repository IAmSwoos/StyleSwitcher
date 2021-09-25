using Terraria;
using Terraria.ModLoader;

namespace StyleSwitcher.Buffs
{
    public class RoyalInfusion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Royal Infusion");
            Description.SetDefault($"Your next successfull attack will deal the damage stored by Royal Guard\nYou cannot absorb more damage while Royal infusion is active\nGetting hit with Royal infusion active will make you loose all your stored damage (only half if the attack is blocked, none if perfect blocked)");
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            StylePlayer p = Main.LocalPlayer.GetModPlayer<StylePlayer>();
            if (p.GuardMeter > 0)
            {
                player.AddBuff(ModContent.BuffType<Buffs.RoyalInfusion>(), 2);
            }
        }
    }
}