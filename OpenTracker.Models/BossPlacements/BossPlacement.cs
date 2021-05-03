﻿using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Boss;
using ReactiveUI;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    ///     This class contains boss placement data.
    /// </summary>
    public class BossPlacement : ReactiveObject, IBossPlacement
    {
        private readonly IMode _mode;

        private readonly IChangeBoss.Factory _changeBossFactory;

        public BossType DefaultBoss { get; }

        private BossType? _boss;
        public BossType? Boss
        {
            get => _boss;
            set => this.RaiseAndSetIfChanged(ref _boss, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
        /// <param name="changeBossFactory">
        ///     An Autofac factory for creating change boss undoable actions.
        /// </param>
        /// <param name="defaultBoss">
        ///     The default boss type for the boss placement.
        /// </param>
        public BossPlacement(IMode mode, IChangeBoss.Factory changeBossFactory, BossType defaultBoss)
        {
            _mode = mode;

            DefaultBoss = defaultBoss;
            _changeBossFactory = changeBossFactory;

            if (DefaultBoss == BossType.Aga)
            {
                Boss = BossType.Aga;
            }
        }

        public BossType? GetCurrentBoss()
        {
            return _mode.BossShuffle ? Boss : DefaultBoss;
        }

        public IUndoable CreateChangeBossAction(BossType? boss)
        {
            return _changeBossFactory(this, boss);
        }

        public void Reset()
        {
            if (DefaultBoss == BossType.Aga)
            {
                Boss = BossType.Aga;
                return;
            }

            Boss = null;
        }

        /// <summary>
        ///     Returns a new boss placement save data instance for this boss placement.
        /// </summary>
        /// <returns>
        ///     A new boss placement save data instance.
        /// </returns>
        public BossPlacementSaveData Save()
        {
            return new()
            {
                Boss = Boss
            };
        }

        /// <summary>
        ///     Loads boss placement save data.
        /// </summary>
        public void Load(BossPlacementSaveData? saveData)
        {
            if (saveData == null)
            {
                return;
            }
            
            Boss = saveData.Boss;
        }
    }
}
