using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Queries.View.UserControl;

namespace Queries.View
{
    /// <summary>
    /// Interaction logic for QueryWindow.xaml
    /// </summary>
    public partial class QueriesWindow : Window
    {
        public QueriesWindow()
        {
            InitializeComponent();
			this.DynGrid.Children.Add(new QueryControl());
		}

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count<1)return;
            this.DynGrid.Children.Clear();
            var t = Type.GetType($"Queries.View.UserControl.{e.AddedItems[0]}Control") ??
                    Type.GetType($"Queries.View.UserControl.NotFoundControl");
            this.DynGrid.Children.Add(Activator.CreateInstance(t) as UIElement);
        }

		private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Popup.IsOpen = true;
		}

		private void Button_LostFocus(object sender, RoutedEventArgs e)
		{
			Popup.IsOpen = false;
		}
	}
}
