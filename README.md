# COI: Extended Mods - Feature Summary  
   
## Patch Notes for Version 104 
- **Cheats Mod**:
   - Changed vehicle editor UI to use collapsible panels.
   - Added upgrade window in cheat UI that allows individual upgrades to vehicles.
   - Fixed many cases where saving wasn't working for prototype data or updating correctly.
   - Changed the way the mod determines if you have `No Disease` cheat on or not.
   - Fixed an issue where game speed could become out of sync with speed index.
   - Fixed a regression issue with Infinite Focus, should not work accurately again.
   - Introduce Fast Ore Sorting cheat v1, this option does not yet save.
   - Re-organized UI by adding a new tab and moving things around.  This will be a continual process, so if you don't like it, don't worry, it will change again.  
   - Re-added Free Power and Free Computing to Economy window after re-organization had left them homeless.
   - Re-added monthly unity cheat

-**Tweaks Mod**:
   - Mine Control Tower has a vehicle window that lists all vehicles assigned to it and allows clicking to view them.
   - Mine Control Tower assignment list can prioritize materials or apply to all.
   - Mine Control Tower vehicle list can remove vehicle assignments from specific vehicles.
   - Fixed an issue where Unlimited Designations was checked but not applied on restart.
   - Added Advanced Information Panel to Forestry Tower Window that shows total trees, amount of designations, and maximum wood throughput.  

## Patch Notes for Version 103
- **Cheats Mod**:
   - Fixed Vehicle costs not properly updating after being edited in Vehicle Editor.
   - Fixed God Mode storage icon persists after entity is deconstructed in some cases.
   - Added a Cheat for 'No Construction Costs' that will remove costs from buildings but not instantly build them.  This also does not affect research, unlike the Instant Build cheat which will always affect research.
   - Added support for saving and loading options.

- **Tweaks Mod**:
   - Added support for saving and loading options.
   - Added fuel type icons to Mass Vehicle Upgrader list

## Patch Notes for Version 102b
- **TweaksMod**:
  - Fixed an issue with Vehicle Mass Upgrader

## Patch Notes for Version 102a
- **Cheats Mod**:
  - Added ability to update all existing prototypes in the game with the Vehicle Editor.
  - Made some changes to the vehicle editor UI, it should consist now mostly of fields you would actually want to edit.
  - Added `stop_current_disease` console command.
- **Tweaks Mod**:
  - Added Vehicle Mass Upgrader.
  
## Patch Notes for Version 102
- **Cheats Mod**:
  - Changed "Maintenance Enabled" to "No Maintenance" to match the rest of the cheats.
  - Fixed "No Disease" cheat was only affecting `DiseaseEffectsMultiplier`
  - Fixed "No Disease" cheat was making diseases 100% worse instead of 100% better
  - Added console command `inflict_permanent_disease` that does what you think it does.
  - Creative Mode now only allows one activation and tooltip has been updated to reflect what it does.

## Patch Notes for Version 101a
- **Cheats Mod**:
  - Award all technology and resources when using "Visit All Locations" or "Defeat All Enemies" cheat for World Map.
  - Fixed CTRL-G keybind to not pull up the Research menu.
  - Fixed a Map Editor crash.

## Patch Notes for Version 101
- **Cheats Mod**:
  - Added Asteroid Spawner to Cheats.
  - Added Initial Vehicle Editor (WIP) to Cheats Mod.
  - Added CTRL-G for God Mode shortcut.  (You have to un-map research from G)
- **Tweaks Mod**:
  - Updated to work with Build 411.
- **General**:
  - Improved UI validation and minor bug fixes.

## Overview
The **COI: Extended** mod suite enhances *Captain of Industry* with tools for customization, cheats, and quality-of-life improvements. Below is a concise summary of what each mod does.

## Mod Features

### Cheats Mod
- **Gameplay**: Instant building/research, instant cargo ships, no disease effects, infinite focus points, no pollution penalties (air, water, ship, vehicle, train), no needs (food, electricity, water, wastewater, computing, waste, biowaste).
- **Research**: Finish current/all research instantly, free research labs.
- **Maintenance**: Fill depot buffers, toggle maintenance on/off.
- **Resources**: Adjust population, unity, unity cap, focus multiplier, free electricity/computing/unity, unlimited water/oil, fill food markets/animal farms, spawn custom asteroids (select materials, radius, ratios).
- **Ships**: No vehicle fuel use, adjust vehicle/ship limits and capacity, instant ship unload/repair/navigation, reveal/scan/visit world map locations, add products to shipyard/storage.
- **Terrain/Environment**: Instant mining/dumping, ignore forestry towers, disable terrain physics, plant/remove trees with custom spacing, control weather (lock, sun/rain intensity, toggle rendering).
- **Vehicles**: Edit vehicle prototypes (WIP, may break saves), god mode for storage (always full/empty).
- **Note**: Use `CTRL-G` to toggle god mode for storage, `F8` to open the Cheats menu.

### Tweaks Mod
- **Camera**: Free movement through terrain/objects.
- **Designations**: Unlimited size for terrain and tower designations.
- **Visuals**: Disable clouds, fog, or weather rendering.
- **Vehicles**: Mass Vehicle Upgrader.
- **Note**: Use `F9` to open the Tweaks menu.

### StoragePlus Mod
- **Storage Control**: Adjust capacities by custom percentages, input exact values, set custom storage sizes.

### ItemSink Mod
- **Product Management**: Universal sink (absorbs unlimited products), universal source (spawns unlimited products).

### Difficulty Settings Mod
- **Difficulty**: Adjust settings from -90% to +500% for more flexibility.

### Solar Extended Mod
- **Solar Panels**: Quarter-size variants for all solar panel types.

## Usage
- **Cheats**: Access via Cheats Window (Gameplay, Resources, Ships, Terrain, Vehicles tabs) with `F8`.
- **Tweaks**: Use Tweaks Window for toggles with `F9`.
- **StoragePlus**: Adjust storage via new UI controls.
- **ItemSink**: Place sinks/sources in-game.
- **Difficulty**: Modify sliders in settings.
- **Solar**: Find quarter-size panels in build menu.

## Notes
- **Vehicle Editor**: Experimental, back up saves before use.

# Install 
## How do I install the mod?
- **Download** latest release `COIExtendedv102b.zip `on the Releases page at https://coie.keranik.com/releases
- Open **Windows Explorer** and paste to it `%APPDATA%\Captain of Industry\Mods`
- Open **COIExtendedv1xx.zip** and extract/copy desired mods to **Mods** directory.
- Open game and **Enable Mods** in the **Settings** menu, then restart game.

## How do I add the mod to a saved game?
- **After** properly installing mods, start the game and choose **Load** at the main menu.
- **Select** the save file that you wish to play but do not press **Load**
- Notice the **Wrench** icon in the bottom right of the panel, click it.
- On this screen you may now enable or disable mods for your save file.
- Some mods may or may not be compatible with your save file.

# Reporting Issues
## How do I report an issue?
- Click **Issues* at the top of the page, and choose a title that summarizes your issue.
- Look for your latest game log file at `%APPDATA%\Captain of Industry\Logs` and attach it to the report.
- If possible, find the saved game file you are having trouble with in `%APPDATA%\Captain of Industry\Saves` and attach it to the report.
- In the body of your report, be as descriptive as possible with your issue and include the following:
-- What behavior are you experiencing?
-- What behavior do you expect to happen?
-- How can one reproduce the behavior you are experiencing?
- Ensuring you follow these simple steps will help you and the mod author fix the problem as soon as possible.

## Thank you for your interest in Captain of Industry: Extended
# Support the mod on Patreon at patreon.com/keranik
# No Patreon? Donate directly at paypal.me/undiscoveredent

