# Godot C# with Facepunch.Steamworks implementation.

Build to use as a C# Godot Project foundation with minimal effort and start using steamworks.

**Setup**: Everything is bare minimum so you will need to make some changes:

1. Replace files in `steamworks/bin` with the latest [Facepunch.Steamworks](https://github.com/Facepunch/Facepunch.Steamworks) dlls.
2. Replace files in `steamworks/sdk` with the latest [Steamworks SDK](https://partner.steamgames.com/doc/gettingstarted) dlls.
3. Update GameAppId in `steamworks/src/SteamService.cs` with your own steam GameAppId.

**Vscode**: To compile and run in `vscode`, make sure "program" is set to your `godot.exe` in `.vscode/launch.json`.

**Export**: Exporting using the provided `vscode tasks`: `Export Windows` or `Export Linux` or `Export All` by pressing `Ctrl+P` and enter `>Task: Run Task` and select.

**Export Windows** exports using the `export_presets`'s Windows Desktop preset and outputs into `.build/Win64` folder and copies `steam_api.dll` and `steam_api64.dll` into the folder. If you want to export from the Godot Editor, you **MUST** manually copy these dlls from the `steamworks/sdk` folder to the build output folder next to the `exe` file.

**Export Linux** export using the `export_presets`'s Linux preset and outputs into `.build/Linux` folder and copies `libsteam_api.so` into the folder. If you want to export from the Godot Editor, you **MUST** manually copy this from the `steamworks/sdk` folder to the build output folder next to the `x86_64` file.

**Export All** performs both Export Windows & Export Linux tasks.