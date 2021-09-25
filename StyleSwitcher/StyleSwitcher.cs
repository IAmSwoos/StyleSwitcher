using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using StyleSwitcher.UI;

namespace StyleSwitcher
{
    public class StyleSwitcher : Mod
    {
		public static int RedOrbCurrencyID;
		
		public static ModHotKey Trickster;
		public static ModHotKey SwordDoppel;
		public static ModHotKey RoyalGuard;
		public static ModHotKey GunMind;
		public static ModHotKey DT;
		public static ModHotKey Style1;
		public static ModHotKey Style2;
		public static ModHotKey Style3;

		private UserInterface _StyleUserInterface;
		private UserInterface _StyleIndicator;
		private UserInterface _DTMeter;

		internal StyleIndicator StyleIndicator;
		internal DTMeter DTMeter;

        public StyleSwitcher()
        {

        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
			StyleSwitcherMessage msgType = (StyleSwitcherMessage)reader.ReadByte();
			switch (msgType)
			{
				case StyleSwitcherMessage.world:
					StyleWorld StyleWorld = ModContent.GetInstance<StyleWorld>();
					int FirstDT = reader.ReadInt32();
					StyleWorld.FirstDT = FirstDT;
					break;
				case StyleSwitcherMessage.syncPlayer:
					byte playernumber = reader.ReadByte();
					StylePlayer modPlayer = Main.player[playernumber].GetModPlayer<StylePlayer>();
					int RMastery = reader.ReadInt32();
					modPlayer.RMastery = RMastery;
					int TMastery = reader.ReadInt32();
					modPlayer.TMastery = TMastery;
					int SMastery = reader.ReadInt32();
					modPlayer.SMastery = SMastery;
					int DMastery = reader.ReadInt32();
					modPlayer.DMastery = DMastery;
					int GMastery = reader.ReadInt32();
					modPlayer.GMastery = GMastery;
					int MMastery = reader.ReadInt32();
					modPlayer.MMastery = MMastery;
					int DSMastery = reader.ReadInt32();
					modPlayer.DSMastery = DSMastery;
					int DT = reader.ReadInt32();
					modPlayer.DT = DT;
					int PurpOrbs = reader.ReadInt32();
					modPlayer.PurpOrbs = PurpOrbs;
					// modPlayer.nonStopParty = reader.ReadBoolean();
					// SyncPlayer will be called automatically, so there is no need to forward this data to other clients.
					break;
					//default:
					//Logger.WarnFormat("ExampleMod: Unknown Message type: {0}", msgType);
					//break;
			}
		}

		public override void Load()
		{
			RedOrbCurrencyID = CustomCurrencyManager.RegisterCurrency(new RedOrbCurrency(ModContent.ItemType<Items.RedOrb>(), 9999999));
			
			Trickster = RegisterHotKey("Trickster", "Up");
			SwordDoppel = RegisterHotKey("Swordmaster/Doppelganger", "Right");
			RoyalGuard = RegisterHotKey("RoyalGuard", "Down");
			GunMind = RegisterHotKey("Gunslinger/Mastermind", "Left");
			DT = RegisterHotKey("Devil Trigger", "V");
			Style1 = RegisterHotKey("Style Action 1", "Z");
			Style2 = RegisterHotKey("Style Action 2", "x");
			Style3 = RegisterHotKey("Style Mastery Action/[c/46dde8:Style Action 3]", "C");

			StyleIndicator = new StyleIndicator();
			_StyleIndicator = new UserInterface();
			_StyleIndicator.SetState(StyleIndicator);

			DTMeter = new DTMeter();
			_DTMeter = new UserInterface();
			_DTMeter.SetState(DTMeter);
			AddEquipTexture(null, EquipType.Head, "DanteDT", "StyleSwitcher/DevilHunterAlt");

			ModTranslation text = CreateTranslation("LivesLeft");
			text = CreateTranslation("DTAwakening");
			text.SetDefault("Demonic energy fills your body, invoking a dark gift");
			AddTranslation(text);
		}

		public override void UpdateUI(GameTime gameTime) 
		{
			_StyleIndicator?.Update(gameTime);
			_DTMeter?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) 
		{
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1) {
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"StyleSwitcher: Style Indicator",
					delegate {
						_StyleIndicator.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"StyleSwitcher: DT Meter",
					delegate {
						_DTMeter.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}	
		}
    }
	internal enum StyleSwitcherMessage : byte
	{
		syncPlayer,world
	}
}