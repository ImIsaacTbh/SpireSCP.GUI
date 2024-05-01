using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpireLabs.GUI
{
    internal class guiHandler
    {

        public class effectThing
        {
            private string v;
            private float duration;

            public effectThing(string name, float time)
            {
                this.name = name;
                this.time = time;
            }

            public string name { get; set; }
            public float time { get; set; }
        }

        internal static bool killLoop = false;
        internal static string joinLeave = string.Empty;
        internal static string[] peenNutMSG = new string[60];
        internal static string[] modifiers = new string[7];
        internal static void fillPeenNutMSG()
        {
            for(int i = 0; i < peenNutMSG.Length; i++)
            {
                peenNutMSG[i] = "\t";
            }
        }

        internal static IEnumerator<float> sendJoinLeave(Player p, char jl)
        {
            string usr = p.DisplayNickname;
                if (jl == 'j')
                {
                    joinLeave = $"Hello, {usr}!";
                    yield return Timing.WaitForSeconds(3);
                    if (joinLeave == $"Hello, {usr}!") joinLeave = string.Empty;
                }
                if (jl == 'l')
                {
                    joinLeave = $"Goodbye, {usr}!";
                    yield return Timing.WaitForSeconds(3);
                    if (joinLeave == $"Goodbye, {usr}!") joinLeave = string.Empty;
                }
        }
        internal static IEnumerator<float> sendHint(Player p, string h, float t)
        {
            hint[p.Id] = h;
            string localHint = h;
            yield return Timing.WaitForSeconds(t);
            if (p.CurrentHint.Content.Contains(localHint)) hint[p.Id] = string.Empty;
            if (p.Role == RoleTypeId.Spectator) hint[p.Id] = string.Empty;
        }

        internal static string[] hint = new string[60];

        internal static void startHints()
        {
            for (int i = 0; i < hint.Length; i++)
            {
                hint[i] = string.Empty;
            }
        }

        //bottom text <align=center><size=15><b><color=#3a5fcf>ObscureLabs</color> <color=#88D4FF>-</color> </b>Discord.gg/f8uEpZWcBv/</size>

        internal static IEnumerator<float> displayGUI(Player p)
        {
            bool pp = true;
            hint[p.Id] = string.Empty;
            yield return Timing.WaitForSeconds(5f);
            Log.Debug("Displaying GUI");
            while (true)
            {
                effectThing[] effects = new effectThing[7];
                try
                {
                    //if(!p.IsAlive || p.Role == RoleTypeId.Spectator || !Round.InProgress)
                    //{}
                    //else
                    //{
                    for (int i = 0; i < p.ActiveEffects.Count(); i++)
                        {
                            if (i > 6) break;
                            try
                            {
                                Log.Debug("Checking Effect Type");
                                switch (p.ActiveEffects.ElementAt(i).name)
                                {
                                    case "Asphyxiated":
                                        effects[i] = new effectThing("<b><color=#95c700>Asphyxiated</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Bleeding":
                                        effects[i] = new effectThing("<b><color=#750004>Bleeding</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Blinded":
                                        effects[i] = new effectThing("<b><color=#394380>Blinded</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Burned":
                                        effects[i] = new effectThing("<b><color=#de831b>Burned</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Deafened":
                                        effects[i] = new effectThing("<b><color=#4a2700>Deafened</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Ensnared":
                                        effects[i] = new effectThing("<b><color=#00750e>Ensnared</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Exhausted":
                                        effects[i] = new effectThing("<b><color=#696969>Exhausted</color></b>>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Flashed":
                                        effects[i] = new effectThing("<b><color=#d6fffd>Flashed</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Invigorated":
                                        effects[i] = new effectThing("<b><color=green>Invigorated</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Poisoned":
                                        effects[i] = new effectThing("<b><color=#02521e>Poisoned</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "DamageReduction":
                                        effects[i] = new effectThing("<b><color=#e3b76b>Damage Reduction</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "MovementBoost":
                                        effects[i] = new effectThing("<b><color=#3bafd9>Movement Boost</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "RainbowTaste":
                                        effects[i] = new effectThing(
                                            "<b><color=#FF0000>R</color><color=#FF7F00>a</color><color=#FFFF00>i</color><color=#7FFF00>n</color><color=#00FF00>b</color><color=#00FF7F>o</color><color=#00FEFF>w</color><color=#007FFF> T</color><color=#0000FF>a</color><color=#7F00FF>s</color><color=#FF00FE>t</color><color=#FF007F>e</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "SeveredHands":
                                        effects[i] = new effectThing("<b><color=red>Severed Hands</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Stained":
                                        effects[i] = new effectThing("<b><color=#543601>Stained</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Vitality":
                                        effects[i] = new effectThing("<b><color=#71c788>Vitality</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Hypothermia":
                                        effects[i] = new effectThing("<b><color=#42e9ff>Hypothermia</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Scp1853":
                                        effects[i] = new effectThing("<b><color=#c9f59a>SCP1853</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "CardiacArrest":
                                        effects[i] = new effectThing("<b><color=red>Cardiac Arrest</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "AntiScp207":
                                        effects[i] = new effectThing("<b><color=#fa70ff>Anti Scp207</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Invisible":
                                        effects[i] = new effectThing("<b><color=#540042>Invisible</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Scp207":
                                        effects[i] = new effectThing("<b><color=#bb80ff>Scp207</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "BodyshotReduction":
                                        effects[i] = new effectThing(
                                            "<b><color=#c9af3a>Bodyshot Reduction</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Hemorrhage":
                                        effects[i] = new effectThing("<b><color=#ff0000>Hemorrhage</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Disabled":
                                        effects[i] = new effectThing("<b><color=#828282>Disabled</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Corroding":
                                        effects[i] = new effectThing("<b><color=#5e8c60>Corroding</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Concussed":
                                        effects[i] = new effectThing("<b><color=#5e728c>Concussed</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Scanned":
                                        effects[i] = new effectThing("<b><color=#ffff00>Scanned</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Ghostly":
                                        effects[i] = new effectThing("<b><color=#f2f3f5>Ghostly</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "Strangled":
                                        effects[i] = new effectThing("<b><color=#687fad>Strangled</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;
                                    case "SilentWalk":
                                        effects[i] = new effectThing("<b><color=#a9a9a9>Silent Walk</color></b>",
                                            p.ActiveEffects.ElementAt(i).TimeLeft);
                                        break;

                                }

                                    //Log.Debug($"{p.DisplayNickname} has effect {effects[i]} as player id {i}");
                            }
                            catch (Exception e)
                            {
                                Log.Info($"Error: {e}");
                            }
                        }
                    //}
                }
                catch (Exception e)
                {
                    Log.Info($"Error: {e}");
                }
                Log.Debug("Got past effects");
                string s = "<align=center><size=32>";
                #region lines
                s += $"\t\n"; //0
                s += $"\t\n"; //1 (TOP OF SCREEN)
                if (hint[p.Id] == string.Empty)
                {
                    s += $"\t\n"; //2
                    s += $"\t\n"; //3
                    s += $"\t\n"; //4
                }
                else
                {
                    if (hint[p.Id].Split(char.Parse("\n")).Length == 0 || hint[p.Id].Length < 70)
                    {
                        s += $"{hint[p.Id]}\n"; //2
                        s += $"\t\n"; //3
                        s += $"\t\n"; //4
                    }
                    else if (hint[p.Id].Split(char.Parse("\n")).Length == 1 || hint[p.Id].Length > 70 && hint[p.Id].Length < 140)
                    {
                        string[] split = hint[p.Id].Split(char.Parse("\n"));
                        s += $"{split[0]}\n"; //2
                        s += $"{split[1]}\n"; //3
                        s += $"\t\n"; //4
                    }
                    else if (hint[p.Id].Split(char.Parse("\n")).Length == 2 || hint[p.Id].Length > 140)
                    {
                        string[] split = hint[p.Id].Split(char.Parse("\n"));
                        s += $"{split[0]}\n"; //2
                        s += $"{split[1]}\n"; //3
                        s += $"{split[2]}\n"; //4
                    }
                }
                Log.Debug("Got past hints");
                s += $"\t\n"; //5
                if (modifiers[0] == null)
                {
                    s += $"<align=right>\t</align>\n"; //6
                }
                else
                {
                    s += $"<align=right>{modifiers[0]}</align>\n"; //6
                }

                if (modifiers[1] == null)
                {
                    s += $"<align=right>\t</align>\n"; //6
                }
                else
                {
                    s += $"<align=right>{modifiers[1]}</align>\n"; //6
                }
                if (modifiers[2] == null)
                {
                    s += $"<align=right>\t</align>\n"; //6
                }
                else
                {
                    s += $"<align=right>{modifiers[2]}</align>\n"; //6
                }
                if (modifiers[3] == null)
                {
                    s += $"<align=right>\t</align>\n"; //6
                }
                else
                {
                    s += $"<align=right>{modifiers[3]}</align>\n"; //6
                }
                if (modifiers[4] == null)
                {
                    s += $"<align=right>\t</align>\n"; //6
                }
                else
                {
                    s += $"<align=right>{modifiers[4]}</align>\n"; //6
                }
                if (modifiers[5] == null)
                {
                    s += $"<align=right>\t</align>\n"; //6
                }
                else
                {
                    s += $"<align=right>{modifiers[5]}</align>\n"; //6
                }
                if (modifiers[6] == null)
                {
                    s += $"<align=right>\t</align>\n"; //6
                }
                else
                {
                    s += $"<align=right>{modifiers[6]}</align>\n"; //6
                }
                s += $"\t\n"; //13
                s += $"\t\n"; //14
                s += $"\t\n"; //15
                s += $"\t\n"; //16
                s += $"\t\n"; //17
                s += $"\t\n"; //18
                Log.Debug("Got to effects");
                if (effects[0] == null || (int)effects[0].time <= 0)
                {
                    s += $"<align=left>\t</align>\n"; //19
                }
                else
                {
                    s += $"<align=left>{effects[0].name} - {(int)effects[0].time}s</align>\n"; //19
                }
                Log.Debug("Did effect 1");
                if (effects[1] == null || (int)effects[1].time <= 0)
                {
                    s += $"<align=left>\t</align>\n"; //20
                }
                else
                {
                    s += $"<align=left>{effects[1].name} - {(int)effects[1].time}s</align>\n"; //20
                }
                Log.Debug("Did effect 1");
                if (effects[2] == null || (int)effects[2].time <= 0)
                {
                    s += $"<align=left>\t</align>\n"; //21
                }
                else
                {
                    s += $"<align=left>{effects[2].name} - {(int)effects[2].time}s</align>\n"; //21
                }
                Log.Debug("Did effect 2");
                if (effects[3] == null || (int)effects[3].time <= 0)
                {
                    s += $"<align=left>\t</align>\n"; //22
                }
                else
                {
                    s += $"<align=left>{effects[3].name} - {(int)effects[3].time}s</align>\n"; //22
                }
                Log.Debug("Did effect 3");
                if (effects[4] == null || (int)effects[4].time <= 0)
                {
                    s += $"<align=left>\t</align>\n"; //23
                }
                else
                {
                    s += $"<align=left>{effects[4].name} - {(int)effects[4].time}s</align>\n"; //23
                }
                Log.Debug("Did effect 4");
                if (effects[5] == null || (int)effects[5].time <= 0)
                {
                    s += $"<align=left>\t</align>\n"; //24
                }
                else
                {
                    s += $"<align=left>{effects[5].name} - {(int)effects[5].time}s</align>\n"; //24
                }
                Log.Debug("Did effect 5");
                if (effects[6] == null || (int)effects[6].time <= 0)
                {
                    s += $"<align=left>\t</align>\n"; //25
                }
                else
                {
                    s += $"<align=left>{effects[6].name} - {(int)effects[6].time}s</align>\n"; //25
                }
                Log.Debug("Did effects");
                s += $"\t\n"; //26
                s += $"\t\n"; //27
                s += $"\t\n"; //28
                s += $"<size=16><color=#3a5fcf>OBSCURE</color>\n"; //29 (BOTTOM OF SCREEN)
                s += $"<color=#88d4ff>LABS</color></size>\n";
                s += $"<size=32><color=#ffffff>\t\n"; //30
                s += $"\t\n"; //31
                s += $"\t\n"; //32
                s += $"\t\n"; //33
                s += $"\t\n"; //34
                s += $"\t\n"; //35
                s += $"\t\n"; //36
                s += $"\t\n"; //37
                s += $"\t\n"; //38
                #endregion
                s += "</align></size>";
                Log.Debug("What the fuck");
                yield return Timing.WaitForSeconds(1f);
                
                    Log.Debug("Completed Message");
                    p.ShowHint(s, 1.25f);
                    Log.Debug("Shown Hint");
                    if (!guiHandler.killLoop)
                        pp = true;
                    else
                        break;
            }
        }
    }
}
