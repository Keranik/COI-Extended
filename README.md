# 🏭 COI:Extended Mod Suite

A comprehensive, modular family of mods for **Captain of Industry**

[![Captain of Industry](https://img.shields.io/badge/Captain%20of%20Industry-Mod-blue)](https://www.captain-of-industry.com/)
![Game Version](https://img.shields.io/badge/Game%20Version-0.8.2c%20b559-green)
![Update](https://img.shields.io/badge/Update%204-Compatible-brightgreen)
![License](https://img.shields.io/badge/License-All%20Rights%20Reserved-red)

[📥 Download Latest Release](https://github.com/Keranik/COI-Extended/releases/latest) · [🐛 Report Bug](https://github.com/Keranik/COI-Extended/issues/new) · [💡 Request Feature](https://github.com/Keranik/COI-Extended/issues/new) · [💬 Discussions](https://github.com/Keranik/COI-Extended/discussions) · [💬 Discord](https://discord.gg/JmVjxdwX)

---

## 📦 Mod Overview

COI:Extended is a family of modular mods you can mix and match in a single save. Pick the full late‑game overhaul, focused utilities, or anything in between.

| Mod | Description | Requires Common? |
|-----|-------------|:---:|
| **COIExtended‑Common** | Shared library: Harmony, mod registry, compact number formatter | — |
| **COIExtended‑Core** | Main gameplay overhaul — late‑game content, fusion, satellites, vehicles, recipes | ✅ |
| **COIExtended‑Automation** | Smart Balancers/Zippers + **ENet circuit automation** (wires, combinators, wireless) | ✅ |
| **COIExtended‑Cheats** | Full cheat / god‑mode toolkit with multi‑tab UI | ✅ |
| **COIExtended‑Tweaks** | QoL tweaks — free camera, efficiency overlay, line placement, forestry tools | ✅ |
| **COIExtended‑StoragePlus** | Storage & buffer overhaul with persistent per‑prototype defaults | ✅ |
| **COIExtended‑Difficulty** | Drastically expanded difficulty sliders (–90 % to +500 %) | No |
| **COIExtended‑RecipeMaker** | Custom recipe creation tool (main‑menu button) | No |
| **COIExtended‑VehicleMaker** | Vehicle cloning & customization *(experimental)* | ✅ |
| **COIExtended‑Sanitizer** | Console tool to clean dead mod configs from saves | No |
| **COIExtended‑ItemSink** | Dev spawner & sink — *effectively deprecated; use the built‑in Sandbox equivalents* | No |

> **Note:** As of v116+ all mod directory names use the **hyphen** format (e.g. `COIExtended-Core`). Delete any old `COIExtended.XYZ` folders before installing.

---

## 📥 Installation

### Requirements

- **Captain of Industry** Update 4 (v0.8.2+) — legally obtained copy
- **COIExtended‑Common** must be installed alongside any mod marked ✅ above

### Steps

1. Download the mod(s) you want from the [Releases](https://github.com/Keranik/COI-Extended/releases) page.
2. Locate your mods folder:
```
   %APPDATA%\Captain of Industry\Mods
```
If the `Mods` folder doesn't exist, create it.
3. Extract each mod into its own subfolder (e.g. `Mods\COIExtended-Core\`).
4. Launch the game → **Mods** menu → enable the mod(s) → restart or continue as prompted.

### Load Order & Compatibility

- **COIExtended‑Common** loads first automatically (dependency system).
- Most mods work standalone or together; **Core** gives you the full overhaul experience.
- **Sanitizer** is only needed for troubleshooting stuck configs.
- **ItemSink** functionality is now available natively via Sandbox mode or through the Cheats mod.

---

## 📚 Feature Guide

### 🔧 COIExtended‑Common *(Required Shared Library)*

- **SharedHarmony** — single copy of 0Harmony.dll eliminates version conflicts.
- **Mod Registry** — auto‑detects installed COIE mods so cross‑mod features activate seamlessly.
- **Compact Number Formatter** — shared utility for displaying values like `1.2K`, `3.4M` across UIs.

---

### 🏭 COIExtended‑Core — Main Overhaul

A massive late‑game expansion that supersedes the legacyLogistics / World / Solar / Structures modlets.

<details>
<summary><b>⚗️ Lithium & Oceanic Materials</b></summary>

- Seawater evaporation → brine intensification → lithium extraction
- New fluids: `Brine (lithium)`, `Brine (concentrated)`, `Brine (spent)`, `Lithium eluate`, `Lithium chloride`, `Lithium carbonate`, `Salt mix (impure)`, `Caustic soda`, `Hydrogen chloride`, `Exhaust (clean)`
- Electrodialysis, chlor‑alkali regeneration, and closed‑loop brine recycling
- Soda ash (Solvay process), impure salt electrolysis, fertilizer conversion, alkaline wastewater treatment
</details>

<details>
<summary><b>🔩 Alloys, Nickel & ScrapRecycling</b></summary>

- New materials: Nickel, Manganese, Cobalt, Alnico alloy, Aluminum foil
- Ladle Furnace enhancements — scrap remelting for iron, copper, aluminum, glass, gold
- Manganese steel and Alnico alloying
- *Nickel is directly mineable; Manganese flakes and Cobalt powder come from trade or variable mines*
</details>

<details>
<summary><b>🔋 Batteries & Silicon Recovery</b></summary>

- NMC and High‑Ni cathodes → Li‑ion battery packs
- Dedicated **ElectrodeCoater** building
- Final‑stage microchip recycling (normal & lithium‑doped)
- Mono‑Si solar cells, silicon wafer recycling → polysilicon recovery
</details>

<details>
<summary><b>☢️ Isotopes, HeavyWater & Deuterium</b></summary>

- Lithium isotope enrichment (Uranium Enrichment Plant)
- Heavy water fractionation & deuterium extraction (Electrolyzer T2)
- Argon production via Air Separator
</details>

<details>
<summary><b>⚛️ Fusion Power & Helium</b></summary>

- D‑T fuel cycle: DT fuel rods, tritium, deuterium, reprocessing loop
- **Fusion Reactor**, **Multistage Turbine**, **Power Generator T3** — GW‑scale power
- Helium chain: air separation, distillation, liquid helium coolant
</details>

<details>
<summary><b>🛰️ Orbital Satellites & Beamed Power</b></summary>

- Satellite types: Solar (standard & HighNi), Relay, Comms, Supercomputer
- **Satellite Assembler**, **Satellite Control Center**, **Satellite Receiver**
- Fully simulated orbital network with health, alignment, priority‑based allocation, and degradation
- Comms satellites generate discoveries to reveal new world locations
- Rocket III (synfuel‑only) as late‑game launch vehicle
- Satellite receivers are multi‑mode: solar power, high‑efficiency power, supercomputer computing, relay, or communications
</details>

<details>
<summary><b>⛽ Synfuel, Vehicles & Logistics</b></summary>

- Synfuel refinery, synfuel generator, HP gas boiler, expanded oil cracking
- Large trucks & excavators (diesel / hydrogen / synfuel variants)
- T2 Synfuel Locomotive, synfuel cargo ships
- Fuel Station IV, T4 cargo depot modules (unit/ loose / fluid)
- Cargo Depot Module (XL): 3 200 capacity, 1 000 throughput
</details>

<details>
<summary><b>⛏️ World, Mines & Fracking</b></summary>

- **Variable Mines** — each mine rolls1–3 random products on spawn; player picks which to extract
- Dedicated mines: Copper, Iron, Gold, Titanium
- Fracking wells: water‑in → gas + wastewater
- Extended world‑gen caps and event tuning
</details>

<details>
<summary><b>🎣 Food, Fishing & Canning</b></summary>

- FishingDocks I/II/III, Fish Farms with configurable bait
- Fish species: sardines, anchovies, mackerel, cod, tuna, swordfish
- Canning chain: empty cans → canned fish / fruit / vegetables / food packs
- Fish scales → organic fertilizer; fish oil → antibiotics
</details>

<details>
<summary><b>⚙️ Overclocking & Maintenance</b></summary>

- Overclock/underclock machines 10–300 % with scaled workers, maintenance, electricity, computing
- Copy/paste overclock settings
- **Maintenance Depot IV** — produces all previous maintenance types
- Animation speeds scale properly with overclock rate
</details>

<details>
<summary><b>🎛️ Built‑In Mod Settings</b></summary>

Persistent, versioned settings exposed in the in‑game Settings menu:

- **Storage &Logistics** — allow‑all storage, remove throughput limits, vehicle pickup duration
- **Gameplay** — liquid dump multiplier, food market capacity, shipyard cargo capacity
- **Transports** — per‑tier throughput for molten, flat, loose, and fluid; cost multipliers for maintenance / electricity / construction
- **Trains** — base capacity multiplier, reduced build duration, wagon & station capacity multipliers, train station throughput multiplier
- **Ore Sorter** — buffer multiplier, speed settings
</details>

---

### 🔗 COIExtended‑Automation — Smart Balancers + ENet Circuit Network

#### Smart Balancer / Smart Zipper

- 8‑port, power‑aware splitter/merger with per‑port priority, productfilters, throughput caps
- Even distribution modes for inputs or outputs
- Dynamic buffer sizing and fully cloneable config

#### 🆕 ENet — Extended Network *(Experimental)*

A complete **circuit automation system** inspired by Factorio's circuit network.

<details>
<summary><b>How It Works</b></summary>

- Connect buildings with **Blue** and **Yellow** wires to form signal networks
- Signals update once per game tick with a one‑tick propagation delay
- Max wire distance: **48 tiles** — use Signal Relays to bridge longer gaps
- Select the **Wire Tool** from the Automation toolbar; press **Tab** to toggle wire color
</details>

<details>
<summary><b>Signal Processing Buildings</b></summary>

| Building | Purpose |
|----------|---------|
| **Signal Emitter** | Outputs up to 20 fixed signal values (constant source) |
| **Arithmetic Processor** | Math on signals — add, subtract, multiply, divide, modulo, bitwise, power; supports `Each` wildcard |
| **Logic Gate** | Conditional output — multiple conditions with AND/OR; multiple output slots |
| **Signal Selector** | Picks signals by index, count, or random selection |
</details>

<details>
<summary><b>Output & Control Buildings</b></summary>

| Building | Purpose |
|----------|---------|
| **Circuit Alarm** | Plays configurable alert sounds on condition; edge‑triggered, volume/pitch/global control |
| **Signal Display** | Compact indicator panel — color driven by 0xRRGGBB signal value; tile multiple for status boards |
| **Power Relay** | Enables/disables power delivery based on a circuit condition (backup generators, load shedding) |
</details>

<details>
<summary><b>Networking Buildings</b></summary>

| Building | Purpose |
|----------|---------|
| **Signal Relay** | Passive junction box — daisy‑chain toextend wire range; no power needed |
| **Wireless Broadcaster** | Transmits wired signals over a configurable radio frequency |
| **Wireless Receiver** | Receives wireless signals and injects them onto local wires |
</details>

<details>
<summary><b>Machine Integration & Signal Types</b></summary>

- Any base‑game machine can be wired — set conditions to auto‑enable/disable, or broadcast product levels
- **Product signals** — every item and fluid
- **Virtual channels** — 36 channels labeled `0–9` and `A–Z`
- **Wildcards** — `Each`, `Anything`, `Everything`
</details>

---

### 🎨 COIExtended‑Tweaks — QoL & Visualization

- **Free Camera** — unconstrained camera with infinite zoom, no terrain clipping
- **Unlimited Designations** — removes size caps on mining, dumping, surface, and forestry zones
- **Unlimited Tower Area** — removes default area limits for towers
- **Disable Clouds / Fog / Weather** — independent toggles for each rendering layer
- **Disable Emissions** — globally disables particle systems (requires save/load to take full effect)
- **Forestry Tower Enhancements** — throughput view and excavator priority controls from the tower UI
- **Vehicle Replacer / Mass Upgrade** — mass‑upgrade trucks, excavators, harvesters, and planters
- **Unlimited Demolish** — always allow demolition of static entities
- **Fix Stuck Trucks** — resolves cargo‑stuck edge cases
- **🆕 Line Placement** — hold `Ctrl+Shift` while placing any entity → click to draw a straight line in any direction
- **🆕 Efficiency Overlay** — toggle to visualize building efficiency at a glance
- **🆕 X‑Ray Tool Toggle** — enable/disable the X‑ray tool in regular gameplay
- **🆕 Transport Pillar Tool** — mass add/delete transport pillars non‑destructively
- **🆕 Transport Visualizer** — see where your transports are going
- **🆕 config.json system** — toggle options are now global (shared across saves); multiplier settings remain save‑specific

#### Console Commands

| Command | Description |
|---------|-------------|
| `list_locomotives` | List all locomotives |
| `randomize_loco_numbers` | Randomize locomotive numbers |
| `set_loco_number` | Set a specific locomotive number |

---

### 📦 COIExtended‑StoragePlus — Storage & Buffer Overhaul

- **Per‑storage capacity** — change capacity via text field; excess is pushed through logistics (not destroyed)
- **Make Default** — writes current capacity to the prototype; future storages spawn with your chosen value
- **Allow All** toggle — allows any compatible product of the same type
- **Import / Export / Transport sliders** — synchronized sliders + numeric fields for truck and belt thresholds
- **Vehicle enforce toggle** — hidden until CustomRoutes is unlocked; grays out when no vehicles are assigned
- **Update4 truck filter support** and new Sandbox options
- All settings persist across save/load

---

### 🎮 COIExtended‑Cheats — God‑Mode Toolkit

<details>
<summary><b>Settlement & Needs</b></summary>

- Direct numeric edits: population, Unity, Unity cap
- Toggles: No Food / Electricity / Clean Water / Computing Need, No Disease Effects
- One‑click: Fill Food Markets, Fill Animal Farms
</details>

<details>
<summary><b>Economy & Progression</b></summary>

- Instant Build/Research, No Construction Costs
- Free Power / Computing / Unity generation
- No Maintenance toggle, Fill All Depots
- Free / Finish Current / All / Repeatable Research
- Infinite Focus, Focus multiplier
- Instant DeleteStorages
- Item Sink / Item Source in any game mode
</details>

<details>
<summary><b>Environment & Resources</b></summary>

- Toggle all pollution types and waste generation
- Unlimited Water, Unlimited Oil
- Unlimited Fuel Gas (requires Core)
- Asteroid spawning with material, mix ratio, and radius controls
</details>

<details>
<summary><b>Terrain & Weather</b></summary>

- Process/clear designations, change terrain material, instant surfaces
- Tree tools: instant plant/remove with spacing
- Lock weather, adjust sun/rain intensity
</details>

<details>
<summary><b>Exploration & Logistics</b></summary>

- Instant cargo ships, disable fuel consumption
- Fast ore sorting, train/vehicle capacity
- Reveal / scan / visit all world locations, defeat all enemies
- Shipyard product adder
</details>

<details>
<summary><b>Vehicles & Vehicle Editor</b></summary>

- Lists all `DrivingEntityProto`;per‑vehicle Edit button for deep tuning (capacity, speed, costs)
</details>

<details>
<summary><b>✨ CreativeMode (one‑click macro)</b></summary>

Enables: 9 999 Unity, free power/computing, explore world map, unlock all tech, instant build, disable maintenance, raise vehicle cap, fill food markets, and more.
</details>

---

### ⚙️ COIExtended‑Difficulty — Extended Difficulty Sliders

Rebuilds vanilla `GameDifficultyConfig` ranges via reflection — sliders from **–90 %** to **+500 %** with fine‑grained steps for:

> Extra starting material · Maintenance cost · Fuel consumption · Rain& farm yield · Base health · Resource mining · Settlement & food consumption · World mine reserves · Unity production · Construction costs · Treesgrowth · Contracts profit · Research cost · Quick actions cost · Disease mortality · Solar efficiency · Pollution impact

---

### 🛠️ COIExtended‑RecipeMaker — Custom Recipe Tool

-Accessible from the main menu after enabling the mod
- Create, edit, and package custom recipes
- Custom recipe packs remain fully compatible across updates

---

### 🚗 COIExtended‑VehicleMaker *(Experimental)*

- Clone and customize vehicles
- Amphibious property copying fixed

---

### 🧹 COIExtended‑Sanitizer — Save Config Cleaner

> ⚠️ This is **not** a generic "remove any mod" tool. It only cleans dead config references.

```
sanitize_configs

```

Removes config instances for mods that only leave configs (Cheats, Tweaks, StoragePlus, etc.). Does **not** remove custom buildings, products, recipes, or entities. Always back up saves first.

> *May be deprecated — the base game appears to have adopted equivalent functionality in Update 4.*

---

## 🐛 Reporting Bugs

When [opening an issue](https://github.com/Keranik/COI-Extended/issues/new), please include:

- Which COIExtended mod(s) you're using and their version
- Captain of Industry game version
- Steps to reproduce
- Screenshots, console log (`F1` → `dump_log`), and/or save file

For general discussion, visit the [Discussions](https://github.com/Keranik/COI-Extended/discussions) tab or join us on [Discord](https://discord.gg/JmVjxdwX).

---

## ❤️ Support the Project

COI:Extended is free and always will be. If you'd like to support continued development:

- [Patreon](https://patreon.com/keranik)
- [Ko‑fi](https://ko-fi.com/keranik)
- [PayPal](https://www.paypal.me/undiscoveredent)

---

📄 License & Legal

Copyright ©2024-2026 Keranik. All Rights Reserved.

COI:Extended is original work created and maintained by Keranik. 

Portions of this mod interface with and are based upon material from Captain of Industry, used in accordance with the Official Captain of Industry Modding Policy.

Required notice: Portions © MaFi Games – used under the Captain of Industry Modding Policy. Not for standalone use.

This mod is provided free of charge and is not standalone software. It requires a licensed copy of Captain of Industry to function.

All rights reserved. Unauthorized copying, modification, or redistribution of this mod is prohibited.

### You may:

- ✅ Download and use this mod for personal gameplay on a lawfully obtained copy of Captain of Industry
- ✅ Share links to this repository
- ✅ Make voluntary donations (Patreon, Ko‑fi, PayPal)
- ✅ Create derivative works for personal use

### You may NOT:

- ❌ Redistribute this mod or any part of it on other platforms without permission
- ❌ Charge a mandatory fee, paywall, or sell this mod
- ❌ Claim this work as your own

For permissions beyond this scope, please [open an issue](https://github.com/Keranik/COI-Extended/issues) to discuss.

---

<div align="center">

Made with ⚙️ for **Captain of Industry**

[⬆ Back to Top](#-coiextended-mod-suite)

</div>
