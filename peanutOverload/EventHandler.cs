using Smod2;
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
    class EventHandler : IEventHandlerRoundStart, IEventHandlerTeamRespawn, IEventHandlerRoundEnd, IEventHandlerCheckEscape, IEventHandlerPlayerHurt
    {
        static IConfigFile Config => ConfigManager.Manager.Config;
        private static System.Random getRandom = new System.Random();
        private PeanutOverload plugin;

        private bool spawnThingy = false;
        private List<Player> players;
        private int waitIntThingy = 0;

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
                    p.SetHealth(1);
                }
                //spawnThingy = true;
            }
            else
            {
                int rnd = getRandom.Next(0, players.Count);
                players.RemoveAt(rnd);
                int rnd2 = getRandom.Next(0, players.Count);
                players.RemoveAt(rnd2);
                int rnd3 = getRandom.Next(0, players.Count);
                players.RemoveAt(rnd3);

                Player ClassDPlayer = players[rnd];
                Player ClassDPlayer2 = players[rnd2];
                Player ClassDPlayer3 = players[rnd3];

                ClassDPlayer.ChangeRole(Role.CLASSD, false, true, false);
                ClassDPlayer2.ChangeRole(Role.CLASSD, false, true, false);
                ClassDPlayer3.ChangeRole(Role.CLASSD, false, true, false);

                ClassDPlayer.GiveItem(ItemType.USP);
                ClassDPlayer2.GiveItem(ItemType.USP);
                ClassDPlayer3.GiveItem(ItemType.USP);

                foreach (Player p in players)
                {
                    p.ChangeRole(Role.SCP_173);
                    p.SetHealth(1);
                }
                //spawnThingy = true;
            }


            //plugin.Server.Map.Broadcast(10, "Doggo Attack: This is an event where the mission of Class D is to escape the facility by any means necessary. One escaped Class D is a victory to Class D", false);
            //plugin.Server.Map.Broadcast(10, "There will spawn one scp 939-89, and any Class D killed by it will become scp 939-53, and aid it in killing Class D.", false);
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
            if (ev.AllowEscape)
            {
                ev.ChangeRole = Role.SPECTATOR;
            }
        }

        public void OnPlayerHurt(PlayerHurtEvent ev)
        {
            if (ev.Player.TeamRole.Role == Role.SCP_173)
            {
                ev.Player.Kill();
            }
        }

        //public void OnFixedUpdate(FixedUpdateEvent ev)
        //{
        //    if (spawnThingy)
        //    {
        //        if(waitIntThingy <= 3)
        //        {
        //            waitIntThingy++;
        //        }
        //        else if (waitIntThingy == 4)
        //        {
        //            foreach (Player p in players)
        //            {
        //                p.ChangeRole(Role.SCP_173);
        //                p.SetHealth(1000);
        //            }
        //            waitIntThingy++;
        //        }
        //    }
        //}

        //public IEnumerable<Task> SpawnPeanut(Player p)
        //{
        //    Task.Wait(TimeSpan.FromMilliseconds(500));
        //    p.ChangeRole(Role.SCP_173);
        //    p.SetHealth(1);
        //    yield return 0;
        //}
    }
}