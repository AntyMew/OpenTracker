﻿using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.BigKeyShuffle
{
    /// <summary>
    ///     This class contains big key shuffle requirement data.
    /// </summary>
    public class BigKeyShuffleRequirement : BooleanRequirement, IBigKeyShuffleRequirement
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
        ///     The expected big key shuffle value.
        /// </param>
        public BigKeyShuffleRequirement(IMode mode, bool expectedValue)
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
            if (e.PropertyName == nameof(IMode.BigKeyShuffle))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.BigKeyShuffle == _expectedValue;
        }
    }
}
