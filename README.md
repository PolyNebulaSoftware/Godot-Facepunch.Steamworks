# Godot C# with Facepunch.Steamworks implementation.

Build to use as a C# Godot Project foundation with minimal effort and start using steamworks.

**Setup**: Everything is bare minimum so you will need to make some changes:

1. Replace files in `steamworks/bin` with the latest [Facepunch.Steamworks](https://github.com/Facepunch/Facepunch.Steamworks) dlls.
2. Replace files in `steamworks/sdk` with the latest [Steamworks SDK](https://partner.steamgames.com/doc/gettingstarted) dlls.
3. Update the `steamworks/steam_appid.txt` with your own steam GameAppId.

**Vscode**: To compile and run in `vscode`, make sure "program" is set to your `godot.exe` in `.vscode/launch.json`.

**Export**: 
- Exporting using the provided `vscode tasks`: `Export Windows` or `Export Linux` or `Export All` by pressing `Ctrl+P` and enter `>Task: Run Task` and select.
- Export using the Godot Editor.

- **Export Windows** exports using the `export_presets`'s Windows Desktop preset and outputs into `.build/Win64` folder.
	- The csproj copies `steam_api.dll` and `steam_api64.dll` to the export data folder.

- **Export Linux** exports using the `export_presets`'s Linux preset and outputs into `.build/Linux` folder.
	- The csproj copies `libsteam_api.so` to the export data folder.

- **steam_appid.txt** in the export directory is required for dlls to load correctly. It is also in `export_preset.cfg`'s `include_filter` setting to export non-resource file otherwise FileAccess returns null in `SteamService.cs`.

- If `.build/Win64` or `.build/Linux` directories does not exit, create them manually or use task `makedir-builddir`
	

**Export All**: performs both Export Windows & Export Linux vscode tasks.

**Steamdeck**: After exporting for linux, if you have ssh enabled and scp for steamdeck. The vscode task `copy-build-steamdeck` will copy the `.build/Linux` directory into the steamdeck. Adjust the target directory in task's command string.
