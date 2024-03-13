using System;
using System.Collections.Generic;
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
            public string Description => "This plugin start a 30 second mini party";
            public string[] Usage { get; } = { "Duration", "Speed" };

            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                if (!sender.CheckPermission("rgbparty"))
                {
                    response = "You need 'rgbparty' permission to use this command!";
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

            public static readonly List<Color> Colors = new List<Color>
            {
                new Color(1, 0.2f, 0.2f),
                new Color(1, 0.5f, 0),
                Color.yellow,
                Color.green*1.1f,
                Color.cyan,
                new Color(0f, 0.65f, 1.3f),
                new Color(0.78f, 0.13f, 1.3f),
            };

            public static IEnumerator<float> Party(float duration, float lerpSpeed)
            {
                var currentColor = Color.clear;

                for (float inter = 0; inter < duration * lerpSpeed; inter += 0.05f)
                {
                    currentColor = Color.Lerp(Colors[(int)(inter % Colors.Count)], Colors[(int)((inter + 1) % Colors.Count)], inter % 1);
                    Map.ChangeLightsColor(currentColor * 1.5f);
                    yield return Timing.WaitForSeconds(0.05f / lerpSpeed);
                }

                for (float intar = 0; intar < 1; intar += 0.05f)
                {
                    Map.ChangeLightsColor(Color.Lerp(currentColor * 1.5f, Color.clear, intar));
                    yield return Timing.WaitForSeconds(0.05f / lerpSpeed);
                }
            }
        }
    }
}