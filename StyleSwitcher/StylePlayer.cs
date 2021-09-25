using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using StyleSwitcher.Projectiles;

namespace StyleSwitcher
{
    // ModPlayer classes provide a way to attach data to Players and act on that data. ExamplePlayer has a lot of functionality related to 
    // several effects and items in ExampleMod. See SimpleModPlayer for a very simple example of how ModPlayer classes work.
    public class StylePlayer : Terraria.ModLoader.ModPlayer
    {
        public int Style = 0;
        public int GuardStorage;
        public int GuardTimer = 0;
        public int GuardMeter;
        public int DodgeCooldown = 0;
        public int TrickCoolDown = 0;
        public int Dodgetime = 0;
        public int StyleActCancel = 0;
        public int trickinvul = 0;
        public int PerfectInfusion;
        public int suspension;
        public int suspendTime;
        public int suspendReset;
        public int meleetype;
        public int Stinger;
        public float StingerSpeed;
        public int StingerTime;
        public int StingerCoolDown;
        public int RMastery;
        public int TMastery;
        public int SMastery;
        public int DMastery;
        public int GMastery;
        public int MMastery;
        public int DSMastery;
        public int DSChallengeStart;
        public int DSChallenge;
        public int DSChallengeFail;
        public int DSChallengePreStart;
        public int RChallenge;
        public int TChallenge;
        public int JCECooldown;
        public int JCE;
        public int DT;
        public int MaxDT;
        public int PurpOrbs;
        public bool DTon;
        public int DTDrainTimer;

        public override void ResetEffects()
        {
            MaxDT = 60 + (20 * PurpOrbs);
            if (DT > MaxDT)
            {
                DT = MaxDT;
            }
            if (GuardTimer > 0)
            {
                GuardTimer -= 1;
            }
            if (StyleActCancel > 0)
            {
                StyleActCancel -= 1;
            }
            if (DodgeCooldown > 0)
            {
                DodgeCooldown -= 1;
            }
            if (TrickCoolDown > 0)
            {
                TrickCoolDown -= 1;
            }
            switch (Style) 
			{
                case 0: player.allDamage += 0; break;
                case 1: player.meleeDamage *= 1.2f; break;
                case 2: player.minionDamage *= 1.2f; break;
                case 3: player.allDamage += 0; break;
                case 4: player.rangedDamage *= 1.2f; break;
                case 5: player.magicDamage *= 1.2f; break;
                case 6: player.magicDamage *= 1.15f; player.meleeDamage *= 1.05f; player.moveSpeed *= 1.2f; player.meleeSpeed *= 1.15f; break;
                case 7: player.magicDamage *= 1.20f; player.meleeDamage *= 1.075f; player.moveSpeed *= 1.2f; player.meleeSpeed *= 1.20f; break;
            }
            if (RChallenge >= 8)
            {
                RMastery = 1;
            }
            if (TChallenge >= 15)
            {
                TMastery = 1;
            }
            if (Dodgetime > 0)
            {
                Dodgetime -= 1;
                player.moveSpeed *= 1.1f;
            }
            if(player.name == "Vergil")
            {
                Style = 7;
            }
            if (JCE > 0)
            {
                JCE -= 1;
                player.moveSpeed *= 0f;
                player.runAcceleration *= 0f;
            }
            if (JCECooldown > 0)
            {
                JCECooldown -= 1;
            }
            if (JCE > 0 && JCE < 11)
            {
                player.immune = true;
                player.AddBuff(10, 2);
            }
            if  (JCE == 10)
            {
                Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.JCE>(), (int)(100 * (player.meleeDamage + player.magicDamage + player.allDamage)), 0, player.whoAmI);
            }
            if (PerfectInfusion > 0)
            {
                PerfectInfusion -= 1;
            }
            if (suspension > 0)
            {
                player.velocity.Y = -0.35f;
                suspension -= 1;
                suspendTime += 1;
                if(Stinger < 1)
                {
                    player.velocity.X = 0;
                }
            }
            if (suspendTime >= 120 )
            {
                suspendReset = 1;
            }
            if ((suspendReset == 1 && player.velocity.Y == 0) || (player.wingTimeMax > 0 && player.wingTime == player.wingTimeMax))
            {
                suspendTime = 0;
                suspendReset = 0;
            }
            if (Stinger > 0)
            {
                if (Stinger > 1)
                {
                    if (player.direction == 1)
			        {
                        player.velocity.X = (float)(10 / ((float)StingerTime / 30));
                    }
                    else
                    {
                        player.velocity.X = -1f * (float)(10 / ((float)StingerTime / 30));
                    }
                }
                else
                {
                    player.velocity.X = 0;
                }
                Stinger -= 1;
            }
            if (trickinvul > 0)
            {
                trickinvul -= 1;
                var CalamityMod = ModLoader.GetMod("CalamityMod");
                if (CalamityMod != null);
                {
                    player.buffImmune[CalamityMod.BuffType("AbyssalFlames")] = true;
				    player.buffImmune[CalamityMod.BuffType("VulnerabilityHex")] = true;
                }
                player.buffImmune[BuffID.OnFire] = true;
			    player.buffImmune[BuffID.CursedInferno] = true;
			    player.buffImmune[153] = true;
            }
            int EOC = NPC.FindFirstNPC(NPCID.EyeofCthulhu);
            if (EOC >= 0 && DSChallengePreStart == 0)
            {
                DSChallengePreStart = 1;
                DSChallengeFail = 0;
                DSChallenge = 0;
                DSChallengeStart = 0;
            }
            if (DSChallengeStart == 1 && player.velocity.Y == 0)
            {
                DSChallengeFail = 1;
            }
            if (EOC < 0)
            {
                DSChallengePreStart = 0;
            }
            if (DTon == true)
            {
                player.moveSpeed *= 1.25f;
                player.meleeSpeed += player.meleeSpeed / 10;
                player.lifeRegen += player.statLifeMax2 / 10;
                player.manaRegen += player.statManaMax2 / 5;
                DTDrainTimer += 1;
            }
            if (DTDrainTimer >= 6)
            {
                DT -= 1;
                DTDrainTimer = 0;
            }
            if (DT < 1)
            {
                DTon = false;
            }
            if(DTon == false)
            {
                DTDrainTimer = 0;
            }
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)StyleSwitcherMessage.syncPlayer);
            packet.Write((byte)player.whoAmI);
            packet.Write(RMastery);
            packet.Write(TMastery);
            packet.Write(SMastery);
            packet.Write(DMastery);
            packet.Write(GMastery);
            packet.Write(MMastery);
            packet.Write(DSMastery);
            packet.Write(PurpOrbs);
            packet.Write(DT);
            //packet.Write(nonStopParty); // While we sync nonStopParty in SendClientChanges, we still need to send it here as well so newly joining players will receive the correct value.
            packet.Send(toWho, fromWho);
        }

        public override TagCompound Save()
        {
            return new TagCompound {
            {"RMastery", RMastery},
            {"TMastery", TMastery},
            {"SMastery", SMastery},
            {"DMastery", DMastery},
            {"GMastery", GMastery},
            {"MMastery", MMastery},
            {"DSMastery", DSMastery},
            {"DT", DT},
            {"PurpOrbs", PurpOrbs},
            };
        }

        public override void Load(TagCompound tag)
        {
            RMastery = tag.GetInt("RMastery");
            TMastery = tag.GetInt("TMastery");
            SMastery = tag.GetInt("SMastery");
            DMastery = tag.GetInt("DMastery");
            GMastery = tag.GetInt("GMastery");
            MMastery = tag.GetInt("MMastery");
            DSMastery = tag.GetInt("DSMastery");
            DT = tag.GetInt("DT");
            PurpOrbs = tag.GetInt("PurpOrbs");
        }

        // Custom Crit Rate
        
        public override void UpdateVanityAccessories() {
			
		}

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff) {
			
		}

        public override void FrameEffects() 
        {
            
        }

        public float critMultiplier = 1.0f; // Base crit multiplier. Critical damage will be damage * this number + damage type modifier.
        public float meleeCritMultiplier = 0.0f; // Melee Crit Multiplier, percentage that will be added onto the critical damage.
        public float rangedCritMultiplier = 0.0f; // Ranged Crit Multiplier, percentage that will be added onto the critical damage.
        public float magicCritMultiplier = 0.0f; // Magic Crit Multiplier, percentage that will be added onto the critical damage.
        public float thrownCritMultiplier = 0.0f; // Thrown Crit Multiplier, percentage that will be added onto the critical damage.

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (crit == true)
            {
                if (item.melee == true) // Melee Crit
                {
                    damage = (int)(damage * (critMultiplier + meleeCritMultiplier)); // Damage gets amplified by the crit multiplier.
                }
                else if (item.ranged == true) // Ranged Crit
                {
                    damage = (int)(damage * (critMultiplier + rangedCritMultiplier));
                }
                else if (item.magic == true) // Magic Crit
                {
                    damage = (int)(damage * (critMultiplier + magicCritMultiplier));
                }
                else if (item.thrown == true) // Thrown Crit
                {
                    damage = (int)(damage * (critMultiplier + thrownCritMultiplier));
                }
                else
                {
                    damage = (int)(damage * critMultiplier); // Damage gets amplified by the crit multiplier.
                }
            }
        }

        //public override void OnHitByProjectile(int damage, bool crit)
        //{
            //if (Chivallry)
            //{
                //damage = 0; 
            //}
        //}
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (StyleSwitcher.Trickster.JustPressed && player.name != "Vergil")
            {
                if(Style == 0 && RMastery == 1 && GMastery == 1 && TMastery == 1 && SMastery == 1 && DMastery == 1 && MMastery == 1)
                {
                    Style = 6;
                }
                else
                {
                    Style = 0;
                }
            }
            if (StyleSwitcher.SwordDoppel.JustPressed && player.name != "Vergil")
            {
                if (Style == 1)
                {
                    Style = 2;
                }
                else
                {
                    Style = 1;
                }
            }
            if (StyleSwitcher.RoyalGuard.JustPressed && player.name != "Vergil")
            {
                if(Style == 3 && RMastery == 1 && GMastery == 1 && TMastery == 1 && SMastery == 1 && DMastery == 1 && MMastery == 1)
                {
                    Style = 6;
                }
                else
                {
                    Style = 3;
                }
            }
            if (StyleSwitcher.GunMind.JustPressed && player.name != "Vergil")
            {
                if (Style == 4)
                {
                    Style = 5;
                }
                else
                {
                    Style = 4;
                }
            }
            if (StyleSwitcher.Style1.JustPressed)
            {
                if (TrickCoolDown < 1 && StyleActCancel < 1 && player.name == "Vergil")
                {
                    Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.TrickTele>(), 1, 0, player.whoAmI);
                    TrickCoolDown = 60;
                    StyleActCancel = 6;
                    player.immune = true;
			        player.immuneTime = 30;
                    if(player.HasBuff(BuffID.Featherfall))
                    {
                        player.velocity.Y = -1f;
                    }
			        else
                    {
                        player.velocity.Y = -5;
                    }
                    Dodgetime = 30;
                    trickinvul = 30;
                }
                if ( StyleActCancel < 1 && Style == 3)
                {
                    StyleActCancel = 30;
                    GuardTimer = 30;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.RoyalBlockShield>(), 0, 0, player.whoAmI);
                }
                if (StyleActCancel < 1 && Style == 0)
                {
                    StyleActCancel = 45;
                    DodgeCooldown = 45;
                    Dodgetime = 30;
                }
                if (StyleActCancel < 1 && Style == 1 && meleetype == 3)
                {
                    Stinger = StingerTime;
                    StyleActCancel = (int)(StingerTime * 1.2f);
                }
            }
            if (StyleSwitcher.Style2.JustPressed)
            {
                if(GuardMeter >= 2 && Style == 3 && StyleActCancel < 1)
                {
                    player.AddBuff(ModContent.BuffType<Buffs.RoyalInfusion>(), 2);
                    PerfectInfusion = 15;
                }
                else if (TrickCoolDown < 1 && StyleActCancel < 1 && Style == 0)
                {
                    Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.TrickTele>(), 1, 0, player.whoAmI);
                    TrickCoolDown = 120;
                    StyleActCancel = 6;
                }
            }
            if (StyleSwitcher.Style3.JustPressed)
            {
                if (JCECooldown < 1 && StyleActCancel < 1 && player.name == "Vergil" && DSMastery == 1)
                {
                    JCE = 130;
                    JCECooldown = 1800;
                    StyleActCancel = 305;
                }
            }
            if (StyleSwitcher.DT.JustPressed && Main.hardMode)
            {
                if(DT >= 60 && DTon == false)
                {
                    DTon = true;
                }
                else
                {
                    DTon = false;
                }
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
            if (GuardTimer > 24)
            {
                if (GuardMeter < 16 && !Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.RoyalInfusion>()))
                {
                    if (GuardMeter == 15)
                    {
                        if(Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.Confidence>()))
                        {
                            GuardStorage += (4 * (int)damage);
                            GuardMeter += 1;
                        }
                        else
                        {
                            GuardStorage += (2 * (int)damage);
                            GuardMeter += 1;
                        }
                    }
                    else
                    {
                        if(Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.Confidence>()))
                        {
                            GuardStorage += (8 * (int)damage);
                            GuardMeter += 2;
                        }
                        else
                        {
                            GuardStorage += (4 * (int)damage);
                            GuardMeter += 2;
                        }
                    RChallenge += 1;
                    }
                }
                var CalamityMod = ModLoader.GetMod("CalamityMod");
                if (CalamityMod != null) 
			    {
                    int WILF = NPC.FindFirstNPC(CalamityMod.NPCType("SupremeCalamitas"));
                    int Wom = NPC.FindFirstNPC(CalamityMod.NPCType("DevourerOfGodsHead"));
                    int Wom2 = NPC.FindFirstNPC(CalamityMod.NPCType("DevourerOfGodsHead2"));
                    if (CalamityMod != null && (WILF >= 0 || Wom >= 0 || Wom2 >= 0))
                    {
                        player.statLife += damage * 2;
                        player.buffImmune[CalamityMod.BuffType("AbyssalFlames")] = true;
				        player.buffImmune[CalamityMod.BuffType("VulnerabilityHex")] = true;
                    }
                }
                DT += 45;
                GuardTimer = 0;
                //Main.PlaySound(4, player.position, 13);
                quiet = true;
                player.immune = true;
                player.immuneTime = 60;
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.RoyalBlockPerfect>(), 1, 10, player.whoAmI);
                return false;
            }
            else if (GuardTimer > 0)
            {
                if (GuardMeter < 16 && !Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.RoyalInfusion>()))
                {
                    
                    GuardStorage += (int)(damage * (float)(GuardTimer / 25));
                    GuardMeter += 1;
                }
                damage -= (int)((4 * damage /5) * (float)(GuardTimer / 25));
                GuardTimer = 0;
                DT += (int)(15 * (float)(GuardTimer / 25));
                //Main.PlaySound(2, player.position, 13);
                if (Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.RoyalInfusion>()))
                {
                    if(GuardMeter > 8)
                    {
                        GuardMeter -= 8;
                        GuardStorage -= 8 * GuardStorage / GuardMeter;
                    }
                    else
                    {
                       GuardMeter = 0;
                       GuardStorage = 0; 
                    }
                }
                player.noKnockback = true;
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.RoyalBlock>(), 1, 5, player.whoAmI);
                RChallenge = 0;
                return true;
            }
            else if (Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.RoyalInfusion>()))
            {
                GuardMeter = 0;
                GuardStorage = 0;
                RChallenge = 0;
                return true;
            }
            else if (Dodgetime > 0)
            {
                player.immune = true;
                player.immuneTime = 5;
                TChallenge += 1;
                return false;
            }
            else if (JCE > 0)
            {
                damage -= 4 * damage / 3;
                return true;
            }
            else
            {
                RChallenge = 0;
                TChallenge = 0;
                return true;
            }
            if (DTon)
            {
                damage -= (int)(damage * 0.25);
            }
		}
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            GuardMeter = 0;
            GuardStorage = 0;
            DT = 0;
            DTon = false;
        }
        
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (GuardTimer > 24)
            {
                player.statLife += (int)damage;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
