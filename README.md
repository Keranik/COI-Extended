# COI: Extended (COIExtended)

COI: Extended unifies former gameplay overhaul modlets (Logistics, World, Solar, Structures + expanding Agriculture/Fishing) into a single **Unified Core** while preserving a small set of **standalone, opt‑in quality‑of‑life / cheat / utility mods**.  
Choose ONE primary path per save: Unified Core (new games), Updated Legacy bundle (finish old overhaul saves), or StandaloneONLY (QoL/cheats only).

> QUICK PICK:  
> • Starting a NEW game? Use the Unified Core.  
> • Finishing an existing overhaul save? Use the Updated Legacy bundle.  
> • Only ever used QoL / cheats? Use StandaloneONLY.  

---

## 🧭 Table of Contents
1. What Is COI: Extended?
2. Choosing the Right Download
3. Unified Core vs Legacy vs Standalone
4. Feature Overview (By Module)
5. Installation & Upgrading
6. Using the Mods (Short How‑To)
7. Reporting Issues
8. Contributing & Requests
9. FAQ
10. Chinese Quick Guide (简体中文速览)
11. License / Attribution / Support
12. Changelog Pointer

---

## 1. What Is COI: Extended?

Originally split into multiple gameplay modlets because of strict product ID limits, COI: Extended now leverages the expanded 65,000 product space to merge major overhaul content into a single **Unified Core**.  
Goals: faster iteration, deeper balancing, less mod clutter, and a stable foundation for new systems (Agriculture, advanced logistics, world progression, etc.).  
Standalone utility / cheat / QoL modlets remain separate so you can customize difficulty or add convenience without committing to the full overhaul.

---

## 2. Choosing the Right Download

Select exactly ONE primary ZIP for any given save environment:

| Purpose | File (example) | Use This If | Save Compatibility | Future Features |
| ------- | -------------- | ----------- | ------------------ | --------------- |
| Unified future gameplay (overhaul) | `COIExtended-112u-ForNewGames.zip` | Starting a brand new game | Incompatible with old overhaul saves | ✅ Yes |
| Continue existing overhaul save | `COIExtended-112-LEGACY-AllMods.zip` | Save used Logistics/World/Solar/Structures | Works with that save | ❌ No (compat only) |
| Only QoL / cheats | `COIExtended-112-StandaloneONLY.zip` | Save never used overhaul modlets | Works with that save | Limited (standalone scope) |
| Historical pin | Tag `v0.7.8.481` | Need reproducibility / comparison | Same as before | ❌ No |

Standalone Modlets (optional for either path—do NOT add legacy overhaul modlets into a Core game):
- ItemSink
- Cheats
- Tweaks
- StoragePlus
- Difficulty

---

## 3. Unified Core vs Legacy vs Standalone

| Aspect | Unified Core | Updated Legacy | StandaloneONLY |
| ------ | ------------ | -------------- | -------------- |
| Target Use | Fresh campaigns | Finish old overhaul saves | Light customization |
| Save Upgrade | Requires NEW save | Continue existing | Continue existing |
| Ongoing Features | Full roadmap | None (compat only) | QoL/Cheat only |
| Agriculture / Fishing Expansion | Integrated & growing | Not backported | N/A |
| Sunset Risk | Long-term path | Will retire | Stays separate |
| Mixing Allowed | Add standalone mods only | Don’t inject Core mid‑save | Can later start Core separately |

---

## 4. Feature Overview

### 4.1 Unified Core (Merged Former Modlets)
Contains content from Logistics Extended, World Extended, Solar Extended, Structures Extended (plus incremental Agriculture: Fishing/Canning).

Highlights (abridged):
- Expanded transport tiers, balancers (small & large), throughput counters & customizable belt speeds.
- Overclocking / underclocking (10–300%) influencing workers, maintenance, power, computing.
- Random world generation controls (map size, sector counts, resource parameters, event tuning).
- Progressive ship upgrades & world event balance changes.
- Solar variants, structural additions, expanded food / canning / fish chains.
- Integrated research progression nodes.

### 4.2 Standalone Modlets

#### Cheats
Settlement (population/unity edits, disable needs), economy (instant build/research, free upkeep, infinite focus), environment (pollution toggles, infinite reserves), terrain (instant dig/dump, mass surface ops, weather locking), exploration/logistics (instant ship contracts, fuel disable, capacity & limits, navigation completion), product spawning/sinking, train multipliers, vehicle prototype editor (custom stats, save/load).

#### Tweaks
Game speed tiers 1–15, free camera, unlimited designation sizes / tower areas, rendering toggles (cloud/fog/weather), vehicle management overview, enhanced mine & forestry UI.

#### StoragePlus
Remove throughput limits, edit capacity (set as new default optionally), allow any supported product types (steam, exhaust, etc.), belt output slider.

#### ItemSink
Universal infinite sink and universal source.

#### Difficulty
Adjust broad difficulty multipliers (–90% to +500%).

### 4.3 Deprecated Legacy Overhaul Modlets
Logistics / World / Solar / Structures exist only in legacy bundle form for save continuity; no new features.

---

## 5. Installation & Upgrading

### 5.1 Unified Core (Fresh Start)
1. Backup saves.
2. Delete all mods that have the COIExtended prefix in `%APPDATA%\Captain of Industry\Mods` (or move it aside).
3. Extract `COIExtended-xxx.zip` to the Mods directory.
5. Enable mods in game settings → start a NEW game.

### 5.2 Updated Legacy
1. Backup your saved games.
2. Replace previous legacy modlet folders with `COIExtended-xxx-LEGACY-AllMods.zip` by extracting the file to your `Mods` directory.
3. Continue existing save. Do NOT add Unified Core.

### 5.3 StandaloneONLY
1. Remove any overhaul modlets (if desired).
2. Install `COIExtended-xxx-StandaloneONLY.zip` by extracting the file to your `Mods` directory..
3. Continue or start save with QoL/cheats only.

### 5.4 Mid-Save Switching Policy
- Legacy overhaul → Core: Not supported (start new save).
- StandaloneONLY → Core: Allowed only by starting fresh.
- Always keep backups.

### 5.5 Adding Mods to an Existing Save
1. Load screen (don’t load yet) → Wrench icon.
2. Toggle standalone mods only.
3. Confirm & load.

---

## 6. Using the Mods (Short How‑To)

| Component | Access / Hotkey | Notes |
| --------- | --------------- | ----- |
| Cheats Window | F8 | Tabs: Settlement, Economy, Environment, Terrain, Vehicles, etc. |
| Tweaks Window | F9 | Speed tiers, camera, rendering toggles |
| Overclocking | Building UI (Core) | 10–300% dynamic scaling |
| StoragePlus | Storage UI | Capacity & throughput edits |
| Vehicle Prototype Editor | Cheats > Vehicles | Save/load custom prototypes |
| Difficulty Sliders | Mod Settings | –90% to +500% |
| World Gen Options | New Game screen | Random map parameters |
| ItemSink / Source | Build menu | Instant spawn/void |

---

## 7. Reporting Issues

Include:
1. Path (Core / Legacy-AllMods / StandaloneONLY)
2. ZIP name & list of enabled standalone mods
3. Reproduction steps (expected vs actual)
4. Log `%APPDATA%\Captain of Industry\Logs`
5. Save `%APPDATA%\Captain of Industry\Saves` (if reproducible)
6. Screenshots / video (optional)

Open a GitHub Issue with a concise, descriptive title.

---

## 8. Contributing & Requests

Focus: Stabilizing Unified Core & Agriculture expansion.  
Request features via GitHub Issues (label suggestion). Provide rationale & balance notes.  
Translations: PRs welcome (English is canonical source).

---

## 9. FAQ

| Question | Answer |
| -------- | ------ |
| Can I migrate an old overhaul save to Core? | No—new save required. |
| Will legacy overhaul get new features? | No, compatibility patches only. |
| Are standalone mods safe with Core? | Yes—just don’t add legacy overhaul modlets. |
| Why unification now? | Product ID limit lifted; reduces maintenance & fragmentation. |
| Performance changes? | Similar or improved vs multiple simultaneous modlets. |
| Where suggest new settings? | Official Discord `#modding` or GitHub Issues. |
| Vehicle Prototype Editor stable? | Experimental—backup first. |

---

## 10. 简体中文速览 (Chinese Quick Guide)

| 你的需求 | 下载 | 说明 |
| -------- | ---- | ---- |
| 新开档 + 未来内容 | `COIExtended-112u-ForNewGames.zip` | 统一核心，需新存档 |
| 继续旧“大改”存档 | `COIExtended-112-LEGACY-AllMods.zip` | 旧功能子模组整合，仅兼容 |
| 只用 QoL / 作弊 | `COIExtended-112-StandaloneONLY.zip` | 独立子模组 |
| 历史复现 | 标签 `v0.7.8.481` | 旧版本参考 |

不要把旧功能子模组与统一核心混用。完成旧存档后再转统一核心。

---

## 11. License / Attribution / Support

All rights reserved to Undiscovered Entertainment for original code, concepts, and assets except where noted. No redistribution or derivative works without explicit written permission.

Captain of Industry assets & IP © MaFi Games; used under their EULA and modding policy. Unofficial, non‑commercial fan project; not endorsed by MaFi Games.

Free to use; donations optional (no extra rights/access).

Support / Donate:
- Patreon: https://patreon.com/keranik
- PayPal: https://paypal.me/undiscoveredent

Contact: Keranik on the official Captain of Industry Discord (`#modding`).

---

## 12. Changelog Pointer

See the latest GitHub Release for full patch notes (Fishing/Canning wave, world/event adjustments, settings expansions, fixes, etc.).

---

### Enjoy COI: Extended! 💚  
If it improves your game, star the repo or share feedback—early adopters shape what comes next.
