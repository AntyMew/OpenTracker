﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    ///     This class contains the auto-tracking result value of an ordered priority list of results.
    /// </summary>
    public class AutoTrackMultipleOverride : AutoTrackValueBase, IAutoTrackMultipleOverride
    {
        private readonly IList<IAutoTrackValue> _values;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="values">
        ///     The list of auto-tracking result values.
        /// </param>
        public AutoTrackMultipleOverride(IList<IAutoTrackValue> values)
        {
            _values = values;

            UpdateValue();

            foreach (var value in values)
            {
                value.PropertyChanged += OnValueChanged;
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IAutoTrackValue interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnValueChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                UpdateValue();
            }
        }

        protected override int? GetNewValue()
        {
            var valuesNotNull = _values.Where(x => x.CurrentValue.HasValue).ToList();

            if (valuesNotNull.Count == 0)
            {
                return null;
            }

            return valuesNotNull.Where(value => value.CurrentValue > 0).Select(
                value => value.CurrentValue!.Value).FirstOrDefault();
        }
    }
}
