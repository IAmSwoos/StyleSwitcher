using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;

namespace StyleSwitcher.UI
{
	// This is fucking agony please save me.
	internal class StyleIndicator : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIText text2;
		private UIElement area;
		private UIImage StyleCircle;
		private UIImage RoyalBar;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement(); 
			area.Left.Set(area.Width.Pixels - 1400, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(40, 0f);

			StyleCircle = new UIImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicator1"));
			StyleCircle.Left.Set(8, 0f);
			StyleCircle.Top.Set(30, 0f);
			StyleCircle.Width.Set(78, 0f);
			StyleCircle.Height.Set(46, 0f);

			RoyalBar = new UIImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter0"));
			RoyalBar.Left.Set(0, 0f);
			RoyalBar.Top.Set(80, 0f);
			RoyalBar.Width.Set(94, 0f);
			RoyalBar.Height.Set(22, 0f);

			text = new UIText("0/0", 0.8f); // text to show stat
			text.Width.Set(78, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(15, 0f);
			text.Left.Set(104, 0f);

			text2 = new UIText("0/0", 0.8f); // text to show stat
			text2.Width.Set(78, 0f);
			text2.Height.Set(34, 0f);
			text2.Top.Set(110, 0f);
			text2.Left.Set(0, 0f);

			area.Append(text);
			area.Append(text2);
			area.Append(StyleCircle);
			area.Append(RoyalBar);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			// This prevents drawing unless we are using an ExampleDamageItem
			var modPlayer = Main.LocalPlayer.GetModPlayer<StylePlayer>();

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			//base.DrawSelf(spriteBatch);

			//var modPlayer = Main.LocalPlayer.GetModPlayer<TectPlayer>();
			// Calculate quotient
			//float quotient = (float)modPlayer.Resilience / modPlayer.MaxResilience; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			//quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			//Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			//hitbox.X += 6;
			//hitbox.Width -= 12;
			//hitbox.Y += 10;
			//hitbox.Height -= 20;

			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			//int left = hitbox.Left;
			//int right = hitbox.Right;
			//int steps = (int)((right - left) * quotient);
			//for (int i = 0; i < steps; i += 1) 
			//{
				//float percent = (float)i / steps; // Alternate Gradient Approach
				//float percent = (float)i / (right - left);
				//spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
			//}
		}

		public override void Update(GameTime gameTime) {
			var modPlayer = Main.LocalPlayer.GetModPlayer<StylePlayer>();
			if(Main.LocalPlayer.statLifeMax2 < 0)
				return;

			if (Main.LocalPlayer.name == "Vergil")
			{
				StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/Projectiles/TrickTele"));
			}
			else if (modPlayer.RMastery == 1 && modPlayer.GMastery == 1 && modPlayer.TMastery == 1 && modPlayer.SMastery == 1 && modPlayer.DMastery == 1 && modPlayer.MMastery == 1)
			{
				switch (modPlayer.Style) 
				{
					case 0: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicatorDark1")); break;
					case 1: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicatorDark2")); break;
					case 2: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicatorDark3")); break;
					case 3: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicatorDark4")); break;
					case 4: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicatorDark5")); break;
					case 5: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicatorDark6")); break;
					case 6: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicatorDark7")); break;
				}
			}
			else
			{
				switch (modPlayer.Style) 
				{
					case 0: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicator1")); break;
					case 1: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicator2")); break;
					case 2: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicator3")); break;
					case 3: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicator4")); break;
					case 4: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicator5")); break;
					case 5: StyleCircle.SetImage(ModContent.GetTexture("StyleSwitcher/UI/StyleIndicator6")); break;
				}
			}
			if (Main.LocalPlayer.name == "Vergil")
			{
				RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/Projectiles/TrickTele"));
			}
			else
			{
				switch (modPlayer.GuardMeter) 
				{
					case 0: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter0")); break;
					case 1: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter1")); break;
					case 2: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter2")); break;
					case 3: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter3")); break;
					case 4: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter4")); break;
					case 5: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter5")); break;
					case 6: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter6")); break;
					case 7: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter7")); break;
					case 8: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter8")); break;
					case 9: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter9")); break;
					case 10: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter10")); break;
					case 11: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter11")); break;
					case 12: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter12")); break;
					case 13: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter13")); break;
					case 14: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter14")); break;
					case 15: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter15")); break;
					case 16: RoyalBar.SetImage(ModContent.GetTexture("StyleSwitcher/UI/RoyalReleaseMeter16")); break;
				}
			}
			// Setting the text per tick to update and show our resource values.
			if(Main.LocalPlayer.name == "Vergil")
			{
				switch (modPlayer.DSMastery)
				{
					case 1: 
						text.SetText($"[c/5a1980:DarkSlayer]\nStyle Action 1: Trick({(int)(100 * (1f - ((float)modPlayer.TrickCoolDown/90f)))}%)\nStyle Action 2: ???\nMastery Action: Judgement Cut End({(int)(100 * (1f - ((float)modPlayer.JCECooldown/1800f)))}%)"); break;
					default: 
						switch (modPlayer.DSChallengeFail)
						{
							case 1:
								text.SetText($"[c/5a1980:DarkSlayer]\nStyle Action 1: Trick({(int)(100 * (1f - ((float)modPlayer.TrickCoolDown/90f)))}%)\nStyle Action 2: ???\nMastery challenge: Defeat Eye of cthulhu without touching the ground(Failed)"); break;
							default:
								text.SetText($"[c/5a1980:DarkSlayer]\nStyle Action 1: Trick({(int)(100 * (1f - ((float)modPlayer.TrickCoolDown/90f)))}%)\nStyle Action 2: ???\nMastery challenge: Defeat Eye of cthulhu without touching the ground"); break;
						} break;
				}
			}
			if(modPlayer.Style == 0)
			{
				
				if(modPlayer.TMastery == 1)
				{
					text.SetText($"[c/eeee66:Trickster]\nStyle action 1: Dodge\nStyle Action 2: Trick({(int)(100 * (1f - ((float)modPlayer.TrickCoolDown/120f)))}%)\nMastery Action: None");
				}
				else
				{
					text.SetText($"[c/eeee66:Trickster]\nStyle action 1: Dodge\nStyle Action 2: Trick({(int)(100 * (1f - ((float)modPlayer.TrickCoolDown/120f)))}%)\nMastery challenge: Dodge 15 attacks in a row ({modPlayer.TChallenge}/15)");
				}
			}
			else if(modPlayer.Style == 1)
			{
				switch (modPlayer.meleetype) 
				{
					case 3: text.SetText($"[c/ee4444:Swordmaster]\nStyle Action 1: Stinger\nStyle Action 2: ???\nMastery challenge: None"); break;
					default: text.SetText($"[c/ee4444:Swordmaster]\nStyle Action 1: ???\nStyle Action 2: ???\nMastery challenge: None"); break;
				}
			}
			else if(modPlayer.Style == 2)
			{
				text.SetText($"[c/ffaa44:Doppelganger]\nStyle Action 1: ???\nStyle Action 2: ???\nMastery challenge: None");
			}
			else if(modPlayer.Style == 3)
			{
				if(modPlayer.RMastery == 1)
				{
					text.SetText($"[c/44ee44:Royalguard]\nStyle Action 1: Royal Block\nStyle Action 2: Royal Infusion\nMastery Action: None");
				}
				else
				{
					text.SetText($"[c/44ee44:Royalguard]\nStyle Action 1: Royal Block\nStyle Action 2: Royal Infusion\nMastery challenge: Fill your Royal Charge using only\nperfect royal blocks without takind damage ({modPlayer.RChallenge}/8)");
				}
			}
			else if(modPlayer.Style == 4)
			{
				text.SetText($"[c/4444ee:Gunslinger]\nStyle Action 1: ???\nStyle Action 2: ???\nMastery challenge: None");
			}
			else if(modPlayer.Style == 5)
			{
				text.SetText($"[c/ff66ff:Mastermind]\nStyle Action 1: ???\nStyle Action 2: ???\nMastery challenge: None");
			}
			else if(modPlayer.Style == 6)
			{
				text.SetText($"[c/5a1980:DarkSlayer]\nStyle Action 1: ???\nStyle Action 2: ???\nStyle Action 3: ???");
			}
			if(Main.LocalPlayer.name == "Vergil")
			{
				text2.SetText($" ");
			}
			else
			{
				text2.SetText($"Stored Damage: {modPlayer.GuardStorage}");
			}
			base.Update(gameTime);
		}
	}
}