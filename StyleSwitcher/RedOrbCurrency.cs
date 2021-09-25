using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace StyleSwitcher
{
	public class RedOrbCurrency : CustomCurrencySingleCoin
	{
		public Color RedOrbCurrencyTextColor = Color.Red;

		public RedOrbCurrency(int coinItemID, long currencyCap) : base(coinItemID, currencyCap) {
		}

		public override void GetPriceText(string[] lines, ref int currentLine, int price) {
			Color color = RedOrbCurrencyTextColor * ((float)Main.mouseTextColor / 255f);
			lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
				{
					color.R,
					color.G,
					color.B,
					Language.GetTextValue("LegacyTooltip.50"),
					price,
					"Red Orbs"
				});
		}
	}
}
