<div align="center">

# 🏭 COIExtended Mod Family

**A comprehensive suite of modular mods for Captain of Industry**

[![Captain of Industry](https://img.shields.io/badge/Captain%20of%20Industry-Mod-blue)](https://www.captain-of-industry.com/)
[![License](https://img.shields.io/badge/License-All%20Rights%20Reserved-red)](#license)

[📥 Installation](#-installation) • [🐛 Report Bug](../../issues) • [💡 Request Feature](../../issues) • [💬 Discussions](../../discussions)

---

</div>

## 📦 Available Mod Packs

COIExtended is a family of modular Captain of Industry mods, ranging from a full late‑game overhaul to focused utility, difficulty, and QoL packs.  **You can mix and match most of them in a single save.**

| Mod Pack | Description |
|----------|-------------|
| **COIExtended.Core** | Main gameplay overhaul with extensive late-game content |
| **COIExtended.Automation** | Smart Balancer / Smart Zipper only |
| **COIExtended.Cheats** | Full cheat / god‑mode toolkit |
| **COIExtended.Difficulty** | Extended difficulty sliders (rebuilds vanilla difficulty menu) |
| **COIExtended.ItemSink** | Dev‑style resource spawner and sink |
| **COIExtended.Sanitizer** | Save config sanitizer for "stuck" configs |
| **COIExtended.StoragePlus** | Storage & buffer overhaul with persistent settings |
| **COIExtended.Tweaks** | UI / gameplay tweaks, free camera, forestry tools, and visual toggles |

> **⚠️ Legacy Notice:** Legacy overhaul modlets are no longer part of this lineup. Legacy users should use the Steam beta branch **`b481`** with their existing mods.

---

## 📥 Installation

### Requirements
- Captain of Industry (latest version recommended)
- Basic file management knowledge

### Installation Steps

1. **Download the mod(s)** you want from the [Releases](../../releases) page
2. **Locate your game's mod folder:**
   ```
   %APPDATA%\Captain of Industry\Mods
   ```
   Or navigate manually:
   - Open File Explorer
   - Type `%appdata%` in the address bar and press Enter
   - Navigate to: `%APPDATA%\Captain of Industry\Mods`
   - If the `Mods` folder doesn't exist, create it

3. **Extract the mod folder(s)** into the `Mods` directory
   - Each mod should be in its own subfolder (e.g., `Mods\COIExtended.Core\`)

4. **Launch the game** and verify the mod appears in the Mods menu

### Load Order & Compatibility

- Most COIExtended mods can be used together
- **COIExtended.Core** should be loaded if you want the full overhaul experience
- Individual utility mods (Automation, Tweaks, StoragePlus, etc.) work standalone
- **COIExtended. Sanitizer** is only needed for troubleshooting stuck configs

---

## 📚 Mod Documentation

<details>
<summary><h3>🏭 COIExtended.Core – Main Overhaul</h3></summary>

The Core pack merges and supersedes the old "Logistics/World/Solar/Structures Extended" modlets and adds substantial late‑game content. 

<details>
<summary><h4>⚡ Late‑Game Progression</h4></summary>

#### Lithium & Oceanic Materials
- Lithium extraction from seawater via heated ponds and thermal desalinators
- New brine types and chemistry:
  - Lithium‑rich, concentrated, and spent brines
  - Ion extraction and electrodialysis → `Lithium chloride` and `Salt mix (impure)`
  - Chlor‑alkali regeneration closes brine loops and produces `Hydrogen chloride`
- Carbonate chemistry:
  - Soda ash (Solvay), caustic soda, impure salt electrolysis, fertilizer from impure salt
  - Alkaline wastewater treatment

#### Batteries & Silicon Recovery
- **Battery Chemistry:**
  - NMC and High‑Ni cathodes
  - Li‑ion battery packs built in robotic assemblers
  - Dedicated **Electrode Coater** for cathode coating
- **Silicon Recovery:**
  - Final‑stage chip recycling (normal & lithium‑doped)
  - Mono‑Si solar cells with lithium‑enhanced wafers
  - Silicon wafer recycling → polysilicon

#### Isotopes, Heavy Water & Deuterium
- Lithium isotope enrichment in the **Uranium Enrichment Plant**
- Heavy water fractionation and deuterium extraction in **Electrolyzer T2**
- Argon production via Air Separator (air separation + argon distillation)

#### Fusion Power & Helium
- **D‑T fuel cycle:**
  - Heavy water + enriched lithium → `Tritium` and `Deuterium`
  - DT fuel rods + reprocessing loop to recover isotopes and slag
- **Fusion reactor:**
  - Feeds **Multistage Turbine** and **Power Generator T3** for GW‑scale power
- **Helium chain:**
  - Helium air separation, distillation, and helium cooling (liquid helium as coolant)

</details>

<details>
<summary><h4>🛰️ Orbital Satellites & Beamed Power/Computing</h4></summary>

#### Satellites & Assembly
- Solar satellites (standard & HighNi), relay satellites, comms satellites, supercomputer satellites
- Satellite thrusters and hardware assembled in the **Satellite Assembler**
- Rocket III (synfuel‑only) as late‑game launch vehicle

#### Network Simulation
- Global **SatellitesManager:**
  - Tracks stored vs deployed satellites per type
  - Computes total power and computing output, scaled by network health and type multipliers
  - Allocates output to ground receivers by **priority**, with proportional sharing under contention
  - Simulates failures and degradation; relay satellites can regenerate health if over‑provided
  - Performs probabilistic **alignment** checks to hook receivers into the network

#### Ground Infrastructure
- **Satellite Control Center:**
  - Per‑type target counts and auto‑deployment from orbit storage
  - Claims existing deployed satellites on construction
- **Satellite Receivers:**
  - Mode‑switchable by satellite type
  - Act as generators (power or computing) and consumers (power or computing) depending on mode
  - Respect maintenance, workers, electricity, and computing requirements

#### Discoveries
- **Comms satellites:**
  - Build discovery progress over time, gated by network health
  - Generate "pending discoveries" that world systems can consume to reveal new locations

</details>

<details>
<summary><h4>⛽ Synfuel & Advanced Logistics</h4></summary>

#### Synfuel Chain
- Synfuel (liquid and gas) products
- Synfuel Refinery, Synfuel Generator, HP Gas Boiler
- Expanded oil chain:
  - Heavy/medium/light oil cracking
  - T2 distillation and enhanced sour‑water handling

#### Vehicles & Fuel
- Large trucks and large excavators with support for diesel, hydrogen, and synfuel
- Synfuel variants of mega vehicles, excavators, tree planters, harvesters
- Synfuel locomotives and synfuel cargo ships
- Dedicated research nodes for synfuel vehicles, cargo docks, and generators

#### Fuel Stations & Depots
- Fuel Station IV with diesel, hydrogen, and synfuel variants
- New T4 cargo depot modules for unit, loose, and fluid with proper colliders and icons

</details>

<details>
<summary><h4>🌍 World, Mines, Fracking & Events</h4></summary>

#### Mines & Variable Deposits
- Dedicated mines: copper, iron, gold, titanium
- **VariableMineEntity:**
  - When spawned, each mine rolls 1–3 possible products from its `AvailableProducts` list (deterministic per site)
  - Player chooses which of those products to extract via UI; buffer and output update accordingly
- Some advanced materials (e.g.  `Manganese flakes`, `Cobalt powder`) are not directly mineable:
  - Obtained via **trade** or variable mines that rolled them

#### Fracking Wells
- Water‑in, gas + wastewater‑out wells that mirror real fracking behavior
- Tied into extended water treatment and waste/brine loops

#### World & Events
- More flexible world‑gen caps:
  - Higher max map dimensions, sectors, locations, and connection distances
- Event tuning:
  - Loot vs "nothing found" odds, pirate behavior, enemy spawn scaling per tier
  - Radioactive loot removed by default (toggleable elsewhere)

</details>

<details>
<summary><h4>🎣 Food, Fishing & Canning</h4></summary>

#### Fishing & Fish Farms
- Fishing Docks I/II/III with proper models and logistics ports
- Fish Farms with:
  - Configurable bait types
  - Adjusted catch rates, bait usage, and buffer sizes
  - Fixes for bait buffer/logistics issues on type changes

#### Fish Chain
- Fish species: sardines, anchovies, mackerel, cod, tuna, swordfish
- Byproducts: fish oil (fluid), fish scales (loose), raw fish

#### Canning
- Canned foods:
  - Empty cans, canned fish, canned fruit/vegetables, canned food packs
- Balanced recipes and value across the chain
- Contracts and quick trades for canned goods

#### Side Uses
- Fish scales → organic fertilizer and burnable fuel
- Fish oil → antibiotics and waste‑dump recipes

</details>

<details>
<summary><h4>⚙️ Maintenance, Overclocking & UI</h4></summary>

#### Overclocking (10–300%)
- Overclock/underclock machines with:
  - Scaled workers, maintenance, electricity, and computing
- Copy/paste overclock settings
- Overclocking tech node moved under Maintenance Depot II

#### Maintenance Depot IV
- T4 Maintenance Depot that can produce **all previous maintenance types**
- Greatly simplifies late‑game maintenance

#### UI & Research
- Revised research tree:
  - Drydock vs Cargo Ship dependencies
  - Ship armor/weapons placement
  - Biofuel and overclocking nodes repositioned
- Numerous bugfixes and tooltip / icon improvements

</details>

<details>
<summary><h4>⚙️ Built-In Mod Settings</h4></summary>

Core ships with a persistent, versioned **Mod Settings DB** that exposes configurable options in the in‑game Settings menu:

#### Storage & Logistics
- Allow all product storage (per‑storage "allow all" baseline)
- Remove storage throughput limit
- Vehicle cargo pickup duration (Instant → Slowest)

#### Gameplay
- Liquid dump recipe multiplier
- Food market capacity multiplier
- Shipyard cargo capacity

#### Transports – Throughput per Tier & Type
- Molten channels T1–T2
- Flat belts T1–T4
- Loose belts T1–T4
- Fluid transports T1–T4

#### Transports – Cost Multipliers
- Transport maintenance multiplier (stacked on top of COIExtended's base ~50% reduction)
- Transport electricity and construction multipliers

#### Trains
- Base train capacity multiplier
- Reduced train build duration toggle
- Added train wagon capacity multiplier (additive)
- Added train station capacity multiplier (additive)

Settings are versioned (`CURRENT_SAVE_VERSION = 120`) and serialized per save.

</details>

</details>

<details>
<summary><h3>🔀 COIExtended.Automation – Smart Balancer / Smart Zipper</h3></summary>

Contains only the **Smart Zipper** (Smart Balancer):

- **8‑port, power‑aware splitter/merger:**
  - Per‑port **priority levels**, **product filters**, and **throughput caps**
  - Per‑port "bucket" accounting to enforce soft rate limits over time
- **Even distribution modes:**
  - Force even on inputs or outputs, overriding priorities
- Dynamic buffer sizing and delay based on effective throughput and connections
- Fully cloneable config and safe serialization

</details>

<details>
<summary><h3>🎨 COIExtended.Tweaks – UI & Gameplay Tweaks</h3></summary>

Tweaks provides non‑cheat quality‑of‑life and visualization improvements. 

<details>
<summary><h4>📷 Camera & Area Limits</h4></summary>

#### Free Camera
- Unlocks an unconstrained camera:
  - No terrain clipping
  - Infinite zoom and free movement

#### Unlimited Designations
- Removes size caps on:
  - Mining and dumping areas
  - Surface designations
  - Forestry harvest zones

#### Unlimited Tower Area
- Removes default area limits for towers (e.g. forestry towers)

</details>

<details>
<summary><h4>🎭 Rendering & Visuals</h4></summary>

#### Disable Clouds / Fog / Weather
- Independently toggle:
  - Cloud rendering
  - Fog rendering
  - Weather effect visibility (visual only; simulation remains)

#### Disable Emissions
- Globally disables emissions/particle systems on all relevant prototypes
- Due to engine limitations, requires a full save/load to fully enable or disable

</details>

<details>
<summary><h4>🚜 Forestry & Vehicle QoL</h4></summary>

#### Forestry Tower Enhancements
- Forestry towers gain:
  - A **total throughput view** for their managed area
  - Direct control over **excavator mining priorities** for that area, configured from the tower UI

#### Vehicle Replacer / Mass Upgrade
- Vehicle Replacer column:
  - Mass‑upgrade tools for trucks, excavators, tree harvesters, and tree planters
  - Pick current type, target type, and quantity (or all) to upgrade

</details>

<details>
<summary><h4>🔧 Other Features</h4></summary>

#### Unlimited Demolish
- Always allow demolition of static entities (`AlwaysAllowBulldoze`)

#### Fix Stuck Trucks
- Tweaks cargo‑stuck edge cases for trucks that pick up dump material

</details>

Tweaks integrates cleanly with both Core and mostly vanilla games. 

</details>

<details>
<summary><h3>📦 COIExtended.StoragePlus – Storage & Buffer Overhaul</h3></summary>

StoragePlus replaces the vanilla storage inspector with a persistent, configurable panel. 

#### Capacity & Defaults
- **Per‑storage capacity**
  - Change capacity via text field; updates underlying buffer capacity
  - If new capacity is lower than current contents, excess is pushed out through the logistics buffer (not destroyed)
- **Make Default**
  - Writes the current capacity back to the storage **prototype** and to StoragePlus config
  - Future storages of that type spawn with your chosen capacity

#### "Allow All" Product Types
- `Allow All` toggle:
  - For non‑nuclear storages: Allows any compatible product of the same **product type**
  - Works with enhanced product picker for products beyond vanilla `StorableProducts`

#### Import/Export & I/O Controls
- Full set of synchronized sliders + numeric fields:
  - `Import` (truck import until %)
  - `Export` (truck export from %)
  - `Transport From` & `Transport Until` (belt/pipe throughput thresholds)
- Drag sliders OR type exact percentages
- Logic keeps import/export mutually sensible

#### Persistence
- StoragePlus config stores per‑prototype defaults and per‑instance behavior
- Your choices persist across save/load

</details>

<details>
<summary><h3>🎮 COIExtended. Cheats – Cheat / God‑Mode Toolkit</h3></summary>

Cheats adds a multi‑tab cheat UI backed by `CheatFlags` and deep integration with the simulation. 

<details>
<summary><h4>🏘️ Settlement & Needs</h4></summary>

- Direct numeric edits: Total **population**, **Unity**, and **Unity cap**
- Needs toggles: `No Food Need`, `No Electricity Need`, `No Clean Water Need`, `No Computing Need`
- `No Disease Effects`: Sets disease effect/mortality multipliers to zero
- One‑click actions:
  - `Fill Food Markets` – maxes configured food buffers
  - `Fill Animal Farms` – adds animals and fills feed/water

</details>

<details>
<summary><h4>💰 Economy & Progression</h4></summary>

- **Build & research:** `Instant Build/Research`, `No Construction Costs`
- **Free resource generation:** Power, Computing, Unity (per tick/month)
- **Maintenance:** `No Maintenance` toggle, `Fill All Depots`
- **Research:** `Free Research`, `Finish Current/All/Repeatable Research`
- **Focus system:** `Infinite Focus`, `Focus Multi` percentage adjustment
- **Gameplay:** `Instant Delete Storages`

</details>

<details>
<summary><h4>🌍 Environment & Resources</h4></summary>

- **Pollution & waste:** Toggle all pollution types and waste generation
- **Virtual reserves:** `Unlimited Water`, `Unlimited Oil`
- **Asteroid spawning:** Choose materials, mix ratio, radius, and spawn

</details>

<details>
<summary><h4>⛰️ Terrain & Weather</h4></summary>

- **Terrain operations:** Process/clear designations, change terrain material, instant surfaces
- **Tree tools:** Instant plant/remove with spacing control
- **Weather:** Lock weather, adjust sun/rain intensity, toggle rendering

</details>

<details>
<summary><h4>🚢 Exploration & Logistics</h4></summary>

- **Ships & fuel:** Instant cargo ships, disable fuel consumption
- **Throughput & capacity:** Fast ore sorting, train/vehicle capacity adjustments
- **World map:** Reveal/scan/visit all locations, defeat all enemies
- **Shipyard product adder:** Add arbitrary products to shipyard or world storages

</details>

<details>
<summary><h4>🚗 Vehicles & Vehicle Editor</h4></summary>

- Scans all `DrivingEntityProto` and lists them
- Each entry has an `Edit` button opening the **Vehicle Editor**
- Allows deep tuning (capacity, speed, costs, etc.)

</details>

<details>
<summary><h4>✨ Creative Mode</h4></summary>

One‑click macro combining:
- 9999 Unity cap & Unity; 9999 monthly Unity
- 9999 MW and 9999 TFlops free per month
- Explore world map; unlock all technology
- Raise vehicle cap; fill food markets
- Enable Instant Build; disable maintenance
- And much more... 

</details>

</details>

<details>
<summary><h3>⚙️ COIExtended.Difficulty – Extended Difficulty Sliders</h3></summary>

Rebuilds the vanilla **GameDifficultyConfig** ranges using reflection, drastically expanding slider options.

- Each difficulty setting gets **much wider ranges**, typically from **–90% up to +500%**, with fine‑grained steps
- Affected settings include:
  - Extra starting material
  - Maintenance cost difficulty
  - Fuel consumption
  - Rain yield and farm yield
  - Base health
  - Resource mining rates
  - Settlement consumption and food consumption
  - World mine reserves
  - Unity production
  - Construction costs
  - Trees growth
  - Extra contracts profit
  - Research cost
  - Quick actions cost
  - Disease mortality
  - Solar power efficiency
  - Pollution impact

Safe to run with vanilla or Core. 

</details>

<details>
<summary><h3>🔧 COIExtended.ItemSink – Dev Spawner & Sink</h3></summary>

- Adds a universal **resource spawner** (source entity)
- Adds a universal **sink** (void entity)
- Useful for:
  - Testing recipes and chains
  - Building blueprints
  - Emergency cleanup in broken saves

</details>

<details>
<summary><h3>🧹 COIExtended.Sanitizer – Save Config Sanitizer</h3></summary>

> **⚠️ Critical Warning:** This is **not** a generic "remove any mod and keep playing" tool. It is only for cleaning **dead config references** from saves.

#### Console Command
```
sanitize_configs
```

#### Behavior
- Reflects into the game's internal dependency resolver and `GameSaver` to:
  - Remove mod/config instances for mods that only leave configs (e.g. `COIExtended.Cheats`, `Tweaks`, `StoragePlus`, etc.)
- Produces a save that no longer hard‑references those configs

#### Does NOT Remove
- Custom buildings, products, recipes, entities, or world changes
- If you remove a mod that added content the save still uses, the save may still crash

**Always back up your saves before running `sanitize_configs`.**

</details>

---

## 🐛 Support & Issues

### Reporting Bugs

Found a bug? Please help improve COIExtended by [opening an issue](../../issues/new)!

**When reporting, please include:**
- Which COIExtended mod(s) you're using
- Captain of Industry version
- Steps to reproduce the issue
- Screenshots or logs if applicable
- Your save file (if relevant and comfortable sharing)

### Feature Requests

Have an idea for a new feature? [Open a feature request](../../issues/new) and describe:
- What you'd like to see added
- Why it would be useful
- Any implementation ideas (optional)

### Discussions

For general questions, tips, or sharing your creations, visit the [Discussions](../../discussions) section! 

---

## 📄 License

**Copyright © Keranik.  All Rights Reserved.**

This mod is **not open source**. It is provided in accordance with the [MaFi Games Modding Policy](https://www.captain-of-industry.com/modding). 

**You may:**
- Download and use this mod for personal gameplay
- Share links to this repository
- Create derivative works for personal use

**You may NOT:**
- Redistribute this mod or any part of it on other platforms
- Use this code in commercial projects
- Claim this work as your own
- Modify and redistribute without explicit permission

For permissions beyond this scope, please [open an issue](../../issues) to discuss. 

---

<div align="center">

**Enjoy building your industrial empire!  🏭**

Made with ⚙️ for Captain of Industry

[⬆ Back to Top](#-coiextended-mod-family)

</div>
