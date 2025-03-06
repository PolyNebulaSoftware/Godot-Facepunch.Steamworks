using Godot;
using Steamworks;
using System;

public partial class Test : Node {

    void ClientRunDemo() {
        // Print Client Info
        GD.Print($"SteamId: {SteamClient.SteamId}, SteamName: {SteamClient.Name}");

        // Get Friends
        foreach (Friend friend in SteamFriends.GetFriends()) {
            GD.Print($"{friend.Id}: {friend.Name} [{(friend.IsOnline ? "Online" : "Offline")}]");
        }
    }

    void ServerRunDemo() {
        GD.Print($"IP: {SteamServer.PublicIp}, SteamId: {SteamServer.SteamId}, HasLicense:{SteamServer.UserHasLicenseForApp(SteamServer.SteamId, SteamService.GameAppId)}");
    }

    public override void _Ready() {
        if (SteamService.IsHeadless()) {
            ServerRunDemo();
        } else {
            ClientRunDemo();
        }
    }
}
