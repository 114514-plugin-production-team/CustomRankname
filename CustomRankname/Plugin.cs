
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using MEC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomRankname
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "HUI114514(QQ3145186196)";
        public override string Name => "customrank";
        public override Version Version => new Version(1, 0);
        public static Plugin plugin;
        public static string paths;
        public static string filepath;
        public static List<PlayerInfo> PlayerInfos { get; set; } = new List<PlayerInfo>();

        public override string Description => "customrank";

        public override Version RequiredApiVersion => new Version(1, 0);

        List<PlayerInfo> list = new List<PlayerInfo>();
        public void OnStart()
        {
            Plugin.PlayerInfos = Plugin.ReadJson();
        }
        public static List<PlayerInfo> ReadJson()
        {
            List<PlayerInfo> list = new List<PlayerInfo>();
            string path = filepath;
            bool flag = !File.Exists(path);
            if (flag)
            {
                list.Add(new PlayerInfo
                {
                    UserId = "steam64Id@steam",
                    Badge = "这是一个单色称号",
                    BadgeColor = "red",
                });
                list.Add(new PlayerInfo
                {
                    UserId = "steam64Id@steam",
                    Badge = "这是一个彩色称号",
                    BadgeColor = "rainbow",
                });
                File.WriteAllText(path, JsonConvert.SerializeObject(list));
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<PlayerInfo>>(File.ReadAllText(path));
            }
            return list;
        }

        public override void Enable()
        {
            ServerEvents.RoundStarted += OnStart;
            PlayerEvents.Joined += Joined;
            paths = Path.Combine(LabApi.Loader.Features.Paths.PathManager.LabApi.ToString(), "CustRank", Server.Port.ToString());
            if (!Directory.Exists(paths))
                Directory.CreateDirectory(paths);
            filepath = Path.Combine(paths, "player.json");
            GameCore.Console.AddLog($"[CustomRank]文件已创建,目录[{filepath}]", Color.blue);
            GameCore.Console.AddLog($"[CustomRank]文件已创建,目录[{filepath}]", Color.blue);

            if (!File.Exists(filepath))
            {
                list.Add(new PlayerInfo
                {
                    UserId = "114514@steam",
                    Badge = "这是一个单色称号",
                    BadgeColor = "red",
                });
                list.Add(new PlayerInfo
                {
                    UserId = "114514@steam",
                    Badge = "这是一个彩色称号",
                    BadgeColor = "rainbow",
                });
                File.WriteAllText(filepath, JsonConvert.SerializeObject(list));
            }
        }

        public override void Disable()
        {
            ServerEvents.RoundStarted -= OnStart;
            PlayerEvents.Joined -= Joined;
        }
        void Joined(PlayerJoinedEventArgs ev)
        {
            if (ev.Player!= null)
            {
                PlayerInfos = Plugin.ReadJson();
                if (PlayerInfos.Any((PlayerInfo x) => x.UserId == ev.Player.UserId))
                {
                    PlayerInfo playerInfo = PlayerInfos.First((PlayerInfo x) => x.UserId == ev.Player.UserId);
                    Timing.CallDelayed(1.5f, delegate
                    {
                        ev.Player.ReferenceHub.serverRoles.SetText(playerInfo.Badge);
                        if (playerInfo.BadgeColor !="rainbow")
                        {
                            ev.Player.ReferenceHub.serverRoles.SetColor(playerInfo.BadgeColor);
                        }
                        else
                        {
                            Timing.RunCoroutine(Badge.ColorCoroutine(ev.Player));
                        }
                    });
                }
            }
        }

        public class PlayerInfo
        {
            public string UserId { get; set; }

            public string Badge { get; set; }

            public string BadgeColor { get; set; }

        }
    }
    public class Badge
    {
        // Token: 0x06000003 RID: 3 RVA: 0x00002069 File Offset: 0x00000269
        public static IEnumerator<float> ColorCoroutine(Player player)
        {
            while(true)
            {
                foreach (string s in Badge.Colors)
                {
                    player.ReferenceHub.serverRoles.SetColor(s);
                    yield return Timing.WaitForSeconds(0.8f);
                }
                yield return Timing.WaitForSeconds(0.5f);
            }
        }

        // Token: 0x04000002 RID: 2
        private static List<string> Colors = new List<string>
        {
            "pink", "red", "brown", "silver", "light_green", "crimson", "cyan", "aqua", "deep_pink", "tomato",
            "yellow", "magenta", "blue_green", "orange", "lime", "green", "emerald", "carmine", "nickel", "mint",
            "army_green", "pumpkin"
        };
    }
}
