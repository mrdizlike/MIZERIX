using System.Collections;
using System.Collections.Generic;
using Discord;
using System;
using UnityEngine;

public class DiscordIntegration : MonoBehaviour
{

    public static Discord.Discord discord;
    public ActivityManager activityManager;
    public bool DiscordDisponible;

    public Discord.Activity activity;

    private void Start()
    {
        foreach(System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
        {
            try
            {
                if (p.ToString() == "System.Diagnostics.Process (Discord)")
                {
                    Debug.Log("Discord enable");
                    DiscordDisponible = true;
                    break;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error: " + e);
            }
        }


        if (DiscordDisponible)
        {
            discord = new Discord.Discord(970969570795978784, (System.UInt64)Discord.CreateFlags.Default);
            activityManager = discord.GetActivityManager();

            GamePanel();
        }
    }

    void GamePanel()
    {
        activity.Party.Size.CurrentSize = 1;
        activity.Party.Size.MaxSize = 5;

        activity.Details = "Classic: 5x5";

        activity.State = "Playing solo";
        activity.Timestamps.Start = ToUnixTime();

        activity.Assets.LargeImage = "maingameicon";
        activity.Assets.LargeText = "maingameicon";

        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Discord integration OK");
            }
        });
    }

    public long ToUnixTime()
    {
        DateTime date = System.DateTime.UtcNow;
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        return Convert.ToInt64((date - epoch).TotalSeconds);
    }

    private void Update()
    {
        if (DiscordDisponible)
        {
            discord.RunCallbacks();
        }
    }
}

