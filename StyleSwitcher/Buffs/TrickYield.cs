using Terraria;
using Terraria.ModLoader;

namespace StyleSwitcher.Buffs
{
    public class TrickYield : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Trick Yield");
            Description.SetDefault("You've been tricked to, so you can't move.");
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity.X = 0;
            player.velocity.Y = 0;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
        }
    }
}