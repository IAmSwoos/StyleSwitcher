using Terraria;
using Terraria.ModLoader;

namespace StyleSwitcher.Buffs
{
    public class Confidence : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Confidence");
            Description.SetDefault("Defense is set to zero, but damage stored from perfect royal blocks is doubled");
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 999999;
        }
    }
}