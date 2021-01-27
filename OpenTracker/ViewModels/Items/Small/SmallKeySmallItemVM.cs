﻿using OpenTracker.Interfaces;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel of the small Items panel control representing a small key.
    /// </summary>
    public class SmallKeySmallItemVM : SmallItemVMBase, IClickHandler
    {
        private readonly IRequirement _spacerRequirement;
        private readonly IRequirement _requirement;
        private readonly IItem _item;

        public bool SpacerVisible =>
            _spacerRequirement.Met;
        public bool Visible =>
            _requirement.Met;
        public bool TextVisible =>
            Visible && _item.Current > 0;
        public string ItemNumber
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(_item.Current.ToString(CultureInfo.InvariantCulture));
                sb.Append(_item.CanAdd() ? "" : "*");

                return sb.ToString();
            }
        }
        public string TextColor
        {
            get
            {
                if (_item == null)
                {
                    return "#ffffff";
                }

                if (!_item.CanAdd())
                {
                    return AppSettings.Instance.Colors.EmphasisFontColor;
                }

                return "#ffffff";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon whose small keys are to be represented.
        /// </param>
        public SmallKeySmallItemVM(IDungeon dungeon)
        {
            if (dungeon == null)
            {
                throw new ArgumentNullException(nameof(dungeon));
            }

            if (dungeon.SmallKeyItem == null)
            {
                throw new ArgumentOutOfRangeException(nameof(dungeon));
            }

            _item = dungeon.SmallKeyItem;

            _spacerRequirement = new AlternativeRequirement(new List<IRequirement>
            {
                new AlwaysDisplayDungeonItemsRequirement(true),
                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]
            });

            _requirement = dungeon.ID == LocationID.EasternPalace ?
                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOn] :
                RequirementDictionary.Instance[RequirementType.NoRequirement];

            AppSettings.Instance.Colors.PropertyChanged += OnColorsChanged;
            _item.PropertyChanged += OnItemChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
            _spacerRequirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ColorSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnColorsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ColorSettings.EmphasisFontColor))
            {
                UpdateTextColor();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(SpacerVisible));
            this.RaisePropertyChanged(nameof(Visible));
            this.RaisePropertyChanged(nameof(TextVisible));
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
        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTextColor();
            this.RaisePropertyChanged(nameof(TextVisible));
            this.RaisePropertyChanged(nameof(ItemNumber));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextColor properties.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(TextColor));
        }

        /// <summary>
        /// Handles left clicks and adds an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            UndoRedoManager.Instance.Execute(new AddItem(_item));
        }

        /// <summary>
        /// Handles right clicks and removes an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            UndoRedoManager.Instance.Execute(new RemoveItem(_item));
        }
    }
}
