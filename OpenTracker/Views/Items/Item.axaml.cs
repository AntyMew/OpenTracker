using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Items
{
    public class Item : UserControl
    {
        public Item()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}