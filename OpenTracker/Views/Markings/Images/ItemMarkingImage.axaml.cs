﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Markings.Images
{
    public class ItemMarkingImage : UserControl
    {
        public ItemMarkingImage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
