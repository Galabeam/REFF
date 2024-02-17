using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using System.Linq;

namespace REFF
{
    public class EventHandlers
    {
        public void OnWaitingForPlayers()
        {
            Server.FriendlyFire = false;
        }

        public void OnRoundEnded(RoundEndedEventArgs _)
        {
            Server.FriendlyFire = true;
            var RandomRole = MainPlugin.Singleton.Config.ClassesToAssign.ToList().RandomItem();
            var OneRandomRole = RandomRole;
            foreach (Player player in Player.List)
            {
                if (MainPlugin.Singleton.Config.ItemsToGive.Count > 0)
                {
                    foreach (ItemType item in MainPlugin.Singleton.Config.ItemsToGive)
                        player.AddItem(item);
                }
                if (MainPlugin.Singleton.Config.ClassesToAssign.Count > 0)
                {
                    if (MainPlugin.Singleton.Config.PlayerRandomClass == true)
                    {
                        var MultipleRandomRoles = MainPlugin.Singleton.Config.ClassesToAssign.ToList().RandomItem();
                        player.Role.Set(MultipleRandomRoles, RoleSpawnFlags.UseSpawnpoint);
                    } else {
                        player.Role.Set(OneRandomRole, RoleSpawnFlags.UseSpawnpoint);
                    }
                }
            }
        }
    }
}
