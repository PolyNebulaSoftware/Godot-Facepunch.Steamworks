# Godot C# Implementation of Facepunch's C# Steamworks implementation.

Build to use as a C# Godot Project foundation with minimal effort and start using steamworks.

**Setup**: Everything is bare minimum so you will need to make some changes:

1. Replace files in `steamworks/bin` with the latest [Facepunch.Steamworks](https://github.com/Facepunch/Facepunch.Steamworks) dlls.
2. Replace files in `steamworks/sdk` with the latest [Steamworks SDK](https://partner.steamgames.com/doc/gettingstarted) dlls.
3. Update GameAppId in `SteamService.cs` with your own steam GameAppId.