﻿using System;
using System.ComponentModel;
using System.Globalization;
using Avalonia.Threading;
using OpenTracker.Models.Items;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Markings.Images
{
    /// <summary>
    /// This class contains the non-static item marking image control ViewModel data.
    /// </summary>
    public class ItemMarkingImageVM : ViewModelBase, IMarkingImageVMBase
    {
        private readonly IItem _item;
        private readonly string _imageSourceBase;

        public string ImageSource => $"{_imageSourceBase}{_item.Current.ToString(CultureInfo.InvariantCulture)}.png";

        public delegate ItemMarkingImageVM Factory(IItem item, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item to be represented.
        /// </param>
        /// <param name="imageSourceBase">
        /// A string representing the base image source.
        /// </param>
        public ItemMarkingImageVM(IItem item, string imageSourceBase)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _imageSourceBase = imageSourceBase ??
                throw new ArgumentNullException(nameof(imageSourceBase));

            _item.PropertyChanged += OnItemChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnItemChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
            }
        }
    }
}
