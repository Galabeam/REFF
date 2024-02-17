using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using ServerHandler = Exiled.Events.Handlers.Server;

namespace REFF
{
    public class MainPlugin : Plugin<Config>
    {
        public override string Name => "REFF";
        public override string Author => "Galabeam (formerly Thunder, originally Kognity)";
        public override Version Version => new(1, 0, 0);
        public override Version RequiredExiledVersion => new(8, 8, 0);
        public override PluginPriority Priority => PluginPriority.Low;

        public static MainPlugin Singleton;
        public static EventHandlers Handlers;

        public override void OnEnabled()
        {
            if (Server.FriendlyFire)
            {
                Log.Warn("FriendlyFire is enabled! REFF will be disabled.");
                return;
            }

            Singleton = this;
            Handlers = new();

            ServerHandler.WaitingForPlayers += Handlers.OnWaitingForPlayers;
            ServerHandler.RoundEnded += Handlers.OnRoundEnded;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerHandler.WaitingForPlayers -= Handlers.OnWaitingForPlayers;
            ServerHandler.RoundEnded -= Handlers.OnRoundEnded;

            Singleton = null;
            Handlers = null;
            base.OnDisabled();
        }
    }

    public class Config : IConfig
    {
        [Description("REFF enabled")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug logs")]
        public bool Debug { get; set; } = false;

        [Description("Items gave at the end of a round.")]
        public List<ItemType> ItemsToGive { get; set; } = new();

        [Description("Random classes assigned at the end of a round.")]
        public List<RoleTypeId> ClassesToAssign { get; set; } = new()
        {
            RoleTypeId.Scp096,
        };

        [Description("T: All players get a different random class from the list. | F: All players get the same random class from the list.")]
        public bool PlayerRandomClass { get; set; } = false;
    }
}
