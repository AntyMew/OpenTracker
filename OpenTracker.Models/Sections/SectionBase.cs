using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Sections;
using ReactiveUI;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    ///     This base class contains section data.
    /// </summary>
    public abstract class SectionBase : ReactiveObject, ISection
    {
        private readonly ISaveLoadManager _saveLoadManager;
        
        private readonly ICollectSection.Factory _collectSectionFactory;
        private readonly IUncollectSection.Factory _uncollectSectionFactory;
        
        private readonly IAutoTrackValue? _autoTrackValue;
        private readonly IRequirement? _requirement;

        public string Name { get; }
        public bool UserManipulated { get; set; }
        public IMarking? Marking { get; }
        
        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            protected set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        // ReSharper disable once InconsistentNaming
        protected int _available;
        public int Available
        {
            get => _available;
            set => this.RaiseAndSetIfChanged(ref _available, value);
        }

        private int _total;
        public int Total
        {
            get => _total;
            protected set => this.RaiseAndSetIfChanged(ref _total, value);
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            private set => this.RaiseAndSetIfChanged(ref _isActive, value);
        }

        private bool _shouldBeDisplayed;
        public bool ShouldBeDisplayed
        {
            get => _shouldBeDisplayed;
            private set => this.RaiseAndSetIfChanged(ref _shouldBeDisplayed, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        ///     The save/load manager.
        /// </param>
        /// <param name="collectSectionFactory">
        ///     An Autofac factory for creating collect section undoable actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        ///     An Autofac factory for creating uncollect section undoable actions.
        /// </param>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The section auto-track value.
        /// </param>
        /// <param name="marking">
        ///     The marking for the section.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        protected SectionBase(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, string name, IAutoTrackValue? autoTrackValue = null,
            IMarking? marking = null, IRequirement? requirement = null)
        {
            _saveLoadManager = saveLoadManager;

            _collectSectionFactory = collectSectionFactory;
            _uncollectSectionFactory = uncollectSectionFactory;
            
            _autoTrackValue = autoTrackValue;
            _requirement = requirement;

            Name = name;
            Marking = marking;
            
            PropertyChanged += OnPropertyChanged;

            if (_requirement is not null)
            {
                _requirement.PropertyChanged += OnRequirementChanged;
            }

            if (_autoTrackValue is not null)
            {
                _autoTrackValue.PropertyChanged += OnAutoTrackValueChanged;
            }
            
            UpdateIsActive();
            UpdateShouldBeDisplayed();
        }

        public bool IsAvailable()
        {
            return Available > 0;
        }

        public abstract bool CanBeCleared(bool force = false);
        public abstract void Clear(bool force);

        public IUndoable CreateCollectSectionAction(bool force)
        {
            return _collectSectionFactory(this, force);
        }

        public bool CanBeUncleared()
        {
            return Available < Total;
        }

        public IUndoable CreateUncollectSectionAction()
        {
            return _uncollectSectionFactory(this);
        }

        public void Reset()
        {
            UserManipulated = false;
            Available = Total;

            if (Marking is not null)
            {
                Marking.Mark = MarkType.Unknown;
            }
        }
        
        /// <summary>
        ///     Returns a new section save data instance for this section.
        /// </summary>
        /// <returns>
        ///     A new section save data instance.
        /// </returns>
        public SectionSaveData Save()
        {
            return new()
            {
                Marking = Marking?.Mark,
                UserManipulated = UserManipulated,
                Available = Available
            };
        }

        /// <summary>
        ///     Loads section save data.
        /// </summary>
        public void Load(SectionSaveData? saveData)
        {
            if (saveData is null)
            {
                return;
            }

            if (saveData.Marking is not null && Marking is not null)
            {
                Marking.Mark = saveData.Marking.Value;
            }

            UserManipulated = saveData.UserManipulated;
            Available = saveData.Available;
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
        protected virtual void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Accessibility):
                case nameof(Available):
                    UpdateShouldBeDisplayed();
                    break;
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Met))
            {
                UpdateIsActive();
            }
        }

        /// <summary>
        ///     Updates the value of the IsActive property.
        /// </summary>
        private void UpdateIsActive()
        {
            IsActive = _requirement is null || _requirement.Met;
        }

        /// <summary>
        ///     Updates the value of the ShouldBeDisplayed property.
        /// </summary>
        protected void UpdateShouldBeDisplayed()
        {
            ShouldBeDisplayed = (IsAvailable() && Accessibility >= AccessibilityLevel.Inspect) || CanBeCleared();
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
        private void OnAutoTrackValueChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                AutoTrackUpdate();
            }
        }

        /// <summary>
        ///     Updates the section value from the auto-tracked value.
        /// </summary>
        private void AutoTrackUpdate()
        {
            if (!_autoTrackValue!.CurrentValue.HasValue)
            {
                return;
            }

            if (Available == Total - _autoTrackValue.CurrentValue.Value)
            {
                return;
            }
            
            Available = Total - _autoTrackValue.CurrentValue.Value;
            _saveLoadManager.Unsaved = true;
        }
    }
}