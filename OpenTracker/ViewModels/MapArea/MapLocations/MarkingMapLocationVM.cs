﻿using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using OpenTracker.ViewModels.Markings;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.MapArea.MapLocations
{
    /// <summary>
    /// This is the ViewModel for the marking map location control.
    /// </summary>
    public class MarkingMapLocationVM : ViewModelBase, IClickHandler
    {
        private readonly IMarking _marking;

        public string ImageSource
        {
            get
            {
                if (_marking.Value == null)
                {
                    return "avares://OpenTracker/Assets/Images/Items/unknown1.png";
                }

                switch (_marking.Value)
                {
                    case MarkingType.Bow:
                    case MarkingType.SilverArrows:
                    case MarkingType.Boomerang:
                    case MarkingType.RedBoomerang:
                    case MarkingType.SmallKey:
                    case MarkingType.BigKey:
                        {
                            return "avares://OpenTracker/Assets/Images/Items/visible-" +
                                $"{_marking.Value.ToString().ToLowerInvariant()}.png";
                        }
                    case MarkingType.Hookshot:
                    case MarkingType.Bomb:
                    case MarkingType.Mushroom:
                    case MarkingType.Powder:
                    case MarkingType.FireRod:
                    case MarkingType.IceRod:
                    case MarkingType.Bombos:
                    case MarkingType.Ether:
                    case MarkingType.Quake:
                    case MarkingType.Shovel:
                    case MarkingType.Lamp:
                    case MarkingType.Hammer:
                    case MarkingType.Flute:
                    case MarkingType.Net:
                    case MarkingType.Book:
                    case MarkingType.MoonPearl:
                    case MarkingType.CaneOfSomaria:
                    case MarkingType.CaneOfByrna:
                    case MarkingType.Cape:
                    case MarkingType.Mirror:
                    case MarkingType.Boots:
                    case MarkingType.Flippers:
                    case MarkingType.HalfMagic:
                    case MarkingType.Aga:
                        {
                            return "avares://OpenTracker/Assets/Images/Items/" +
                                $"{_marking.Value.ToString().ToLowerInvariant()}1.png";
                        }
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        {
                            var item = ItemDictionary.Instance[Enum.Parse<ItemType>(
                                _marking.Value.ToString())];

                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _marking.Value.ToString().ToLowerInvariant() +
                                Math.Min(item.Current + 1, item.Maximum).ToString(
                                CultureInfo.InvariantCulture) + ".png";
                        }
                    case MarkingType.Sword:
                        {
                            var sword = ItemDictionary.Instance[ItemType.Sword];
                            int itemNumber;

                            if (sword.Current == 0)
                            {
                                itemNumber = 0;
                            }
                            else
                            {
                                itemNumber = Math.Min(sword.Current + 1, sword.Maximum);
                            }

                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _marking.Value.ToString().ToLowerInvariant() +
                                $"{itemNumber.ToString(CultureInfo.InvariantCulture)}.png";
                        }
                    case MarkingType.HCFront:
                    case MarkingType.HCLeft:
                    case MarkingType.HCRight:
                    case MarkingType.EP:
                    case MarkingType.SP:
                    case MarkingType.SW:
                    case MarkingType.DPFront:
                    case MarkingType.DPLeft:
                    case MarkingType.DPRight:
                    case MarkingType.DPBack:
                    case MarkingType.TT:
                    case MarkingType.IP:
                    case MarkingType.MM:
                    case MarkingType.TRFront:
                    case MarkingType.TRLeft:
                    case MarkingType.TRRight:
                    case MarkingType.TRBack:
                    case MarkingType.GT:
                        {
                            return "avares://OpenTracker/Assets/Images/" +
                                $"{_marking.Value.ToString().ToLowerInvariant()}.png";
                        }
                    case MarkingType.ToH:
                        {
                            return "avares://OpenTracker/Assets/Images/th.png";
                        }
                    case MarkingType.PoD:
                        {
                            return "avares://OpenTracker/Assets/Images/pd.png";
                        }
                    case MarkingType.Ganon:
                        {
                            return "avares://OpenTracker/Assets/Images/ganon.png";
                        }
                }

                return null;
            }
        }

        public MarkingSelectVM MarkingSelect { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The section marking to be represented.
        /// </param>
        public MarkingMapLocationVM(IMarkableSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            _marking = section.Marking;
            MarkingSelect = MarkingSelectVMFactory.GetMarkingSelectVM(section);

            _marking.PropertyChanging += OnMarkingChanging;
            _marking.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanging event on the IMarking interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanging event.
        /// </param>
        private void OnMarkingChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Value))
            {
                UnsubscribeFromMarkingItem();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMarking interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Value))
            {
                SubscribeToMarkingItem();
                UpdateImage();
            }
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
        private void OnMarkedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateImage();
            }
        }

        /// <summary>
        /// Unsubscribes from the marking item.
        /// </summary>
        private void UnsubscribeFromMarkingItem()
        {
            if (_marking.Value.HasValue)
            {
                switch (_marking.Value.Value)
                {
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Sword:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        {
                            ItemType itemType = Enum.Parse<ItemType>(_marking.Value.Value.ToString());
                            ItemDictionary.Instance[itemType].PropertyChanged -= OnMarkedItemChanged;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Subscribes to the marking item.
        /// </summary>
        private void SubscribeToMarkingItem()
        {
            if (_marking.Value.HasValue)
            {
                switch (_marking.Value.Value)
                {
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Sword:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        {
                            ItemType itemType = Enum.Parse<ItemType>(_marking.Value.Value.ToString());
                            ItemDictionary.Instance[itemType].PropertyChanged += OnMarkedItemChanged;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
        /// </summary>
        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Handles left clicks and opens the marking select popup.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            MarkingSelect.PopupOpen = true;
        }

        /// <summary>
        /// Handles right clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
        }
    }
}
