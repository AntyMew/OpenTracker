﻿using System.Collections.Specialized;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.PinnedLocations.Notes
{
    /// <summary>
    /// This class contains the pinned location note area control ViewModel data.
    /// </summary>
    public class PinnedLocationNoteAreaVM : ViewModelBase, IPinnedLocationNoteAreaVM
    {
        private readonly ILocation _location;
        private readonly IUndoRedoManager _undoRedoManager;

        public IPinnedLocationNoteVMCollection Notes { get; }
        public HorizontalAlignment Alignment =>
            Notes.Count == 0 ? HorizontalAlignment.Center : HorizontalAlignment.Left;

        public ReactiveCommand<Unit, Unit> Add { get; }

        private bool _canAdd;
        private bool CanAdd
        {
            get => _canAdd;
            set => this.RaiseAndSetIfChanged(ref _canAdd, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        /// <param name="notesFactory">
        /// An Autofac factory for creating the notes collection.
        /// </param>
        public PinnedLocationNoteAreaVM(
            ILocation location, IUndoRedoManager undoRedoManager, IPinnedLocationNoteVMCollection.Factory notesFactory)
        {
            _location = location;
            _undoRedoManager = undoRedoManager;

            Notes = notesFactory(location);

            Add = ReactiveCommand.Create(AddImpl, this.WhenAnyValue(x => x.CanAdd));

            Notes.CollectionChanged += OnNotesChanged;

            UpdateCanAdd();
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the IPinnedLocationNoteVMCollection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the CollectionChanged event.
        /// </param>
        private async void OnNotesChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            await UpdateCanAddAsync();
        }

        /// <summary>
        /// Updates the CanAdd property for whether a note can be added.
        /// </summary>
        private void UpdateCanAdd()
        {
            CanAdd = Notes.Count < 4;
        }

        /// <summary>
        /// Updates the CanAdd property for whether a note can be added asynchronously.
        /// </summary>
        private async Task UpdateCanAddAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(UpdateCanAdd);
        }

        /// <summary>
        /// Adds a new note to the location.
        /// </summary>
        private void AddImpl()
        {
            _undoRedoManager.NewAction(_location.CreateAddNoteAction());
        }
    }
}
