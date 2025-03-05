using Godot;
using Steamworks;
using System;

public partial class Test : Node {
    public override void _Ready() {
        GD.Print($"SteamId: {SteamClient.SteamId}, SteamName: {SteamClient.Name}");
    }
}
