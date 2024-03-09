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
        public class RgbParty : ICommand, IUsageProvider
        {
            public string Command => "RGBparty";

            public string[] Aliases => new[] { "rgbp" };

            public string Description => "Disco!! Party!!";

            public string[] Usage { get; } = { "Duration", "Speed" };

            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                if (!sender.CheckPermission("rgbparty"))
                {
                    response = "You don't have permission to use this command! (rgbparty)";
                    return false;
                }

                var duration = 30f;
                if (arguments.Count >= 1 && float.TryParse(arguments.At(0), out float newDuration))
                {
                    duration = newDuration;
                }
                
                var lerpSpeed = 2.5f;
                if (arguments.Count >= 2 && float.TryParse(arguments.At(1), out float newLerpSpeed))
                {
                    lerpSpeed = newLerpSpeed;
                }

                Map.Broadcast(5, Plugin.Instance.Config.PartyStartMessage);
                Log.Info("Party Started!");
                Cassie.Message(Plugin.Instance.Config.PartyStartCassie, true, true, true);
                Party(duration, lerpSpeed).RunCoroutine();
                response = $"Party Started for {duration} seconds!";
                return true;
            }

            public static IEnumerator<float> Party(float duration, float lerpSpeed)
            {
                var colors = new List<Color>
                {
                    new Color(1, 0.2f, 0.2f), //default red is dim
                    new Color(1, 0.5f, 0), //orange
                    Color.yellow,
                    Color.green*1.1f,
                    Color.cyan,
                    new Color(0F, 0.65F, 1.3F), //nice blue
                    new Color(0.78F, 0.13F, 1.3F), //purple
                };

                var currentColor = Color.white;

                //lerp between colors
                for (float i = 0; i < duration * lerpSpeed; i += 0.05f)
                {
                    currentColor = Color.Lerp(colors[(int)(i % colors.Count)], colors[(int)((i + 1) % colors.Count)], i % 1);
                    Map.ChangeLightsColor(currentColor * 1.5f);
                    yield return Timing.WaitForSeconds(0.05f / lerpSpeed);
                }

                //lerp back to white
                for (float i = 0; i < 1; i += 0.1f)
                {
                    Map.ChangeLightsColor(Color.Lerp(currentColor * 1.5f, Color.white, i));
                    yield return Timing.WaitForSeconds(0.1f);
                }
            }
        }
    }
}
