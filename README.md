# COIExtended Mod Family

COIExtended is a family of modular Captain of Industry mods, ranging from a full late‑game overhaul to focused utility, difficulty, and QoL packs. You can mix and match most of them in a single save.

Current packs:

- `COIExtended.Core` – main gameplay overhaul.
- `COIExtended.Automation` – Smart Balancer / Smart Zipper only.
- `COIExtended.Cheats` – full cheat / god‑mode toolkit.
- `COIExtended.Difficulty` – extended difficulty sliders (rebuilds the vanilla difficulty menu).
- `COIExtended.ItemSink` – dev‑style resource spawner and sink.
- `COIExtended.Sanitizer` – save config sanitizer for “stuck” configs.
- `COIExtended.StoragePlus` – storage & buffer overhaul with persistent settings.
- `COIExtended.Tweaks` – UI / gameplay tweaks, free camera, forestry tools, and visual toggles.

Legacy overhaul modlets are no longer part of this lineup; legacy users are directed to the Steam beta branch **`b481`** with their existing mods.

---

## COIExtended.Core – Main Overhaul

The Core pack merges and supersedes the old “Logistics/World/Solar/Structures Extended” modlets and adds substantial late‑game content.

### Late‑Game Progression

- **Lithium & oceanic materials**
  - Lithium extraction from seawater via heated ponds and thermal desalinators.
  - New brine types and chemistry:
    - Lithium‑rich, concentrated, and spent brines.
    - Ion extraction and electrodialysis → `Lithium chloride` and `Salt mix (impure)`.
    - Chlor‑alkali regeneration closes brine loops and produces `Hydrogen chloride`.
  - Carbonate chemistry:
    - Soda ash (Solvay), caustic soda, impure salt electrolysis, fertilizer from impure salt.
    - Alkaline wastewater treatment.

- **Batteries & silicon recovery**
  - Battery Chemistry:
    - NMC and High‑Ni cathodes.
    - Li‑ion battery packs built in robotic assemblers.
    - Dedicated **Electrode Coater** for cathode coating.
  - Silicon Recovery:
    - Final‑stage chip recycling (normal & lithium‑doped).
    - Mono‑Si solar cells with lithium‑enhanced wafers.
    - Silicon wafer recycling → polysilicon.

- **Isotopes, heavy water & deuterium**
  - Lithium isotope enrichment in the **Uranium Enrichment Plant**.
  - Heavy water fractionation and deuterium extraction in **Electrolyzer T2**.
  - Argon production via Air Separator (air separation + argon distillation).

- **Fusion power & helium**
  - D‑T fuel cycle:
    - Heavy water + enriched lithium → `Tritium` and `Deuterium`.
    - DT fuel rods + reprocessing loop to recover isotopes and slag.
  - Fusion reactor:
    - Feeds **Multistage Turbine** and **Power Generator T3** for GW‑scale power.
  - Helium chain:
    - Helium air separation, distillation, and helium cooling (liquid helium as coolant).

### Orbital Satellites & Beamed Power/Computing

- **Satellites & assembly**
  - Solar satellites (standard & HighNi), relay satellites, comms satellites, supercomputer satellites.
  - Satellite thrusters and hardware assembled in the **Satellite Assembler**.
  - Rocket III (synfuel‑only) as late‑game launch vehicle.

- **Network simulation**
  - Global **SatellitesManager**:
    - Tracks stored vs deployed satellites per type.
    - Computes total power and computing output, scaled by network health and type multipliers.
    - Allocates output to ground receivers by **priority**, with proportional sharing under contention.
    - Simulates failures and degradation; relay satellites can regenerate health if over‑provided.
    - Performs probabilistic **alignment** checks to hook receivers into the network.

- **Ground infrastructure**
  - **Satellite Control Center**:
    - Per‑type target counts and auto‑deployment from orbit storage.
    - Claims existing deployed satellites on construction.
  - **Satellite Receivers**:
    - Mode‑switchable by satellite type.
    - Act as generators (power or computing) and consumers (power or computing) depending on mode.
    - Respect maintenance, workers, electricity, and computing requirements.

- **Discoveries**
  - Comms satellites:
    - Build discovery progress over time, gated by network health.
    - Generate “pending discoveries” that world systems can consume to reveal new locations.

### Synfuel & Advanced Logistics

- **Synfuel chain**
  - Synfuel (liquid and gas) products.
  - Synfuel Refinery, Synfuel Generator, HP Gas Boiler.
  - Expanded oil chain:
    - Heavy/medium/light oil cracking.
    - T2 distillation and enhanced sour‑water handling.

- **Vehicles & fuel**
  - Large trucks and large excavators with support for diesel, hydrogen, and synfuel.
  - Synfuel variants of mega vehicles, excavators, tree planters, harvesters.
  - Synfuel locomotives and synfuel cargo ships.
  - Dedicated research nodes for synfuel vehicles, cargo docks, and generators.

- **Fuel stations & depots**
  - Fuel Station IV with diesel, hydrogen, and synfuel variants.
  - New T4 cargo depot modules for unit, loose, and fluid with proper colliders and icons.

### World, Mines, Fracking & Events

- **Mines & variable deposits**
  - Dedicated mines: copper, iron, gold, titanium.
  - **VariableMineEntity**:
    - When spawned, each mine rolls 1–3 possible products from its `AvailableProducts` list (deterministic per site).
    - Player chooses which of those products to extract via UI; buffer and output update accordingly.
  - Some advanced materials (e.g. `Manganese flakes`, `Cobalt powder`) are not directly mineable:
    - Obtained via **trade** or variable mines that rolled them.

- **Fracking wells**
  - Water‑in, gas + wastewater‑out wells that mirror real fracking behavior.
  - Tied into extended water treatment and waste/brine loops.

- **World & events**
  - More flexible world‑gen caps:
    - Higher max map dimensions, sectors, locations, and connection distances.
  - Event tuning:
    - Loot vs “nothing found” odds, pirate behavior, enemy spawn scaling per tier.
    - Radioactive loot removed by default (toggleable elsewhere).

### Food, Fishing & Canning

- **Fishing & fish farms**
  - Fishing Docks I/II/III with proper models and logistics ports.
  - Fish Farms with:
    - Configurable bait types.
    - Adjusted catch rates, bait usage, and buffer sizes.
    - Fixes for bait buffer/logistics issues on type changes.

- **Fish chain**
  - Fish species: sardines, anchovies, mackerel, cod, tuna, swordfish.
  - Byproducts: fish oil (fluid), fish scales (loose), raw fish.

- **Canning**
  - Canned foods:
    - Empty cans, canned fish, canned fruit/vegetables, canned food packs.
  - Balanced recipes and value across the chain.
  - Contracts and quick trades for canned goods.

- **Side uses**
  - Fish scales → organic fertilizer and burnable fuel.
  - Fish oil → antibiotics and waste‑dump recipes.

### Maintenance, Overclocking & UI

- **Overclocking (10–300%)**
  - Overclock/underclock machines with:
    - Scaled workers, maintenance, electricity, and computing.
  - Copy/paste overclock settings.
  - Overclocking tech node moved under Maintenance Depot II.

- **Maintenance Depot IV**
  - T4 Maintenance Depot that can produce **all previous maintenance types**.
  - Greatly simplifies late‑game maintenance.

- **UI & research**
  - Revised research tree:
    - Drydock vs Cargo Ship dependencies.
    - Ship armor/weapons placement.
    - Biofuel and overclocking nodes repositioned.
  - Numerous bugfixes and tooltip / icon improvements carried over from the 113–114 wave.

---

## COIExtended.Automation – Smart Balancer / Smart Zipper

Contains only the **Smart Zipper** (Smart Balancer):

- 8‑port, power‑aware splitter/merger:
  - Per‑port **priority levels**, **product filters**, and **throughput caps**.
  - Per‑port “bucket” accounting to enforce soft rate limits over time.
- Even distribution modes:
  - Force even on inputs or outputs, overriding priorities.
- Dynamic buffer sizing and delay based on effective throughput and connections.
- Fully cloneable config and safe serialization.

---

## COIExtended.Tweaks – UI & Gameplay Tweaks

Tweaks provides non‑cheat quality‑of‑life and visualization improvements.

### Camera & Area Limits

- **Free Camera**
  - Unlocks an unconstrained camera:
    - No terrain clipping.
    - Infinite zoom and free movement.

- **Unlimited Designations**
  - Removes size caps on:
    - Mining and dumping areas.
    - Surface designations.
    - Forestry harvest zones.

- **Unlimited Tower Area**
  - Removes default area limits for towers (e.g. forestry towers).

### Rendering & Visuals

- **Disable Clouds / Fog / Weather**
  - Independently toggle:
    - Cloud rendering.
    - Fog rendering.
    - Weather effect visibility (visual only; simulation remains).

- **Disable Emissions**
  - Globally disables emissions/particle systems on all relevant prototypes.
  - Due to engine limitations, requires a full save/load to fully enable or disable.

### Demolition & Stability

- **Unlimited Demolish**
  - Always allow demolition of static entities (`AlwaysAllowBulldoze`).

- **Fix Stuck Trucks**
  - Tweaks cargo‑stuck edge cases:
    - Fix for trucks that pick up dump material but get stuck because another truck finished the dump designation first.

### Forestry & Vehicle QoL

- **Forestry tower enhancements**
  - Forestry towers gain:
    - A **total throughput view** for their managed area (so you can see how effective the patch is).
    - Direct control over **excavator mining priorities** for that area, configured from the tower UI.

- **Vehicle replacer / mass upgrade**
  - Vehicle Replacer column:
    - Mass‑upgrade tools for trucks, excavators, tree harvesters, and tree planters.
    - Pick current type, target type, and quantity (or all) to upgrade.

Tweaks integrates cleanly with both Core and mostly vanilla games.

---

## COIExtended.StoragePlus – Storage & Buffer Overhaul

StoragePlus replaces the vanilla storage inspector with a persistent, configurable panel.

### Capacity & Defaults

- **Per‑storage capacity**
  - Change capacity via text field; updates underlying buffer capacity.
  - If new capacity is lower than current contents, excess is pushed out through the logistics buffer and stored via the AssetTransactionManager (not silently destroyed).

- **Make Default**
  - Writes the current capacity back to the storage **prototype** and to StoragePlus config.
  - Future storages of that type spawn with your chosen capacity.

### “Allow All” Product Types

- `Allow All` toggle:
  - For non‑nuclear storages:
    - Allows any compatible product of the same **product type** to be stored (e.g., any loose or any fluid of that type).
  - Works with the enhanced product picker:
    - You can assign products beyond the vanilla `StorableProducts` list.

### Import/Export & I/O Controls

- Full set of synchronized sliders + numeric fields:
  - `Import` (truck import until %).
  - `Export` (truck export from %).
  - `Transport From` & `Transport Until` (belt/pipe throughput thresholds).
- You can:
  - Drag sliders OR type exact percentages.
  - Quickly set patterns like:
    - Import until 30%, export from 70%.
    - Transport from 50% to 100%, etc.
- Logic keeps import/export mutually sensible (e.g. setting one side may adjust the other).

### Persistence

- StoragePlus config stores:
  - Per‑prototype default capacity.
  - Per‑instance slider and threshold behavior.
- Your choices persist across save/load; no need to redo sliders every session.

Extras:

- Enhanced quick remove and alerts.
- Vehicle assigners and “enforce only assigned vehicles” retained and improved.

---

## COIExtended.Cheats – Cheat / God‑Mode Toolkit

Cheats adds a multi‑tab cheat UI backed by `CheatFlags` and deep integration with the simulation.

### Settlement & Needs

- Direct numeric edits:
  - Total **population**, **Unity**, and **Unity cap**.
- Needs toggles:
  - `No Food Need`, `No Electricity Need`, `No Clean Water Need`, `No Computing Need`.
  - `No Disease Effects`:
    - Sets disease effect/mortality multipliers to zero via game properties.
- One‑click actions:
  - `Fill Food Markets` – maxes configured food buffers.
  - `Fill Animal Farms` – adds animals and fills feed/water.

### Economy & Progression

- Build & research:
  - `Instant Build/Research` – sets InstaBuild on.
  - `No Construction Costs` – free construction.

- Free resource generation:
  - `Power` – free MW per tick/month.
  - `Computing` – free TFlops per tick/month.
  - `Unity` – free Unity per month.

- Maintenance:
  - `No Maintenance` toggle.
  - `Fill All Depots` – instantly fills all maintenance buffers.

- Research:
  - `Free Research` – labs do not consume products.
  - `Finish Current Research`.
  - `Finish All Research`.
  - `Finish Repeatable Research` – maxes out all repeatables via internal API.

- Focus system:
  - `Infinite Focus` – removes cap on assignable focus.
  - `Focus Multi` – numeric % field that adjusts FocusPointsMultiplier via property modifiers.

- Gameplay toggle:
  - `Instant Delete Storages` – deleting storages clears them immediately instead of cleaning mode.

### Environment & Resources

- Pollution & waste:
  - `No Air/Water/Ship/Vehicle/Train Pollution`.
  - `No Waste Generated`, `No Biowaste Production`, `No Wastewater Production`.

- Virtual reserves:
  - `Unlimited Water` – massively increase groundwater capacities and set daily replenish to 100%.
  - `Unlimited Oil` – massively increase virtual crude oil reserves.

- Asteroid spawning:
  - Choose Terrain Material 1 & optional Material 2.
  - Set mix ratio via slider.
  - Override radius (0–1000 tiles).
  - `Spawn Asteroid` – calls AsteroidsManager to spawn into orbit.

### Terrain & Weather

- Terrain operations:
  - `Ignore Tower Designation` – optionally skip tower‑managed areas.
  - `Disable Terrain Physics` – stop physics simulation while modifying terrain.
  - `Process All Mining Designations`.
  - `Process All Dumping Designations`.
  - `Clear/Place All Pending Surfaces`.
  - `Change Terrain At Designations` – convert terrain material within dumping designations.
  - `Instant Build Surfaces` – resolves surfaces immediately on assignment.

- Tree tools:
  - `Tree Spacing` control.
  - `Instantly Plant Trees` across forestry designations with spacing.
  - `Instantly Remove Trees` (harvest + remove stumps) for all selected.

- Weather:
  - `Lock Weather` – freeze current conditions.
  - `Reset All Weather` – restores baseline sun/rain per weather type, sets sunny.
  - `Sun Intensity` / `Rain Intensity` – global overrides (0–100) applied to all weathers.
  - `Disable Rendering` / `Enable Rendering` – toggle fog, cloud, weather effect visuals.

### Exploration & Logistics

- Ships & fuel:
  - `Instant Cargo Ships` – skip travel time on contracts.
  - `Disable Fuel Consumption` – uses property override to prevent fuel consumption.

- Throughput & capacity:
  - `Fast Ore Sorting` – temporarily boost ore sorting output to 8000 per cycle, with clean revert.
  - `Train Capacity (%)` – adjust trains capacity multiplier.
  - `Vehicle Limit` – set max vehicle count.
  - `Extra Capacity (%)` – adjust trucks capacity multiplier.

- Cargo ships & world map:
  - `Total Cargo Ships` – number of discovered ships.
  - `Unload Ship` – dump all traveling fleet cargo into dock.
  - `Repair Ship` – fully repair cargo ship via scheduled command.
  - `Finish Navigation` – completes all navigation activities.
  - `Reveal All Locations`, `Scan All Locations`, `Visit All Locations`, `Defeat All Enemies`.

- Shipyard product adder:
  - Product dropdown, quantity field, and “Storage First” toggle.
  - Add arbitrary product to shipyard or world storages.
  - Validation that quantity and product are reasonable and truck‑compatible when using dock.

### Vehicles & Vehicle Editor

- Scans all `DrivingEntityProto` and lists them.
- Each entry has an `Edit` button:
  - Opens the **Vehicle Editor** for that prototype.
  - Allows deep tuning (capacity, speed, costs, etc.) through a dedicated window.

### Creative Mode

One‑click macro with a long list of cheats combined, including:

- 9999 Unity cap & Unity; 9999 monthly Unity.
- 9999 MW and 9999 TFlops free per month.
- Explore world map; unlock all technology.
- Raise vehicle cap; fill food markets.
- Enable Instant Build; disable maintenance.
- Disable fog, weather, clouds, fuel consumption.
- Max out water/oil reserves and groundwater replenish.
- Add 99 cargo ships; clear all notifications.

---

## COIExtended.Difficulty – Extended Difficulty Sliders

`COIExtended.Difficulty` rebuilds the vanilla **GameDifficultyConfig** ranges using reflection, drastically expanding slider options.

- Each PercentSettingInfo (e.g. `ExtraStartingMaterialInfo`, `MaintenanceDiffInfo`, etc.) gets **much wider ranges**, typically from around **–90% up to +500%**, with fine‑grained steps.
- Affected difficulty settings include (but are not limited to):

  - Extra starting material.
  - Maintenance cost difficulty.
  - Fuel consumption.
  - Rain yield and farm yield.
  - Base health.
  - Resource mining rates.
  - Settlement consumption and food consumption.
  - World mine reserves.
  - Unity production.
  - Construction costs.
  - Trees growth.
  - Extra contracts profit.
  - Research cost.
  - Quick actions cost.
  - Disease mortality.
  - Solar power efficiency.
  - Pollution impact.

- It also rebuilds the **AllOptions** array so the difficulty UI uses the new `PercentSettingInfo` objects.

Because it only modifies difficulty metadata, it’s safe to run with vanilla or Core.

---

## COIExtended.ItemSink – Dev Spawner & Sink

- Adds:
  - A universal **resource spawner** (source entity).
  - A universal **sink** (void entity).
- Useful for:
  - Testing recipes and chains.
  - Building blueprints.
  - Emergency cleanup in broken saves.

---

## COIExtended.Sanitizer – Save Config Sanitizer

> **Critical Warning:** This is **not** a generic “remove any mod and keep playing” tool. It is only for cleaning **dead config references** from saves.

- Console command:
  - `sanitize_configs`
- Behavior:
  - Reflects into the game’s internal dependency resolver and `GameSaver` to:
    - Remove mod/config instances for mods that only leave configs (e.g. `COIExtended.Cheats`, `Tweaks`, `StoragePlus`, etc.).
  - Produces a save that no longer hard‑references those configs, allowing it to load without the associated UI/config mods.

- Does **not** remove:
  - Custom buildings, products, recipes, entities, or world changes.
- If you remove a mod that added content the save still uses:
  - The save may still crash or behave unpredictably; Sanitizer cannot fix that.

Always back up your saves before running `sanitize_configs`.

---

## COIExtended.Core – Mod Settings (Built‑In Settings Layer)

Core also ships with a persistent, versioned **Mod Settings DB** (`ModSettingsDb`) that exposes a large set of configurable options in the in‑game Settings menu:

- **Storage & logistics**
  - Allow all product storage (per‑storage “allow all” baseline).
  - Remove storage throughput limit.
  - Vehicle cargo pickup duration (Instant → Slowest).

- **Gameplay**
  - Liquid dump recipe multiplier.
  - Food market capacity multiplier.
  - Shipyard cargo capacity.

- **Transports – throughput per tier & type**
  - Molten channels T1–T2.
  - Flat belts T1–T4.
  - Loose belts T1–T4.
  - Fluid transports T1–T4.

- **Transports – cost multipliers**
  - Transport maintenance multiplier (stacked on top of COIExtended’s base ~50% reduction).
  - Transport electricity and construction multipliers.

- **Trains**
  - Base train capacity multiplier.
  - Reduced train build duration toggle.
  - Added train wagon capacity multiplier (additive).
  - Added train station capacity multiplier (additive).

Settings are:

- Versioned (`CURRENT_SAVE_VERSION = 120`) and serialized per save.
- Back‑filled into old saves if missing.
- Accessible and tweakable via the in‑game Settings UI.

---

## Notes on Legacy

Legacy overhaul modlets are no longer in the active pack lineup. If you must use them:

- Stay on the base game’s **`b481`** beta branch and keep your existing legacy mod stack.
- New development and support is focused on `COIExtended.Core` and the modular packs described above.
