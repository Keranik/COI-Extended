using Mafi;
using Mafi.Base;
using Mafi.Core.Entities;
using Mafi.Core.Prototypes;
using Mafi.Core.World;
using Mafi.Core.World.Entities;
using Mafi.Core.Products;
using Mafi.Core.Economy;
using Mafi.Collections.ImmutableCollections;
using System.Reflection;
using UnityEngine;
using System;
using System.Collections.Generic;
using Mafi.Core.Research;
using Mafi.Core.Fleet;
using COIExtended.Extensions;
using Mafi.Collections;
using Mafi.Core;

namespace COIExtended.World
{
    public static class WorldMapLocationsHolder
    {
        public static Dictionary<string, WorldMapLocation> Locations = new Dictionary<string, WorldMapLocation>();
    }

    

    [GlobalDependency(RegistrationMode.AsAllInterfaces, false, false)]
    public class NewMapGenerator : IWorldMapGenerator
    {
               
        public class COIEMapLinker
        {
            private readonly ProtosDb m_protosDb;
            private WorldMap m_map;
            
            public COIEMapLinker(ProtosDb protosDb, WorldMap map)
            {
                m_protosDb = protosDb;
                m_map = map;
            }
            public void SetEntityProto(EntityProto.ID entityId, WorldMapLocation location)
            {
                WorldMapEntityProto protoToAssign = m_protosDb.GetOrThrow<WorldMapEntityProto>(entityId);
                 Option<WorldMapEntityProto> optionProtoToAssign = Option.Some(protoToAssign);
                PropertyInfo entityProtoProperty = typeof(WorldMapLocation).GetProperty("EntityProto", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                MethodInfo setMethod = entityProtoProperty.GetSetMethod(nonPublic: true);
                setMethod.Invoke(location, new object[] { optionProtoToAssign });
                
            }

            public void SetEnemyProto(WorldMapLocation location, BattleFleet fleet)
            {
                BattleFleet protoToAssign = fleet;
                Option<BattleFleet> optionProtoToAssign = Option.Some(protoToAssign);
                PropertyInfo entityProtoProperty = typeof(WorldMapLocation).GetProperty("Enemy", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                MethodInfo setMethod = entityProtoProperty.GetSetMethod(nonPublic: true);
                setMethod.Invoke(location, new object[] { optionProtoToAssign });

            }
            public void SetTechnologyProto(Proto.ID techID, WorldMapLocation location)
            {
                location.Loot = new WorldMapLoot();
                TechnologyProto techUnlockProto = m_protosDb.GetOrThrow<TechnologyProto>(techID);
                location.Loot.Value.ProtosToUnlock = ImmutableArray.Create(techUnlockProto);
                location.Loot.Value.IsTreasure = true;
            }
            public void GenerateWorldEvents()
            {
                // Scale based on this number, we can add difficulty options once we get a baseline
                int difMod = 100;
                // Collect up to T4 distance, each tier meant to represent a ship upgrade
                // anything more than T4 distance (if possible) will be unnamed T5 and beyond.
                float distanceFromHome, T1Distance, T2Distance, T3Distance, T4Distance;

                // We can adjust these numbers at any point we just need a baseline to test
                T1Distance = 1000;
                T2Distance = 2000;
                T3Distance = 3000;
                T4Distance = 4000;

                // Default to Tier 1 in case something goes awry
                int tierToMake = 1;

                // Generate a random number 1-100
                System.Random chance = new System.Random();
                bool generateEnemy = false;
                bool generateLoot = false;
                int randomNumber = chance.Next(1, 101);



                // Only generate events for our new locations, don't iterate through old locations if they still exist
                foreach (KeyValuePair<string, WorldMapLocation> locationEntry in WorldMapLocationsHolder.Locations)
                {
                    randomNumber = chance.Next(1, 101);
                    string locationKey = locationEntry.Key;
                    WorldMapLocation mapLocation = locationEntry.Value;
                    tierToMake = 1;

                    // Don't generate a world event on a location that has an Entity or Loot already
                    if (mapLocation.EntityProto.HasValue || mapLocation.Loot.HasValue)
                        continue;

                    // Find out how far from home we are at this new location
                    distanceFromHome = CalculateDistanceFromHome(mapLocation.Position);

                    // Increase tier based on location
                    if (distanceFromHome > T1Distance) tierToMake++;
                    if (distanceFromHome > T2Distance) tierToMake++;
                    if (distanceFromHome > T3Distance) tierToMake++;
                    if (distanceFromHome > T4Distance) tierToMake++;

                    // Re-set to false
                    generateEnemy = false;
                    generateLoot = false;
                    randomNumber = chance.Next(1, 101);

                    // 40% chance for loot, 40% chance for enemy, 20% chance for both
                    if (randomNumber <= 40) generateLoot = true;
                    else if (randomNumber <= 80) generateEnemy = true;
                    else { generateLoot = true; generateEnemy = true; }

                    if (generateLoot)
                        GenerateLoot(mapLocation, tierToMake);

                    if (generateEnemy)
                    {
                        if (distanceFromHome < 1000)
                            continue;
                        else
                        GenerateEnemy(mapLocation, tierToMake);
                    }
                        
                    
                }
            }

            public void GenerateLoot(WorldMapLocation location, int tier)
            {
                WorldMapLoot worldMapLoot = new WorldMapLoot(); // We will always be adding some type of loot here.
                AssetValue rewardsList; // Decide what items to add after, but we will always add something.
                System.Random chance = new System.Random();
                int randomNumber = chance.Next(10, 41);

                // Start with a base of 40 items, add 10-40 more, then multiply by tier.
                // For example, 1.5x for Tier 2, 2x for Tier 3, etc.
                float tierMultiplier = 1f + (0.5f * (tier - 1));
                int baseQuantity = 40;
                int adjustedQuantity = baseQuantity + randomNumber * (int)tierMultiplier;

                ProductProto CP1 = m_protosDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts);
                ProductProto CP2 = m_protosDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts2);
                ProductProto CP3 = m_protosDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts3);
                ProductProto CP4 = m_protosDb.GetOrThrow<ProductProto>(Ids.Products.ConstructionParts4);
                ProductProto Titanium = m_protosDb.GetOrThrow<ProductProto>(NewIDs.Products.TitaniumIngots);
                switch (tier)
                {
                    // For now I'm just going to give Construction Parts, we'll add the random choices once it works.
                    case 1:
                        rewardsList = new AssetValue(CP1, adjustedQuantity.Quantity());
                        break;
                    case 2:
                        rewardsList = new AssetValue(CP2, adjustedQuantity.Quantity());
                        break;
                    case 3:
                        rewardsList = new AssetValue(CP3, adjustedQuantity.Quantity());
                        break;
                        
                    case 4:
                        rewardsList = new AssetValue(CP4, adjustedQuantity.Quantity());
                        break;
                        
                    default: // Tier 5
                        rewardsList = new AssetValue(Titanium, adjustedQuantity.Quantity());
                        break;
                }


                // Add the AssetValue to the world map.
                worldMapLoot.Products += rewardsList;
                location.Loot = worldMapLoot;

            }
            public void GenerateEnemy(WorldMapLocation location, int tier)
            {
                BattleFleet battleFleet = new BattleFleet("Pirates", isHuman: false);

                int numOfScouts=0, numOfPatrols=0, numOfCruisers=0, numOfBattleships=0;
                int gunsT0=0, gunsT1=0, gunsT2=0, gunsT3=0, armorsT1=0, armorsT2=0;

                // Based on tier, randomly pick fleet compositions
                // Tier 1: 1 Scout Only
                // Tier 2: 1 Scout and 1 Patrol or 2 Scouts
                // Tier 3: 1 Patrol and 1 Cruiser or 2 Patrols
                // Tier 4: 1 Cruiser and 1 Battleship or 2 Cruisers
                // Tier 5: 2 Cruisers and 1 Battleship

                // Randomly adjust but only slightly the amount of guns and armor each ship has
                // Some ships do not have weapon or armor slots in certain locations and that's
                // why they are hard-marked as 0.

                switch (tier)
                {
                    case 1:
                        numOfScouts = 1;
                        break;
                    case 2:
                        // 50% chance for each composition
                        if (UnityEngine.Random.value > 0.5f)
                        {
                            numOfScouts = 1;
                            numOfPatrols = 1;
                        }
                        else
                        {
                            numOfScouts = 2;
                        }
                        break;
                    case 3:
                        // 50% chance for each composition
                        if (UnityEngine.Random.value > 0.5f)
                        {
                            numOfPatrols = 1;
                            numOfCruisers = 1;
                        }
                        else
                        {
                            numOfPatrols = 2;
                        }
                        break;
                    case 4:
                        // 50% chance for each composition
                        if (UnityEngine.Random.value > 0.5f)
                        {
                            numOfCruisers = 1;
                            numOfBattleships = 1;
                        }
                        else
                        {
                            numOfCruisers = 2;
                        }
                        break;
                    case 5:
                        numOfCruisers = 2;
                        numOfBattleships = 1;
                        break;
                    default:
                        throw new ArgumentException("Invalid tier");
                }

                // Randomly adjust but only slightly the amount of guns and armor each ship has
                gunsT0 = UnityEngine.Random.Range(1, 3); // Example range
                gunsT1 = UnityEngine.Random.Range(0, 2);
                gunsT2 = UnityEngine.Random.Range(0, 2);
                gunsT3 = UnityEngine.Random.Range(0, 2);
                armorsT1 = UnityEngine.Random.Range(0, 2);
                armorsT2 = UnityEngine.Random.Range(0, 2);

                // Adding ships to the fleet
                for (int i = 0; i < numOfScouts; i++)
                {
                    battleFleet.AddEntity(CreateFleetShip(Ids.Fleet.Hulls.Scout, gunsT0, gunsT1, 0, 0, 0, 0));
                }
                for (int i = 0; i < numOfPatrols; i++)
                {
                    battleFleet.AddEntity(CreateFleetShip(Ids.Fleet.Hulls.Patrol, gunsT0, gunsT1, gunsT2, 0, armorsT1, armorsT2));
                }
                for (int i = 0; i < numOfCruisers; i++)
                {
                    battleFleet.AddEntity(CreateFleetShip(Ids.Fleet.Hulls.Cruiser, gunsT0, gunsT1, gunsT2, gunsT3, armorsT1, armorsT2));
                }
                for (int i = 0; i < numOfBattleships; i++)
                {
                    battleFleet.AddEntity(CreateFleetShip(Ids.Fleet.Hulls.Battleship, gunsT0, gunsT1, gunsT2, gunsT3, armorsT1, armorsT2));
                }

                SetEnemyProto(location, battleFleet);
            }
            

            private FleetEntity CreateFleetShip(FleetEntityHullProto.ID hullId, int gunsT0 = 0, int gunsT1 = 0, int gunsT2 = 0, int gunsT3 = 0, int armorsT1 = 0, int armorsT2 = 0)
            {
                FleetEntityHullProto orThrow = m_protosDb.GetOrThrow<FleetEntityHullProto>(hullId);
                Lyst<FleetWeaponProto> lyst = new Lyst<FleetWeaponProto>();
                lyst.AddRepeated(m_protosDb.GetOrThrow<FleetWeaponProto>(Ids.Fleet.Weapons.Gun0), gunsT0);
                lyst.AddRepeated(m_protosDb.GetOrThrow<FleetWeaponProto>(Ids.Fleet.Weapons.Gun1), gunsT1);
                lyst.AddRepeated(m_protosDb.GetOrThrow<FleetWeaponProto>(Ids.Fleet.Weapons.Gun2), gunsT2);
                lyst.AddRepeated(m_protosDb.GetOrThrow<FleetWeaponProto>(Ids.Fleet.Weapons.Gun3), gunsT3);
                Lyst<FleetEntityPartProto> lyst2 = new Lyst<FleetEntityPartProto>();
                lyst2.AddRepeated(m_protosDb.GetOrThrow<FleetEntityPartProto>(Ids.Fleet.Armor.ArmorT1), armorsT1);
                lyst2.AddRepeated(m_protosDb.GetOrThrow<FleetEntityPartProto>(Ids.Fleet.Armor.ArmorT2), armorsT2);
                return new FleetEntity(orThrow, lyst.ToImmutableArray(), lyst2.ToImmutableArray());
            }

            private IEnumerable<FleetWeaponProto> CreateWeapons(int count, FleetWeaponProto.ID weaponId)
            {
                for (int i = 0; i < count; i++)
                {
                    yield return m_protosDb.GetOrThrow<FleetWeaponProto>(weaponId);
                }
            }

            private IEnumerable<FleetEntityPartProto> CreateArmors(int count, FleetEntityPartProto.ID armorId)
            {
                for (int i = 0; i < count; i++)
                {
                    yield return m_protosDb.GetOrThrow<FleetEntityPartProto>(armorId);
                }
            }


            public void FindPlaceToPutEntity(EntityProto.ID entityId, int minDistanceFromHome, int maxDistanceFromHome)
            {
                List<WorldMapLocation> suitableLocations = new List<WorldMapLocation>();

                foreach (KeyValuePair<string, WorldMapLocation> locationEntry in WorldMapLocationsHolder.Locations)
                {
                    WorldMapLocation locationValue = locationEntry.Value;

                    if (locationValue == null || locationValue.Entity.HasValue || locationValue.EntityProto.HasValue)
                        continue;

                    double distanceFromHome = CalculateDistanceFromHome(locationValue.Position);
                    if (distanceFromHome >= minDistanceFromHome && distanceFromHome <= maxDistanceFromHome)
                    {
                        suitableLocations.Add(locationValue);
                    }
                }

                if (suitableLocations.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, suitableLocations.Count); // Use UnityEngine.Random
                    WorldMapLocation selectedLocation = suitableLocations[randomIndex];

                    Debug.Log($"Placing: {entityId} at {selectedLocation}");
                    SetEntityProto(entityId, selectedLocation);
                }
                else
                {
                    Debug.Log($"No suitable location found for entity: {entityId}");
                }
            }



            public void FindPlaceToPutEntityPair(EntityProto.ID entityId, TechnologyProto.ID techId, int minDistanceFromHome, int maxDistanceFromHome)
            {
                foreach (KeyValuePair<string, WorldMapLocation> locationEntry in WorldMapLocationsHolder.Locations)
                {
                    string locationKey = locationEntry.Key; // This is the key in the dictionary, e.g., "Location1"
                    WorldMapLocation locationValue = locationEntry.Value; // This is the actual WorldMapLocation object


                    if (locationValue == null)
                        continue;

                    if (locationValue.Entity.HasValue || locationValue.EntityProto.HasValue)
                        continue;

                    double distanceFromHome = CalculateDistanceFromHome(locationValue.Position);
                    if (distanceFromHome >= minDistanceFromHome && distanceFromHome <= maxDistanceFromHome)
                    {
                        Debug.Log($"Placing: {entityId} {techId}");
                        SetEntityProto(entityId, locationValue);
                        SetTechnologyProto(techId, locationValue);
                        return; // Early return after setting the technology
                    }
                }

                Debug.Log($"No suitable location found for entity pair: {entityId} {techId}");
            }


            public void FindPlaceToPutTechnology(TechnologyProto.ID technologyId, int minDistanceFromHome, int maxDistanceFromHome)
            {
                foreach (KeyValuePair<string, WorldMapLocation> locationEntry in WorldMapLocationsHolder.Locations)
                {
                    string locationKey = locationEntry.Key; // This is the key in the dictionary, e.g., "Location1"
                    WorldMapLocation locationValue = locationEntry.Value; // This is the actual WorldMapLocation object


                    if (locationValue == null)
                        continue;

                    if (locationValue.Entity.HasValue || locationValue.EntityProto.HasValue)
                        continue;

                    double distanceFromHome = CalculateDistanceFromHome(locationValue.Position);
                    if (distanceFromHome >= minDistanceFromHome && distanceFromHome <= maxDistanceFromHome)
                    {
                        Debug.Log($"Placing: {technologyId}");
                        SetTechnologyProto(technologyId, locationValue);
                        return; // Early return after setting the technology
                    }
                }

                Debug.Log($"No suitable location found for technology: {technologyId}");
            }


            public float CalculateDistanceFromHome(Vector2i targetPosition)
            {
                Vector2i homePosition = new Vector2i(1860, 2717);
                int deltaX = targetPosition.X - homePosition.X;
                int deltaY = targetPosition.Y - homePosition.Y;
                float distance = (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                return distance;
            }

        }

        private readonly ProtosDb m_protosDb;

        public NewMapGenerator(ProtosDb protosDb)
        {
            m_protosDb= protosDb;

        }
public WorldMap CreateWorldMap()
         {
            ProtosDb protosDb = m_protosDb;
            WorldMap worldMap = new WorldMap();



            // New locations
            WorldMapLocation worldMapLocation1 = new WorldMapLocation("COI: Extended (Home)", new Vector2i(2495, 2955));
            worldMap.AddLocation(worldMapLocation1, false);
            WorldMapLocation worldMapLocation2 = new WorldMapLocation("Location 1", new Vector2i(1835, 658));
            worldMap.AddLocation(worldMapLocation2, false);
            WorldMapLocation worldMapLocation3 = new WorldMapLocation("Location 2", new Vector2i(250, 2775));
            worldMap.AddLocation(worldMapLocation3, false);
            WorldMapLocation worldMapLocation4 = new WorldMapLocation("Location 3", new Vector2i(1219, 2225));
            worldMap.AddLocation(worldMapLocation4, false);
            WorldMapLocation worldMapLocation5 = new WorldMapLocation("Location 4", new Vector2i(1111, 1075));
            worldMap.AddLocation(worldMapLocation5, false);
            WorldMapLocation worldMapLocation6 = new WorldMapLocation("Location 5", new Vector2i(3000, 2754));
            worldMap.AddLocation(worldMapLocation6, false);
            WorldMapLocation worldMapLocation7 = new WorldMapLocation("Location 6", new Vector2i(1822, 2932));
            worldMap.AddLocation(worldMapLocation7, false);
            WorldMapLocation worldMapLocation8 = new WorldMapLocation("Location 7", new Vector2i(2396, 353));
            worldMap.AddLocation(worldMapLocation8, false);
            WorldMapLocation worldMapLocation9 = new WorldMapLocation("Location 8", new Vector2i(1457, 3014));
            worldMap.AddLocation(worldMapLocation9, false);
            WorldMapLocation worldMapLocation10 = new WorldMapLocation("Location 9", new Vector2i(2858, 2416));
            worldMap.AddLocation(worldMapLocation10, false);
            WorldMapLocation worldMapLocation11 = new WorldMapLocation("Location 10", new Vector2i(2148, 695));
            worldMap.AddLocation(worldMapLocation11, false);
            WorldMapLocation worldMapLocation12 = new WorldMapLocation("Location 11", new Vector2i(577, 3901));
            worldMap.AddLocation(worldMapLocation12, false);
            WorldMapLocation worldMapLocation13 = new WorldMapLocation("Location 12", new Vector2i(3203, 1905));
            worldMap.AddLocation(worldMapLocation13, false);
            WorldMapLocation worldMapLocation14 = new WorldMapLocation("Location 13", new Vector2i(296, 451));
            worldMap.AddLocation(worldMapLocation14, false);
            WorldMapLocation worldMapLocation15 = new WorldMapLocation("Location 14", new Vector2i(253, 1356));
            worldMap.AddLocation(worldMapLocation15, false);
            WorldMapLocation worldMapLocation16 = new WorldMapLocation("Location 15", new Vector2i(989, 3678));
            worldMap.AddLocation(worldMapLocation16, false);
            WorldMapLocation worldMapLocation17 = new WorldMapLocation("Location 16", new Vector2i(527, 2448));
            worldMap.AddLocation(worldMapLocation17, false);
            WorldMapLocation worldMapLocation18 = new WorldMapLocation("Location 17", new Vector2i(2752, 3158));
            worldMap.AddLocation(worldMapLocation18, false);
            WorldMapLocation worldMapLocation19 = new WorldMapLocation("Location 18", new Vector2i(2104, 2057));
            worldMap.AddLocation(worldMapLocation19, false);
            WorldMapLocation worldMapLocation20 = new WorldMapLocation("Location 19", new Vector2i(387, 1832));
            worldMap.AddLocation(worldMapLocation20, false);
            WorldMapLocation worldMapLocation21 = new WorldMapLocation("Location 20", new Vector2i(2329, 2664));
            worldMap.AddLocation(worldMapLocation21, false);
            WorldMapLocation worldMapLocation22 = new WorldMapLocation("Location 21", new Vector2i(2344, 1850));
            worldMap.AddLocation(worldMapLocation22, false);
            WorldMapLocation worldMapLocation23 = new WorldMapLocation("Location 22", new Vector2i(2528, 818));
            worldMap.AddLocation(worldMapLocation23, false);
            WorldMapLocation worldMapLocation24 = new WorldMapLocation("Location 23", new Vector2i(871, 1694));
            worldMap.AddLocation(worldMapLocation24, false);
            WorldMapLocation worldMapLocation25 = new WorldMapLocation("Location 24", new Vector2i(2982, 1141));
            worldMap.AddLocation(worldMapLocation25, false);
            WorldMapLocation worldMapLocation26 = new WorldMapLocation("Location 25", new Vector2i(1992, 2668));
            worldMap.AddLocation(worldMapLocation26, false);
            WorldMapLocation worldMapLocation27 = new WorldMapLocation("Location 26", new Vector2i(1892, 1368));
            worldMap.AddLocation(worldMapLocation27, false);
            WorldMapLocation worldMapLocation28 = new WorldMapLocation("Location 27", new Vector2i(3509, 1551));
            worldMap.AddLocation(worldMapLocation28, false);
            WorldMapLocation worldMapLocation29 = new WorldMapLocation("Location 28", new Vector2i(2308, 1539));
            worldMap.AddLocation(worldMapLocation29, false);
            WorldMapLocation worldMapLocation30 = new WorldMapLocation("Location 29", new Vector2i(1300, 3780));
            worldMap.AddLocation(worldMapLocation30, false);
            WorldMapLocation worldMapLocation31 = new WorldMapLocation("Location 30", new Vector2i(177, 3126));
            worldMap.AddLocation(worldMapLocation31, false);
            WorldMapLocation worldMapLocation32 = new WorldMapLocation("Location 31", new Vector2i(753, 763));
            worldMap.AddLocation(worldMapLocation32, false);
            WorldMapLocation worldMapLocation33 = new WorldMapLocation("Location 32", new Vector2i(2115, 3282));
            worldMap.AddLocation(worldMapLocation33, false);
            WorldMapLocation worldMapLocation34 = new WorldMapLocation("Location 33", new Vector2i(1461, 403));
            worldMap.AddLocation(worldMapLocation34, false);
            WorldMapLocation worldMapLocation35 = new WorldMapLocation("Location 34", new Vector2i(540, 1058));
            worldMap.AddLocation(worldMapLocation35, false);
            WorldMapLocation worldMapLocation36 = new WorldMapLocation("Location 35", new Vector2i(2790, 3514));
            worldMap.AddLocation(worldMapLocation36, false);
            WorldMapLocation worldMapLocation37 = new WorldMapLocation("Location 36", new Vector2i(848, 2395));
            worldMap.AddLocation(worldMapLocation37, false);
            WorldMapLocation worldMapLocation38 = new WorldMapLocation("Location 37", new Vector2i(1412, 2594));
            worldMap.AddLocation(worldMapLocation38, false);
            WorldMapLocation worldMapLocation39 = new WorldMapLocation("Location 38", new Vector2i(839, 3159));
            worldMap.AddLocation(worldMapLocation39, false);
            WorldMapLocation worldMapLocation40 = new WorldMapLocation("Location 39", new Vector2i(3397, 2891));
            worldMap.AddLocation(worldMapLocation40, false);
            WorldMapLocation worldMapLocation41 = new WorldMapLocation("Location 40", new Vector2i(1591, 3614));
            worldMap.AddLocation(worldMapLocation41, false);
            WorldMapLocation worldMapLocation42 = new WorldMapLocation("Location 41", new Vector2i(2135, 3723));
            worldMap.AddLocation(worldMapLocation42, false);
            WorldMapLocation worldMapLocation43 = new WorldMapLocation("Location 42", new Vector2i(228, 2308));
            worldMap.AddLocation(worldMapLocation43, false);
            WorldMapLocation worldMapLocation44 = new WorldMapLocation("Location 43", new Vector2i(1549, 860));
            worldMap.AddLocation(worldMapLocation44, false);
            WorldMapLocation worldMapLocation45 = new WorldMapLocation("Location 44", new Vector2i(1467, 1785));
            worldMap.AddLocation(worldMapLocation45, false);
            WorldMapLocation worldMapLocation46 = new WorldMapLocation("Location 45", new Vector2i(2840, 1742));
            worldMap.AddLocation(worldMapLocation46, false);
            WorldMapLocation worldMapLocation47 = new WorldMapLocation("Location 46", new Vector2i(747, 2789));
            worldMap.AddLocation(worldMapLocation47, false);
            WorldMapLocation worldMapLocation48 = new WorldMapLocation("Location 47", new Vector2i(2200, 1152));
            worldMap.AddLocation(worldMapLocation48, false);
            WorldMapLocation worldMapLocation49 = new WorldMapLocation("Location 48", new Vector2i(3534, 2552));
            worldMap.AddLocation(worldMapLocation49, false);
            WorldMapLocation worldMapLocation50 = new WorldMapLocation("Location 49", new Vector2i(1116, 77));
            worldMap.AddLocation(worldMapLocation50, false);
            WorldMapLocation worldMapLocation51 = new WorldMapLocation("Location 50", new Vector2i(1314, 3402));
            worldMap.AddLocation(worldMapLocation51, false);
            WorldMapLocation worldMapLocation52 = new WorldMapLocation("Location 51", new Vector2i(86, 3507));
            worldMap.AddLocation(worldMapLocation52, false);
            WorldMapLocation worldMapLocation53 = new WorldMapLocation("Location 52", new Vector2i(2834, 775));
            worldMap.AddLocation(worldMapLocation53, false);
            WorldMapLocation worldMapLocation54 = new WorldMapLocation("Location 53", new Vector2i(1681, 2414));
            worldMap.AddLocation(worldMapLocation54, false);
            WorldMapLocation worldMapLocation55 = new WorldMapLocation("Location 54", new Vector2i(2029, 1762));
            worldMap.AddLocation(worldMapLocation55, false);
            WorldMapLocation worldMapLocation56 = new WorldMapLocation("Location 55", new Vector2i(2483, 3486));
            worldMap.AddLocation(worldMapLocation56, false);
            WorldMapLocation worldMapLocation57 = new WorldMapLocation("Location 56", new Vector2i(693, 1369));
            worldMap.AddLocation(worldMapLocation57, false);
            WorldMapLocation worldMapLocation58 = new WorldMapLocation("Location 57", new Vector2i(683, 3610));
            worldMap.AddLocation(worldMapLocation58, false);
            WorldMapLocation worldMapLocation59 = new WorldMapLocation("Location 58", new Vector2i(1909, 992));
            worldMap.AddLocation(worldMapLocation59, false);
            WorldMapLocation worldMapLocation60 = new WorldMapLocation("Location 59", new Vector2i(1763, 316));
            worldMap.AddLocation(worldMapLocation60, false);
            WorldMapLocation worldMapLocation61 = new WorldMapLocation("Location 60", new Vector2i(1565, 3932));
            worldMap.AddLocation(worldMapLocation61, false);
            WorldMapLocation worldMapLocation62 = new WorldMapLocation("Location 61", new Vector2i(1179, 695));
            worldMap.AddLocation(worldMapLocation62, false);
            WorldMapLocation worldMapLocation63 = new WorldMapLocation("Location 62", new Vector2i(3472, 2150));
            worldMap.AddLocation(worldMapLocation63, false);
            WorldMapLocation worldMapLocation64 = new WorldMapLocation("Location 63", new Vector2i(2075, 226));
            worldMap.AddLocation(worldMapLocation64, false);
            WorldMapLocation worldMapLocation65 = new WorldMapLocation("Location 64", new Vector2i(2166, 2944));
            worldMap.AddLocation(worldMapLocation65, false);
            WorldMapLocation worldMapLocation66 = new WorldMapLocation("Location 65", new Vector2i(876, 2013));
            worldMap.AddLocation(worldMapLocation66, false);
            WorldMapLocation worldMapLocation67 = new WorldMapLocation("Location 66", new Vector2i(751, 261));
            worldMap.AddLocation(worldMapLocation67, false);
            WorldMapLocation worldMapLocation68 = new WorldMapLocation("Location 67", new Vector2i(1634, 1524));
            worldMap.AddLocation(worldMapLocation68, false);
            WorldMapLocation worldMapLocation69 = new WorldMapLocation("Location 68", new Vector2i(2495, 2144));
            worldMap.AddLocation(worldMapLocation69, false);
            WorldMapLocation worldMapLocation70 = new WorldMapLocation("Location 69", new Vector2i(91, 810));
            worldMap.AddLocation(worldMapLocation70, false);
            WorldMapLocation worldMapLocation71 = new WorldMapLocation("Location 70", new Vector2i(2607, 1480));
            worldMap.AddLocation(worldMapLocation71, false);
            WorldMapLocation worldMapLocation72 = new WorldMapLocation("Location 71", new Vector2i(2802, 2053));
            worldMap.AddLocation(worldMapLocation72, false);
            WorldMapLocation worldMapLocation73 = new WorldMapLocation("Location 72", new Vector2i(3093, 1499));
            worldMap.AddLocation(worldMapLocation73, false);
            WorldMapLocation worldMapLocation74 = new WorldMapLocation("Location 73", new Vector2i(3639, 1828));
            worldMap.AddLocation(worldMapLocation74, false);
            WorldMapLocation worldMapLocation75 = new WorldMapLocation("Location 74", new Vector2i(1804, 2100));
            worldMap.AddLocation(worldMapLocation75, false);
            WorldMapLocation worldMapLocation76 = new WorldMapLocation("Location 75", new Vector2i(2468, 3788));
            worldMap.AddLocation(worldMapLocation76, false);
            WorldMapLocation worldMapLocation77 = new WorldMapLocation("Location 76", new Vector2i(1083, 2659));
            worldMap.AddLocation(worldMapLocation77, false);
            WorldMapLocation worldMapLocation78 = new WorldMapLocation("Location 77", new Vector2i(1865, 3868));
            worldMap.AddLocation(worldMapLocation78, false);
            WorldMapLocation worldMapLocation79 = new WorldMapLocation("Location 78", new Vector2i(397, 3407));
            worldMap.AddLocation(worldMapLocation79, false);
            WorldMapLocation worldMapLocation80 = new WorldMapLocation("Location 79", new Vector2i(1180, 1601));
            worldMap.AddLocation(worldMapLocation80, false);
            WorldMapLocation worldMapLocation81 = new WorldMapLocation("Location 80", new Vector2i(3311, 1154));
            worldMap.AddLocation(worldMapLocation81, false);
            WorldMapLocation worldMapLocation82 = new WorldMapLocation("Location 81", new Vector2i(1149, 2985));
            worldMap.AddLocation(worldMapLocation82, false);
            WorldMapLocation worldMapLocation83 = new WorldMapLocation("Location 82", new Vector2i(72, 1598));
            worldMap.AddLocation(worldMapLocation83, false);
            WorldMapLocation worldMapLocation84 = new WorldMapLocation("Location 83", new Vector2i(1561, 1189));
            worldMap.AddLocation(worldMapLocation84, false);
            WorldMapLocation worldMapLocation85 = new WorldMapLocation("Location 84", new Vector2i(67, 1921));
            worldMap.AddLocation(worldMapLocation85, false);
            WorldMapLocation worldMapLocation86 = new WorldMapLocation("Location 85", new Vector2i(1776, 3301));
            worldMap.AddLocation(worldMapLocation86, false);
            WorldMapLocation worldMapLocation87 = new WorldMapLocation("Location 86", new Vector2i(2605, 1151));
            worldMap.AddLocation(worldMapLocation87, false);
            WorldMapLocation worldMapLocation88 = new WorldMapLocation("Location 87", new Vector2i(3085, 3118));
            worldMap.AddLocation(worldMapLocation88, false);
            WorldMapLocation worldMapLocation89 = new WorldMapLocation("Location 88", new Vector2i(3248, 2448));
            worldMap.AddLocation(worldMapLocation89, false);
            WorldMapLocation worldMapLocation90 = new WorldMapLocation("Location 89", new Vector2i(3734, 2308));
            worldMap.AddLocation(worldMapLocation90, false);
            WorldMapLocation worldMapLocation91 = new WorldMapLocation("Location 90", new Vector2i(50, 2550));
            worldMap.AddLocation(worldMapLocation91, false);
            WorldMapLocation worldMapLocation92 = new WorldMapLocation("Location 91", new Vector2i(2229, 2338));
            worldMap.AddLocation(worldMapLocation92, false);
            WorldMapLocation worldMapLocation93 = new WorldMapLocation("Location 92", new Vector2i(518, 2109));
            worldMap.AddLocation(worldMapLocation93, false);
            WorldMapLocation worldMapLocation94 = new WorldMapLocation("Location 93", new Vector2i(2647, 2647));
            worldMap.AddLocation(worldMapLocation94, false);
            WorldMapLocation worldMapLocation95 = new WorldMapLocation("Location 94", new Vector2i(932, 517));
            worldMap.AddLocation(worldMapLocation95, false);
            WorldMapLocation worldMapLocation96 = new WorldMapLocation("Location 95", new Vector2i(1496, 62));
            worldMap.AddLocation(worldMapLocation96, false);
            WorldMapLocation worldMapLocation97 = new WorldMapLocation("Location 96", new Vector2i(534, 3063));
            worldMap.AddLocation(worldMapLocation97, false);
            WorldMapLocation worldMapLocation98 = new WorldMapLocation("Location 97", new Vector2i(313, 3728));
            worldMap.AddLocation(worldMapLocation98, false);
            WorldMapLocation worldMapLocation99 = new WorldMapLocation("Location 98", new Vector2i(2652, 523));
            worldMap.AddLocation(worldMapLocation99, false);
            WorldMapLocation worldMapLocation100 = new WorldMapLocation("Location 99", new Vector2i(1156, 1902));
            worldMap.AddLocation(worldMapLocation100, false);
            WorldMapLocation worldMapLocation101 = new WorldMapLocation("Location 100", new Vector2i(79, 1111));
            worldMap.AddLocation(worldMapLocation101, false);
            worldMap.AddConnection(worldMapLocation1, worldMapLocation18);
            worldMap.AddConnection(worldMapLocation1, worldMapLocation65);
            worldMap.AddConnection(worldMapLocation1, worldMapLocation21);
            worldMap.AddConnection(worldMapLocation1, worldMapLocation94);
            worldMap.AddConnection(worldMapLocation2, worldMapLocation11);
            worldMap.AddConnection(worldMapLocation2, worldMapLocation59);
            worldMap.AddConnection(worldMapLocation2, worldMapLocation60);
            worldMap.AddConnection(worldMapLocation2, worldMapLocation44);
            worldMap.AddConnection(worldMapLocation3, worldMapLocation91);
            worldMap.AddConnection(worldMapLocation3, worldMapLocation31);
            worldMap.AddConnection(worldMapLocation3, worldMapLocation97);
            worldMap.AddConnection(worldMapLocation3, worldMapLocation17);
            worldMap.AddConnection(worldMapLocation4, worldMapLocation100);
            worldMap.AddConnection(worldMapLocation4, worldMapLocation66);
            worldMap.AddConnection(worldMapLocation4, worldMapLocation37);
            worldMap.AddConnection(worldMapLocation4, worldMapLocation38);
            worldMap.AddConnection(worldMapLocation5, worldMapLocation62);
            worldMap.AddConnection(worldMapLocation5, worldMapLocation84);
            worldMap.AddConnection(worldMapLocation5, worldMapLocation32);
            worldMap.AddConnection(worldMapLocation5, worldMapLocation44);
            worldMap.AddConnection(worldMapLocation6, worldMapLocation10);
            worldMap.AddConnection(worldMapLocation6, worldMapLocation94);
            worldMap.AddConnection(worldMapLocation6, worldMapLocation88);
            worldMap.AddConnection(worldMapLocation6, worldMapLocation89);
            worldMap.AddConnection(worldMapLocation7, worldMapLocation26);
            worldMap.AddConnection(worldMapLocation7, worldMapLocation65);
            worldMap.AddConnection(worldMapLocation7, worldMapLocation86);
            worldMap.AddConnection(worldMapLocation7, worldMapLocation9);
            worldMap.AddConnection(worldMapLocation8, worldMapLocation99);
            worldMap.AddConnection(worldMapLocation8, worldMapLocation64);
            worldMap.AddConnection(worldMapLocation8, worldMapLocation11);
            worldMap.AddConnection(worldMapLocation8, worldMapLocation23);
            worldMap.AddConnection(worldMapLocation9, worldMapLocation7);
            worldMap.AddConnection(worldMapLocation9, worldMapLocation82);
            worldMap.AddConnection(worldMapLocation9, worldMapLocation51);
            worldMap.AddConnection(worldMapLocation10, worldMapLocation6);
            worldMap.AddConnection(worldMapLocation10, worldMapLocation72);
            worldMap.AddConnection(worldMapLocation11, worldMapLocation2);
            worldMap.AddConnection(worldMapLocation11, worldMapLocation8);
            worldMap.AddConnection(worldMapLocation12, worldMapLocation58);
            worldMap.AddConnection(worldMapLocation12, worldMapLocation98);
            worldMap.AddConnection(worldMapLocation12, worldMapLocation16);
            worldMap.AddConnection(worldMapLocation12, worldMapLocation79);
            worldMap.AddConnection(worldMapLocation13, worldMapLocation63);
            worldMap.AddConnection(worldMapLocation13, worldMapLocation46);
            worldMap.AddConnection(worldMapLocation13, worldMapLocation73);
            worldMap.AddConnection(worldMapLocation13, worldMapLocation72);
            worldMap.AddConnection(worldMapLocation14, worldMapLocation70);
            worldMap.AddConnection(worldMapLocation14, worldMapLocation67);
            worldMap.AddConnection(worldMapLocation14, worldMapLocation32);
            worldMap.AddConnection(worldMapLocation14, worldMapLocation95);
            worldMap.AddConnection(worldMapLocation15, worldMapLocation101);
            worldMap.AddConnection(worldMapLocation15, worldMapLocation83);
            worldMap.AddConnection(worldMapLocation15, worldMapLocation35);
            worldMap.AddConnection(worldMapLocation15, worldMapLocation57);
            worldMap.AddConnection(worldMapLocation16, worldMapLocation12);
            worldMap.AddConnection(worldMapLocation16, worldMapLocation30);
            worldMap.AddConnection(worldMapLocation16, worldMapLocation51);
            worldMap.AddConnection(worldMapLocation17, worldMapLocation3);
            worldMap.AddConnection(worldMapLocation17, worldMapLocation37);
            worldMap.AddConnection(worldMapLocation17, worldMapLocation43);
            worldMap.AddConnection(worldMapLocation17, worldMapLocation93);
            worldMap.AddConnection(worldMapLocation18, worldMapLocation1);
            worldMap.AddConnection(worldMapLocation18, worldMapLocation36);
            worldMap.AddConnection(worldMapLocation19, worldMapLocation75);
            worldMap.AddConnection(worldMapLocation19, worldMapLocation55);
            worldMap.AddConnection(worldMapLocation19, worldMapLocation92);
            worldMap.AddConnection(worldMapLocation19, worldMapLocation22);
            worldMap.AddConnection(worldMapLocation20, worldMapLocation93);
            worldMap.AddConnection(worldMapLocation20, worldMapLocation85);
            worldMap.AddConnection(worldMapLocation20, worldMapLocation83);
            worldMap.AddConnection(worldMapLocation21, worldMapLocation1);
            worldMap.AddConnection(worldMapLocation22, worldMapLocation19);
            worldMap.AddConnection(worldMapLocation22, worldMapLocation29);
            worldMap.AddConnection(worldMapLocation23, worldMapLocation8);
            worldMap.AddConnection(worldMapLocation23, worldMapLocation53);
            worldMap.AddConnection(worldMapLocation23, worldMapLocation87);
            worldMap.AddConnection(worldMapLocation24, worldMapLocation66);
            worldMap.AddConnection(worldMapLocation24, worldMapLocation80);
            worldMap.AddConnection(worldMapLocation25, worldMapLocation81);
            worldMap.AddConnection(worldMapLocation25, worldMapLocation73);
            worldMap.AddConnection(worldMapLocation25, worldMapLocation87);
            worldMap.AddConnection(worldMapLocation26, worldMapLocation7);
            worldMap.AddConnection(worldMapLocation27, worldMapLocation68);
            worldMap.AddConnection(worldMapLocation27, worldMapLocation48);
            worldMap.AddConnection(worldMapLocation27, worldMapLocation84);
            worldMap.AddConnection(worldMapLocation28, worldMapLocation74);
            worldMap.AddConnection(worldMapLocation28, worldMapLocation73);
            worldMap.AddConnection(worldMapLocation29, worldMapLocation22);
            worldMap.AddConnection(worldMapLocation29, worldMapLocation71);
            worldMap.AddConnection(worldMapLocation30, worldMapLocation16);
            worldMap.AddConnection(worldMapLocation30, worldMapLocation61);
            worldMap.AddConnection(worldMapLocation30, worldMapLocation41);
            worldMap.AddConnection(worldMapLocation31, worldMapLocation3);
            worldMap.AddConnection(worldMapLocation31, worldMapLocation79);
            worldMap.AddConnection(worldMapLocation32, worldMapLocation5);
            worldMap.AddConnection(worldMapLocation32, worldMapLocation14);
            worldMap.AddConnection(worldMapLocation33, worldMapLocation86);
            worldMap.AddConnection(worldMapLocation33, worldMapLocation56);
            worldMap.AddConnection(worldMapLocation33, worldMapLocation42);
            worldMap.AddConnection(worldMapLocation34, worldMapLocation60);
            worldMap.AddConnection(worldMapLocation34, worldMapLocation96);
            worldMap.AddConnection(worldMapLocation35, worldMapLocation15);
            worldMap.AddConnection(worldMapLocation36, worldMapLocation18);
            worldMap.AddConnection(worldMapLocation36, worldMapLocation76);
            worldMap.AddConnection(worldMapLocation37, worldMapLocation4);
            worldMap.AddConnection(worldMapLocation37, worldMapLocation17);
            worldMap.AddConnection(worldMapLocation37, worldMapLocation77);
            worldMap.AddConnection(worldMapLocation38, worldMapLocation4);
            worldMap.AddConnection(worldMapLocation38, worldMapLocation54);
            worldMap.AddConnection(worldMapLocation39, worldMapLocation97);
            worldMap.AddConnection(worldMapLocation39, worldMapLocation47);
            worldMap.AddConnection(worldMapLocation40, worldMapLocation49);
            worldMap.AddConnection(worldMapLocation40, worldMapLocation88);
            worldMap.AddConnection(worldMapLocation41, worldMapLocation30);
            worldMap.AddConnection(worldMapLocation42, worldMapLocation33);
            worldMap.AddConnection(worldMapLocation42, worldMapLocation78);
            worldMap.AddConnection(worldMapLocation43, worldMapLocation17);
            worldMap.AddConnection(worldMapLocation44, worldMapLocation2);
            worldMap.AddConnection(worldMapLocation44, worldMapLocation5);
            worldMap.AddConnection(worldMapLocation45, worldMapLocation68);
            worldMap.AddConnection(worldMapLocation45, worldMapLocation75);
            worldMap.AddConnection(worldMapLocation46, worldMapLocation13);
            worldMap.AddConnection(worldMapLocation47, worldMapLocation39);
            worldMap.AddConnection(worldMapLocation48, worldMapLocation27);
            worldMap.AddConnection(worldMapLocation49, worldMapLocation40);
            worldMap.AddConnection(worldMapLocation49, worldMapLocation90);
            worldMap.AddConnection(worldMapLocation50, worldMapLocation96);
            worldMap.AddConnection(worldMapLocation51, worldMapLocation9);
            worldMap.AddConnection(worldMapLocation51, worldMapLocation16);
            worldMap.AddConnection(worldMapLocation52, worldMapLocation98);
            worldMap.AddConnection(worldMapLocation53, worldMapLocation23);
            worldMap.AddConnection(worldMapLocation54, worldMapLocation38);
            worldMap.AddConnection(worldMapLocation55, worldMapLocation19);
            worldMap.AddConnection(worldMapLocation56, worldMapLocation33);
            worldMap.AddConnection(worldMapLocation57, worldMapLocation15);
            worldMap.AddConnection(worldMapLocation58, worldMapLocation12);
            worldMap.AddConnection(worldMapLocation59, worldMapLocation2);
            worldMap.AddConnection(worldMapLocation60, worldMapLocation2);
            worldMap.AddConnection(worldMapLocation60, worldMapLocation34);
            worldMap.AddConnection(worldMapLocation61, worldMapLocation30);
            worldMap.AddConnection(worldMapLocation62, worldMapLocation5);
            worldMap.AddConnection(worldMapLocation63, worldMapLocation13);
            worldMap.AddConnection(worldMapLocation64, worldMapLocation8);
            worldMap.AddConnection(worldMapLocation65, worldMapLocation1);
            worldMap.AddConnection(worldMapLocation65, worldMapLocation7);
            worldMap.AddConnection(worldMapLocation66, worldMapLocation4);
            worldMap.AddConnection(worldMapLocation66, worldMapLocation24);
            worldMap.AddConnection(worldMapLocation67, worldMapLocation14);
            worldMap.AddConnection(worldMapLocation68, worldMapLocation27);
            worldMap.AddConnection(worldMapLocation68, worldMapLocation45);
            worldMap.AddConnection(worldMapLocation69, worldMapLocation72);
            worldMap.AddConnection(worldMapLocation70, worldMapLocation14);
            worldMap.AddConnection(worldMapLocation71, worldMapLocation29);
            worldMap.AddConnection(worldMapLocation72, worldMapLocation10);
            worldMap.AddConnection(worldMapLocation72, worldMapLocation13);
            worldMap.AddConnection(worldMapLocation72, worldMapLocation69);
            worldMap.AddConnection(worldMapLocation73, worldMapLocation13);
            worldMap.AddConnection(worldMapLocation73, worldMapLocation25);
            worldMap.AddConnection(worldMapLocation73, worldMapLocation28);
            worldMap.AddConnection(worldMapLocation74, worldMapLocation28);
            worldMap.AddConnection(worldMapLocation75, worldMapLocation19);
            worldMap.AddConnection(worldMapLocation75, worldMapLocation45);
            worldMap.AddConnection(worldMapLocation76, worldMapLocation36);
            worldMap.AddConnection(worldMapLocation77, worldMapLocation37);
            worldMap.AddConnection(worldMapLocation78, worldMapLocation42);
            worldMap.AddConnection(worldMapLocation79, worldMapLocation12);
            worldMap.AddConnection(worldMapLocation79, worldMapLocation31);
            worldMap.AddConnection(worldMapLocation80, worldMapLocation24);
            worldMap.AddConnection(worldMapLocation81, worldMapLocation25);
            worldMap.AddConnection(worldMapLocation82, worldMapLocation9);
            worldMap.AddConnection(worldMapLocation83, worldMapLocation15);
            worldMap.AddConnection(worldMapLocation83, worldMapLocation20);
            worldMap.AddConnection(worldMapLocation84, worldMapLocation5);
            worldMap.AddConnection(worldMapLocation84, worldMapLocation27);
            worldMap.AddConnection(worldMapLocation85, worldMapLocation20);
            worldMap.AddConnection(worldMapLocation86, worldMapLocation7);
            worldMap.AddConnection(worldMapLocation86, worldMapLocation33);
            worldMap.AddConnection(worldMapLocation87, worldMapLocation23);
            worldMap.AddConnection(worldMapLocation87, worldMapLocation25);
            worldMap.AddConnection(worldMapLocation88, worldMapLocation6);
            worldMap.AddConnection(worldMapLocation88, worldMapLocation40);
            worldMap.AddConnection(worldMapLocation89, worldMapLocation6);
            worldMap.AddConnection(worldMapLocation90, worldMapLocation49);
            worldMap.AddConnection(worldMapLocation91, worldMapLocation3);
            worldMap.AddConnection(worldMapLocation92, worldMapLocation19);
            worldMap.AddConnection(worldMapLocation93, worldMapLocation17);
            worldMap.AddConnection(worldMapLocation93, worldMapLocation20);
            worldMap.AddConnection(worldMapLocation94, worldMapLocation1);
            worldMap.AddConnection(worldMapLocation94, worldMapLocation6);
            worldMap.AddConnection(worldMapLocation95, worldMapLocation14);
            worldMap.AddConnection(worldMapLocation96, worldMapLocation34);
            worldMap.AddConnection(worldMapLocation96, worldMapLocation50);
            worldMap.AddConnection(worldMapLocation97, worldMapLocation3);
            worldMap.AddConnection(worldMapLocation97, worldMapLocation39);
            worldMap.AddConnection(worldMapLocation98, worldMapLocation12);
            worldMap.AddConnection(worldMapLocation98, worldMapLocation52);
            worldMap.AddConnection(worldMapLocation99, worldMapLocation8);
            worldMap.AddConnection(worldMapLocation100, worldMapLocation4);
            worldMap.AddConnection(worldMapLocation101, worldMapLocation15);
            


            Debug.Log("[COIE]: ADDING TO DICTIONARY"); // THIS NEVER CHANGES ITS FINE TO LEAVE IT
            WorldMapLocationsHolder.Locations.Add("Location1", worldMapLocation1);
            WorldMapLocationsHolder.Locations.Add("Location2", worldMapLocation2);
            WorldMapLocationsHolder.Locations.Add("Location3", worldMapLocation3);
            WorldMapLocationsHolder.Locations.Add("Location4", worldMapLocation4);
            WorldMapLocationsHolder.Locations.Add("Location5", worldMapLocation5);
            WorldMapLocationsHolder.Locations.Add("Location6", worldMapLocation6);
            WorldMapLocationsHolder.Locations.Add("Location7", worldMapLocation7);
            WorldMapLocationsHolder.Locations.Add("Location8", worldMapLocation8);
            WorldMapLocationsHolder.Locations.Add("Location9", worldMapLocation9);
            WorldMapLocationsHolder.Locations.Add("Location10", worldMapLocation10);
            WorldMapLocationsHolder.Locations.Add("Location11", worldMapLocation11);
            WorldMapLocationsHolder.Locations.Add("Location12", worldMapLocation12);
            WorldMapLocationsHolder.Locations.Add("Location13", worldMapLocation13);
            WorldMapLocationsHolder.Locations.Add("Location14", worldMapLocation14);
            WorldMapLocationsHolder.Locations.Add("Location15", worldMapLocation15);
            WorldMapLocationsHolder.Locations.Add("Location16", worldMapLocation16);
            WorldMapLocationsHolder.Locations.Add("Location17", worldMapLocation17);
            WorldMapLocationsHolder.Locations.Add("Location18", worldMapLocation18);
            WorldMapLocationsHolder.Locations.Add("Location19", worldMapLocation19);
            WorldMapLocationsHolder.Locations.Add("Location20", worldMapLocation20);
            WorldMapLocationsHolder.Locations.Add("Location21", worldMapLocation21);
            WorldMapLocationsHolder.Locations.Add("Location22", worldMapLocation22);
            WorldMapLocationsHolder.Locations.Add("Location23", worldMapLocation23);
            WorldMapLocationsHolder.Locations.Add("Location24", worldMapLocation24);
            WorldMapLocationsHolder.Locations.Add("Location25", worldMapLocation25);
            WorldMapLocationsHolder.Locations.Add("Location26", worldMapLocation26);
            WorldMapLocationsHolder.Locations.Add("Location27", worldMapLocation27);
            WorldMapLocationsHolder.Locations.Add("Location28", worldMapLocation28);
            WorldMapLocationsHolder.Locations.Add("Location29", worldMapLocation29);
            WorldMapLocationsHolder.Locations.Add("Location30", worldMapLocation30);
            WorldMapLocationsHolder.Locations.Add("Location31", worldMapLocation31);
            WorldMapLocationsHolder.Locations.Add("Location32", worldMapLocation32);
            WorldMapLocationsHolder.Locations.Add("Location33", worldMapLocation33);
            WorldMapLocationsHolder.Locations.Add("Location34", worldMapLocation34);
            WorldMapLocationsHolder.Locations.Add("Location35", worldMapLocation35);
            WorldMapLocationsHolder.Locations.Add("Location36", worldMapLocation36);
            WorldMapLocationsHolder.Locations.Add("Location37", worldMapLocation37);
            WorldMapLocationsHolder.Locations.Add("Location38", worldMapLocation38);
            WorldMapLocationsHolder.Locations.Add("Location39", worldMapLocation39);
            WorldMapLocationsHolder.Locations.Add("Location40", worldMapLocation40);
            WorldMapLocationsHolder.Locations.Add("Location41", worldMapLocation41);
            WorldMapLocationsHolder.Locations.Add("Location42", worldMapLocation42);
            WorldMapLocationsHolder.Locations.Add("Location43", worldMapLocation43);
            WorldMapLocationsHolder.Locations.Add("Location44", worldMapLocation44);
            WorldMapLocationsHolder.Locations.Add("Location45", worldMapLocation45);
            WorldMapLocationsHolder.Locations.Add("Location46", worldMapLocation46);
            WorldMapLocationsHolder.Locations.Add("Location47", worldMapLocation47);
            WorldMapLocationsHolder.Locations.Add("Location48", worldMapLocation48);
            WorldMapLocationsHolder.Locations.Add("Location49", worldMapLocation49);
            WorldMapLocationsHolder.Locations.Add("Location50", worldMapLocation50);
            WorldMapLocationsHolder.Locations.Add("Location51", worldMapLocation51);
            WorldMapLocationsHolder.Locations.Add("Location52", worldMapLocation52);
            WorldMapLocationsHolder.Locations.Add("Location53", worldMapLocation53);
            WorldMapLocationsHolder.Locations.Add("Location54", worldMapLocation54);
            WorldMapLocationsHolder.Locations.Add("Location55", worldMapLocation55);
            WorldMapLocationsHolder.Locations.Add("Location56", worldMapLocation56);
            WorldMapLocationsHolder.Locations.Add("Location57", worldMapLocation57);
            WorldMapLocationsHolder.Locations.Add("Location58", worldMapLocation58);
            WorldMapLocationsHolder.Locations.Add("Location59", worldMapLocation59);
            WorldMapLocationsHolder.Locations.Add("Location60", worldMapLocation60);
            WorldMapLocationsHolder.Locations.Add("Location61", worldMapLocation61);
            WorldMapLocationsHolder.Locations.Add("Location62", worldMapLocation62);
            WorldMapLocationsHolder.Locations.Add("Location63", worldMapLocation63);
            WorldMapLocationsHolder.Locations.Add("Location64", worldMapLocation64);
            WorldMapLocationsHolder.Locations.Add("Location65", worldMapLocation65);
            WorldMapLocationsHolder.Locations.Add("Location66", worldMapLocation66);
            WorldMapLocationsHolder.Locations.Add("Location67", worldMapLocation67);
            WorldMapLocationsHolder.Locations.Add("Location68", worldMapLocation68);
            WorldMapLocationsHolder.Locations.Add("Location69", worldMapLocation69);
            WorldMapLocationsHolder.Locations.Add("Location70", worldMapLocation70);
            WorldMapLocationsHolder.Locations.Add("Location71", worldMapLocation71);
            WorldMapLocationsHolder.Locations.Add("Location72", worldMapLocation72);
            WorldMapLocationsHolder.Locations.Add("Location73", worldMapLocation73);
            WorldMapLocationsHolder.Locations.Add("Location74", worldMapLocation74);
            WorldMapLocationsHolder.Locations.Add("Location75", worldMapLocation75);
            WorldMapLocationsHolder.Locations.Add("Location76", worldMapLocation76);
            WorldMapLocationsHolder.Locations.Add("Location77", worldMapLocation77);
            WorldMapLocationsHolder.Locations.Add("Location78", worldMapLocation78);
            WorldMapLocationsHolder.Locations.Add("Location79", worldMapLocation79);
            WorldMapLocationsHolder.Locations.Add("Location80", worldMapLocation80);
            WorldMapLocationsHolder.Locations.Add("Location81", worldMapLocation81);
            WorldMapLocationsHolder.Locations.Add("Location82", worldMapLocation82);
            WorldMapLocationsHolder.Locations.Add("Location83", worldMapLocation83);
            WorldMapLocationsHolder.Locations.Add("Location84", worldMapLocation84);
            WorldMapLocationsHolder.Locations.Add("Location85", worldMapLocation85);
            WorldMapLocationsHolder.Locations.Add("Location86", worldMapLocation86);
            WorldMapLocationsHolder.Locations.Add("Location87", worldMapLocation87);
            WorldMapLocationsHolder.Locations.Add("Location88", worldMapLocation88);
            WorldMapLocationsHolder.Locations.Add("Location89", worldMapLocation89);
            WorldMapLocationsHolder.Locations.Add("Location90", worldMapLocation90);
            WorldMapLocationsHolder.Locations.Add("Location91", worldMapLocation91);
            WorldMapLocationsHolder.Locations.Add("Location92", worldMapLocation92);
            WorldMapLocationsHolder.Locations.Add("Location93", worldMapLocation93);
            WorldMapLocationsHolder.Locations.Add("Location94", worldMapLocation94);
            WorldMapLocationsHolder.Locations.Add("Location95", worldMapLocation95);
            WorldMapLocationsHolder.Locations.Add("Location96", worldMapLocation96);
            WorldMapLocationsHolder.Locations.Add("Location97", worldMapLocation97);
            WorldMapLocationsHolder.Locations.Add("Location98", worldMapLocation98);
            WorldMapLocationsHolder.Locations.Add("Location99", worldMapLocation99);
            WorldMapLocationsHolder.Locations.Add("Location100", worldMapLocation100);
            WorldMapLocationsHolder.Locations.Add("Location101", worldMapLocation101);

            worldMap.SetHomeLocation(worldMapLocation1);
            COIEMapLinker linkMap = new COIEMapLinker(protosDb, worldMap);
            linkMap.FindPlaceToPutEntityPair(NewIDs.World.OilRigCost1, Ids.Technology.OilDrilling, 0, 500);
            linkMap.FindPlaceToPutEntity(NewIDs.World.Settlement1, 301, 1000);
            linkMap.FindPlaceToPutEntityPair(NewIDs.World.CargoBuildProgressWreckCost1, Ids.Technology.CargoShip, 0, 1500);
            linkMap.FindPlaceToPutEntity(NewIDs.World.Settlement2, 301, 2000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.SulfurMine, 500, 4000);
            linkMap.FindPlaceToPutEntityPair(NewIDs.World.OilRigCost2, Ids.Technology.OilDrilling, 1500, 4000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.SettlementForFuel, 301, 1000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.CoalMine, 500, 3000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.QuartzMine, 1500, 3000);
            linkMap.FindPlaceToPutEntityPair(NewIDs.World.OilRigCost3, Ids.Technology.OilDrilling, 2000, 4000 );
            linkMap.FindPlaceToPutEntity(NewIDs.World.WaterWell,500,3000);
            linkMap.FindPlaceToPutEntityPair(NewIDs.World.CargoBuildProgressWreckCost2, Ids.Technology.CargoShip, 1500, 4000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.Settlement3, 1500, 3000);
            linkMap.FindPlaceToPutEntityPair(NewIDs.World.UraniumMine, Ids.Technology.NuclearEnergy, 1500, 3000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.Settlement4, 1500, 4000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.CoalMine, 500, 2000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.QuartzMine, 500, 4000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.LimestoneMine, 500, 4000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.RockMine, 2000, 4000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.OilRigCost3, 500, 3000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.Settlement5, 2000, 3000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.SettlementForUranium, 500, 3000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.QuartzMine, 2000, 3000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.UraniumMine, 2000, 3000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.LimestoneMine,2000,3000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.IlmeniteMine, 2500, 6000);
            linkMap.FindPlaceToPutEntity(NewIDs.World.SettlementForIlmenite, 2500, 6000);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.ShipRadar, 550, 1200);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.ShipRadarT2, 1201, 2500);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.WheatSeeds, 500, 1500);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.CornSeeds, 500, 1500);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.SoybeansSeeds, 500, 1500);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.Microchip, 1500, 2500);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.CanolaSeeds, 500, 1500);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.SugarCaneSeeds, 500, 1500);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.FruitSeeds, 500, 1500);
            linkMap.FindPlaceToPutTechnology(Ids.Technology.PoppySeeds, 500, 1500);

            linkMap.GenerateWorldEvents();

            return worldMap;

        }

        

    }

    
}
