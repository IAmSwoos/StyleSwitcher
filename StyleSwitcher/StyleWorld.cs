using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;

namespace StyleSwitcher
{
	public class StyleWorld : ModWorld
	{
		public int FirstDT;

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) 
		{

			
		}
		public override void PostUpdate() 
		{
			if(Main.hardMode && FirstDT == 0)
			{
				Main.LocalPlayer.GetModPlayer<StylePlayer>().DT = 60;
				Main.LocalPlayer.GetModPlayer<StylePlayer>().DTon = true;
				FirstDT = 1;
				string key = "Mods.StyleSwitcher.DTAwakening";
				Color messageColor = Color.Purple;
				if (Main.netMode == NetmodeID.Server) // Server
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
			}
		}

		public override TagCompound Save()
        {
            return new TagCompound {
            {"FirstDT", FirstDT},
            };
		}

		public override void Load(TagCompound tag)
        {
            FirstDT = tag.GetInt("FirstDT");
		}
	}
}