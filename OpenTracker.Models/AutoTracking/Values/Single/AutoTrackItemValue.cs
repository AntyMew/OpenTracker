﻿using System.ComponentModel;
using OpenTracker.Models.Items;
using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    ///     This class contains the auto-tracking result value of a hidden item count.
    /// </summary>
    public class AutoTrackItemValue : ReactiveObject, IAutoTrackItemValue
    {
        private readonly IItem _item;

        public int? CurrentValue => _item.Current;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="item">
        ///     The item for which the count is represented.
        /// </param>
        public AutoTrackItemValue(IItem item)
        {
            _item = item;

            _item.PropertyChanged += OnItemChanged;
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                this.RaisePropertyChanged(nameof(CurrentValue));
            }
        }
    }
}
