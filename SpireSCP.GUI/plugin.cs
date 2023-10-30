﻿using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using MEC;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using Exiled.CustomItems.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.API.Features.Doors;
using System;
using SpireLabs.GUI;
using PluginAPI.Events;
using Exiled.Events.EventArgs.Server;

namespace SpireSCP.GUI
{
    public class Plugin : Plugin<config>
    {
        public override string Name => "Spire Labs GUI";
        public override string Author => "ImIsaacTbh";
        public override System.Version Version => new System.Version(1, 0, 0);
        public override System.Version RequiredExiledVersion => new System.Version(8, 0, 1);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Joined += OnPlayerJoined;
            Exiled.Events.Handlers.Player.Verified += JoinMSG;
            Exiled.Events.Handlers.Player.Left += LeaveMSG;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStart;
        }

        public override void OnDisabled()
        {

        }
        
        private void OnPlayerJoined(JoinedEventArgs ev)
        {
            Timing.RunCoroutine(guiHandler.displayGUI(ev.Player));
        }

        private void JoinMSG(VerifiedEventArgs ev)
        {
            Timing.RunCoroutine(guiHandler.sendJoinLeave(ev.Player, 'j'));
        }

        private void LeaveMSG(LeftEventArgs ev)
        {
            Timing.RunCoroutine(guiHandler.sendJoinLeave(ev.Player, 'l'));
        }
        private void OnRoundStart()
        {
            guiHandler.startHints();
        }
    }
}