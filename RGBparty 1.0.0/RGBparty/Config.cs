using Exiled.API.Interfaces;
using System.ComponentModel;

namespace RGBparty
{
    public class Config : IConfig
    {
        [Description("Plugin is Enabled?")]
        public bool IsEnabled { get; set; } = true;
        [Description("Plugin debug is enabled?")]
        public bool Debug {  get; set; } = false;
        [Description("Party command send broadcast")]
        public string PartyStartMessage { get; set; } = "PARTY STARTED!";
        [Description("Party command send cassie message")]
        public string PartyStartCassie { get; set; } = "Xmas_JingleBells .g4 Attention All Personnel . That is not test announcement . Please have good time . Just good time .";
    }
}
