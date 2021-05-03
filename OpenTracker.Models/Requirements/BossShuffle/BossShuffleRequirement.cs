﻿using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.BossShuffle
{
    /// <summary>
    ///     This class contains boss shuffle requirement data.
    /// </summary>
    public class BossShuffleRequirement : BooleanRequirement, IBossShuffleRequirement
    {
        private readonly IMode _mode;
        private readonly bool _expectedValue;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
        /// <param name="expectedValue">
        ///     The expected boss shuffle value.
        /// </param>
        public BossShuffleRequirement(IMode mode, bool expectedValue)
        {
            _mode = mode;
            _expectedValue = expectedValue;

            _mode.PropertyChanged += OnModeChanged;

            UpdateValue();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        ///     The event sender.
        /// </param>
        /// <param name="e">
        ///     The PropertyChanged event args.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.BossShuffle))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.BossShuffle == _expectedValue;
        }
    }
}
