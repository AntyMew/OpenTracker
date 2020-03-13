﻿using Avalonia;
using Avalonia.Media;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class MapLocationControlVM : ViewModelBase, IMapLocationControlVM
    {
        private readonly AppSettingsVM _appSettings;
        private readonly Game _game;
        private readonly MainWindowVM _mainWindow;
        private readonly MapLocation _mapLocation;

        private double _canvasX;
        public double CanvasX
        {
            get => _canvasX;
            private set => this.RaiseAndSetIfChanged(ref _canvasX, value);
        }

        private double _canvasY;
        public double CanvasY
        {
            get => _canvasY;
            private set => this.RaiseAndSetIfChanged(ref _canvasY, value);
        }

        private double _size;
        public double Size
        {
            get => _size;
            private set => this.RaiseAndSetIfChanged(ref _size, value);
        }

        private Thickness _borderSize;
        public Thickness BorderSize
        {
            get => _borderSize;
            private set => this.RaiseAndSetIfChanged(ref _borderSize, value);
        }

        private Accessibility _accessibility;
        public Accessibility Accessibility
        {
            get => _accessibility;
            private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        private IBrush _color;
        public IBrush Color
        {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        private bool _visible;
        public bool Visible
        {
            get => _visible;
            set => this.RaiseAndSetIfChanged(ref _visible, value);
        }

        public MapLocationControlVM(AppSettingsVM appSettings, Game game,
            MainWindowVM mainWindow, MapLocation mapLocation)
        {
            _appSettings = appSettings;
            _game = game;
            _mainWindow = mainWindow;
            _mapLocation = mapLocation;

            game.Mode.PropertyChanged += GameModeChanged;

            foreach (Item item in game.Items.Values)
                item.PropertyChanged += ItemChanged;

            SetSizeAndPosition();
            SetColor();
            SetVisibility();
        }

        private void SetSizeAndPosition()
        {
            if (_game.Mode.EntranceShuffle.HasValue &&
                _game.Mode.EntranceShuffle.Value)
            {
                Size = 40.0;
                BorderSize = new Thickness(5);
            }
            else
            {
                Size = 70.0;
                BorderSize = new Thickness(9);
            }

            CanvasX = _mapLocation.X - (Size / 2);
            CanvasY = _mapLocation.Y - (Size / 2);
        }

        public void SetColor()
        {
            Color = _appSettings.AccessibilityColors[_mapLocation.Location.GetAccessibility(_game.Mode, _game.Items)];
        }

        public void SetVisibility()
        {
            Visible = _game.Mode.Validate(_mapLocation.VisibilityMode) && (_appSettings.DisplayAllLocations ||
                (_mapLocation.Location.GetAccessibility(_game.Mode, _game.Items) != Accessibility.Cleared &&
                _mapLocation.Location.GetAccessibility(_game.Mode, _game.Items) != Accessibility.None));
        }

        public void PinLocation()
        {
            PinnedLocationControlVM existingPinnedLocation = null;

            foreach (PinnedLocationControlVM pinnedLocation in _mainWindow.PinnedLocations)
            {
                if (pinnedLocation.Location == _mapLocation.Location)
                    existingPinnedLocation = pinnedLocation;
            }

            if (existingPinnedLocation != null)
                _mainWindow.PinnedLocations.Remove(existingPinnedLocation);

            _mainWindow.PinnedLocations.Insert(0, new PinnedLocationControlVM(_game, _mainWindow, _mapLocation.Location));
        }

        private void GameModeChanged(object sender, PropertyChangedEventArgs e)
        {
            SetSizeAndPosition();
            SetColor();
            SetVisibility();
        }

        private void ItemChanged(object sender, PropertyChangedEventArgs e)
        {
            SetColor();
            SetVisibility();
        }
    }
}
