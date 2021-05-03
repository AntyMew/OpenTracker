﻿using System.ComponentModel;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Mode;
using ReactiveUI;

namespace OpenTracker.Models.Modes
{
    /// <summary>
    ///     This class contains game mode settings data.
    /// </summary>
    public class Mode : ReactiveObject, IMode
    {
        private readonly IChangeItemPlacement.Factory _changeItemPlacementFactory;
        private readonly IChangeMapShuffle.Factory _changeMapShuffleFactory;
        private readonly IChangeCompassShuffle.Factory _changeCompassShuffleFactory;
        private readonly IChangeSmallKeyShuffle.Factory _changeSmallKeyShuffleFactory;
        private readonly IChangeBigKeyShuffle.Factory _changeBigKeyShuffleFactory;
        private readonly IChangeWorldState.Factory _changeWorldStateFactory;
        private readonly IChangeEntranceShuffle.Factory _changeEntranceShuffleFactory;
        private readonly IChangeBossShuffle.Factory _changeBossShuffleFactory;
        private readonly IChangeEnemyShuffle.Factory _changeEnemyShuffleFactory;
        private readonly IChangeGuaranteedBossItems.Factory _changeGuaranteedBossItemsFactory;
        private readonly IChangeGenericKeys.Factory _changeGenericKeysFactory;
        private readonly IChangeTakeAnyLocations.Factory _changeTakeAnyLocationsFactory;
        private readonly IChangeKeyDropShuffle.Factory _changeKeyDropShuffleFactory;
        private readonly IChangeShopShuffle.Factory _changeShopShuffleFactory;

        private ItemPlacement _itemPlacement = ItemPlacement.Advanced;
        public ItemPlacement ItemPlacement
        {
            get => _itemPlacement;
            set => this.RaiseAndSetIfChanged(ref _itemPlacement, value);
        }

        private bool _mapShuffle;
        public bool MapShuffle
        {
            get => _mapShuffle;
            set => this.RaiseAndSetIfChanged(ref _mapShuffle, value);
        }

        private bool _compassShuffle;
        public bool CompassShuffle
        {
            get => _compassShuffle;
            set => this.RaiseAndSetIfChanged(ref _compassShuffle, value);
        }

        private bool _smallKeyShuffle;
        public bool SmallKeyShuffle
        {
            get => _smallKeyShuffle;
            set => this.RaiseAndSetIfChanged(ref _smallKeyShuffle, value);
        }

        private bool _bigKeyShuffle;
        public bool BigKeyShuffle
        {
            get => _bigKeyShuffle;
            set => this.RaiseAndSetIfChanged(ref _bigKeyShuffle, value);
        }

        private WorldState _worldState = WorldState.StandardOpen;
        public WorldState WorldState
        {
            get => _worldState;
            set => this.RaiseAndSetIfChanged(ref _worldState, value);
        }

        private EntranceShuffle _entranceShuffle;
        public EntranceShuffle EntranceShuffle
        {
            get => _entranceShuffle;
            set => this.RaiseAndSetIfChanged(ref _entranceShuffle, value);
        }

        private bool _bossShuffle;
        public bool BossShuffle
        {
            get => _bossShuffle;
            set => this.RaiseAndSetIfChanged(ref _bossShuffle, value);
        }

        private bool _enemyShuffle;
        public bool EnemyShuffle
        {
            get => _enemyShuffle;
            set => this.RaiseAndSetIfChanged(ref _enemyShuffle, value);
        }

        private bool _guaranteedBossItems;
        public bool GuaranteedBossItems
        {
            get => _guaranteedBossItems;
            set => this.RaiseAndSetIfChanged(ref _guaranteedBossItems, value);
        }

        private bool _genericKeys;
        public bool GenericKeys
        {
            get => _genericKeys;
            set => this.RaiseAndSetIfChanged(ref _genericKeys, value);
        }

        private bool _takeAnyLocations;
        public bool TakeAnyLocations
        {
            get => _takeAnyLocations;
            set => this.RaiseAndSetIfChanged(ref _takeAnyLocations, value);
        }

        private bool _keyDropShuffle;
        public bool KeyDropShuffle
        {
            get => _keyDropShuffle;
            set => this.RaiseAndSetIfChanged(ref _keyDropShuffle, value);
        }

        private bool _shopShuffle;
        public bool ShopShuffle
        {
            get => _shopShuffle;
            set => this.RaiseAndSetIfChanged(ref _shopShuffle, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="changeItemPlacementFactory">
        ///     An Autofac factory for creating undoable actions to change item placement setting.
        /// </param>
        /// <param name="changeMapShuffleFactory">
        ///     An Autofac factory for creating undoable actions to change map shuffle setting.
        /// </param>
        /// <param name="changeCompassShuffleFactory">
        ///     An Autofac factory for creating undoable actions to change compass shuffle setting.
        /// </param>
        /// <param name="changeSmallKeyShuffleFactory">
        ///     An Autofac factory for creating undoable actions to change small key shuffle setting.
        /// </param>
        /// <param name="changeBigKeyShuffleFactory">
        ///     An Autofac factory for creating undoable actions to change big key shuffle setting.
        /// </param>
        /// <param name="changeWorldStateFactory">
        ///     An Autofac factory for creating undoable actions to change world state setting.
        /// </param>
        /// <param name="changeEntranceShuffleFactory">
        ///     An Autofac factory for creating undoable actions to change entrance shuffle setting.
        /// </param>
        /// <param name="changeBossShuffleFactory">
        ///     An Autofac factory for creating undoable actions to change boss shuffle setting.
        /// </param>
        /// <param name="changeEnemyShuffleFactory">
        ///     An Autofac factory for creating undoable actions to change enemy shuffle setting.
        /// </param>
        /// <param name="changeGuaranteedBossItemsFactory">
        ///     An Autofac factory for creating undoable actions to change guaranteed boss items setting.
        /// </param>
        /// <param name="changeGenericKeysFactory">
        ///     An Autofac factory for creating undoable actions to change generic keys setting.
        /// </param>
        /// <param name="changeTakeAnyLocationsFactory">
        ///     An Autofac factory for creating undoable actions to change take any locations setting.
        /// </param>
        /// <param name="changeKeyDropShuffleFactory">
        ///     An Autofac factory for creating undoable actions to change key drop shuffle setting.
        /// </param>
        /// <param name="changeShopShuffleFactory">
        ///     An Autofac factory for creating undoable actions to change shop shuffle setting.
        /// </param>
        public Mode(
            IChangeItemPlacement.Factory changeItemPlacementFactory, IChangeMapShuffle.Factory changeMapShuffleFactory,
            IChangeCompassShuffle.Factory changeCompassShuffleFactory,
            IChangeSmallKeyShuffle.Factory changeSmallKeyShuffleFactory,
            IChangeBigKeyShuffle.Factory changeBigKeyShuffleFactory, IChangeWorldState.Factory changeWorldStateFactory,
            IChangeEntranceShuffle.Factory changeEntranceShuffleFactory,
            IChangeBossShuffle.Factory changeBossShuffleFactory, IChangeEnemyShuffle.Factory changeEnemyShuffleFactory,
            IChangeGuaranteedBossItems.Factory changeGuaranteedBossItemsFactory,
            IChangeGenericKeys.Factory changeGenericKeysFactory,
            IChangeTakeAnyLocations.Factory changeTakeAnyLocationsFactory,
            IChangeKeyDropShuffle.Factory changeKeyDropShuffleFactory,
            IChangeShopShuffle.Factory changeShopShuffleFactory)
        {
            _changeItemPlacementFactory = changeItemPlacementFactory;
            _changeMapShuffleFactory = changeMapShuffleFactory;
            _changeCompassShuffleFactory = changeCompassShuffleFactory;
            _changeSmallKeyShuffleFactory = changeSmallKeyShuffleFactory;
            _changeBigKeyShuffleFactory = changeBigKeyShuffleFactory;
            _changeWorldStateFactory = changeWorldStateFactory;
            _changeEntranceShuffleFactory = changeEntranceShuffleFactory;
            _changeBossShuffleFactory = changeBossShuffleFactory;
            _changeEnemyShuffleFactory = changeEnemyShuffleFactory;
            _changeGuaranteedBossItemsFactory = changeGuaranteedBossItemsFactory;
            _changeGenericKeysFactory = changeGenericKeysFactory;
            _changeTakeAnyLocationsFactory = changeTakeAnyLocationsFactory;
            _changeKeyDropShuffleFactory = changeKeyDropShuffleFactory;
            _changeShopShuffleFactory = changeShopShuffleFactory;

            PropertyChanged += OnPropertyChanged;
        }

        public IUndoable CreateChangeItemPlacementAction(ItemPlacement newValue)
        {
            return _changeItemPlacementFactory(newValue);
        }

        public IUndoable CreateChangeMapShuffleAction(bool newValue)
        {
            return _changeMapShuffleFactory(newValue);
        }

        public IUndoable CreateChangeCompassShuffleAction(bool newValue)
        {
            return _changeCompassShuffleFactory(newValue);
        }

        public IUndoable CreateChangeSmallKeyShuffleAction(bool newValue)
        {
            return _changeSmallKeyShuffleFactory(newValue);
        }

        public IUndoable CreateChangeBigKeyShuffleAction(bool newValue)
        {
            return _changeBigKeyShuffleFactory(newValue);
        }

        public IUndoable CreateChangeWorldStateAction(WorldState newValue)
        {
            return _changeWorldStateFactory(newValue);
        }

        public IUndoable CreateChangeEntranceShuffleAction(EntranceShuffle newValue)
        {
            return _changeEntranceShuffleFactory(newValue);
        }

        public IUndoable CreateChangeBossShuffleAction(bool newValue)
        {
            return _changeBossShuffleFactory(newValue);
        }

        public IUndoable CreateChangeEnemyShuffleAction(bool newValue)
        {
            return _changeEnemyShuffleFactory(newValue);
        }

        public IUndoable CreateChangeGuaranteedBossItemsAction(bool newValue)
        {
            return _changeGuaranteedBossItemsFactory(newValue);
        }

        public IUndoable CreateChangeGenericKeysAction(bool newValue)
        {
            return _changeGenericKeysFactory(newValue);
        }

        public IUndoable CreateChangeTakeAnyLocationsAction(bool newValue)
        {
            return _changeTakeAnyLocationsFactory(newValue);
        }

        public IUndoable CreateChangeKeyDropShuffleAction(bool newValue)
        {
            return _changeKeyDropShuffleFactory(newValue);
        }

        public IUndoable CreateChangeShopShuffleAction(bool newValue)
        {
            return _changeShopShuffleFactory(newValue);
        }
        
        /// <summary>
        ///     Returns save data for this mode instance.
        /// </summary>
        /// <returns>
        ///     Save data for this mode instance.
        /// </returns>
        public ModeSaveData Save()
        {
            return new()
            {
                ItemPlacement = ItemPlacement,
                MapShuffle = MapShuffle,
                CompassShuffle = CompassShuffle,
                SmallKeyShuffle = SmallKeyShuffle,
                BigKeyShuffle = BigKeyShuffle,
                WorldState = WorldState,
                EntranceShuffle = EntranceShuffle,
                BossShuffle = BossShuffle,
                EnemyShuffle = EnemyShuffle,
                GuaranteedBossItems = GuaranteedBossItems,
                GenericKeys = GenericKeys,
                TakeAnyLocations = TakeAnyLocations,
                KeyDropShuffle = KeyDropShuffle,
                ShopShuffle = ShopShuffle
            };
        }

        /// <summary>
        ///     Loads the save data to this mode instance.
        /// </summary>
        /// <param name="saveData">
        ///     The save data to be loaded.
        /// </param>
        public void Load(ModeSaveData? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            ItemPlacement = saveData.ItemPlacement;
            MapShuffle = saveData.MapShuffle;
            CompassShuffle = saveData.CompassShuffle;
            SmallKeyShuffle = saveData.SmallKeyShuffle;
            BigKeyShuffle = saveData.BigKeyShuffle;
            WorldState = saveData.WorldState;
            EntranceShuffle = saveData.EntranceShuffle;
            BossShuffle = saveData.BossShuffle;
            EnemyShuffle = saveData.EnemyShuffle;
            GuaranteedBossItems = saveData.GuaranteedBossItems;
            GenericKeys = saveData.GenericKeys;
            TakeAnyLocations = saveData.TakeAnyLocations;
            KeyDropShuffle = saveData.KeyDropShuffle;
            ShopShuffle = saveData.ShopShuffle;
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(WorldState) when WorldState == WorldState.Inverted:
                    ItemPlacement = ItemPlacement.Advanced;
                    break;
                case nameof(GenericKeys) when GenericKeys:
                    SmallKeyShuffle = true;
                    break;
            }
        }
    }
}