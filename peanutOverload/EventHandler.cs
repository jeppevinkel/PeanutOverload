﻿using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Smod2.EventSystem.Events;
using System.Threading.Tasks;
using System.Collections;

namespace peanutOverload
{
    class EventHandler : IEventHandlerRoundStart, IEventHandlerTeamRespawn, IEventHandlerRoundEnd, IEventHandlerCheckEscape, IEventHandlerSetRoleMaxHP, IEventHandlerPlayerHurt
    {
        static IConfigFile Config => ConfigManager.Manager.Config;
        private static System.Random getRandom = new System.Random();
        private PeanutOverload plugin;

        public EventHandler(PeanutOverload plugin)
        {
            this.plugin = plugin;
        }

        public void OnRoundStart(RoundStartEvent ev)
        {
            plugin.roundStarted = true;
            if (Config.GetBoolValue("po_gamemodemanager", true))
            {
                if (GamemodeManager.GamemodeManager.CurrentMode != plugin) return;
            }
            else
            {
                if (!plugin.isActive) return;
            }

            List<Player> players;
            players = plugin.Server.GetPlayers();

            if (players.Count <= 0) return;

            if (players.Count <= 3)
            {
                int rnd = getRandom.Next(0, players.Count);
                Player ClassDPlayer = players[rnd];
                players.RemoveAt(rnd);
                ClassDPlayer.ChangeRole(Role.CLASSD);
                ClassDPlayer.GiveItem(ItemType.USP);

                foreach (Player p in players)
                {
                    p.ChangeRole(Role.SCP_173);
                    //p.SetHealth(310);
                    //p.Damage(5);
                }
            }
            else
            {
                int rnd = getRandom.Next(0, players.Count);
                Player ClassDPlayer = players[rnd];
                players.RemoveAt(rnd);
                int rnd2 = getRandom.Next(0, players.Count);
                Player ClassDPlayer2 = players[rnd2];
                players.RemoveAt(rnd2);
                int rnd3 = getRandom.Next(0, players.Count);
                Player ClassDPlayer3 = players[rnd3];
                players.RemoveAt(rnd3);


                ClassDPlayer.ChangeRole(Role.CLASSD, false, true, false);
                ClassDPlayer2.ChangeRole(Role.CLASSD, false, true, false);
                ClassDPlayer3.ChangeRole(Role.CLASSD, false, true, false);

                ClassDPlayer.GiveItem(ItemType.USP);
                ClassDPlayer2.GiveItem(ItemType.USP);
                ClassDPlayer3.GiveItem(ItemType.USP);

                foreach (Player p in players)
                {
                    p.ChangeRole(Role.SCP_173, false, true, false);
                    //p.SetHealth(310);
                    //p.Damage(5);
                }
            }


            plugin.Server.Map.Broadcast(8, "Peanut Overload: 1-3 Class-D. The rest is peanut. Peanut has low hp, but is super fast. All Class-D start with a pistol.", false);
            plugin.Server.Map.Broadcast(8, "Rules: No Class-D teamkills while the event is active. Class-D are randomly selected.", false);
        }

        public void OnTeamRespawn(TeamRespawnEvent ev)
        {
            if (Config.GetBoolValue("po_gamemodemanager", true))
            {
                if (GamemodeManager.GamemodeManager.CurrentMode != plugin) return;
            }
            else
            {

                if (!plugin.isActive) return;
            }

            ev.PlayerList = null;
        }

        public void OnRoundEnd(RoundEndEvent ev)
        {
            plugin.roundStarted = false;
            if (Config.GetBoolValue("po_gamemodemanager", true))
            {
                if (GamemodeManager.GamemodeManager.CurrentMode != plugin) return;
            }
            else
            {
                if (!plugin.isActive) return;
            }

            plugin.isActive = false;
        }

        public void OnCheckEscape(PlayerCheckEscapeEvent ev)
        {
            if (Config.GetBoolValue("po_gamemodemanager", true))
            {
                if (GamemodeManager.GamemodeManager.CurrentMode != plugin) return;
            }
            else
            {
                if (!plugin.isActive) return;
            }

            if (ev.AllowEscape)
            {
                ev.ChangeRole = Role.SPECTATOR;
            }
        }

        public void OnPlayerHurt(PlayerHurtEvent ev)
        {
            if (Config.GetBoolValue("po_gamemodemanager", true))
            {
                if (GamemodeManager.GamemodeManager.CurrentMode != plugin) return;
            }
            else
            {
                if (!plugin.isActive) return;
            }

            if (ev.Player.TeamRole.Role == Role.SCP_173)
            {
                //ev.Damage = 62;
                ev.Damage = ev.Player.TeamRole.MaxHP * 0.3f;
            }
        }

        public void OnSetRoleMaxHP(SetRoleMaxHPEvent ev)
        {
            //if (Config.GetBoolValue("po_gamemodemanager", true))
            //{
            //    if (GamemodeManager.GamemodeManager.CurrentMode != plugin) return;
            //}
            //else
            //{
            //    if (!plugin.isActive) return;
            //}

            //if (ev.Role == Role.SCP_173)
            //{
            //    ev.MaxHP = 310;
            //}
        }
    }
}
