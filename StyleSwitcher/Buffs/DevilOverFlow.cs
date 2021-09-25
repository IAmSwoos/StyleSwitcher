using Terraria;
using Terraria.ModLoader;

namespace StyleSwitcher.Buffs
{
    public class DevilOverFlow : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Devil Over Flow");
            Description.SetDefault($"You can't use another devil star\nDT recieved from DT drops halved");
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}