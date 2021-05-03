using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    public class SectionIconVM : ViewModelBase, ISectionIconVM
    {
        private readonly IUndoRedoManager _undoRedoManager;
        
        private readonly ISectionIconImageProvider _imageProvider;
        private readonly ISection _section;

        public string ImageSource => _imageProvider.ImageSource;

        public bool LabelVisible { get; }
        public string Label => _section.Available.ToString(CultureInfo.InvariantCulture);
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="imageProvider">
        /// The section icon image control provider.
        /// </param>
        /// <param name="section">
        /// The section data.
        /// </param>
        /// <param name="labelVisible">
        /// A boolean representing whether the label is visible.
        /// </param>
        public SectionIconVM(
            IUndoRedoManager undoRedoManager, ISectionIconImageProvider imageProvider, ISection section,
            bool labelVisible)
        {
            _imageProvider = imageProvider;
            _section = section;

            LabelVisible = labelVisible;
            _undoRedoManager = undoRedoManager;

            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _imageProvider.PropertyChanged += OnImageProviderChanged;
            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISectionIconImageProvider interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnImageProviderChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISectionIconImageProvider.ImageSource))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnSectionChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Available))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Label)));
            }
        }

        /// <summary>
        /// Creates an undoable action to collect the section and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        private void CollectSection(bool force)
        {
            _undoRedoManager.NewAction(_section.CreateCollectSectionAction(force));
        }

        /// <summary>
        /// Creates an undoable action to un-collect the section and send it to the undo/redo manager.
        /// </summary>
        private void UncollectSection()
        {
            _undoRedoManager.NewAction(_section.CreateUncollectSectionAction());
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The PointerReleased event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    CollectSection((e.KeyModifiers & KeyModifiers.Control) > 0);
                    break;
                case MouseButton.Right:
                    UncollectSection();
                    break;
            }
        }
    }
}