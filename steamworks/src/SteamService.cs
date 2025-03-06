using Godot;
using Steamworks;
using System;

public partial class SteamService : Node {
    public static bool IsOnline { get; private set; } = false;
    public readonly string GameDescription = "Godot Game on Steam";
    public static uint GameAppId { get; private set; } = 0;

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
            IsOnline = false;
            GD.PushWarning($"Failed to connect to steam: {e.Message}");
            OS.Alert($"Failed to connect to steam: {e.Message}", "Steam Error");

            GetTree().Quit();
        }

        return IsOnline;
    }

    public bool ConnectAsServer() {
        if (SteamClient.IsValid) return IsOnline;
        
        string dirName = System.IO.Path.GetDirectoryName(OS.GetExecutablePath());

        var serverInit = new SteamServerInit(dirName, GameDescription) {
            GamePort = 28100,
            Secure = true,
            QueryPort = 28101
        };

        try {
            SteamServer.Init(GameAppId, serverInit);

            if (!SteamServer.IsValid) {
                IsOnline = false;
                return IsOnline;
            }

        } catch (Exception e) {
            // Couldn't init for some reason (dll errors, blocked ports)
            IsOnline = false;
            GD.PushWarning($"Failed to connect to steam: {e.Message}");
            OS.Alert($"Failed to connect to steam: {e.Message}", "Steam Error");

            GetTree().Quit();
        }

        return IsOnline;
    }

    public static bool IsHeadless() { // true = Rendering and window disabled, generally for server builds
        return DisplayServer.GetName() == "headless";
    }

    public override void _Ready() {
        var appIdTxt = FileAccess.Open("res://steamworks/steam_appid.txt", FileAccess.ModeFlags.Read);
        if (!uint.TryParse(appIdTxt.GetLine().Trim(), out uint gameAppId)) {
            GD.PushWarning($"Failed to read steam_appid: {appIdTxt}.");
            return;
        }
        GameAppId = gameAppId;
        
        
        //Create steam_appid.txt for dlls
        string currentDir = System.IO.Directory.GetCurrentDirectory();
        string filePath = System.IO.Path.Combine(currentDir, "steam_appid.txt");

        if (!System.IO.File.Exists(filePath)) {
            System.IO.File.WriteAllText(filePath, gameAppId.ToString());
        }
        

        if (IsHeadless()) {
            //Running as Dedicated Server
            ConnectAsServer();
        } else {
            //Running as Steam Client
            ConnectAsClient();
        }
    }

    public override void _Process(double delta) {
        if (IsHeadless()) {
            SteamServer.RunCallbacks();
        } else {
            SteamClient.RunCallbacks();
        }
    }

    public override void _ExitTree() {
        if (IsHeadless()) {
            SteamClient.Shutdown();
        } else {
            SteamServer.Shutdown();
        }
    }
}
