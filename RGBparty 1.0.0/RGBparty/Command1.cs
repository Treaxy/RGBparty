using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;
using UnityEngine;

namespace RGBparty
{
    internal class ConsoleCommand
    {
        [CommandHandler(typeof(RemoteAdminCommandHandler))]

        public class RGBparty : ICommand
        {
            public string Command => "RGBparty";

            public string[] Aliases => new[] { "rgbp" };

            public string Description => "Disco!! Party!!";

            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {

                if (!sender.CheckPermission("rgbparty"))
                {
                    response = null;
                    return false;
                }

                Map.Broadcast(5, Plugin.Instance.Config.PartyStartMessage);
                Log.Info("Party Started!");
                Cassie.Message(Plugin.Instance.Config.PartyStartCassie, true, true, true);
                float count = 0f;
                for (; ; )
                {
                    if (count > 30f)
                    {
                        break;
                    }
                    Timing.CallDelayed(count + 1f, () => { Map.ChangeLightsColor(Color.red); });
                    Timing.CallDelayed(count + 2f, () => { Map.ChangeLightsColor(Color.yellow); });
                    Timing.CallDelayed(count + 3f, () => { Map.ChangeLightsColor(Color.cyan); });
                    Timing.CallDelayed(count + 4f, () => { Map.ChangeLightsColor(Color.magenta); });
                    Timing.CallDelayed(count + 5f, () => { Map.ChangeLightsColor(Color.green); });
                    Timing.CallDelayed(count + 6f, () => { Map.ChangeLightsColor(Color.blue); });
                    Timing.CallDelayed(count + 7f, () => { Map.ChangeLightsColor(Color.white); });
                    count += 7f;
                }

                response = "Party Started!!";
                return true;
            }
        }
    }
}