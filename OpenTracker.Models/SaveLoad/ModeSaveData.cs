﻿using OpenTracker.Models.Modes;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This class contains mode setting save data.
    /// </summary>
    public class ModeSaveData
    {
        public ItemPlacement ItemPlacement { get; set; } = ItemPlacement.Advanced;
        public bool MapShuffle { get; set; }
        public bool CompassShuffle { get; set; }
        public bool SmallKeyShuffle { get; set; }
        public bool BigKeyShuffle { get; set; }
        public WorldState WorldState { get; set; }
        public EntranceShuffle EntranceShuffle { get; set; }
        public bool BossShuffle { get; set; }
        public bool EnemyShuffle { get; set; }
        public bool GuaranteedBossItems { get; set; }
        public bool GenericKeys { get; set; }
        public bool TakeAnyLocations { get; set; }
        public bool KeyDropShuffle { get; set; }
        public bool ShopShuffle { get; set; }
    }
}
