﻿using OpenTracker.Models.Dungeons;
using System;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This is the class for creating locations.
    /// </summary>
    public static class LocationFactory
    {
        /// <summary>
        /// Returns the name of a specified location.
        /// </summary>
        /// <param name="id">
        /// The identity of the location.
        /// </param>
        /// <returns>
        /// A string representing the name of the location.
        /// </returns>
        public static string GetLocationName(LocationID id)
        {
            return id switch
            {
                LocationID.LinksHouse => "Link's House",
                LocationID.Pedestal => "Pedestal",
                LocationID.LumberjackCave => "Lumberjacks",
                LocationID.BlindsHouse => "Blind's House",
                LocationID.TheWell => "The Well",
                LocationID.BottleVendor => "Bottle Vendor",
                LocationID.ChickenHouse => "Chicken House",
                LocationID.Tavern => "Tavern",
                LocationID.SickKid => "Sick Kid",
                LocationID.MagicBat => "Magic Bat",
                LocationID.RaceGame => "Race Game",
                LocationID.Library => "Library",
                LocationID.MushroomSpot => "Mushroom Spot",
                LocationID.ForestHideout => "Forest Hideout",
                LocationID.CastleSecret => "Uncle",
                LocationID.WitchsHut => "Witch's Hut",
                LocationID.SahasrahlasHut => "Sahash's Hut",
                LocationID.BonkRocks => "Bonk Rocks",
                LocationID.KingsTomb => "King's Tomb",
                LocationID.AginahsCave => "Aginah's Cave",
                LocationID.GroveDiggingSpot => "Dig Spot",
                LocationID.Dam => "Dam",
                LocationID.MiniMoldormCave => "Mini-Moldorm",
                LocationID.IceRodCave => "Ice Rod Cave",
                LocationID.Hobo => "Hobo",
                LocationID.PyramidLedge => "Pyramid Ledge",
                LocationID.FatFairy => "Fat Fairy",
                LocationID.HauntedGrove => "Haunted Grove",
                LocationID.HypeCave => "Hype Cave",
                LocationID.BombosTablet => "Bombos Tablet",
                LocationID.SouthOfGrove => "South of Grove",
                LocationID.DiggingGame => "Digging Game",
                LocationID.WaterfallFairy => "Waterfall Fairy",
                LocationID.ZoraArea => "Zora Area",
                LocationID.Catfish => "Catfish",
                LocationID.GraveyardLedge => "Graveyard Ledge",
                LocationID.DesertLedge => "Desert Ledge",
                LocationID.CShapedHouse => "C-Shaped House",
                LocationID.TreasureGame => "Treasure Game",
                LocationID.BombableShack => "Bombable Shack",
                LocationID.Blacksmith => "Blacksmiths",
                LocationID.PurpleChest => "Purple Chest",
                LocationID.HammerPegs => "Hammer Pegs",
                LocationID.BumperCave => "Bumper Cave",
                LocationID.LakeHyliaIsland => "Lake Hylia Island",
                LocationID.MireShack => "Mire Shack",
                LocationID.CheckerboardCave => "Checkerboard Cave",
                LocationID.OldMan => "Old Man",
                LocationID.SpectacleRock => "Spectacle Rock",
                LocationID.EtherTablet => "Ether Tablet",
                LocationID.SpikeCave => "Spike Cave",
                LocationID.SpiralCave => "Spiral Cave",
                LocationID.ParadoxCave => "Paradox Cave",
                LocationID.SuperBunnyCave => "Super-Bunny Cave",
                LocationID.HookshotCave => "Hookshot Cave",
                LocationID.FloatingIsland => "Floating Island",
                LocationID.MimicCave => "Mimic Cave",
                LocationID.HyruleCastle => "Escape",
                LocationID.AgahnimTower => "Agahnim's Tower",
                LocationID.EasternPalace => "Eastern Palace",
                LocationID.DesertPalace => "Desert Palace",
                LocationID.TowerOfHera => "Tower of Hera",
                LocationID.PalaceOfDarkness => "Palace of Darkness",
                LocationID.SwampPalace => "Swamp Palace",
                LocationID.SkullWoods => "Skull Woods",
                LocationID.ThievesTown => "Thieves' Town",
                LocationID.IcePalace => "Ice Palace",
                LocationID.MiseryMire => "Misery Mire",
                LocationID.TurtleRock => "Turtle Rock",
                LocationID.GanonsTower => "Ganon's Tower",
                LocationID.LumberjackHouseEntrance => "Lumber House",
                LocationID.LumberjackCaveEntrance => "Lumberjacks",
                LocationID.DeathMountainEntryCave => "DM Entry",
                LocationID.DeathMountainExitCave => "DM Exit",
                LocationID.KakarikoFortuneTellerEntrance => "Kak Fortune",
                LocationID.WomanLeftDoor => "Woman Left",
                LocationID.WomanRightDoor => "Woman Right",
                LocationID.LeftSnitchHouseEntrance => "Left Snitch",
                LocationID.RightSnitchHouseEntrance => "Right Snitch",
                LocationID.BlindsHouseEntrance => "Blind's House",
                LocationID.TheWellEntrance => "The Well",
                LocationID.ChickenHouseEntrance => "Chicken House",
                LocationID.GrassHouseEntrance => "Grass House",
                LocationID.TavernFront => "Front Tavern",
                LocationID.KakarikoShop => "Kakariko Shop",
                LocationID.BombHutEntrance => "Bomb Hut",
                LocationID.SickKidEntrance => "Sick Kid",
                LocationID.BlacksmithHouse => "Blacksmiths",
                LocationID.MagicBatEntrance => "Magic Bat",
                LocationID.ChestGameEntrance => "Chest Game",
                LocationID.RaceHouseLeft => "Race House Left",
                LocationID.RaceHouseRight => "Race House Right",
                LocationID.LibraryEntrance => "Library",
                LocationID.ForestHideoutEntrance => "Forest Hideout",
                LocationID.ForestChestGameEntrance => "Forest Chest Game",
                LocationID.CastleSecretEntrance => "Castle Secret Entrance",
                LocationID.CastleMainEntrance => "Castle Main Entrance",
                LocationID.CastleLeftEntrance => "Castle Left Entrance",
                LocationID.CastleRightEntrance => "Castle Right Entrance",
                LocationID.CastleTowerEntrance => "Castle Tower Entrance",
                LocationID.DamEntrance => "Dam",
                LocationID.CentralBonkRocksEntrance => "Central Bonk Rocks",
                LocationID.WitchsHutEntrance => "Witch's Hut",
                LocationID.WaterfallFairyEntrance => "Waterfall Fairy",
                LocationID.SahasrahlasHutEntrance => "Sahasrahla's Hut",
                LocationID.TreesFairyCaveEntrance => "Trees Fairy Cave",
                LocationID.PegsFairyCaveEntrance => "Pegs Fairy Cave",
                LocationID.EasternPalaceEntrance => "Eastern Palace",
                LocationID.HoulihanHole => "Houlihan Hole",
                LocationID.SanctuaryGrave => "Sanctuary Grave",
                LocationID.NorthBonkRocks => "North Bonk Rocks",
                LocationID.KingsTombEntrance => "King's Tomb",
                LocationID.GraveyardLedgeEntrance => "Graveyard Ledge",
                LocationID.DesertLeftEntrance => "Desert Left Entrance",
                LocationID.DesertBackEntrance => "Desert Back Entrance",
                LocationID.DesertRightEntrance => "Desert Right Entrance",
                LocationID.DesertFrontEntrance => "Desert Front Entrance",
                LocationID.AginahsCaveEntrance => "Aginah's Cave",
                LocationID.ThiefCaveEntrance => "Thief Cave",
                LocationID.RupeeCaveEntrance => "Rupee Cave",
                LocationID.SkullWoodsBack => "Skull Woods Back",
                LocationID.ThievesTownEntrance => "Thieves Town",
                LocationID.CShapedHouseEntrance => "C-Shaped House",
                LocationID.HammerHouse => "Hammer House",
                LocationID.DarkVillageFortuneTellerEntrance => "Dark Village Fortune Teller",
                LocationID.DarkChapelEntrance => "Dark Chapel",
                LocationID.ShieldShop => "Shield Shop",
                LocationID.DarkLumberjack => "Dark Lumberjack",
                LocationID.TreasureGameEntrance => "Treasure Game",
                LocationID.BombableShackEntrance => "Bombable Shack",
                LocationID.HammerPegsEntrance => "Hammer Pegs",
                LocationID.BumperCaveExit => "Bumper Cave Exit",
                LocationID.BumperCaveEntrance => "Bumper Cave Entry",
                LocationID.HypeCaveEntrance => "Hype Cave",
                LocationID.SwampPalaceEntrance => "Swamp Palace",
                LocationID.DarkCentralBonkRocksEntrance => "Bonk Rocks",
                LocationID.SouthOfGroveEntrance => "South of Grove",
                LocationID.BombShop => "Bomb Shop",
                LocationID.ArrowGameEntrance => "Arrow Game",
                LocationID.DarkHyliaFortuneTeller => "Dark Hylia Fortune Teller",
                LocationID.DarkTreesFairyCaveEntrance => "Dark Trees Fairy Cave",
                LocationID.DarkSahasrahlaEntrance => "Dark Saha",
                LocationID.PalaceOfDarknessEntrance => "Palace of Darkness",
                LocationID.DarkWitchsHut => "Dark Witch's Hut",
                LocationID.DarkFluteSpotFiveEntrance => "Dark Flute Spot 5",
                LocationID.FatFairyEntrance => "Fat Fairy",
                LocationID.GanonHole => "Ganon Hole",
                LocationID.DarkIceRodCaveEntrance => "Dark Ice Rod Cave",
                LocationID.DarkFakeIceRodCaveEntrance => "Dark Fake Ice Rod Cave",
                LocationID.DarkIceRodRockEntrance => "Dark Ice Rod Rock",
                LocationID.HypeFairyCaveEntrance => "Hype Fairy Cave",
                LocationID.FortuneTellerEntrance => "Fortune Teller",
                LocationID.LakeShop => "Lake Shop",
                LocationID.UpgradeFairy => "Upgrade Fairy",
                LocationID.MiniMoldormCaveEntrance => "Mini-Moldorm",
                LocationID.IceRodCaveEntrance => "Ice Rod Cave",
                LocationID.IceBeeCaveEntrance => "Ice Bee Cave",
                LocationID.IceFairyCaveEntrance => "Ice Fairy Cave",
                LocationID.IcePalaceEntrance => "Ice Palace",
                LocationID.MiseryMireEntrance => "Misery Mire",
                LocationID.MireShackEntrance => "Mire Shack",
                LocationID.MireRightShackEntrance => "Mire Right Shack",
                LocationID.MireCaveEntrance => "Mire Cave",
                LocationID.CheckerboardCaveEntrance => "Checkerboard Cave",
                LocationID.DeathMountainEntranceBack => "DM Entry Back",
                LocationID.OldManResidence => "Old Man Residence",
                LocationID.OldManBackResidence => "Old Man Back Residence",
                LocationID.DeathMountainExitFront => "Death Mountain Exit Front",
                LocationID.SpectacleRockLeft => "Spectacle Rock Left",
                LocationID.SpectacleRockRight => "Spectacle Rock Right",
                LocationID.SpectacleRockTop => "Spectacle Rock Top",
                LocationID.SpikeCaveEntrance => "Spike Cave",
                LocationID.DarkMountainFairyEntrance => "Dark Mountain Fairy",
                LocationID.TowerOfHeraEntrance => "Tower Of Hera",
                LocationID.SpiralCaveBottom => "Spiral Cave Bottom",
                LocationID.EDMFairyCaveEntrance => "EDM Fairy Cave",
                LocationID.ParadoxCaveMiddle => "Paradox Cave Middle",
                LocationID.ParadoxCaveBottom => "Paradox Cave Bottom",
                LocationID.EDMConnectorBottom => "EDM Connector Bottom",
                LocationID.SpiralCaveTop => "Spiral Cave Top",
                LocationID.MimicCaveEntrance => "Mimic Cave",
                LocationID.EDMConnectorTop => "EDM Connector Top",
                LocationID.ParadoxCaveTop => "Paradox Cave Top",
                LocationID.SuperBunnyCaveBottom => "Super-Bunny Cave Bottom",
                LocationID.DeathMountainShop => "Death Mountain Shop",
                LocationID.SuperBunnyCaveTop => "Super-Bunny Cave Top",
                LocationID.HookshotCaveEntrance => "Hookshot Cave",
                LocationID.TurtleRockEntrance => "Turtle Rock",
                LocationID.GanonsTowerEntrance => "Ganon's Tower",
                LocationID.TRLedgeLeft => "TR Ledge Left",
                LocationID.TRLedgeRight => "TR Ledge Right",
                LocationID.TRSafetyDoor => "TR Safety Door",
                LocationID.HookshotCaveTop => "Hookshot Cave Top",
                LocationID.LinksHouseEntrance => "Link's House",
                LocationID.TreesFairyCaveTakeAny => "Trees Fairy Cave",
                LocationID.PegsFairyCaveTakeAny => "Pegs Fairy Cave",
                LocationID.KakarikoFortuneTellerTakeAny => "Kak Fortune",
                LocationID.GrassHouseTakeAny => "Grass House",
                LocationID.ForestChestGameTakeAny => "Forest Chest Game",
                LocationID.LumberjackHouseTakeAny => "Lumber House",
                LocationID.LeftSnitchHouseTakeAny => "Left Snitch",
                LocationID.RightSnitchHouseTakeAny => "Right Snitch",
                LocationID.BombHutTakeAny => "Bomb Hut",
                LocationID.IceFairyCaveTakeAny => "Ice Fairy Cave",
                LocationID.RupeeCaveTakeAny => "Rupee Cave",
                LocationID.CentralBonkRocksTakeAny => "Central Bonk Rocks",
                LocationID.ThiefCaveTakeAny => "Thief Cave",
                LocationID.IceBeeCaveTakeAny => "Ice Bee Cave",
                LocationID.FortuneTellerTakeAny => "Fortune Teller",
                LocationID.HypeFairyCaveTakeAny => "Hype Fairy Cave",
                LocationID.ChestGameTakeAny => "Chest Game",
                LocationID.EDMFairyCaveTakeAny => "EDM Fairy Cave",
                LocationID.DarkChapelTakeAny => "Dark Chapel",
                LocationID.DarkVillageFortuneTellerTakeAny => "Dark Village Fortune Teller",
                LocationID.DarkTreesFairyCaveTakeAny => "Dark Trees Fairy Cave",
                LocationID.DarkSahasrahlaTakeAny => "Dark Saha",
                LocationID.DarkFluteSpotFiveTakeAny => "Dark Flute Spot 5",
                LocationID.ArrowGameTakeAny => "Arrow Game",
                LocationID.DarkCentralBonkRocksTakeAny => "Dark Central Bonk Rocks",
                LocationID.DarkIceRodCaveTakeAny => "Dark Ice Rod Cave",
                LocationID.DarkFakeIceRodCaveTakeAny => "Dark Fake Ice Rod Cave",
                LocationID.DarkIceRodRockTakeAny => "Dark Ice Rod Rock",
                LocationID.DarkMountainFairyTakeAny => "Dark Mountain Fairy",
                LocationID.MireRightShackTakeAny => "Mire Right Shack",
                LocationID.MireCaveTakeAny => "Mire Cave",
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        /// Returns a new base location of a specified location ID.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new base location.
        /// </returns>
        public static ILocation GetBaseLocation(LocationID id)
        {
            return new Location(
                id, GetLocationName(id), MapLocationFactory.GetMapLocations(id));
        }

        /// <summary>
        /// Returns a new location of a specified location ID.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new location.
        /// </returns>
        public static ILocation GetLocation(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                case LocationID.AgahnimTower:
                case LocationID.EasternPalace:
                case LocationID.DesertPalace:
                case LocationID.TowerOfHera:
                case LocationID.PalaceOfDarkness:
                case LocationID.SwampPalace:
                case LocationID.SkullWoods:
                case LocationID.ThievesTown:
                case LocationID.IcePalace:
                case LocationID.MiseryMire:
                case LocationID.TurtleRock:
                case LocationID.GanonsTower:
                    {
                        return DungeonFactory.GetDungeon(id);
                    }
                default:
                    {
                        return GetBaseLocation(id);
                    }
            }
        }
    }
}
