using Godot;
using Steamworks;
using System;

public partial class SteamService : Node {
    public bool IsOnline = false;
    public readonly string GameDescription = "Godot Game on Steam";
    static readonly uint GameAppId = 2182890;

    public bool ConnectAsClient() {
        if (SteamClient.IsValid) return IsOnline;

        try {
            SteamClient.Init(GameAppId, true);

            if (!SteamClient.IsValid) {
                IsOnline = false;
                return IsOnline;
            }

        } catch (Exception e) {
            // Couldn't init for some reason (steam is closed etc)
            GD.PushWarning($"Failed to connect to steam: {e.Message}");
            OS.Alert("Steam is required to run this.", "Steam Required");

            GetTree().Quit();
        }

        return IsOnline;
    }

    public bool ConnectAsServer() {
        if (SteamClient.IsValid) return IsOnline;
        
        string dirName = System.IO.Path.GetDirectoryName(OS.GetExecutablePath());

        var serverInit = new SteamServerInit(dirName, GameDescription) {
            GamePort = 28015,
            Secure = true,
            QueryPort = 28016
        };

        try {
            SteamServer.Init(4000, serverInit);

        } catch ( Exception ) {
            // Couldn't init for some reason (dll errors, blocked ports)

        }

        return serverInit.DedicatedServer;
    }

    public override void _Ready() {
        if (DisplayServer.GetName() == "headless") {
            //Running as Dedicated Server
            ConnectAsServer();
        } else {
            //Running as Steam Client
            ConnectAsClient();
        }
    }

    public override void _Process(double delta) {
        if (DisplayServer.GetName() == "headless") {
            SteamServer.RunCallbacks();
        } else {
            SteamClient.RunCallbacks();
        }
    }

    public override void _ExitTree() {
        SteamClient.Shutdown();
    }
}
